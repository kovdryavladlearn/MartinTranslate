using MartinTranslate.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebServices();

var app = builder.Build();

app.UseExceptionHandler(options => { });

await app.InitialiseDatabaseAsync();

app.UseHealthChecks("/health");
app.UseStaticFiles();

app.UseSwaggerUi(settings =>
{
    settings.Path = "/swagger";
    settings.DocumentPath = "/api/specification.json";
});

app.MapFallbackToFile("index.html");

app.Map("/", () => Results.Redirect("/swagger"));

app.MapEndpoints();

app.Run();

public partial class Program { }
