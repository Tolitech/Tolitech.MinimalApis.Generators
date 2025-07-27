# Tolitech.Presentation.API.Generators

## Visão Geral

Este projeto implementa um gerador de código Roslyn para ASP.NET, que identifica automaticamente todas as classes que implementam a interface `IEndpoint` e gera código para registrar esses endpoints em uma aplicação web. O objetivo é automatizar o registro de rotas, reduzindo o trabalho manual e promovendo padronização.

## Funcionamento

O gerador escaneia o projeto em busca de classes que implementam `Tolitech.Presentation.API.Generators.Abstractions.IEndpoint` e não são abstratas. Para cada classe encontrada, o gerador cria um método de extensão que registra todos os endpoints na aplicação.

## Exemplo de Código Gerado

```csharp
public static class EndpointRouteBuilderExtensions
{
    public static void MapEndpoints(this IEndpointRouteBuilder app)
    {
        MeuEndpoint.MapEndpoint(app);
        OutroEndpoint.MapEndpoint(app);
        // ... outros endpoints
    }
}
```

## Como Usar

1. **Implemente a interface IEndpoint em seus endpoints** (veja README do projeto Abstractions).
2. **Adicione o Tolitech.Presentation.API.Generators como analisador no seu projeto**.
3. **Chame o método gerado para registrar todos os endpoints:**

```csharp
app.MapEndpoints();
```

## Benefícios
- Registro automático de endpoints
- Redução de código repetitivo
- Facilidade de manutenção
- Integração transparente com ASP.NET

## Requisitos
- .NET Standard 2.0 (compatível com .NET 6+)
- Microsoft.CodeAnalysis.CSharp

## Exemplo Completo

```csharp
// Definição do endpoint
public sealed class TestEndpoint : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/test", () => "Testando endpoint!");
    }
}

// Registro automático
app.MapEndpoints();
```

## Observações
Este projeto depende do Tolitech.Presentation.API.Generators.Abstractions para identificar os endpoints. Recomenda-se utilizar ambos em conjunto.