namespace Smeuj.Platform.Domain;

public class Smeu(string value, ulong discordId,
    DateTimeOffset submittedOn, DateTimeOffset processedOn, int version) {

    public Smeu(int id, string value, int authorId, ulong discordId,
        DateTimeOffset submittedOn, DateTimeOffset processedOn, int version)
        :this(value, discordId, submittedOn, processedOn, version) {
        Id = id;
        AuthorId = authorId;
    }
    
    public int Id { get; private set; }

    public string Value { get; } = value;

    public int AuthorId { get; }

    public required Author Author { get; init;}

    public ulong DiscordId { get; } = discordId;

    public DateTimeOffset SubmittedOn { get; } = submittedOn;

    public DateTimeOffset ProcessedOn { get; } = processedOn;

    public int Version { get; private set; } = version;

}