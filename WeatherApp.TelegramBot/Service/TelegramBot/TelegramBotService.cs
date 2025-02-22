using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using WeatherApp.Core.UserService;
using WeatherApp.Infastructure.Models;
using WeatherApp.TelegramBot.Service.TelegramBot;
using WeatherApp.TelegramBot.Service.WeatherApi;

namespace MyTelegramBot
{
    public class TelegramBotService : ITelegramBot
    {
        private readonly TelegramBotClient _botClient;
        private CancellationTokenSource _cts;
        private readonly IWeatherApi _weatherApi;
        private readonly UserService _userService;

        public TelegramBotService(IConfiguration configuration, IWeatherApi weatherApi, UserService userService)
        {
            _botClient = new TelegramBotClient(configuration["TelegramApi"]) ?? throw new ArgumentNullException(nameof(configuration));
            _cts = new CancellationTokenSource();
            _weatherApi = weatherApi;
            _userService = userService;
        }

        private async Task SendCityButtonAsync(long chatId, string city)
        {
            var replyKeyboard = new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton(city)
            })
            {
                ResizeKeyboard = true,
                OneTimeKeyboard = true
            };

            await _botClient.SendMessage(
                chatId: chatId,
                text: "Оберіть місто для отримання погоди:",
                replyMarkup: replyKeyboard
            );
        }

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message && update.Message is { } message)
            {
                long chatId = message.Chat.Id;

                if (message.Text == "/start")
                {
                    string welcomeMessage = "Вітаю! Використайте команду /weather, для того щоб отримати інформацію про погоду!";
                    await botClient.SendMessage(message.Chat.Id, welcomeMessage, cancellationToken: cancellationToken);

                    return;
                }

                if (message.Text.StartsWith("/weather", StringComparison.OrdinalIgnoreCase))
                {
                    var city = message.Text.Replace("/weather", "").Trim();

                    if (string.IsNullOrEmpty(city))
                    {
                        await botClient.SendMessage(chatId, "Вкажіть місто! Наприклад: /weather Київ");
                        return;
                    }

                    var weatherInfo = await _weatherApi.GetWeatherAsync(city);
                    await botClient.SendMessage(chatId, $"Погода у {city}:\nТемпература: {weatherInfo.Main.Temp}", cancellationToken: cancellationToken);

                    await _userService.AddUser(chatId, city);
                    await SendCityButtonAsync(chatId, city);
                }
                else if (!string.IsNullOrEmpty(message.Text))
                {
                    string cityFromButton = message.Text.Replace("/weather", "").Trim();

                    await _userService.AddUser(chatId, message.Text);

                    var weatherInfo = await _weatherApi.GetWeatherAsync(cityFromButton);
                    await botClient.SendMessage(chatId, $"Погода у {cityFromButton}:\nТемпература: {weatherInfo.Main.Temp}", cancellationToken: cancellationToken);
                }

            }
        }

        private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Error: {exception.Message}");
            return Task.CompletedTask;
        }

        public void StartBot()
        {
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }
            };

            _botClient.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                _cts.Token
            );
        }

        public void StopBot()
        {
            _cts.Cancel();
        }

        public async Task SendWeatherToUser(long userId)
        {
            var user = await _userService.GetUserById(userId);

            if (user != null)
            {
                var weatherInfo = await _weatherApi.GetWeatherAsync(user.FavoriteCity);
                object value = await _botClient.SendMessage(userId, $"Погода у {user.FavoriteCity}:\nТемпература: {weatherInfo.Main.Temp}");

            }
        }

        public async Task SendWeatherToAll()
        {
            var users = await _userService.GetAllUsers();

            foreach (var user in users.ToList())
            {
                try
                {
                    var weatherInfo = await _weatherApi.GetWeatherAsync(user.FavoriteCity);
                    await _botClient.SendMessage(user.TelegramId, $"Погода у {user.FavoriteCity}:\nТемпература: {weatherInfo.Main.Temp}");
                }
                catch (ApiRequestException ex) when (ex.ErrorCode == 403)
                {
                    Console.WriteLine($"Користувач {user} заблокував бота.");
                    users.Remove(user);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка відправки до {user}: {ex.Message}");
                }
            }
        }

    }
}
