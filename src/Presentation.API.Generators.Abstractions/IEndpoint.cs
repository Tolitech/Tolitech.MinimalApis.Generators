using Microsoft.AspNetCore.Routing;

namespace Tolitech.Presentation.API.Generators.Abstractions;

/// <summary>
/// Defines a contract for mapping an endpoint to an <see cref="IEndpointRouteBuilder"/>.
/// </summary>
/// <remarks>Implementations of this interface should provide the logic to configure and map specific endpoints
/// within an application's routing system. This is typically used to set up routes for handling HTTP requests in a web
/// application.</remarks>
public interface IEndpoint
{
    /// <summary>
    /// Maps the endpoint to the specified <see cref="IEndpointRouteBuilder"/>.
    /// </summary>
    /// <param name="app">The <see cref="IEndpointRouteBuilder"/> to which the endpoint will be mapped. Cannot be null.</param>
    static abstract void MapEndpoint(IEndpointRouteBuilder app);
}