using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;

using Tolitech.Presentation.API.Generators.UnitTests.Implementations.Endpoints;

namespace Tolitech.Presentation.API.Generators.UnitTests;

/// <summary>
/// Contains unit tests for verifying the behavior of endpoint registration in a web application.
/// </summary>
public class GeneratorTests
{
    /// <summary>
    /// Tests that the MapEndpoints method does not throw an exception.
    /// </summary>
    [Fact]
    public void Endpoint_MapEndpoints_ShouldNotThrowsException()
    {
        // Arrange
        var builder = WebApplication.CreateBuilder();
        var app = builder.Build();

        // Act
        app.MapEndpointsGenerated();

        // Assert
        Assert.True(true);
    }

    /// <summary>
    /// Tests that the <see cref="TestEndpoint.Handle"/> method returns a result of type <see cref="Results{T1, T2}"/>
    /// where <c>T1</c> is <see cref="Ok"/> and <c>T2</c> is <see cref="BadRequest"/>.
    /// </summary>
    [Fact]
    public void Endpoint_Handle_ShouldReturnsOkResult()
    {
        // Arrange && Act
        var result = TestEndpoint.Handle();

        // Assert
        Assert.IsType<Results<Ok, BadRequest>>(result);
    }
}