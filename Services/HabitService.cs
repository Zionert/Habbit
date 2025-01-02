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

        // Метод для перевірки існування користувача
        public async Task<bool> CheckUserExistsAsync(string auth0Id)
        {
            var response = await _httpClient.GetAsync($"https://habbit-api1-cgafgqa6c3cfdvhj.polandcentral-01.azurewebsites.net/api/users/{auth0Id}");
            if (response.IsSuccessStatusCode)
            {
                return true; // Користувач існує
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return false; // Користувача немає
            }
            else
            {
                throw new Exception($"Error checking user existence: {response.StatusCode}");
            }
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

            var response = await _httpClient.PostAsync("https://habbit-api1-cgafgqa6c3cfdvhj.polandcentral-01.azurewebsites.net/api/users", content);
            if (!response.IsSuccessStatusCode)
            {
                var errorDetails = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {errorDetails}");
            }

            return response.IsSuccessStatusCode;
        }

        // Головний метод для логування/створення користувача
        public async Task<bool> EnsureUserExistsAsync(string auth0Id, string username, string name, string email, string avatarUrl, string theme)
        {
            var userExists = await CheckUserExistsAsync(auth0Id);

            if (!userExists)
            {
                Console.WriteLine("User does not exist. Creating new user...");
                return await CreateUserAsync(auth0Id, username, name, email, avatarUrl, theme);
            }

            Console.WriteLine("User already exists.");
            return true; // Користувач існує, тому не потрібно нічого створювати
        }
    }
}
