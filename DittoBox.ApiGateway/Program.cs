var builder = WebApplication.CreateBuilder(args);

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Add YARP from config
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

// Order matters!
app.UseCors("AllowAll"); // Debe ir antes de MapReverseProxy

app.MapGet("/health", () => Results.Ok(new { status = "Healthy" }));

app.MapReverseProxy();

app.Run();
