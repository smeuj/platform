using Microsoft.AspNetCore.Http.HttpResults;
using Smeuj.Platform.App.Features.Home;
using Smeuj.Platform.Domain;

namespace Smeuj.Platform.App.Common; 

public static class Components {
    
    public static IResult Home(HomeModel homeModel) {
        return new RazorComponentResult<Home>(new{ Model = homeModel});
    }
    
    public static IResult Suggestions(Smeu[] suggestions) {
        return new RazorComponentResult<Suggestions>(new{ Value = suggestions});
    }
    
    public static IResult SmeujList(Smeu[] suggestions) {
        return new RazorComponentResult<SmeujList>(new{ Smeuj = suggestions});
    }

    public static IResult Search(Smeu[] results) {
        return new RazorComponentResult<SearchResults>(new { Results = results});
    }
}