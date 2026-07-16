namespace eExploreKL.Models;

// One multiple-choice question belonging to a landmark's quiz.
// New table added for the quiz feature; created via raw SQL in SeedData
// (see SeedData.cs) rather than an EF migration, so it doesn't disturb the
// existing Migrations history.
public class QuizQuestion
{
    public int Id { get; set; }
    public int LandmarkId { get; set; }
    public string QuestionText { get; set; } = "";
    public string OptionA { get; set; } = "";
    public string OptionB { get; set; } = "";
    public string OptionC { get; set; } = "";
    public string OptionD { get; set; } = "";
    public string CorrectOption { get; set; } = "A"; // A / B / C / D
    public string Difficulty { get; set; } = "Easy";  // Easy / Medium / Hard
    public int Points { get; set; } = 10;
}
