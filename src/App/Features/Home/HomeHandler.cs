using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Smeuj.Platform.App.Common;
using Smeuj.Platform.Domain;
using Smeuj.Platform.Infrastructure.Database;

namespace Smeuj.Platform.App.Features.Home;

public class HomeHandler : IHomeHandler {
    private readonly Database context;
    private readonly ILogger<HomeHandler> logger;

    public HomeHandler(Database context, ILogger<HomeHandler> logger) {
        this.context = context;
        this.logger = logger;
        logger.LogTrace("HomeHandler;ctor");
    }
    
    public async Task<IResult> GetHomeAsync(CancellationToken ct) {
        var total = await context.Smeuj.CountAsync(ct);
        var random = new Random();
        var offset = random.Next(0, total - 2);

        logger.LogDebug("GetHomeAsync; Getting 3 Smeu suggestions from the database");
        var suggestions = await context.Smeuj.Skip(offset).Take(3).ToArrayAsync(ct);
        var model = new HomeModel(suggestions);

        logger.LogInformation("GetHomeAsync; Returning home with 3 suggestions");;
        return Components.Home(model);
    }

    public Task<Smeu[]> GetSuggestionsAsync(CancellationToken ct) {
        throw new NotImplementedException();
    }

    public Task<RazorComponentResult<SmeujList>> RefreshSuggestionsAsync() {
        throw new NotImplementedException();
    }
}

public interface IHomeHandler {

    Task<IResult> GetHomeAsync(CancellationToken ct);

    Task<Smeu[]> GetSuggestionsAsync(CancellationToken ct);

    Task<RazorComponentResult<SmeujList>> RefreshSuggestionsAsync();
}