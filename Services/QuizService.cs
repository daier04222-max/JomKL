using eExploreKL.Models;
using Microsoft.EntityFrameworkCore;

namespace eExploreKL.Services
{
    /// <summary>
    /// Read-only operations for the quiz module: the landmark catalogue
    /// (with each question bank's size/points and the current user's best
    /// score, sourced from the existing UserProgress table) and the
    /// questions for one landmark.
    /// </summary>
    public class QuizService
    {
        private readonly ApplicationDbContext _db;

        public QuizService(ApplicationDbContext db) => _db = db;

        public List<LandmarkQuizInfo> GetLandmarksWithProgress(int? userId)
        {
            var questionStats = _db.QuizQuestions
                .GroupBy(q => q.LandmarkId)
                .Select(g => new { LandmarkId = g.Key, Count = g.Count(), Points = g.Sum(q => q.Points) })
                .ToList();

            var landmarks = _db.Landmarks.OrderBy(l => l.Id).ToList();

            var progressByLandmark = userId is null
                ? new Dictionary<int, UserProgress>()
                : _db.UserProgresses
                    .Where(p => p.UserId == userId)
                    .ToDictionary(p => p.LandmarkId, p => p);

            var result = new List<LandmarkQuizInfo>();
            foreach (var lm in landmarks)
            {
                var stat = questionStats.FirstOrDefault(q => q.LandmarkId == lm.Id);
                progressByLandmark.TryGetValue(lm.Id, out var progress);

                result.Add(new LandmarkQuizInfo
                {
                    LandmarkId = lm.Id,
                    Name = lm.Name,
                    Category = lm.Category ?? "",
                    Description = lm.Description ?? "",
                    QuestionCount = stat?.Count ?? 0,
                    TotalPoints = stat?.Points ?? 0,
                    Completed = progress?.QuizCompleted ?? false,
                    BestScore = progress != null && progress.QuizCompleted ? progress.Score : (int?)null
                });
            }
            return result;
        }

        public Landmark? GetLandmark(int landmarkId) =>
            _db.Landmarks.FirstOrDefault(l => l.Id == landmarkId);

        public List<QuizQuestion> GetQuestions(int landmarkId) =>
            _db.QuizQuestions
               .Where(q => q.LandmarkId == landmarkId)
               .OrderBy(q => q.Id)
               .ToList();
    }
}
