using Microsoft.AspNetCore.Http.HttpResults;
using Smeuj.Platform.App.Features.Home;

namespace Smeuj.Platform.App.Common; 

public static class Components {
    
    public static IResult Home(HomeModel homeModel) {
        return new RazorComponentResult<Home>(new{ Model = homeModel});
    }
    
}