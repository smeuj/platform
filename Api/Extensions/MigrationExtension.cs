using System;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Nouwan.Smeuj.Api.Extensions
{
    public static class MigrationExtension
    {
        public static IApplicationBuilder Migrate(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var runner = scope.ServiceProvider.GetService<IMigrationRunner>();
            if( runner == null) throw new InvalidOperationException("Runner not registered!");
            
            runner.Processor.BeginTransaction();
            runner.MigrationLoader.LoadMigrations();
            runner.ListMigrations();
            runner.MigrateUp();
            runner.Processor.CommitTransaction();
            return app;
        }
    }
}
