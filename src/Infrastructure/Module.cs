using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Smeuj.Platform.Infrastructure.Database;

namespace Smeuj.Platform.Infrastructure; 

public static class Module {

    public static void RegisterInfrastructure(this IServiceCollection services) {

        services.AddDbContext<Database.Database>();
    }

    public static void MigrateDatabase(this IServiceProvider serviceProvider) {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<Database.Database>();
        context.Database.Migrate();
    }
}