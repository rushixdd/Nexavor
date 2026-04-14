var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

// Add YARP or Ocelot packages/configuration based on the selected template options.

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/health", () => Results.Ok(new { gateway = "GatewayName", status = "Healthy" }));

app.Run();
