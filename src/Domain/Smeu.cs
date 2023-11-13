
namespace Smeuj.Platform.Domain;

public class Smeu(string value, ulong discordId,
    DateTimeOffset submittedOn, DateTimeOffset processedOn) {

    public Smeu(int id, string value, int authorId, ulong discordId,
        DateTimeOffset submittedOn, DateTimeOffset processedOn, int version)
        :this(value, discordId, submittedOn, processedOn) {
        Id = id;
        AuthorId = authorId;
        Version = version;
    }
    
    public int Id { get; private set; }

    public string Value { get; } = value;

    public int AuthorId { get; }

    public required Author Author { get; init;}

    public ulong DiscordId { get; } = discordId;

    public DateTimeOffset SubmittedOn { get; } = submittedOn;

    public DateTimeOffset ProcessedOn { get; } = processedOn;

    public int Version { get; private set; }

    public ICollection<Inspiration> Inspirations { get; init; } = new List<Inspiration>(1);

    public ICollection<Example> Examples { get; init; } = new List<Example>(1);

    public void AddInspiration(Inspiration inspiration) {
        Inspirations.Add(inspiration);
    }

    public void AddExample(Example example) {
        Examples.Add(example);
    }
}