using System;
using System.Reflection;
using FluentMigrator.Runner;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Nouwan.Smeuj.Api.Extensions;
using Nouwan.Smeuj.DataAccess;
using Nouwan.Smeuj.DataAccess.Migrations;
using Nouwan.Smeuj.Framework;
using OpenIddict.Validation.AspNetCore;
using Serilog;

namespace Nouwan.Smeuj.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" }); });

            services.AddFluentMigratorCore().ConfigureRunner(conf =>
                conf.AddPostgres()
                    .WithGlobalConnectionString(Configuration.GetConnectionString("default"))
                    .WithMigrationsIn(Assembly.GetAssembly(typeof(InitialMigration))));
            services.AddMediatR(typeof(Startup));

            new ProjectRegistrations().Register(services);

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
            });

            services.AddOpenIddict()
                .AddValidation(options =>
                {

                    options.SetIssuer("https://localhost:5002/");
                    options.AddAudiences("smeuj_api");

                    options.UseSystemNetHttp();
                    options.UseAspNetCore();
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var logger = LoggerFactory.CreateLogger<Startup>();
             
            logger.Debug("Configure; Start configure.");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseSerilogRequestLogging();
            logger.Information("Configure; Configured app");

            logger.Debug("Configure; Starting migrating the database");
            app.Migrate();
            logger.Information("Configure; Finished migrating the database");
        }
    }
}