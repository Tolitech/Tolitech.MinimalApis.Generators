# Tolitech.MinimalApis.Generators.Abstractions

## Overview

This project provides an abstraction for endpoint definition in ASP.NET applications, making it easier to standardize and automatically register HTTP routes. The main goal is to allow endpoints to be defined via interfaces, promoting organization and extensibility.

## Main Interface

```csharp
public interface IEndpoint
{
    static abstract void MapEndpoint(IEndpointRouteBuilder app);
}
```

- **IEndpoint**: Interface to be implemented by classes representing endpoints. The static `MapEndpoint` method is responsible for registering routes in the application's pipeline.

## How to Use

1. **Implement the IEndpoint interface:**

```csharp
using Tolitech.MinimalApis.Generators.Abstractions;
using Microsoft.AspNetCore.Routing;

public sealed class MyEndpoint : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/my-endpoint", () => "Hello, world!");
    }
}
```

2. **Use the generator to automatically register all endpoints:**

When used together with Tolitech.MinimalApis.Generators, all endpoints implementing `IEndpoint` will be automatically registered.

## Benefits
- Standardization of endpoint definitions
- Easier testing and maintenance
- Integration with code generators

## Requirements
- ASP.NET Core

## Examples
Below is an example of implementation and registration:

```csharp
public sealed class TestEndpoint : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/test", () => "Testing endpoint!");
    }
}
```

When running the application, the `/test` endpoint will be available automatically if the generator is used.

## Notes
This project is intended to be used together with Tolitech.MinimalApis.Generators for automated endpoint registration.