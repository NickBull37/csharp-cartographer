using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace csharp_cartographer._05.Services.Investigation
{
    public class SyntaxInvestigator : ISyntaxInvestigator
    {
        public SyntaxInvestigator()
        {
        }

        public void InvestigateTokenData(SyntaxTree syntaxTree)
        {
            // Create a compilation with the syntax tree and necessary references
            var compilation = CSharpCompilation.Create("UnifiedAnalysis")
                .AddSyntaxTrees(syntaxTree)
                .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location));

            // Get the semantic model
            var semanticModel = compilation.GetSemanticModel(syntaxTree);

            // Get the root node of the syntax tree
            var root = syntaxTree.GetRoot();

            // Extract all tokens from the syntax tree
            var tokens = root.DescendantTokens();

            // Iterate through each token and gather all information
            foreach (var token in tokens)
            {
                Console.WriteLine($"Token Text: {token.Text}");
                Console.WriteLine($"Token Kind: {token.Kind()}");
                Console.WriteLine($"Token Span: {token.Span}");
                Console.WriteLine($"Leading Trivia: {token.LeadingTrivia}");
                Console.WriteLine($"Trailing Trivia: {token.TrailingTrivia}");

                // Syntax Information
                var parentNode = token.Parent;
                if (parentNode != null)
                {
                    Console.WriteLine($"Parent Node Kind: {parentNode.Kind()}");
                    Console.WriteLine($"Parent Node Type: {parentNode.GetType().Name}");

                    if (parentNode is VariableDeclaratorSyntax variableDeclarator)
                    {
                        Console.WriteLine($"Variable Name: {variableDeclarator.Identifier.Text}");
                    }
                    else if (parentNode is MethodDeclarationSyntax methodDeclaration)
                    {
                        Console.WriteLine($"Method Name: {methodDeclaration.Identifier.Text}");
                    }
                    else if (parentNode is ClassDeclarationSyntax classDeclaration)
                    {
                        Console.WriteLine($"Class Name: {classDeclaration.Identifier.Text}");
                    }
                }

                // Semantic Information
                var symbolInfo = semanticModel.GetSymbolInfo(token.Parent);
                if (symbolInfo.Symbol != null)
                {
                    Console.WriteLine($"Symbol Name: {symbolInfo.Symbol.Name}");
                    Console.WriteLine($"Symbol Kind: {symbolInfo.Symbol.Kind}");
                    Console.WriteLine($"Containing Type: {symbolInfo.Symbol.ContainingType}");
                    Console.WriteLine($"Containing Namespace: {symbolInfo.Symbol.ContainingNamespace}");
                }

                var typeInfo = semanticModel.GetTypeInfo(token.Parent);
                if (typeInfo.Type != null)
                {
                    Console.WriteLine($"Type Name: {typeInfo.Type.Name}");
                    Console.WriteLine($"Type Kind: {typeInfo.Type.TypeKind}");
                    Console.WriteLine($"Is Nullable: {typeInfo.Nullability.FlowState == NullableFlowState.MaybeNull}");
                }

                // Contextual Information
                if (token.Parent is IdentifierNameSyntax identifier)
                {
                    var symbol = semanticModel.GetSymbolInfo(identifier).Symbol;
                    if (symbol != null)
                    {
                        var references = root.DescendantNodes()
                            .OfType<IdentifierNameSyntax>()
                            .Where(id => semanticModel.GetSymbolInfo(id).Symbol?.Equals(symbol) == true);

                        Console.WriteLine("References:");
                        foreach (var reference in references)
                        {
                            Console.WriteLine($"  Found at {reference.GetLocation().GetLineSpan()}");
                        }
                    }
                }
            }
        }
    }
}
