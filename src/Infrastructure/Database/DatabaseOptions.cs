namespace Smeuj.Platform.Infrastructure.Database; 

public class DatabaseOptions {
    public const string DataProtection = "Database";

    public string ConnectionString { get; set; } = string.Empty;
}