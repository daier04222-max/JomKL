using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using eExploreKL.Models;
using Microsoft.AspNetCore.Http;

namespace eExploreKL.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public LoginModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            // 如果已经登录，直接跳转到 Dashboard
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                Response.Redirect("/Dashboard");
            }
        }

        public IActionResult OnPost(string email, string password)
        {
            // 1. 验证输入是否为空
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                TempData["ErrorMessage"] = "Please enter both email and password.";
                return Page();
            }

            // 2. 从数据库查询用户（根据邮箱）
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            // 3. 检查用户是否存在
            if (user == null)
            {
                TempData["ErrorMessage"] = "Invalid email or password.";
                return Page();
            }

            // 4. 验证密码
            if (password != user.PasswordHash)
            {
                TempData["ErrorMessage"] = "Invalid email or password.";
                return Page();
            }

            // 5. 登录成功！保存用户信息到 Session
            HttpContext.Session.SetString("UserName", user.UserName);
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("UserRole", user.Role ?? "Learner");

            // 6. 跳转到 Dashboard
            return RedirectToPage("/Dashboard");
        }
    }
}