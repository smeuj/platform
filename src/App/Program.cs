using Microsoft.AspNetCore.Http.HttpResults;
using Smeuj.Platform.App.Features.Home;
using Smeuj.Platform.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

//Register services
builder.Services.RegisterInfrastructure();

// Add services to the container.
builder.Services.AddRazorComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapGet("/", () => new RazorComponentResult<Home>());
app.MapPost("/clicked", () => new RazorComponentResult<Test>());


app.Services.MigrateDatabase();
app.Run();