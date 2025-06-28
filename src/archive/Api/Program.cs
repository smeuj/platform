using System.Text.Json.Serialization;
using Scalar.AspNetCore;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddOpenApi();

builder.Services.ConfigureHttpJsonOptions(options => {
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});


var app = builder.Build();

var todosApi = app.MapGroup("/todos");
todosApi.MapGet("/", () => Results.Ok());

if (app.Environment.IsDevelopment()) {
    app.MapScalarApiReference();
    app.MapOpenApi();
}


app.Run();

public record Todo(int Id, string? Title, DateOnly? DueBy = null, bool IsComplete = false);

[JsonSerializable(typeof(Todo[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext {
}