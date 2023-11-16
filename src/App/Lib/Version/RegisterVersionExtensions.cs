using System.Reflection;
using Sqids;

namespace Smeuj.Platform.App.Lib.Version; 

public static class RegisterVersionExtensions {
    
    public static void RegisterVersion(this IServiceCollection services) {

        services.AddSingleton(provider => {
            var encoder = provider.GetRequiredService<SqidsEncoder<int>>();
            var assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;
            
            if (assemblyVersion == null) {
                throw new InvalidOperationException("Could not get assembly version");
            }

            var encoded = encoder.Encode(assemblyVersion.Major, assemblyVersion.Minor,
                assemblyVersion.Revision, assemblyVersion.Build);
            return new VersionOptions(encoded);
        });
        
    }
}