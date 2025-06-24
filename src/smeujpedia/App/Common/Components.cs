using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Smeuj.Platform.App.Pages.Home;
using Smeuj.Platform.App.Pages.Profile;
using Smeuj.Platform.Domain;

namespace Smeuj.Platform.App.Common; 

public static class Components {
    
    public static IResult Home(HomeModel homeModel) {
        return new RazorComponentResult<Home>(new{ Model = homeModel});
    }
    
    public static IResult Suggestions(Smeu[] smeuj) {
        return new RazorComponentResult<Suggestions>(new{ Value = smeuj});
    }
    
    public static IResult SmeujList(Smeu[] smeuj) {
        return new RazorComponentResult<SmeujList>(new{ Smeuj = smeuj});
    }

    public static IResult Search(Smeu[] results) {
        return new RazorComponentResult<SearchResults>(new { Results = results});
    }

    public static IResult Profile(ClaimsPrincipal user) {
        return new RazorComponentResult<Profile>(new { User = user});
    }
}