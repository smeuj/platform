﻿namespace Smeuj.Platform.Domain;

public class Smeu(int id, string value, int authorId, ulong discordId,
    DateTimeOffset submittedOn, DateTimeOffset processedOn, int version) {
    
    public int Id { get; private set; } = id;

    public string Value { get; } = value;

    public int AuthorId { get; } = authorId;

    public required Author Author { get; init;}

    public ulong DiscordId { get; } = discordId;

    public DateTimeOffset SubmittedOn { get; } = submittedOn;

    public DateTimeOffset ProcessedOn { get; } = processedOn;

    public int Version { get; private set; } = version;

}