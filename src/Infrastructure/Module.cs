using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Smeuj.Platform.Infrastructure.Database;

namespace Smeuj.Platform.Infrastructure; 

public static class Module {

    public static void AddInfrastructure(this IServiceCollection services) {

        services.AddDbContext<Database.SmeujContext>();
    }

    public static void MigrateDatabase(this IServiceProvider serviceProvider) {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<Database.SmeujContext>();
        context.Database.Migrate();
    }
}