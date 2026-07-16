using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using eExploreKL.Models;
using System.Linq;

namespace JomKL.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public RegisterModel(ApplicationDbContext context)
        {
            _context = context;
        }

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

            // Check if the email is already registered
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == email);
            if (existingUser != null)
            {
                TempData["Message"] = "Email is already registered.";
                return Page();
            }

            // Create new user record
            var newUser = new User
            {
                UserName = fullName,
                Email = email,
                PasswordHash = password, // Compares directly with password in your login logic
                Role = "Learner"
            };

            // Save the user to the database
            _context.Users.Add(newUser);
            _context.SaveChanges();

            TempData["Message"] = "Registration successful for " + fullName + "! You can now log in.";
            return RedirectToPage("/Login");
        }
    }
}