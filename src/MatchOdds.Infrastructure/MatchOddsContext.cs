using MatchOdds.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MatchOdds.Infrastructure
{
    public class MatchOddsContext : DbContext
    {
        public MatchOddsContext(DbContextOptions<MatchOddsContext> options) : base(options)
        {
        }

        public MatchOddsContext()
        {
        }

        public DbSet<Match> Match { get; set; }
        public DbSet<Odd> Odd { get; set; }

        //https://blog.devgenius.io/3-different-ways-to-implement-value-object-in-csharp-10-d8f43e1fa4dc
        //protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        //{
        //    configurationBuilder.Properties<Match>().HaveConversion<OddConverter>();
        //}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Match>(e =>
            {               
                e.ToTable("Match").HasKey(x => x.ID);
                e.HasIndex(p => new { p.MatchDate, p.TeamA }).IsUnique();
                e.HasIndex(p => new { p.MatchDate, p.TeamB }).IsUnique();
                e.HasIndex(p => new { p.MatchDate, p.TeamA, p.TeamB }).IsUnique();
                e.Property(x => x.Description).HasMaxLength(255).HasDefaultValue(string.Empty);
                e.Property(x => x.TeamA).HasMaxLength(255).HasDefaultValue(string.Empty);
                e.Property(x => x.TeamB).HasMaxLength(255).HasDefaultValue(string.Empty);
                e.Property(x => x.Sport);
            });

            modelBuilder.Entity<Odd>(e =>
            {
                e.ToTable("Odd").HasKey(x => x.ID);
                e.Property(x => x.Specifier).HasMaxLength(255).HasDefaultValue(string.Empty);
                e.Property(x => x.MatchOdd);
            });
        }
    }
}