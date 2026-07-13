using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JomKL.Pages
{
    public class LoginModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnPost(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                TempData["ErrorMessage"] = "Please enter both email and password.";
                return Page();
            }

            if (email == "admin@kldiscovery.com" && password == "Admin12345")
            {
                return Content("Success: Redirecting to Administrator Dashboard...");
            }
            else if (email == "student@kldiscovery.com" && password == "Student12345")
            {
                return Content("Success: Redirecting to Learner Dashboard...");
            }

            TempData["ErrorMessage"] = "Invalid email or password.";
            return Page();
        }
    }
}