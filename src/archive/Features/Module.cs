using Microsoft.Extensions.DependencyInjection;

namespace Features;

public static class Module {

    public static void AddFeatures(this IServiceCollection services) {
        services.AddMediatR(config => {
            config.RegisterServicesFromAssembly(typeof(Module).Assembly);
        });
    }
    
}