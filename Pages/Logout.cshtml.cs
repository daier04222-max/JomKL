using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace eExploreKL.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            // 清除 Session 中的所有数据
            HttpContext.Session.Clear();
            
            // 跳转到登录页
            return RedirectToPage("/Login");
        }
    }
}