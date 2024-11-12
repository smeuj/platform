using AutoFixture;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Smeuj.Platform.App.Pages.Home;
using Smeuj.Platform.Domain;
using Smeuj.Platform.Infrastructure.Database;

namespace Smeuj.Platform.App.Tests;

[TestClass]
public class HomeHandlerTests {
    private readonly ILogger<HomeHandler> mockLogger = Substitute.For<ILogger<HomeHandler>>();
    private readonly IHttpContextAccessor mockAccessor = Substitute.For<IHttpContextAccessor>();
    private HomeHandler homeHandler = null!;
    private SmeujContext context = null!;
    private readonly Fixture fixture = new();
    private List<Smeu> smeuj = new(0);
    private readonly HttpContext httpContext = new DefaultHttpContext();
    private readonly IMediator mediator = Substitute.For<IMediator>();

    [TestInitialize]
    public void Init() {
        var testGuid = Guid.NewGuid();

        mockAccessor.HttpContext.Returns(httpContext);
        context = new SmeujContext($"Data Source={testGuid}_tests.db");
        context.Database.Migrate();

        homeHandler = new HomeHandler(context, mockAccessor, mockLogger, mediator);
    }

    [TestCleanup]
    public void After() {
        context.Database.CloseConnection();
        context.Database.EnsureDeleted();
        context.Dispose();
    }
    
    [TestMethod]
    public async Task GetHome_WithRecords_ExpectSuggestions() {
        //arrange
        var ct = CancellationToken.None;
        await AddSmeuj();

        //act
        var result = await homeHandler.GetHomeAsync(string.Empty, ct);

        //assert
        var model = result.GetParameter<HomeModel>();
        model.Should().NotBeNull();
        model.Smeuj.Should().NotBeEmpty();
        model.Smeuj.Length.Should().Be(6);
        model.Smeuj.Should().BeSubsetOf(smeuj);
    }
    
    [TestMethod]
    public async Task GetHome_WithRecordsAndHtmxRequest_ExpectSuggestions() {
        //arrange
        var ct = CancellationToken.None;
        httpContext.Request.Headers["HX-Request"] = "true";
        await AddSmeuj();

        //act
        var result = await homeHandler.GetHomeAsync(string.Empty, ct);

        //assert
        var smeujResults = result.GetParameter<Smeu[]>();
        smeujResults.Should().NotBeNull();
        smeujResults.Should().NotBeEmpty();
        smeujResults.Length.Should().Be(6);
        smeujResults.Should().BeSubsetOf(smeuj);
    }
    
    [TestMethod]
    public async Task GetHome_WithManyRecords_ExpectDifferentSuggestions() {
        //arrange
        var ct = CancellationToken.None;
        await AddSmeuj(200);

        //act
        var result1 = await homeHandler.GetHomeAsync(string.Empty, ct);
        var result2 = await homeHandler.GetHomeAsync(string.Empty, ct);

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
    public async Task GetHome_WithManyRecordsAndHtmxRequest_ExpectDifferentSuggestions() {
        //arrange
        var ct = CancellationToken.None;
        httpContext.Request.Headers["HX-Request"] = "true";
        await AddSmeuj(200);

        //act
        var result1 = await homeHandler.GetHomeAsync(string.Empty, ct);
        var result2 = await homeHandler.GetHomeAsync(string.Empty, ct);

        //assert
        var smeuj1 = result1.GetParameter<Smeu[]>();
        var smeuj2 = result2.GetParameter<Smeu[]>();
        smeuj1.Should().NotBeNull();
        smeuj1.Should().NotBeEmpty();
        smeuj1.Length.Should().Be(6);
        smeuj2.Length.Should().Be(6);
        smeuj1.Should().BeSubsetOf(smeuj);
        smeuj2.Should().BeSubsetOf(smeuj);
        smeuj1.Should().NotBeEquivalentTo(smeuj2);
    }
    
    [TestMethod]
    public async Task GetHome_WithSingleSearch_ExpectCorrectResults() {
        //arrange
        var ct = CancellationToken.None;
        await AddSmeuj();

        //act
        var result = await homeHandler.GetHomeAsync(smeuj[3].Value, ct);

        //assert
        var model = result.GetParameter<HomeModel>();
        model.Should().NotBeNull();
        model.Smeuj.Should().NotBeEmpty();
        model.Smeuj.Length.Should().Be(1);
        model.Smeuj.Single().Should().Be(smeuj[3]);
    }
    
    [TestMethod]
    public async Task GetHome_WithSingleSearchAndHtmxRequest_ExpectCorrectResults() {
        //arrange
        var ct = CancellationToken.None;
        httpContext.Request.Headers["HX-Request"] = "true";
        await AddSmeuj();

        //act
        var result = await homeHandler.GetHomeAsync(smeuj[3].Value, ct);

        //assert
        var resultSmeuj = result.GetParameter<Smeu[]>();
        resultSmeuj.Should().NotBeNull();
        resultSmeuj.Should().NotBeEmpty();
        resultSmeuj.Length.Should().Be(1);
        resultSmeuj.Single().Should().Be(smeuj[3]);
    }
    
    [TestMethod]
    public async Task GetHome_WithMultipleSearch_ExpectCorrectResults() {
        //arrange
        var ct = CancellationToken.None;
        await AddSmeuj(200);
        const string searchTerm = "Smeu1";
        
        //act
        var result = await homeHandler.GetHomeAsync("Smeu1", ct);

        //assert
        var model = result.GetParameter<HomeModel>();
        model.Should().NotBeNull();
        model.Smeuj.Should().NotBeEmpty();
        model.Smeuj.Should().AllSatisfy(smeu => smeu.Value.Should().Contain(searchTerm.ToLowerInvariant()), 
            "Should only have search results");
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
        //this can sometimes fail due to the random nature of the suggestions
        suggestions1.Should().NotBeEquivalentTo(suggestions2);
    }

    private async Task AddSmeuj(int count = 10) {
        smeuj = fixture.Build<Smeu>()
            .FromFactory<int>(row => new Smeu( $"smeu{row}_{Guid.NewGuid()}",fixture.Create<ulong>(), 
                DateTimeOffset.Now, DateTimeOffset.Now) {
                Author = fixture.Create<Author>()
            })
           .CreateMany(count).ToList();
        context.Smeuj.AddRange(smeuj);
        await context.SaveChangesAsync();
    }
}