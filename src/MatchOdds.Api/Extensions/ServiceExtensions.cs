using MatchOdds.Domain.Interfaces;
using MatchOdds.Domain.Repositories;
using MatchOdds.Domain.Services;
using MatchOdds.Domain.UnitOfWork;
using MatchOdds.Infrastructure;
using MatchOdds.Infrastructure.Repositories;
using MatchOdds.Logger;
using Microsoft.EntityFrameworkCore;

namespace MatchOdds.Api.Configuration
{
    public static class ServiceExtensions
    {
        public static void AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure DBContext
            services.AddDbContext<MatchOddsContext>(options => options.EnableSensitiveDataLogging()
            .UseSqlServer(configuration.GetConnectionString("matchOddsConString"),
            b => b.MigrationsAssembly("MatchOdds.Infrastructure")));
        }

        /// <summary>
        /// Migrate database with migrations if not applied and exist
        /// </summary>
        /// <param name="app"></param>
        public static void UseDatabaseMigrate(this IApplicationBuilder app)
        {
            try
            {
                using (var serviceScope = app.ApplicationServices.CreateScope())
                {
                    // Takes all of our migrations files and apply them against the database in case they are not implemented
                    serviceScope.ServiceProvider.GetService<MatchOddsContext>().Database.Migrate();
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void ConfigureRepositoryServices(this IServiceCollection services)
        {
            // Configure Repositories
            services.AddScoped<IMatchRepository, MatchRepository>()
                    .AddScoped<IOddRepository, OddRepository>()
                    .AddScoped<IUnitOfWork, UnitOfWork>();

            // Configure Wrapper Of Repositories
            services.AddScoped<IMatchRepositoryService, MatchRepositoryService>()
                    .AddScoped<IOddRepositoryService, OddRepositoryService>();
        }

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
    }
}
