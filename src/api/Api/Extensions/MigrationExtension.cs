using System.Collections.Generic;
using System.Collections.Immutable;
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
            var runners = scope.ServiceProvider.GetServices<IMigrationRunner>()
                .ToImmutableArray();
            
            if (!runners.Any())
            {
                logger.LogWarning("No migration runners found. You should probably check your registrations.");
                return;
            }

            var test = ImmutableList.Create<string>();

            var list = new List<string>();
            
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