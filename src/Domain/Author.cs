namespace Smeuj.Platform.Domain; 

public class Author(int id, string publicName, ulong discordId, DateTimeOffset authorSince, int version) {
    public int Id { get; private set; } = id;

    public string PublicName { get; } = publicName;

    public ulong DiscordId { get; } = discordId;

    public DateTimeOffset AuthorSince { get; } = authorSince;

    public int Version { get; private set; } = version;
}