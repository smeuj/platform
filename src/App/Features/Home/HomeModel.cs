using Smeuj.Platform.Domain;

namespace Smeuj.Platform.App.Features.Home; 

public class HomeModel(Smeu[] suggestions) {
    public HomeModel():this(Array.Empty<Smeu>()) {
    }
    
    public Smeu[] Suggestions { get;} = suggestions;
}