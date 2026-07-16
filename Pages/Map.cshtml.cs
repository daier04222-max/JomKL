using eExploreKL.Models;
using eExploreKL.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eExploreKL.Pages
{
    // Simple overview of every KL landmark, with a shortcut into its quiz.
    public class MapModel : PageModel
    {
        private readonly QuizService _quizzes;

        public MapModel(QuizService quizzes) => _quizzes = quizzes;

        public List<LandmarkQuizInfo> Landmarks { get; set; } = new();

        public void OnGet()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            Landmarks = _quizzes.GetLandmarksWithProgress(userId);
        }
    }
}