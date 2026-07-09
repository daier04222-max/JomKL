namespace eExploreKL.Models;

public class UserProgress
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int LandmarkId { get; set; }
    public bool IsUnlocked { get; set; }
    public bool QuizCompleted { get; set; }
    public int Score { get; set; }
    public DateTime? CompletedDate { get; set; }
}