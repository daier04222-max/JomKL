using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JomKL.Pages
{
    public class RegisterModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnPost(string fullName, string email, string password)
        {
            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                TempData["Message"] = "All fields are required.";
                return Page();
            }

            TempData["Message"] = "Registration successful for " + fullName + "!";
            return Page();
        }
    }
}