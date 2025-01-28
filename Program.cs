using System.Net.WebSockets;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using wsapi.Context;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5156");

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddSingleton<WebSocketService>();

builder.Services.AddDbContext<ApplicationDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "WSAPI",
        Version = "v1",
        Description = "API para WebSocket e banco de dados.",
    });
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WSAPI v1");
    c.RoutePrefix = string.Empty; 
});
app.UseCors();
app.UseWebSockets();
app.MapControllers();

await app.RunAsync();