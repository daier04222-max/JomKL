using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using eExploreKL.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace eExploreKL.Pages;

public class BadgeInfo
{
    public string Name { get; set; } = "";
    public string ImageUrl { get; set; } = "";
}

public class DashboardModel : PageModel
{
    private readonly ApplicationDbContext _context;

    // 构造函数，注入数据库上下文
    public DashboardModel(ApplicationDbContext context)
    {
        _context = context;
    }

    // 用户信息
    public string UserName { get; set; } = "Learner";
    public string JoinDate { get; set; } = "";

    // 地标进度
    public int TotalLandmarks { get; set; } = 0;
    public int UnlockedLandmarks { get; set; } = 0;

    // 测验进度
    public int TotalQuizzes { get; set; } = 0;
    public int CompletedQuizzes { get; set; } = 0;

    // 积分和徽章
    public int TotalScore { get; set; } = 0;
    public int BadgeCount { get; set; } = 0;

    // 完成度百分比
    public int CompletionRate { get; set; } = 0;

    // 徽章列表
    public List<BadgeInfo> Badges { get; set; } = new List<BadgeInfo>();

    public void OnGet()
    {
        // ============================================================
        // ✅ 从 Session 获取当前登录用户
        // ============================================================

        // 从 Session 获取当前登录的用户名
        string currentUserName = HttpContext.Session.GetString("UserName") ?? "";

        // 如果用户未登录，跳转到登录页
        if (string.IsNullOrEmpty(currentUserName))
        {
            Response.Redirect("/Login");
            return;
        }

        // ---- 1. 查询用户信息 ----
        var user = _context.Users
            .FirstOrDefault(u => u.UserName == currentUserName);

        if (user == null)
        {
            // 如果用户不存在，跳转到登录页
            Response.Redirect("/Login");
            return;
        }

        UserName = user.UserName;
        JoinDate = user.JoinDate.ToString("yyyy-MM-dd");

        // ---- 2. 查询地标总数 ----
        TotalLandmarks = _context.Landmarks.Count();

        // ---- 3. 查询用户解锁的地标数 ----
        UnlockedLandmarks = _context.UserProgresses
            .Where(p => p.UserId == user.Id && p.IsUnlocked == true)
            .Count();

        // ---- 4. 查询测验总数 ----
        TotalQuizzes = _context.Landmarks.Count();

        // ---- 5. 查询用户完成的测验数 ----
        CompletedQuizzes = _context.UserProgresses
            .Where(p => p.UserId == user.Id && p.QuizCompleted == true)
            .Count();

        // ---- 6. 查询用户总积分 ----
        TotalScore = _context.UserProgresses
            .Where(p => p.UserId == user.Id)
            .Sum(p => (int?)p.Score) ?? 0;

        // ---- 7. 根据进度生成徽章 ----
        Badges = new List<BadgeInfo>();

        if (UnlockedLandmarks >= 1)
        {
            Badges.Add(new BadgeInfo { Name = "First Step", ImageUrl = "/images/badge-first.png" });
        }
        if (UnlockedLandmarks >= 5)
        {
            Badges.Add(new BadgeInfo { Name = "Explorer", ImageUrl = "/images/badge-explorer.png" });
        }
        if (UnlockedLandmarks >= 10)
        {
            Badges.Add(new BadgeInfo { Name = "Master Explorer", ImageUrl = "/images/badge-master.png" });
        }
        if (CompletedQuizzes >= 5)
        {
            Badges.Add(new BadgeInfo { Name = "Quiz Master", ImageUrl = "/images/badge-quiz.png" });
        }

        BadgeCount = Badges.Count;

        // ---- 8. 计算完成度 ----
        CompletionRate = TotalLandmarks > 0 
            ? (UnlockedLandmarks * 100 / TotalLandmarks) 
            : 0;
    }
}