using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;

using Tolitech.Presentation.API.Generators.Abstractions;

namespace Tolitech.Presentation.API.Generators.IntegrationTests.Implementations.Endpoints;

/// <summary>
/// Defines the endpoints for testing purposes within the application.
/// </summary>
/// <remarks>This class is responsible for setting up and handling specific HTTP routes related to testing. It
/// provides methods to register these routes and handle incoming requests, ensuring that the application can respond
/// appropriately to test-related HTTP requests.</remarks>
internal sealed class TestEndpoint : IEndpoint
{
    private const string Route = "/test1";

    /// <summary>
    /// Registers the endpoint for the application.
    /// </summary>
    /// <remarks>This method sets up the necessary endpoints for handling HTTP requests. It maps a GET request
    /// to a specific route and configures metadata such as the endpoint name and tags.</remarks>
    /// <param name="app">The <see cref="IEndpointRouteBuilder"/> used to configure the application's routing.</param>
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(Route, Handle)
            .WithName("GetTest1")
            .WithTags("Tests")
            .WithSummary("Tests")
            .WithDescription("Tests");
    }

    /// <summary>
    /// Handles the request and returns a result indicating success.
    /// </summary>
    /// <returns>A <see cref="Results{T1, T2}"/> object containing an <see cref="Ok"/> result.</returns>
    public static Results<Ok, BadRequest> Handle()
    {
        return TypedResults.Ok();
    }
}