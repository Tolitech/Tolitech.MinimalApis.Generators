# Tolitech.Presentation.API.Generators

## Overview

This project implements a Roslyn code generator for ASP.NET that automatically identifies all classes implementing the `IEndpoint` interface and generates code to register these endpoints in a web application. The goal is to automate route registration, reduce manual work, and promote standardization.

## How It Works

The generator scans the project for classes implementing `Tolitech.Presentation.API.Generators.Abstractions.IEndpoint` that are not abstract. For each class found, the generator creates an extension method that registers all endpoints in the application.

## Example of Generated Code

```csharp
public static class EndpointRouteBuilderExtensions
{
    public static void MapEndpoints(this IEndpointRouteBuilder app)
    {
        MyEndpoint.MapEndpoint(app);
        AnotherEndpoint.MapEndpoint(app);
        // ... other endpoints
    }
}
```

## How to Use

1. **Implement the IEndpoint interface in your endpoints** (see Abstractions project README).
2. **Add Tolitech.Presentation.API.Generators as an analyzer to your project**.
3. **Call the generated method to register all endpoints:**

```csharp
app.MapEndpoints();
```

## Benefits
- Automatic endpoint registration
- Reduction of repetitive code
- Easier maintenance
- Seamless integration with ASP.NET

## Requirements
- .NET Standard 2.0 (compatible with .NET 6+)
- Microsoft.CodeAnalysis.CSharp

## Complete Example

```csharp
// Endpoint definition
public sealed class TestEndpoint : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/test", () => "Testing endpoint!");
    }
}

// Automatic registration
app.MapEndpoints();
```

## Notes
This project depends on Tolitech.Presentation.API.Generators.Abstractions to identify endpoints. It is recommended to use both together.