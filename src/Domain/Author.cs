namespace Smeuj.Platform.Domain;

public class Author(string publicName, ulong discordId, DateTimeOffset authorSince) {
    public Author(int id, string publicName, ulong discordId, DateTimeOffset authorSince, int version)
        : this(publicName, discordId, authorSince) {
        Id = id;
        Version = version;
    }


    public int Id { get; private set; }

    public string PublicName { get; } = publicName;

    public ulong DiscordId { get; } = discordId;

    public DateTimeOffset AuthorSince { get; } = authorSince;

    public int Version { get; private set; }
}