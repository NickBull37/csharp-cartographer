using csharp_cartographer_backend._01.Configuration.Configs;
using csharp_cartographer_backend._05.Services.AiAnalysis;
using csharp_cartographer_backend._05.Services.Charts;
using csharp_cartographer_backend._05.Services.Files;
using csharp_cartographer_backend._05.Services.Roslyn;
using csharp_cartographer_backend._05.Services.SyntaxHighlighting;
using csharp_cartographer_backend._05.Services.Tags;
using csharp_cartographer_backend._05.Services.Tokens;
using csharp_cartographer_backend._05.Services.Tokens.Maps;
using csharp_cartographer_backend._06.Workflows.Artifacts;
using csharp_cartographer_backend._07.Clients.ChatGpt;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Instance = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";

        context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);

        var activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
        context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);
    };
});

// configure strongly typed settings object
builder.Services.Configure<CartographerConfig>(builder.Configuration.GetSection("CartographerConfig"));

// Set custom file size limit for incoming requests
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 100000; // 100 KB limit (in bytes)
});

// configure DI for csharp-cartographer services
builder.Services.AddScoped<IAiAnalysisService, AiAnalysisService>();
builder.Services.AddScoped<IClassificationWizard, ClassificationWizard>();
builder.Services.AddScoped<IFileProcessor, FileProcessor>();
builder.Services.AddScoped<INavTokenGenerator, NavTokenGenerator>();
builder.Services.AddScoped<IRoslynAnalyzer, RoslynAnalyzer>();
builder.Services.AddScoped<IRoslynCorrector, RoslynCorrector>();
builder.Services.AddScoped<ISemanticLibrary, SemanticLibrary>();
builder.Services.AddScoped<ISyntaxHighlighter, SyntaxHighlighter>();
builder.Services.AddScoped<ITokenChartGenerator, TokenChartGenerator>();
builder.Services.AddScoped<ITokenChartWizard, TokenChartWizard>();
builder.Services.AddScoped<ITokenMapper, TokenMapper>();
builder.Services.AddScoped<ITokenTagGenerator, TokenTagGenerator>();

// configure DI for csharp-cartographer clients
builder.Services.AddHttpClient<IChatGptClient, ChatGptClient>((client) =>
{
    client.Timeout = TimeSpan.FromSeconds(60);
});

// configure DI for csharp-cartographer workflows
builder.Services.AddScoped<IGenerateArtifactWorkflow, GenerateArtifactWorkflow>();

var app = builder.Build();

app.UseCors(policy => policy
    .AllowAnyHeader()
    .AllowAnyMethod()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
