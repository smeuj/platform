using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Smeuj.Platform.Domain;
using Smeuj.Platform.Infrastructure.Database;
using Smeuj.Platform.Infrastructure.Queries.Smeuj;

namespace Features.Smeuj.Suggestions;

public record GetSuggestions : IRequest<Smeu[]>;

public class GetSuggestionsHandler(SmeujContext context, ILogger<GetSuggestionsHandler> logger)
    : IRequestHandler<GetSuggestions,Smeu[]> {
    
    public async Task<Smeu[]> Handle(GetSuggestions request, CancellationToken cancellationToken) {
        var smeujCount = await context.Smeuj.CountAsync(cancellationToken);

        if (smeujCount == 0) {
            return Array.Empty<Smeu>();
        }

        const int suggestionsCount = 6;
        var suggestedSmeuj = await context.Smeuj.GetSuggestions(suggestionsCount)
            .ToArrayAsync(cancellationToken);

        logger.LogDebug("GetHomeAsync; Getting {SuggestionCount} Smeu suggestions from the database", suggestionsCount);
        return suggestedSmeuj;
    }
}