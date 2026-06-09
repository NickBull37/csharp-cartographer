using csharp_cartographer_backend._01.Configuration.Configs;
using csharp_cartographer_backend._01.Configuration.Enums;
using csharp_cartographer_backend._03.Models.Artifacts;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace csharp_cartographer_backend._02.Utilities.Logging
{
    public interface ICartographerLogger
    {
        void LogArtifactData(Artifact artifact);
    }

    public class CartographerLogger : ICartographerLogger
    {
        private readonly ILogger<CartographerLogger> _logger;
        private readonly CartographerConfig _config;

        private readonly JsonSerializerOptions options = new()
        {
            WriteIndented = true
        };

        public CartographerLogger(ILogger<CartographerLogger> logger, IOptions<CartographerConfig> config)
        {
            _logger = logger;
            _config = config.Value;
        }

        public void LogArtifactData(Artifact artifact)
        {
            if (_config.ShouldLogSemanticData)
                LogSemanticData(artifact);

            if (_config.ShouldLogUnidentifiedTokens)
                LogUnidentifiedTokens(artifact);
        }

        private void LogSemanticData(Artifact artifact)
        {
            var identifiers = artifact.NavTokens
                .Where(token => token.PrimaryKind is PrimaryKind.Identifier);

            foreach (var token in identifiers)
            {
                var locationData = new
                {
                    token.SemanticData?.IsInUploadedFile,
                    token.SemanticData?.IsInSourceCompilation,
                    token.SemanticData?.IsInReferencedAssemblies,
                    token.SemanticData?.ContainingNamespace,
                    token.SemanticData?.ContainingAssembly,
                };

                var symbolData = new
                {
                    token.SemanticData?.IsAliasSymbol,
                    token.SemanticData?.IsNamespaceSymbol,
                    token.SemanticData?.IsTypeSymbol,
                    token.SemanticData?.IsNamedTypeSymbol,
                    token.SemanticData?.IsDeclaredSymbol,
                    token.SemanticData?.IsOperation,
                    token.SemanticData?.SymbolName,
                    token.SemanticData?.SymbolKind,
                    token.SemanticData?.ContainingType,
                };

                var typeData = new
                {
                    token.SemanticData?.TypeKind,
                    token.SemanticData?.ConvertedTypeKind,
                    token.SemanticData?.IsTypeSymbol,
                    token.SemanticData?.IsNamedTypeSymbol,
                    token.SemanticData?.IsConvertedTypeSymbol,
                };

                var aliasData = new
                {
                    token.SemanticData?.AliasName,
                    token.SemanticData?.AliasTargetName,
                };

                var memberishData = new
                {
                    token.SemanticData?.MemberType,
                    token.SemanticData?.MemberTypeKind,
                };

                var symbolCharacteristics = new
                {
                    token.SemanticData?.Accessibility,
                    token.SemanticData?.IsAbstract,
                    token.SemanticData?.IsAsync,
                    token.SemanticData?.IsConst,
                    token.SemanticData?.IsDiscard,
                    token.SemanticData?.IsExtern,
                    token.SemanticData?.IsForEachVar,
                    token.SemanticData?.IsImplicitlyDeclared,
                    token.SemanticData?.IsIndexer,
                    token.SemanticData?.IsOptional,
                    token.SemanticData?.IsOriginalDefinition,
                    token.SemanticData?.IsOverride,
                    token.SemanticData?.IsReadOnly,
                    token.SemanticData?.IsSealed,
                    token.SemanticData?.IsStatic,
                    token.SemanticData?.IsRequired,
                    token.SemanticData?.IsUsingVar,
                    token.SemanticData?.IsVirtual,
                    token.SemanticData?.IsVolatile,
                    token.SemanticData?.IsWriteOnly,
                    token.SemanticData?.IsExplicitlyNamedTupleElement,
                };

                var logMessage =
                    $"{Environment.NewLine}================================ {token.Index} - {token.Text} ================================" +
                    $"{Environment.NewLine}----------- Location Data -----------{Environment.NewLine}{JsonSerializer.Serialize(locationData, options)}" +
                    $"{Environment.NewLine}----------- Symbol Data -----------{Environment.NewLine}{JsonSerializer.Serialize(symbolData, options)}" +
                    $"{Environment.NewLine}----------- Type Data -----------{Environment.NewLine}{JsonSerializer.Serialize(typeData, options)}" +
                    $"{Environment.NewLine}----------- Alias Data -----------{Environment.NewLine}{JsonSerializer.Serialize(aliasData, options)}" +
                    $"{Environment.NewLine}----------- Member-ish Data -----------{Environment.NewLine}{JsonSerializer.Serialize(memberishData, options)}" +
                    $"{Environment.NewLine}----------- Symbol Characteristics -----------{Environment.NewLine}{JsonSerializer.Serialize(symbolCharacteristics, options)}" +
                    $"{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}";

                _logger.LogInformation("{LogMessage}", logMessage);
            }
        }

        private void LogUnidentifiedTokens(Artifact artifact)
        {
            var tokens = artifact.NavTokens
                .Where(token => token.HighlightColor == "color-red")
                .Select(token => new
                {
                    token.Index,
                    token.Text,
                    token.Classifications.Original,
                    token.Classifications.Corrected,
                    token.HighlightColor,
                    token.PrimaryKind,
                    token.SemanticRole,
                    token.Kind,
                });

            var json = JsonSerializer.Serialize(tokens, options);
            _logger.LogInformation("{newline}{json}", Environment.NewLine, json);
        }
    }
}
