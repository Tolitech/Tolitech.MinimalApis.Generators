using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Tolitech.Presentation.API.Generators;

/// <summary>
/// Represents a source generator that identifies classes implementing a specific interface and generates code to
/// register them as endpoints in a web application.
/// </summary>
/// <remarks>This generator scans the compilation for classes that implement the <c>IEndpointDefinition</c>
/// interface and are not abstract. It then generates an extension method to register all such classes as endpoints in a
/// <c>WebApplication</c>.</remarks>
[Generator]
public class Generator : IIncrementalGenerator
{
    /// <summary>
    /// Initializes the incremental generator by setting up the syntax and compilation providers to identify and process
    /// classes implementing the IEndpointDefinition interface.
    /// </summary>
    /// <remarks>This method configures the generator to collect class declarations that implement the
    /// IEndpointDefinition interface and generates source code to register these endpoints with a web application. The
    /// generated code includes a static method to register all identified endpoints.</remarks>
    /// <param name="context">The context for the incremental generator initialization, providing access to syntax and compilation providers.</param>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var endpointInterfaces = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (s, _) => s is ClassDeclarationSyntax cds && cds.BaseList != null,
                transform: static (ctx, _) => ctx.Node as ClassDeclarationSyntax)
            .Where(cds => cds != null)
            .Collect();

        var combined = context.CompilationProvider.Combine(endpointInterfaces);

        context.RegisterSourceOutput(combined, (spc, source) =>
        {
            var (compilation, classes) = source;
            var endpointDefSymbol = compilation.GetTypeByMetadataName("Tolitech.Presentation.API.Generators.Abstractions.IEndpoint");

            if (endpointDefSymbol is null)
            {
                return;
            }

            List<string> endpointImplementations = [];

            foreach (var cds in classes)
            {
                var model = compilation.GetSemanticModel(cds!.SyntaxTree);
                if (model.GetDeclaredSymbol(cds) is not INamedTypeSymbol classSymbol)
                {
                    continue;
                }

                if (classSymbol.IsAbstract)
                {
                    continue;
                }

                if (classSymbol.AllInterfaces.Any(i => SymbolEqualityComparer.Default.Equals(i, endpointDefSymbol)))
                {
                    endpointImplementations.Add(classSymbol.ToDisplayString());
                }
            }

            // Generate code for registration
            StringBuilder sb = new();
            sb.AppendLine("using Microsoft.AspNetCore.Routing;")
                .AppendLine()
                .AppendLine("namespace Tolitech.Presentation.API.Generators;")
                .AppendLine()
                .AppendLine("/// <summary>")
                .AppendLine("/// Provides extension methods for registering endpoint definitions.")
                .AppendLine("/// </summary>")
                .AppendLine("internal static class EndpointRouteBuilderExtensions")
                .AppendLine("{")
                .AppendLine("    /// <summary>")
                .AppendLine("    /// Registers all application endpoints to the specified <see cref=\"IEndpointRouteBuilder\"/>.")
                .AppendLine("    /// </summary>")
                .AppendLine("    /// <param name=\"app\">The endpoint route builder to which endpoints will be registered.</param>")
                .AppendLine("    internal static void MapEndpoints(this IEndpointRouteBuilder app)")
                .AppendLine("    {");

            foreach (string? impl in endpointImplementations.Distinct())
            {
                sb.AppendLine($"        {impl}.MapEndpoint(app);");
            }

            sb.AppendLine("    }")
                .AppendLine("}");

            spc.AddSource("EndpointRouteBuilderExtensions.g.cs", SourceText.From(sb.ToString(), Encoding.UTF8));
        });
    }
}