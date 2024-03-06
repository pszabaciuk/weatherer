using RepoDb;
using Weatherer.Config;
using Weatherer.Server.Extensions;
using Weatherer.Server.Persistence;
using Weatherer.Server.Service;
using Weatherer.Server.Workers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection("Database"));
builder.Services.Configure<WeatherServiceOptions>(builder.Configuration.GetSection("WeatherClient"));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<WeatherDownloadWorker>();
builder.Services.AddTransient<ICityRepository, CityRepository>();
builder.Services.AddTransient<IWeatherRepository, WeatherRepository>();
builder.Services.AddHttpClient<IWeatherService, WeatherService>(client =>
 {
     WeatherServiceOptions options = builder.Configuration.GetRequiredOptions<WeatherServiceOptions>("WeatherClient");
     client.BaseAddress = new Uri(options.BaseAddress);
 });

GlobalConfiguration
    .Setup()
    .UseSqlite();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
