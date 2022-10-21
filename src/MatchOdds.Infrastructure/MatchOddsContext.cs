using MatchOdds.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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

        public DbSet<Match> Matches { get; set; }
        public DbSet<Odd> Odds { get; set; }

        //https://blog.devgenius.io/3-different-ways-to-implement-value-object-in-csharp-10-d8f43e1fa4dc
        //protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        //{
        //    configurationBuilder.Properties<Match>().HaveConversion<OddConverter>();
        //}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Match>(e =>
            {
                e.ToTable("Matches").HasKey(x => x.ID);
                e.HasIndex(p => new { p.MatchDate, p.TeamA }).IsUnique();
                e.HasIndex(p => new { p.MatchDate, p.TeamB }).IsUnique();
                e.HasIndex(p => new { p.MatchDate, p.TeamA, p.TeamB }).IsUnique();
                e.Property(x => x.Description).HasMaxLength(255).HasDefaultValue(string.Empty).IsRequired();
                e.Property(x => x.MatchDate).HasMaxLength(128).HasDefaultValue(string.Empty).IsRequired();
                e.Property(x => x.MatchTime).HasMaxLength(10).HasDefaultValue(string.Empty).IsRequired();
                e.Property(x => x.TeamA).HasMaxLength(20).HasDefaultValue(string.Empty).IsRequired();
                e.Property(x => x.TeamB).HasMaxLength(20).HasDefaultValue(string.Empty).IsRequired();
                e.Property(x => x.Sport).IsRequired();
                e.HasMany(x => x.Odds).WithOne(z => z.Match).OnDelete(DeleteBehavior.ClientCascade);

            });

            modelBuilder.Entity<Odd>(e =>
            {
                e.ToTable("Odds").HasKey(x => x.ID);
                e.Property(x => x.Specifier).HasMaxLength(255).HasDefaultValue(string.Empty);
                e.Property(x => x.MatchOdd).HasColumnName("Odd");
            });
        }
    }
}