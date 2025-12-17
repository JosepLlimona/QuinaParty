using QuinaPartyServer.Hubs;
using static QuinaPartyServer.Hubs.GameHub;

var builder = WebApplication.CreateBuilder(args);

// Add SignalR services
builder.Services.AddSignalR();

// Optional: CORS (useful for JS clients)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .SetIsOriginAllowed(_ => true);
    });
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors();

// Map SignalR hub
app.MapHub<GameHub>("/Game");

// Optional health endpoint
app.MapGet("/", () => "SignalR API is running");

app.Run();