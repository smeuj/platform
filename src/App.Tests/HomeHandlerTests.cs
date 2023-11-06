using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Smeuj.Platform.App.Features.Home;
using Smeuj.Platform.Domain;
using Smeuj.Platform.Infrastructure.Database;

namespace Smeuj.Platform.App.Tests;

[TestClass]
public class HomeHandlerTests {
    private readonly ILogger<HomeHandler> mockLogger = Substitute.For<ILogger<HomeHandler>>();
    private HomeHandler homeHandler = null!;
    private Database context = null!;
    private readonly Fixture fixture = new();
    private readonly List<Smeu> smeuj = new(10);

    [TestInitialize]
    public void Init() {
        var testGuid = Guid.NewGuid();

        context = new Database($"Data Source={testGuid}_tests.db");
        context.Database.Migrate();

        homeHandler = new HomeHandler(context, mockLogger);
    }

    [TestCleanup]
    public void After() {
        context.Database.CloseConnection();
        context.Database.EnsureDeleted();
        context.Dispose();
    }

    [TestMethod]
    public async Task GetHome_WithoutSuggestions_ExpectEmptySuggestions() {
        //arrange
        var ct = CancellationToken.None;

        //act
        var result = await homeHandler.GetHomeAsync(ct);

        //assert
        var model = result.GetParameter<HomeModel>();
        model.Should().NotBeNull();
        model.Smeuj.Should().BeEmpty();
    }

    [TestMethod]
    public async Task GetHome_WithRecords_ExpectSuggestions() {
        //arrange
        var ct = CancellationToken.None;
        await AddSmeuj();

        //act
        var result = await homeHandler.GetHomeAsync(ct);

        //assert
        var model = result.GetParameter<HomeModel>();
        model.Should().NotBeNull();
        model.Smeuj.Should().NotBeEmpty();
        model.Smeuj.Length.Should().Be(6);
        model.Smeuj.Should().BeSubsetOf(smeuj);
    }

    [TestMethod]
    public async Task GetHome_WithManyRecords_ExpectDifferentSuggestions() {
        //arrange
        var ct = CancellationToken.None;
        await AddSmeuj(200);

        //act
        var result1 = await homeHandler.GetHomeAsync(ct);
        var result2 = await homeHandler.GetHomeAsync(ct);

        //assert
        var model = result1.GetParameter<HomeModel>();
        var model2 = result2.GetParameter<HomeModel>();
        model.Should().NotBeNull();
        model.Smeuj.Should().NotBeEmpty();
        model.Smeuj.Length.Should().Be(6);
        model2.Smeuj.Length.Should().Be(6);
        model.Smeuj.Should().BeSubsetOf(smeuj);
        model2.Smeuj.Should().BeSubsetOf(smeuj);
        model.Smeuj.Should().NotBeEquivalentTo(model2.Smeuj);
    }

    [TestMethod]
    public async Task GetSuggestions_WithoutSuggestions_ExpectEmptySuggestions() {
        //arrange
        var ct = CancellationToken.None;

        //act
        var result = await homeHandler.GetSuggestionsAsync(ct);

        //assert
        var suggestions = result.GetParameter<Smeu[]>();
        suggestions.Should().NotBeNull();
        suggestions.Should().BeEmpty();
    }

    [TestMethod]
    public async Task GetSuggestions_WithRecords_ExpectSuggestions() {
        //arrange
        var ct = CancellationToken.None;
        await AddSmeuj();

        //act
        var result = await homeHandler.GetSuggestionsAsync(ct);

        //assert
        var suggestions = result.GetParameter<Smeu[]>();
        suggestions.Should().NotBeNull();
        suggestions.Should().NotBeEmpty();
        suggestions.Length.Should().Be(6);
        suggestions.Should().BeSubsetOf(smeuj);
    }

    [TestMethod]
    public async Task GetSuggestions_WithManyRecords_ExpectDifferentSuggestions() {
        //arrange
        var ct = CancellationToken.None;
        await AddSmeuj(200);

        //act
        var result1 = await homeHandler.GetSuggestionsAsync(ct);
        var result2 = await homeHandler.GetSuggestionsAsync(ct);

        //assert
        var suggestions1 = result1.GetParameter<Smeu[]>();
        var suggestions2 = result2.GetParameter<Smeu[]>();
        suggestions1.Should().NotBeNull();
        suggestions1.Should().NotBeEmpty();
        suggestions1.Length.Should().Be(6);
        suggestions2.Length.Should().Be(6);
        suggestions1.Should().BeSubsetOf(smeuj);
        suggestions2.Should().BeSubsetOf(smeuj);
        suggestions1.Should().NotBeEquivalentTo(suggestions2);
    }

    private async Task AddSmeuj(int count = 10) {
        foreach (var i in Enumerable.Range(0, count)) {
            var suggestion = fixture.Create<Smeu>();
            smeuj.Add(suggestion);
        }

        context.Smeuj.AddRange(smeuj);
        await context.SaveChangesAsync();
    }
}