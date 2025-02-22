using MyTelegramBot;
using WeatherApp.Infastructure.Repository.UserRepository;
using WeatherApp.TelegramBot.Service.WeatherApi;
using WeatherApp.TelegramBot.Service.TelegramBot;
using WeatherApp.Infastructure.Repository.WeatherRepository;
using WeatherApp.Core.UserService;
using WeatherApp.Core.WeatherService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var weatherService = new WeatherApiService(configuration);

builder.Services.AddHttpClient();

builder.Services.AddScoped<TelegramBotService>();
builder.Services.AddScoped<ITelegramBot, TelegramBotService>();

builder.Services.AddScoped<IWeatherApi, WeatherApiService>();

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<WeatherService>();
builder.Services.AddScoped<IWeatherService, WeatherService>();

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<WeatherRepository>();
builder.Services.AddScoped<IWeatherRepository, WeatherRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var botService = scope.ServiceProvider.GetRequiredService<TelegramBotService>();
    botService.StartBot();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();