using Features.Suggestions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Smeuj.Platform.App.Common;
using Smeuj.Platform.Domain;
using Smeuj.Platform.Infrastructure.Database;

namespace Smeuj.Platform.App.Pages.Home;

public class HomeHandler : IHomeHandler {
    private readonly SmeujContext context;
    private readonly IHttpContextAccessor accessor;
    private readonly ILogger<HomeHandler> logger;
    private readonly IMediator mediator;

    public HomeHandler(SmeujContext context, IHttpContextAccessor accessor, ILogger<HomeHandler> logger, IMediator mediator) {
        this.context = context;
        this.accessor = accessor;
        this.logger = logger;
        this.mediator = mediator;
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

        var suggestions = await mediator.Send(new GetSuggestions(), ct);
        logger.LogInformation("GetHomeAsync; Returning {SuggestionCount} suggestions",
            suggestions.Length);

        return accessor.IsHtmx()
            ? Components.Suggestions(suggestions)
            : Components.Home(new HomeModel(suggestions));
    }

    public async Task<IResult> GetSuggestionsAsync(CancellationToken ct) {
        var suggestions = await mediator.Send(new GetSuggestions(), ct);
        logger.LogInformation("GetSuggestionsAsync; Returning suggestions with {SuggestionCount} suggestions",
            suggestions.Length);
        return Components.SmeujList(suggestions);
    }



    private async Task<Smeu[]> SearchAsync(string search, CancellationToken ct) {
        var searchTerm = search.ToLowerInvariant();
        var smeuj = await context.Smeuj.Where(s => s.Value.Contains(searchTerm)).ToArrayAsync(ct);

        logger.LogDebug("SearchAsync; Found {Count} for search {Search}", smeuj.Length, searchTerm);
        return smeuj;
    }
}