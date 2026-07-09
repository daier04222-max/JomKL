using eExploreKL.Models;
using Microsoft.EntityFrameworkCore;

namespace eExploreKL.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

        // 检查是否已经有数据了
        if (context.Landmarks.Any())
        {
            return; // 已经有数据，不再重复添加
        }

        // ==================== 添加地标数据 ====================
        var landmarks = new Landmark[]
        {
            new Landmark { Name = "Petronas Twin Towers", Description = "Iconic twin skyscrapers in KLCC", Category = "Landmark", ImageUrl = "/images/petronas.jpg" },
            new Landmark { Name = "Batu Caves", Description = "Limestone hill with Hindu temples", Category = "Cultural", ImageUrl = "/images/batu-caves.jpg" },
            new Landmark { Name = "Merdeka Square", Description = "Historic independence square", Category = "Historical", ImageUrl = "/images/merdeka.jpg" },
            new Landmark { Name = "KL Tower", Description = "Telecommunications tower with observation deck", Category = "Landmark", ImageUrl = "/images/kl-tower.jpg" },
            new Landmark { Name = "Central Market", Description = "Art deco style market with local crafts", Category = "Cultural", ImageUrl = "/images/central-market.jpg" },
            new Landmark { Name = "National Museum", Description = "Museum of Malaysian history", Category = "Historical", ImageUrl = "/images/national-museum.jpg" },
            new Landmark { Name = "Thean Hou Temple", Description = "Beautiful Chinese temple", Category = "Cultural", ImageUrl = "/images/thean-hou.jpg" },
            new Landmark { Name = "Aquaria KLCC", Description = "Oceanarium under KLCC", Category = "Entertainment", ImageUrl = "/images/aquaria.jpg" },
            new Landmark { Name = "Perdana Botanical Garden", Description = "Lush gardens near Parliament", Category = "Nature", ImageUrl = "/images/perdana.jpg" },
            new Landmark { Name = "Jalan Alor", Description = "Famous street food night market", Category = "Food", ImageUrl = "/images/jalan-alor.jpg" }
        };

        context.Landmarks.AddRange(landmarks);

        // ==================== 添加测试用户 ====================
        var users = new User[]
        {
            new User { UserName = "YUAN WENJIE", Email = "yuan@test.com", PasswordHash = "fakehash", Role = "Learner", JoinDate = DateTime.Now.AddDays(-30) },
            new User { UserName = "TestUser", Email = "test@test.com", PasswordHash = "fakehash", Role = "Learner", JoinDate = DateTime.Now.AddDays(-10) }
        };

        context.Users.AddRange(users);

        // ==================== 保存所有数据 ====================
        context.SaveChanges();

        // ==================== 添加用户进度数据（需要先有用户和地标） ====================
        // 获取刚插入的用户
        var user = context.Users.FirstOrDefault(u => u.UserName == "YUAN WENJIE");
        var allLandmarks = context.Landmarks.ToList();

        if (user != null && allLandmarks.Any())
        {
            var progresses = new UserProgress[]
            {
                new UserProgress { UserId = user.Id, LandmarkId = allLandmarks[0].Id, IsUnlocked = true, QuizCompleted = true, Score = 10, CompletedDate = DateTime.Now.AddDays(-20) },
                new UserProgress { UserId = user.Id, LandmarkId = allLandmarks[1].Id, IsUnlocked = true, QuizCompleted = true, Score = 10, CompletedDate = DateTime.Now.AddDays(-18) },
                new UserProgress { UserId = user.Id, LandmarkId = allLandmarks[2].Id, IsUnlocked = true, QuizCompleted = false, Score = 0, CompletedDate = null },
                new UserProgress { UserId = user.Id, LandmarkId = allLandmarks[3].Id, IsUnlocked = true, QuizCompleted = true, Score = 8, CompletedDate = DateTime.Now.AddDays(-15) },
                new UserProgress { UserId = user.Id, LandmarkId = allLandmarks[4].Id, IsUnlocked = true, QuizCompleted = false, Score = 0, CompletedDate = null },
                new UserProgress { UserId = user.Id, LandmarkId = allLandmarks[5].Id, IsUnlocked = false, QuizCompleted = false, Score = 0, CompletedDate = null },
                new UserProgress { UserId = user.Id, LandmarkId = allLandmarks[6].Id, IsUnlocked = false, QuizCompleted = false, Score = 0, CompletedDate = null },
                new UserProgress { UserId = user.Id, LandmarkId = allLandmarks[7].Id, IsUnlocked = false, QuizCompleted = false, Score = 0, CompletedDate = null },
                new UserProgress { UserId = user.Id, LandmarkId = allLandmarks[8].Id, IsUnlocked = false, QuizCompleted = false, Score = 0, CompletedDate = null },
                new UserProgress { UserId = user.Id, LandmarkId = allLandmarks[9].Id, IsUnlocked = false, QuizCompleted = false, Score = 0, CompletedDate = null }
            };

            context.UserProgresses.AddRange(progresses);
            context.SaveChanges();
        }
    }
}