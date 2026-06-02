using csharp_cartographer_backend._01.Configuration.Configs;
using csharp_cartographer_backend._02.Utilities.ActionResponse;
using csharp_cartographer_backend._03.Models.Artifacts;
using csharp_cartographer_backend._03.Models.Files;
using csharp_cartographer_backend._03.Models.Tokens.TokenMaps;
using csharp_cartographer_backend._05.Services.Charts;
using csharp_cartographer_backend._05.Services.Files;
using csharp_cartographer_backend._05.Services.Insights;
using csharp_cartographer_backend._05.Services.SyntaxHighlighting;
using csharp_cartographer_backend._05.Services.Tokens;
using csharp_cartographer_backend._05.Services.Tokens.Maps;
using csharp_cartographer_backend._08.Controllers.Artifacts.Dtos;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Text.Json;

namespace csharp_cartographer_backend._06.Workflows.Artifacts
{
    public class GenerateArtifactWorkflow : IGenerateArtifactWorkflow
    {
        private readonly IFileProcessor _fileProcessor;
        private readonly IInsightService _insightService;
        private readonly INavTokenGenerator _navTokenGenerator;
        private readonly ISyntaxHighlighter _syntaxHighlighter;
        private readonly ITokenChartGenerator _tokenChartGenerator;
        private readonly ITokenMapper _tokenMapper;
        private readonly ILogger<GenerateArtifactWorkflow> _logger;
        private readonly CartographerConfig _config;

        private readonly JsonSerializerOptions options = new() { WriteIndented = true };

        public GenerateArtifactWorkflow(
            IFileProcessor fileProcessor,
            IInsightService insightService,
            INavTokenGenerator navTokenGenerator,
            ISyntaxHighlighter syntaxHighlighter,
            ITokenChartGenerator tokenChartGenerator,
            ITokenMapper tokenMapper,
            ILogger<GenerateArtifactWorkflow> logger,
            IOptions<CartographerConfig> config)
        {
            _fileProcessor = fileProcessor;
            _insightService = insightService;
            _navTokenGenerator = navTokenGenerator;
            _syntaxHighlighter = syntaxHighlighter;
            _tokenChartGenerator = tokenChartGenerator;
            _tokenMapper = tokenMapper;
            _logger = logger;
            _config = config.Value;
        }

        public async Task<ActionResponse<Artifact>> GenerateDemoArtifact(string fileName, CancellationToken cancellationToken)
        {
            FileData fileData = _fileProcessor.ReadInDemoFileData(fileName);

            var actionResponse = await GenerateArtifact(fileData, cancellationToken);

            var insight = _insightService.GetDemoFileInsight(fileName);
            if (insight is not null)
            {
                actionResponse.Content.Insight = insight;
            }

            return actionResponse;
        }

        public async Task<ActionResponse<Artifact>> GenerateUserArtifact(GenerateArtifactDto requestDto, CancellationToken cancellationToken)
        {
            FileData fileData = _fileProcessor.ReadInUploadedFileData(requestDto);
            return await GenerateArtifact(fileData, cancellationToken);
        }

        private async Task<ActionResponse<Artifact>> GenerateArtifact(FileData fileData, CancellationToken cancellationToken)
        {
            /*
             *   Steps to generate an artifact:
             * 
             *   0. Read in source code from user uploaded file & generate FileData.
             *   1. Start stopwatch and set first checkpoint.
             *   2. Generate a list of nav tokens from the source file.
             *   3. Generate a token chart for each token and its ancestors.
             *   4. Add semantic details to each token and it's map.
             *   5. Add syntax highlighting for each token.
             *   6. Stop stopwatch and capture total elapsed time.
             *   7. Build artifact timings.
             *   8. Build artifact.
             *   *  Log artifact data (optional)
             *   9. Return artifact.
             */

            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                TimeSpan checkpoint = TimeSpan.Zero;

                var navTokens = await _navTokenGenerator.GenerateNavTokens(fileData, cancellationToken);
                var tokenGenTime = TimeSinceCheckpoint(stopwatch, ref checkpoint);

                _tokenMapper.MapNavTokens(navTokens);
                var mapTime = TimeSinceCheckpoint(stopwatch, ref checkpoint);

                _tokenChartGenerator.GenerateTokenCharts(navTokens);
                var chartGenTime = TimeSinceCheckpoint(stopwatch, ref checkpoint);

                _syntaxHighlighter.AddSyntaxHighlightingToNavTokens(navTokens);
                var highlightTime = stopwatch.Elapsed - checkpoint;

                stopwatch.Stop();
                var totalTime = stopwatch.Elapsed;

                var timings = new ArtifactTimes(
                    tokenGenTime,
                    chartGenTime,
                    mapTime,
                    highlightTime,
                    totalTime
                );

                var artifact = new Artifact(
                    fileData.FileName,
                    navTokens,
                    timings
                );

                LogArtifactData(artifact);

                return ActionResponse<Artifact>.Success(artifact);
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred.");
                return ActionResponse<Artifact>.Failure("An exception occurred during artifact generation.");
            }
        }

        private void LogArtifactData(Artifact artifact)
        {
            if (_config.ShouldLogSemanticData)
            {
                var identifierTokens = artifact.NavTokens
                    .Where(token => token.PrimaryKind is PrimaryKind.Identifier);

                foreach (var token in identifierTokens)
                {
                    var tokenData = new
                    {
                        token.Index,
                        token.Text,
                    };

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

            if (_config.ShouldLogUnidentifiedTokens)
            {
                var tokens = artifact.NavTokens
                    .Where(token => token.HighlightColor == "color-red")
                    .Select(token => new
                    {
                        token.Index,
                        token.Text,
                        token.Classifications.Original,
                        token.Classifications.Corrected,
                        token.Classifications.Final,
                        token.HighlightColor,
                        token.PrimaryKind,
                        token.SemanticRole,
                        token.Kind,
                    });

                var json = JsonSerializer.Serialize(tokens, options);
                _logger.LogInformation("{newline}{json}", Environment.NewLine, json);
            }
        }

        private static TimeSpan TimeSinceCheckpoint(Stopwatch stopwatch, ref TimeSpan checkpoint)
        {
            TimeSpan elapsed = stopwatch.Elapsed - checkpoint;
            checkpoint = stopwatch.Elapsed;
            return elapsed;
        }
    }
}
