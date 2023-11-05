using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Smeuj.Platform.App.Tests;

public static class ResultTestExtensions {
    public static TParameter GetParameter<TParameter>(this IResult result) where TParameter : class {
        
        var typed = result as RazorComponentResult;
        foreach (var parameter in typed.Parameters ?? throw new InvalidOperationException()) {
            if (parameter.Value is TParameter possibleValue) return possibleValue;
        }

        throw new InvalidOperationException();
    }
}