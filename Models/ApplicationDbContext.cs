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
    }
}