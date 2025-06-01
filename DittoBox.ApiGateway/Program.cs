var builder = WebApplication.CreateBuilder(args);

// Add YARP and load config from appsettings
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

app.MapGet("/health", () => Results.Ok(new { status = "Healthy" }));

// Enable routing via YARP
app.MapReverseProxy();

app.Run();
