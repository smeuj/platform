using Microsoft.AspNetCore.Http.HttpResults;
using Smeuj.Platform.App.Features.Home;
using Smeuj.Platform.Domain;

namespace Smeuj.Platform.App.Common; 

public static class Components {
    
    public static IResult Home(HomeModel homeModel) {
        return new RazorComponentResult<Home>(new{ Model = homeModel});
    }
    
    public static IResult SmeujList(Smeu[] suggestions) {
        return new RazorComponentResult<SmeujList>(new{ Smeuj = suggestions});
    }
}