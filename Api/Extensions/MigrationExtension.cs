using System.Linq;
using System.Threading.Tasks;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Nouwan.SmeujPlatform.Api.Extensions
{
    public static class MigrationExtension
    {
        
        public static void Migrate(this IApplicationBuilder app, ILogger logger)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var runners = scope.ServiceProvider.GetService<IMigrationRunner[]>();

            if (runners == null || !runners.Any())
            {
                logger.LogWarning("No migration runners found. You should probably check your registrations.");
                return;
            }

            Parallel.ForEach(runners, runner =>
            {
                runner.Processor.BeginTransaction();
                runner.MigrationLoader.LoadMigrations();
                runner.ListMigrations();
                runner.MigrateUp();
                runner.Processor.CommitTransaction();
            });
        }
    }
}