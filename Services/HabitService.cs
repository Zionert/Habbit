using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Habbit.Resources.Models;
using System.Net.Http;
using Auth0.ManagementApi.Models;

namespace Habbit.Services
{
    public class HabitService
    {
        private readonly HttpClient _httpClient;

        public HabitService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        // Метод для створення нового користувача через API
        public async Task<bool> CreateUserAsync(string auth0Id, string username, string name, string email, string avatarUrl, string theme)
        {
            var user = new
            {
                Auth0Id = auth0Id,
                Username = username,
                Name = name,
                Email = email,
                AvatarUrl = avatarUrl,
                Preferences = new
                {
                    Theme = theme
                }
            };

            var json = JsonSerializer.Serialize(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://192.168.0.4:5233/api/users", content);
            if (!response.IsSuccessStatusCode)
            {
                var errorDetails = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {errorDetails}");
            }

            return response.IsSuccessStatusCode;
        }
    }
}
