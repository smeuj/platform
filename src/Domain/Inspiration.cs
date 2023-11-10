namespace Smeuj.Platform.Domain;

public class Inspiration(
    InspirationType type,
    DateTimeOffset submittedOn,
    string? value = null) {

    public Inspiration(int id, InspirationType type, DateTimeOffset submittedOn, int smeuId, int? authorId, string? value)
        :this(type, submittedOn, value) {
        Id = id;
        SmeuId = smeuId;
        AuthorId = authorId;
    }
    
    public int Id { get; private set; }

    public InspirationType Type { get; } = type;

    public DateTimeOffset SubmittedOn { get; } = submittedOn;

    public DateTimeOffset ProcessedOn { get; } = DateTimeOffset.Now;

    public int SmeuId { get; }

    public int? AuthorId { get; }

    public Author? Author { get; init; }

    public string? Value { get; } = value;
}