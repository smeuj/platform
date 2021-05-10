using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Nouwan.SmeujPlatform.Api.Extensions;
using OpenIddict.Validation.AspNetCore;
using Serilog;

namespace Nouwan.SmeujPlatform.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            Messages.Application.ProjectRegistrations.Register(services, Configuration);
            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" }); });
            services.AddMediatR(typeof(Startup));
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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger )
        {
            logger.LogDebug("Configure; Start configure.");
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
            logger.LogInformation("Configure; Configured app");
            
            logger.LogDebug("Configure; Starting migrating the database");
            app.Migrate(logger);
            logger.LogInformation("Configure; Finished migrating the database");
        }
    }
}