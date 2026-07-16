using Microsoft.EntityFrameworkCore;

namespace eExploreKL.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Landmark> Landmarks { get; set; }
        public DbSet<UserProgress> UserProgresses { get; set; }

        // Added for the quiz feature. Table is created via raw SQL in
        // SeedData.Initialize (no EF migration) so it doesn't touch the
        // existing Migrations history.
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
    }
}