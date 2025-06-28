namespace Smeuj.Platform.App.Pages.Home;

public interface IHomeHandler {
    Task<IResult> GetHomeAsync(string? search, CancellationToken ct);

    Task<IResult> GetSuggestionsAsync(CancellationToken ct);
}