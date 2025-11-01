# Tolitech.MinimalApis.Generators.Abstractions

## Visão Geral

Este projeto fornece uma abstração para definição de endpoints em aplicações ASP.NET, facilitando a padronização e o registro automático de rotas HTTP. O principal objetivo é permitir que endpoints sejam definidos por meio de interfaces, promovendo organização e extensibilidade.

## Interface Principal

```csharp
public interface IEndpoint
{
    static abstract void MapEndpoint(IEndpointRouteBuilder app);
}
```

- **IEndpoint**: Interface que deve ser implementada por classes que representam endpoints. O método estático `MapEndpoint` é responsável por registrar as rotas no pipeline da aplicação.

## Como Usar

1. **Implemente a interface IEndpoint:**

```csharp
using Tolitech.MinimalApis.Generators.Abstractions;
using Microsoft.AspNetCore.Routing;

public sealed class MeuEndpoint : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/meu-endpoint", () => "Olá, mundo!");
    }
}
```

2. **Utilize o gerador para registrar todos os endpoints automaticamente:**

Ao utilizar o projeto junto com o Tolitech.MinimalApis.Generators, todos os endpoints que implementam `IEndpoint` serão registrados automaticamente.

## Vantagens
- Padronização na definição de endpoints
- Facilidade para testes e manutenção
- Integração com geradores de código

## Requisitos
- ASP.NET Core

## Exemplos
Veja abaixo um exemplo de implementação e registro:

```csharp
public sealed class TestEndpoint : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/test", () => "Testando endpoint!");
    }
}
```

Ao rodar a aplicação, o endpoint `/test` estará disponível automaticamente se o gerador for utilizado.

## Observações
Este projeto é destinado a ser utilizado em conjunto com o Tolitech.MinimalApis.Generators para automação do registro de endpoints.