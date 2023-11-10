namespace Smeuj.Platform.Domain;

public class Example(
    string value,
    DateTimeOffset submittedOn) {

    public Example(int id, string value, DateTimeOffset submittedOn, DateTimeOffset processedOn, int smeuId, int version, 
        int authorId):this(value, submittedOn) {
        Id = id;
        AuthorId = authorId;
        ProcessedOn = processedOn;
        Version = version;
        SmeuId = smeuId;
    }
    
    public int Id { get; private set; }

    public string Value { get; } = value;

    public DateTimeOffset SubmittedOn { get; } = submittedOn;

    public DateTimeOffset ProcessedOn { get; } = DateTimeOffset.Now;
    
    public int SmeuId { get; private set; }

    public int Version { get; private set; }

    public int AuthorId { get; }

    public Author? Author { get; init; }
}