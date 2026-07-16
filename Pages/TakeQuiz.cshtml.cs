using System.Text.Json;
using eExploreKL.Models;
using eExploreKL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eExploreKL.Pages
{
    // Take one landmark's quiz. OnGet shows the questions; OnPost grades the
    // submission on the server, saves the attempt into UserProgress, then
    // redirects to the Result page (Post/Redirect/Get).
    public class TakeQuizModel : PageModel
    {
        private readonly QuizService _quizzes;
        private readonly QuizProgressService _progress;

        public TakeQuizModel(QuizService quizzes, QuizProgressService progress)
        {
            _quizzes = quizzes;
            _progress = progress;
        }

        public Landmark Landmark { get; set; } = new();
        public List<QuizQuestion> Questions { get; set; } = new();

        [BindProperty]
        public List<QuizAnswer> Answers { get; set; } = new();

        private int? CurrentUserId => HttpContext.Session.GetInt32("UserId");

        public IActionResult OnGet(int id)
        {
            if (CurrentUserId is null)
            {
                TempData["ErrorMessage"] = "Please log in to take a quiz.";
                return RedirectToPage("/Login");
            }

            var landmark = _quizzes.GetLandmark(id);
            if (landmark is null) return RedirectToPage("/Quiz");

            var questions = _quizzes.GetQuestions(id);
            if (questions.Count == 0)
            {
                return RedirectToPage("/Quiz");
            }

            Landmark = landmark;
            Questions = questions;
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            var userId = CurrentUserId;
            if (userId is null) return RedirectToPage("/Login");

            var landmark = _quizzes.GetLandmark(id);
            if (landmark is null) return RedirectToPage("/Quiz");

            var questions = _quizzes.GetQuestions(id);

            // One selected option per question (last one wins if duplicated).
            var selectedByQuestion = Answers
                .GroupBy(a => a.QuestionId)
                .ToDictionary(g => g.Key, g => g.Last().SelectedOption);

            var result = new QuizAttemptResult
            {
                LandmarkId = landmark.Id,
                LandmarkName = landmark.Name,
                TotalQuestions = questions.Count
            };

            foreach (var q in questions)
            {
                selectedByQuestion.TryGetValue(q.Id, out var selected);
                var isCorrect = selected != null &&
                    string.Equals(selected, q.CorrectOption, StringComparison.OrdinalIgnoreCase);

                if (isCorrect)
                {
                    result.CorrectAnswers++;
                    result.PointsEarned += q.Points;
                }
                result.MaxPoints += q.Points;

                result.Review.Add(new QuestionReview
                {
                    QuestionText = q.QuestionText,
                    SelectedOption = selected,
                    CorrectOption = q.CorrectOption,
                    CorrectAnswerText = q.CorrectOption switch
                    {
                        "A" => q.OptionA,
                        "B" => q.OptionB,
                        "C" => q.OptionC,
                        _ => q.OptionD
                    },
                    IsCorrect = isCorrect
                });
            }

            // Saves into the existing UserProgress table, so the Dashboard
            // page picks this up automatically.
            result.IsNewBestScore = _progress.SaveAttempt(userId.Value, landmark.Id, result.PointsEarned);

            TempData["QuizResult"] = JsonSerializer.Serialize(result);
            return RedirectToPage("/QuizResult");
        }
    }
}
