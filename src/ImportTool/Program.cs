using Discord;
using Discord.Rest;
using Microsoft.EntityFrameworkCore;
using Smeuj.Platform.Domain;
using Smeuj.Platform.Infrastructure.Database;

Console.WriteLine("Smeu Smokkelaar Import Tool");

var context = new Database();
context.Database.Migrate();

var client = new DiscordRestClient();

await client.LoginAsync(TokenType.Bot, "");
var guild = await client.GetGuildAsync(0);
var channel = await guild.GetTextChannelAsync(0);

var firstMessage = await channel.GetMessageAsync(0);
var calls = channel.GetMessagesAsync(0, Direction.After, 5000);
var list = new List<RestMessage>(5001) { firstMessage };
await foreach (var messages in calls) {
    list.AddRange(messages);
}

var sortedMessages = list.OrderBy(row => row.CreatedAt).ToArray();
foreach (var msg in sortedMessages) {
    if (msg.Author.IsBot) {
        await ParseMessage(msg, context);
    }
}

return;

async Task ParseMessage(IMessage message, Database database) {
    Console.WriteLine("Found message: " + message);
    var lines = message.Content.Split("\n");

    var smeuString = lines[0].Replace("**", "");

    var authorLine = lines.Single(row => row.StartsWith("Genoemd"));

    var authorParts = authorLine.Split(" ");
    var authorString = authorParts[2];

    var dateString = authorParts[4] + " " + authorParts[5];
    var date = DateTimeOffset.Parse(dateString);

    var author = database.Authors.SingleOrDefault(row => row.PublicName == authorString);

    if (author == null) {
        Console.WriteLine("Author not found: " + authorString);
        var discordId = GetDiscordIdFromUser();

        author = new Author(authorString, authorString, discordId, date);

        database.Authors.Add(author);
        await database.SaveChangesAsync();
    }

    var duplicate = await database.Smeuj.Where(row => row.Value == smeuString).AnyAsync();
    if (duplicate) {
        Console.WriteLine("Smeu already exists: " + smeuString + " press any key to continue");
        return;
    }

    var smeu = new Smeu(smeuString, message.Id, date, DateTimeOffset.Now) {
        Author = author,
    };

    var inspirationLine = lines.SingleOrDefault(line => line.StartsWith("Geïnspireerd door"));
    if (inspirationLine is not null) {
        var inspirationString = inspirationLine.Replace("Geïnspireerd door", "").Trim();

        var inspirationAuthor = database.Authors.SingleOrDefault(row => row.PublicName == inspirationString);
        var inspiration = inspirationAuthor == null
            ? GetInspirationFromUser(inspirationString, message.Timestamp)
            : new Inspiration(InspirationType.Author, message.Timestamp) {
                Author = author
            };

        smeu.AddInspiration(inspiration);
    }

    var exampleLines = lines.Where(line => line.StartsWith('"')).ToArray();

    foreach (var exampleLine in exampleLines) {
        var startExample = exampleLine.IndexOf('"');
        var endExample = exampleLine.LastIndexOf('"');
        var exampleString = exampleLine.Substring(startExample, endExample).Replace("\"", "");

        var startAuthorAndDate = exampleLine.IndexOf('(');
        var authorAndDate = exampleLine.Substring(startAuthorAndDate, exampleLine.Length - startAuthorAndDate)
            .Split(',');

        var exampleAuthorString = authorAndDate[1].Replace("(", "");

        var exampleAuthor = database.Authors.SingleOrDefault(row => row.PublicName == exampleAuthorString);
        if (author is null) {
            exampleAuthor = new Author(exampleAuthorString, exampleAuthorString, GetDiscordIdFromUser(),
                message.Timestamp);
        }

        var exampleDateString = authorAndDate[1].Replace(")", "");
        var exampleDate = DateTimeOffset.Parse(exampleDateString);

        var example = new Example(exampleString, exampleDate) {
            Author = exampleAuthor
        };

        smeu.AddExample(example);
    }

    database.Smeuj.Add(smeu);

    try {
        await database.SaveChangesAsync();
    }
    catch (DbUpdateException) {
        Console.WriteLine("It seems like Smeu " + smeuString +
                          " already exists in the database. Please check manually and perform corrections press any key to continue");
        Console.ReadKey();
    }
}

Inspiration GetInspirationFromUser(string inspirationString, DateTimeOffset messageTimestamp) {
    Console.WriteLine("Could not find an Author for the following Inspiration: " + inspirationString);
    var type = GetInspirationTypeFromUser();

    if (type == InspirationType.Author) {
        return new Inspiration(type, messageTimestamp) {
            Author = new Author(inspirationString, inspirationString, GetDiscordIdFromUser(), messageTimestamp)
        };
    }

    return new Inspiration(type, messageTimestamp, inspirationString);
}

InspirationType GetInspirationTypeFromUser() {
    while (true) {
        Console.WriteLine("Please enter the type of Inspiration 0=Author, 1=Text, 2=Url");
        var userInput = Console.ReadLine();

        if (userInput == null) {
            Console.WriteLine("Invalid Input");
            continue;
        }

        var parsed = int.TryParse(userInput, out var id);

        if (!parsed) {
            Console.WriteLine("Invalid Input");
            continue;
        }

        if (Enum.IsDefined(typeof(InspirationType), id)) {
            var type = (InspirationType)id;
            return type;
        }

        Console.WriteLine("Invalid Input");
    }
}

ulong? GetDiscordIdFromUser() {
    while (true) {
        const string question = "Enter Discord Id for new author";
        Console.WriteLine(question);
        var discordId = Console.ReadLine();

        if (string.IsNullOrEmpty(discordId)) {
            Console.WriteLine("No Discord Id provided. Do you want to save the author without a Discord Id? y/n");
            var answer = Console.ReadLine();
            if (answer == "y") {
                return null;
            }

            continue;
        }

        var parsed = ulong.TryParse(discordId, out var id);

        if (!parsed) {
            continue;
        }

        return id;
    }
}