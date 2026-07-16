namespace eExploreKL.Models;

// One answer submitted by the learner for a single question.
public class QuizAnswer
{
    public int QuestionId { get; set; }
    public string? SelectedOption { get; set; } // A / B / C / D or null
}

// The graded result shown on the Result page (carried through TempData as JSON).
public class QuizAttemptResult
{
    public int LandmarkId { get; set; }
    public string LandmarkName { get; set; } = "";
    public int CorrectAnswers { get; set; }
    public int TotalQuestions { get; set; }
    public int PointsEarned { get; set; }
    public int MaxPoints { get; set; }
    public bool IsNewBestScore { get; set; }
    public List<QuestionReview> Review { get; set; } = new();
}

// Per-question feedback for the answer-review section.
public class QuestionReview
{
    public string QuestionText { get; set; } = "";
    public string? SelectedOption { get; set; }
    public string CorrectOption { get; set; } = "";
    public string CorrectAnswerText { get; set; } = "";
    public bool IsCorrect { get; set; }
}

// One card on the quiz catalogue: a landmark plus the current user's progress.
public class LandmarkQuizInfo
{
    public int LandmarkId { get; set; }
    public string Name { get; set; } = "";
    public string Category { get; set; } = "";
    public string Description { get; set; } = "";
    public int QuestionCount { get; set; }
    public int TotalPoints { get; set; }
    public bool Completed { get; set; }
    public int? BestScore { get; set; } // current user's best score, if any
}
