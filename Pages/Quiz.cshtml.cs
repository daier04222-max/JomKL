using eExploreKL.Models;
using eExploreKL.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eExploreKL.Pages
{
    // Landmark quiz catalogue. Lists every landmark; the ones with a question
    // bank can be started, and (if logged in) the learner's best score is shown.
    public class QuizModel : PageModel
    {
        private readonly QuizService _quizzes;

        public QuizModel(QuizService quizzes) => _quizzes = quizzes;

        public List<LandmarkQuizInfo> Landmarks { get; set; } = new();
        public bool IsLoggedIn { get; set; }

        public void OnGet()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            IsLoggedIn = userId.HasValue;
            Landmarks = _quizzes.GetLandmarksWithProgress(userId);
        }
    }
}
