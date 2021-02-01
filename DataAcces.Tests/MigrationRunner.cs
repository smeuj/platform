using System;
using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Nouwan.Smeuj.DataAccess.Migrations;

namespace Nouwan.Smeuj.DataAccess.Tests
{
    internal class Migrate
    {
        public static void Run()
        {
            var serviceProvider = CreateServices();

            // Put the database update into a scope to ensure
            // that all resources will be disposed.
            using var scope = serviceProvider.CreateScope();
            UpdateDatabase(scope.ServiceProvider);
        }

        /// <summary>
        /// Configure the dependency injection services
        /// </summary>
        private static IServiceProvider CreateServices()
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddPostgres()
                        .WithGlobalConnectionString(DbTestHelper.ConnectionString)
                        .WithMigrationsIn(Assembly.GetAssembly(typeof(InitialMigration))))
                .BuildServiceProvider(false);
        }

        /// <summary>
        /// Update the database
        /// </summary>
        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            if(runner.HasMigrationsToApplyUp())
            {
                runner.MigrateUp();
            }
        }
    }
}
