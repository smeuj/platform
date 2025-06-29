using FluentResults;

namespace Smeuj.Archive.Api;

internal interface IHandler<in TParameters, TResult> where TResult : IResultBase {

    public Task<TResult> HandleAsync(TParameters parameters, CancellationToken ct);
}
