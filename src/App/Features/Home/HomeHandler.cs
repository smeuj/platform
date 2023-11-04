using Microsoft.EntityFrameworkCore;
using Smeuj.Platform.App.Common;
using Smeuj.Platform.Domain;
using Smeuj.Platform.Infrastructure.Database;

namespace Smeuj.Platform.App.Features.Home;

public interface IHomeHandler {

    Task<IResult> GetHomeAsync(CancellationToken ct);

    Task<IResult> GetSuggestionsAsync(CancellationToken ct);
}

public class HomeHandler : IHomeHandler {
    private readonly Database context;
    private readonly ILogger<HomeHandler> logger;

    public HomeHandler(Database context, ILogger<HomeHandler> logger) {
        this.context = context;
        this.logger = logger;
        logger.LogTrace("HomeHandler;ctor");
    }
    
    public async Task<IResult> GetHomeAsync(CancellationToken ct) {
        
        var suggestions = await CreateSuggestionsAsync(ct);
        var model = new HomeModel(suggestions);

        logger.LogInformation("GetHomeAsync; Returning home with {SuggestionCount} suggestions", suggestions.Length);
        return Components.Home(model);
    }
    
    public async Task<IResult> GetSuggestionsAsync(CancellationToken ct) {

        var suggestions = await CreateSuggestionsAsync(ct);
        logger.LogInformation("GetSuggestionsAsync; Returning suggestions with {SuggestionCount} suggestions", suggestions.Length);
        
        return Components.SmeujList(suggestions);
    }

    private async Task<Smeu[]> CreateSuggestionsAsync(CancellationToken ct) {
        const int suggestionCount = 6;
        const int offsetCorrection = suggestionCount - 1;
        var total = await context.Smeuj.CountAsync(ct) - offsetCorrection;
        
        var random = new Random();
        var offset = random.Next(0, total);

        logger.LogDebug("GetHomeAsync; Getting {SuggestionCount} Smeu suggestions from the database", suggestionCount);
        var suggestions = await context.Smeuj.Skip(offset).Take(suggestionCount).ToArrayAsync(ct);
        return suggestions.OrderBy(_ => Guid.NewGuid()).ToArray();
    }
}