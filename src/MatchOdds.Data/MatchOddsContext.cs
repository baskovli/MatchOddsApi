using MatchOdds.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MatchOdds.Data
{
    public class MatchOddsContext : DbContext
    {
        public MatchOddsContext(DbContextOptions<MatchOddsContext> options) : base(options)
        {
        }

        public MatchOddsContext()
        {
        }

        public DbSet<Match> Matches { get; set; }
        public DbSet<Odd> Odds { get; set; }


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