using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Enable CORS for frontend calls
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Set port: use Render's PORT if available, otherwise 5000 for local
var port = Environment.GetEnvironmentVariable("PORT") ?? "5020";
builder.WebHost.UseUrls($"http://*:{port}");

var app = builder.Build();

// Swagger UI
app.UseSwagger();
app.UseSwaggerUI();

// Enable CORS
app.UseCors();

app.UseAuthorization();

app.MapGet("/", () => "Backend is running!");
app.MapControllers();

app.Run();
