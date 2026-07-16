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
        if (!context.Landmarks.Any())
        {
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
            new User { UserName = "TestUser", Email = "test@test.com", PasswordHash = "fakehash", Role = "Learner", JoinDate = DateTime.Now.AddDays(-10) },
            new User { UserName = "Admin", Email = "admin@kldiscovery.com", PasswordHash = "Admin12345", Role = "Admin", JoinDate = DateTime.Now },
            new User { UserName = "Student", Email = "student@kldiscovery.com", PasswordHash = "Student12345", Role = "Learner", JoinDate = DateTime.Now }
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

        // ==================== Quiz questions (added for the quiz feature) ====================
        // Runs every startup. Creates the QuizQuestions table if it doesn't exist yet
        // (no EF migration involved, so the existing Migrations history is untouched)
        // and seeds the question bank once.
        SeedQuizQuestions(context);
    }

    private static void SeedQuizQuestions(ApplicationDbContext context)
    {
        context.Database.ExecuteSqlRaw(@"
            CREATE TABLE IF NOT EXISTS QuizQuestions (
                Id INTEGER NOT NULL CONSTRAINT PK_QuizQuestions PRIMARY KEY AUTOINCREMENT,
                LandmarkId INTEGER NOT NULL,
                QuestionText TEXT NOT NULL,
                OptionA TEXT NOT NULL,
                OptionB TEXT NOT NULL,
                OptionC TEXT NOT NULL,
                OptionD TEXT NOT NULL,
                CorrectOption TEXT NOT NULL,
                Difficulty TEXT NOT NULL,
                Points INTEGER NOT NULL
            );");

        if (context.QuizQuestions.Any())
        {
            return; // already seeded
        }

        var byName = context.Landmarks.ToDictionary(l => l.Name, l => l.Id);

        void Add(string landmarkName, params QuizQuestion[] qs)
        {
            if (!byName.TryGetValue(landmarkName, out var lmId)) return;
            foreach (var q in qs) q.LandmarkId = lmId;
            context.QuizQuestions.AddRange(qs);
        }

        Add("Petronas Twin Towers",
            new QuizQuestion { QuestionText = "How tall are the Petronas Twin Towers?", OptionA = "381 metres", OptionB = "452 metres", OptionC = "508 metres", OptionD = "600 metres", CorrectOption = "B", Difficulty = "Easy", Points = 10 },
            new QuizQuestion { QuestionText = "The famous Skybridge connects the two towers at which floors?", OptionA = "20th and 21st", OptionB = "41st and 42nd", OptionC = "60th and 61st", OptionD = "86th and 87th", CorrectOption = "B", Difficulty = "Medium", Points = 15 },
            new QuizQuestion { QuestionText = "Which architect designed the Petronas Twin Towers?", OptionA = "Cesar Pelli", OptionB = "Zaha Hadid", OptionC = "Norman Foster", OptionD = "I. M. Pei", CorrectOption = "A", Difficulty = "Hard", Points = 20 },
            new QuizQuestion { QuestionText = "In which year were the Petronas Twin Towers officially opened?", OptionA = "1990", OptionB = "1995", OptionC = "1999", OptionD = "2004", CorrectOption = "C", Difficulty = "Medium", Points = 15 });

        Add("Batu Caves",
            new QuizQuestion { QuestionText = "The main shrine at Batu Caves is dedicated to which Hindu deity?", OptionA = "Lord Murugan", OptionB = "Lord Ganesha", OptionC = "Lord Shiva", OptionD = "Lord Vishnu", CorrectOption = "A", Difficulty = "Easy", Points = 10 },
            new QuizQuestion { QuestionText = "Roughly how many colourful steps lead up to the Temple Cave?", OptionA = "100", OptionB = "183", OptionC = "272", OptionD = "365", CorrectOption = "C", Difficulty = "Easy", Points = 10 },
            new QuizQuestion { QuestionText = "The giant golden statue at the entrance of Batu Caves stands about how tall?", OptionA = "15 metres", OptionB = "27 metres", OptionC = "43 metres", OptionD = "60 metres", CorrectOption = "C", Difficulty = "Medium", Points = 15 },
            new QuizQuestion { QuestionText = "Which annual Hindu festival draws over a million visitors to Batu Caves?", OptionA = "Deepavali", OptionB = "Thaipusam", OptionC = "Holi", OptionD = "Ponggal", CorrectOption = "B", Difficulty = "Medium", Points = 15 });

        Add("Merdeka Square",
            new QuizQuestion { QuestionText = "What historic event happened at Merdeka Square on 31 August 1957?", OptionA = "The first Malaysian GP", OptionB = "The Malayan flag was raised for independence", OptionC = "The opening of KL Tower", OptionD = "The coronation of the first Agong", CorrectOption = "B", Difficulty = "Easy", Points = 10 },
            new QuizQuestion { QuestionText = "Which Moorish-style building with a clock tower faces Merdeka Square?", OptionA = "Sultan Abdul Samad Building", OptionB = "Carcosa Seri Negara", OptionC = "Istana Negara", OptionD = "KL Railway Station", CorrectOption = "A", Difficulty = "Medium", Points = 15 },
            new QuizQuestion { QuestionText = "Merdeka Square is home to one of the world's tallest what?", OptionA = "Fountains", OptionB = "Ferris wheels", OptionC = "Flagpoles", OptionD = "Sundials", CorrectOption = "C", Difficulty = "Medium", Points = 15 },
            new QuizQuestion { QuestionText = "Before independence, the field at Merdeka Square was mainly used for which sport?", OptionA = "Football", OptionB = "Cricket", OptionC = "Polo", OptionD = "Sepak takraw", CorrectOption = "B", Difficulty = "Hard", Points = 20 });

        Add("KL Tower",
            new QuizQuestion { QuestionText = "What is the primary function of Menara KL Tower?", OptionA = "An office block", OptionB = "A telecommunications tower", OptionC = "A hotel", OptionD = "A mosque minaret", CorrectOption = "B", Difficulty = "Easy", Points = 10 },
            new QuizQuestion { QuestionText = "Menara KL stands on which forested hill in the city centre?", OptionA = "Bukit Bintang", OptionB = "Bukit Tunku", OptionC = "Bukit Nanas", OptionD = "Bukit Jalil", CorrectOption = "C", Difficulty = "Medium", Points = 15 },
            new QuizQuestion { QuestionText = "How tall is Menara KL Tower?", OptionA = "321 metres", OptionB = "380 metres", OptionC = "421 metres", OptionD = "452 metres", CorrectOption = "C", Difficulty = "Medium", Points = 15 },
            new QuizQuestion { QuestionText = "Menara KL hosts an international annual event for which extreme sport?", OptionA = "BASE jumping", OptionB = "Bungee jumping", OptionC = "Wingsuit flying", OptionD = "Rock climbing", CorrectOption = "A", Difficulty = "Hard", Points = 20 });

        Add("Central Market",
            new QuizQuestion { QuestionText = "Central Market originally opened in 1888 as what?", OptionA = "A textile mill", OptionB = "A wet market", OptionC = "A railway depot", OptionD = "A tin exchange", CorrectOption = "B", Difficulty = "Medium", Points = 15 },
            new QuizQuestion { QuestionText = "What is Central Market best known for today?", OptionA = "Electronics bargains", OptionB = "Malaysian arts, crafts and souvenirs", OptionC = "Luxury fashion brands", OptionD = "A night food court only", CorrectOption = "B", Difficulty = "Easy", Points = 10 },
            new QuizQuestion { QuestionText = "The Art Deco facade of Central Market is painted in which distinctive colour?", OptionA = "Pale blue", OptionB = "Bright red", OptionC = "Mint green", OptionD = "Golden yellow", CorrectOption = "A", Difficulty = "Medium", Points = 15 },
            new QuizQuestion { QuestionText = "Central Market sits close to which street, the heart of KL's Chinatown?", OptionA = "Jalan Alor", OptionB = "Jalan Tuanku Abdul Rahman", OptionC = "Petaling Street", OptionD = "Jalan P. Ramlee", CorrectOption = "C", Difficulty = "Easy", Points = 10 });

        Add("Thean Hou Temple",
            new QuizQuestion { QuestionText = "Thean Hou Temple is dedicated to which goddess?", OptionA = "Mazu, the heavenly sea goddess", OptionB = "Guan Yin only", OptionC = "Nuwa", OptionD = "Chang'e", CorrectOption = "A", Difficulty = "Medium", Points = 15 },
            new QuizQuestion { QuestionText = "Thean Hou Temple was built by which Malaysian Chinese community?", OptionA = "The Hokkien community", OptionB = "The Hainanese community", OptionC = "The Cantonese community", OptionD = "The Teochew community", CorrectOption = "B", Difficulty = "Hard", Points = 20 },
            new QuizQuestion { QuestionText = "Thean Hou is often described as one of the largest what in Southeast Asia?", OptionA = "Buddhist monasteries", OptionB = "Chinese temples", OptionC = "Hindu shrines", OptionD = "Mosques", CorrectOption = "B", Difficulty = "Easy", Points = 10 },
            new QuizQuestion { QuestionText = "During which festival is Thean Hou Temple famously covered in red lanterns?", OptionA = "Mid-Autumn Festival", OptionB = "Chinese New Year", OptionC = "Wesak Day", OptionD = "Hungry Ghost Festival", CorrectOption = "B", Difficulty = "Easy", Points = 10 });

        context.SaveChanges();
    }
}