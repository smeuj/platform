using Sqids;

namespace Smeuj.Platform.App.Lib.Sqids; 

public static class RegisterSqidsExtensions {

    public static void RegisterSqids(this IServiceCollection services, IConfiguration configurationManager) {

        var options = new SqidsConfigOptions();
        configurationManager.GetSection(SqidsConfigOptions.Section).Bind(options);
        services.AddSingleton(new SqidsEncoder<int>(new SqidsOptions {
            Alphabet = options.Alphabet,
            MinLength = options.MinLength
        }));
        
    }
    
}