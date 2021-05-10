using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nouwan.SmeujPlatform.Messages.Domain;
using Nouwan.SmeujPlatform.Messages.Infrastructure.Migrations;
using Nouwan.SmeujPlatform.Shared.Infrastructure;

namespace Nouwan.SmeujPlatform.Messages.Infrastructure
{
    public static  class ProjectRegistrations
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IDbConnectionFactory<Message>, DbConnectionFactory<Message>>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddFluentMigratorCore().ConfigureRunner(conf =>
                conf.AddPostgres()
                    .WithGlobalConnectionString(configuration.GetConnectionString(nameof(Message)))
                    .WithMigrationsIn(Assembly.GetAssembly(typeof(InitialMigration))));
        }
    }
}
