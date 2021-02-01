using Serilog;

namespace Nouwan.Smeuj.Framework
{
    public static class LoggerFactory
    {
        public static ILogger CreateLogger<T>() => Log.Logger.ForContext<T>();
    }
}