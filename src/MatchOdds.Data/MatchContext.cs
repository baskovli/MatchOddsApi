using Microsoft.EntityFrameworkCore;

namespace MatchOdds.Data
{
    public class MatchContext : DbContext
    {
        public MatchContext(DbContextOptions<MatchContext> options) : base(options)
        {
        }

        public MatchContext()
        {
        }

        public DbSet<Match> Matches { get; set; }
        public DbSet<MatchOdd> MatchOdds { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Match>()
                .HasIndex(p => new { p.MatchDate, p.TeamA }).IsUnique();
            modelBuilder.Entity<Match>()
                .HasIndex(p => new { p.MatchDate, p.TeamB }).IsUnique();
            modelBuilder.Entity<Match>()
                .HasIndex(p => new { p.MatchDate, p.TeamA, p.TeamB }).IsUnique();
        }
    }
}