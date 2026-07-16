using eExploreKL.Models;

namespace eExploreKL.Services
{
    /// <summary>
    /// Saves a graded quiz attempt. Deliberately writes into the existing
    /// UserProgress table (UserId, LandmarkId, IsUnlocked, QuizCompleted,
    /// Score, CompletedDate) instead of a new table, so the Dashboard page
    /// picks up quiz results automatically without any changes to it.
    /// </summary>
    public class QuizProgressService
    {
        private readonly ApplicationDbContext _db;

        public QuizProgressService(ApplicationDbContext db) => _db = db;

        /// <summary>
        /// Records one attempt. The stored score is always the learner's best
        /// attempt for that landmark. Returns true when this attempt set a
        /// new personal best (which also covers the first-ever attempt).
        /// </summary>
        public bool SaveAttempt(int userId, int landmarkId, int pointsEarned)
        {
            var progress = _db.UserProgresses
                .FirstOrDefault(p => p.UserId == userId && p.LandmarkId == landmarkId);

            bool isNewBest;

            if (progress is null)
            {
                _db.UserProgresses.Add(new UserProgress
                {
                    UserId = userId,
                    LandmarkId = landmarkId,
                    IsUnlocked = true,
                    QuizCompleted = true,
                    Score = pointsEarned,
                    CompletedDate = DateTime.Now
                });
                isNewBest = true;
            }
            else
            {
                isNewBest = !progress.QuizCompleted || pointsEarned > progress.Score;
                progress.IsUnlocked = true;
                progress.QuizCompleted = true;
                progress.CompletedDate = DateTime.Now;
                if (isNewBest)
                {
                    progress.Score = pointsEarned;
                }
            }

            _db.SaveChanges();
            return isNewBest;
        }
    }
}
