using System.Threading.Channels;
using TravelBookingSystem.Search.API.Hubs;
using TravelBookingSystem.Search.API.Services;
using TravelBookingSystem.Search.Application.Services;
using TravelBookingSystem.Search.Core.Interfaces;
using TravelBookingSystem.Search.Core.Jobs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSignalR();

builder.Services.AddMemoryCache();
builder.Services.AddSingleton(Channel.CreateUnbounded<SearchJob>());

builder.Services.AddScoped<ISearchRequestService, SearchRequestService>();
builder.Services.AddScoped<ISearchNotificationService, SignalRSearchNotificationService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<SearchWorker>(); 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};
app.MapControllers();
app.MapHub<SearchHub>("/searchHub");


app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}