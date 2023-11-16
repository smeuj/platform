namespace Smeuj.Platform.App.Lib.Version; 

internal class VersionOptions(string encodedVersion) {
    private string EncodedVersion { get; } = encodedVersion;
    public string CacheBusting => $"ver={EncodedVersion}";
}