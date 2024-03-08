using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Smeuj.Platform.Domain;
using Smeuj.Platform.Infrastructure.Database;

namespace Features.Smeuj.Search;

public record SearchSmeujRequest(string SearchQuery) : IRequest<Smeu[]>;

public class SearchSmeujHandler(SmeujContext context, ILogger<SearchSmeujHandler> logger)
    : IRequestHandler<SearchSmeujRequest, Smeu[]> {
    public async Task<Smeu[]> Handle(SearchSmeujRequest request, CancellationToken ct) {

        logger.LogTrace("Handle; Searching Smeuj for request: {SearchQuery}", request.SearchQuery);
        
        var searchTerm = request.SearchQuery.ToLowerInvariant();
        var smeuj = await context.Smeuj.Where(s => s.Value.Contains(searchTerm))
            .ToArrayAsync(ct);

        logger.LogDebug("Handle; Found {Count} for search {Search}", smeuj.Length, searchTerm);
        return smeuj;
    }
    
}