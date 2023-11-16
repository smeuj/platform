namespace Smeuj.Platform.App.Lib.Sqids; 

public class SqidsConfigOptions {
    public const string Section = "Sqids";

    public string Alphabet { get; set; } = string.Empty;
    public int MinLength { get; set; }
}