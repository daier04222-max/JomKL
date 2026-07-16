<<<<<<< HEAD
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
=======
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using eExploreKL.Models; 

namespace eExploreKL.Pages
{
    public class MapModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public MapModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Landmark> Landmarks { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Landmarks = await _context.Landmarks.ToListAsync();
        }
    }
}
>>>>>>> 00f46d873c2924276d44f6bf8175deb766a48f9e
