using System.Text.Json;
using eExploreKL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eExploreKL.Pages
{
    public class QuizResultModel : PageModel
    {
        public QuizAttemptResult Result { get; set; } = new();

        public IActionResult OnGet()
        {
            if (TempData["QuizResult"] is not string json || string.IsNullOrEmpty(json))
            {
                return RedirectToPage("/Quiz");
            }

            var result = JsonSerializer.Deserialize<QuizAttemptResult>(json);
            if (result is null) return RedirectToPage("/Quiz");

            Result = result;
            return Page();
        }
    }
}
