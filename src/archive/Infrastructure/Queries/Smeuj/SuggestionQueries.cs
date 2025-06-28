using Microsoft.EntityFrameworkCore;
using Smeuj.Platform.Domain;

namespace Smeuj.Platform.Infrastructure.Queries.Smeuj;

public static class SuggestionQueries {
    
    public static IQueryable<Smeu> GetSuggestions(this IQueryable<Smeu> smeuj, int suggestionsCount) {
        return smeuj.OrderBy(row => EF.Functions.Random()).Take(suggestionsCount);
    }
    
}