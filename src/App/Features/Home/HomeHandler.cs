using Microsoft.EntityFrameworkCore;
using Smeuj.Platform.App.Common;
using Smeuj.Platform.Domain;
using Smeuj.Platform.Infrastructure.Database;

namespace Smeuj.Platform.App.Features.Home;

public interface IHomeHandler {
    Task<IResult> GetHomeAsync(string? search, CancellationToken ct);

    Task<IResult> GetSuggestionsAsync(CancellationToken ct);
}

public class HomeHandler : IHomeHandler {
    private readonly Database context;
    private readonly IHttpContextAccessor accessor;
    private readonly ILogger<HomeHandler> logger;

    public HomeHandler(Database context, IHttpContextAccessor accessor, ILogger<HomeHandler> logger) {
        this.context = context;
        this.accessor = accessor;
        this.logger = logger;
        logger.LogTrace("HomeHandler;ctor");
    }

    public async Task<IResult> GetHomeAsync(string? search, CancellationToken ct) {
        if (!string.IsNullOrEmpty(search)) {
            var searchResults = await SearchAsync(search, ct);

            logger.LogInformation("GetHomeAsync; Found {Count} for search {Search}", searchResults.Length, search);
            return accessor.IsHtmx()
                ? Components.Search(searchResults)
                : Components.Home(new HomeModel(search, searchResults));
        }

        var suggestions = await CreateSuggestionsAsync(ct);
        logger.LogInformation("GetHomeAsync; Returning {SuggestionCount} suggestions",
            suggestions.Length);
        
        return accessor.IsHtmx()
            ? Components.Suggestions(suggestions)
            : Components.Home(new HomeModel(suggestions));
    }

    public async Task<IResult> GetSuggestionsAsync(CancellationToken ct) {
        var suggestions = await CreateSuggestionsAsync(ct);
        logger.LogInformation("GetSuggestionsAsync; Returning suggestions with {SuggestionCount} suggestions",
            suggestions.Length);
        return Components.SmeujList(suggestions);
    }

    private async Task<Smeu[]> CreateSuggestionsAsync(CancellationToken ct) {
        const int suggestionCount = 6;
        const int offsetCorrection = suggestionCount - 1;
        var smeujCount = await context.Smeuj.CountAsync(ct);

        if (smeujCount == 0) return Array.Empty<Smeu>();

        var maxOffset = smeujCount - offsetCorrection;
        var random = new Random();
        var offset = random.Next(0, maxOffset);
        var suggestions = await context.Smeuj.Skip(offset).Take(suggestionCount).ToArrayAsync(ct);

        logger.LogDebug("GetHomeAsync; Getting {SuggestionCount} Smeu suggestions from the database", suggestionCount);
        return suggestions.OrderBy(_ => Guid.NewGuid()).ToArray();
    }

    private async Task<Smeu[]> SearchAsync(string search, CancellationToken ct) {

        var searchTerm = search.ToLowerInvariant();
        var smeuj = await context.Smeuj.Where(s => s.Value.Contains(searchTerm)).ToArrayAsync(ct);

        logger.LogDebug("SearchAsync; Found {Count} for search {Search}", smeuj.Length, searchTerm);
        return smeuj;
    }
}