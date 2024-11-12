using Smeuj.Platform.Domain;

namespace Smeuj.Platform.App.Pages.Home; 

public class HomeModel(Smeu[] smeuj) {

    public HomeModel():this(Array.Empty<Smeu>()) {
    }

    public HomeModel(string search, Smeu[] searchResults):this(searchResults) {
        Search = search;
    }
    
    public string? Search { get; }
    
    public Smeu[] Smeuj { get;} = smeuj;

    public bool IsSearch => !string.IsNullOrEmpty(Search);
}