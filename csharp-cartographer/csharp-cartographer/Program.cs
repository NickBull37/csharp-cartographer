using csharp_cartographer._05.Services.Artifacts;
using csharp_cartographer._05.Services.FileProcessing;
using csharp_cartographer._05.Services.SyntaxHighlighting;
using csharp_cartographer._05.Services.Tokens;
using csharp_cartographer._05.Services.TokenTags;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 62914560; // 30 MB limit (in bytes)
});

// configure DI for SS NAVIGATOR services
builder.Services.AddScoped<IArtifactGenerator, ArtifactGenerator>();
builder.Services.AddScoped<IFileProcessor, FileProcessor>();
builder.Services.AddScoped<ISyntaxHighlighter, SyntaxHighlighter>();
builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();
builder.Services.AddScoped<ITokenTagGenerator, TokenTagGenerator>();
builder.Services.AddScoped<ITokenTagWizard, TokenTagWizard>();

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
