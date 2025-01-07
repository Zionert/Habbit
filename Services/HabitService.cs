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

        // Отримання Strength
        // Отримання Strength
        public async Task<double?> GetStrengthProgressAsync(string auth0Id)
        {
            var response = await _httpClient.GetAsync($"https://habbit-api1-cgafgqa6c3cfdvhj.polandcentral-01.azurewebsites.net/api/users/{auth0Id}/strength");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<dynamic>();
                return (double?)result?.Strength;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null; // Користувача або дані немає
            }
            else
            {
                throw new Exception($"Error fetching Strength progress: {response.StatusCode}");
            }
        }

        // Отримання Intelligence
        public async Task<double?> GetIntelligenceProgressAsync(string auth0Id)
        {
            var response = await _httpClient.GetAsync($"https://habbit-api1-cgafgqa6c3cfdvhj.polandcentral-01.azurewebsites.net/api/users/{auth0Id}/intelligence");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<dynamic>();
                return (double?)result?.Intelligence;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null; // Користувача або дані немає
            }
            else
            {
                throw new Exception($"Error fetching Intelligence progress: {response.StatusCode}");
            }
        }

        // Отримання Charisma
        public async Task<double?> GetCharismaProgressAsync(string auth0Id)
        {
            var response = await _httpClient.GetAsync($"https://habbit-api1-cgafgqa6c3cfdvhj.polandcentral-01.azurewebsites.net/api/users/{auth0Id}/charisma");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<dynamic>();
                return (double?)result?.Charisma;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null; // Користувача або дані немає
            }
            else
            {
                throw new Exception($"Error fetching Charisma progress: {response.StatusCode}");
            }
        }

        // Оновлення прогресу Strength
        public async Task<bool> UpdateAttributeProgressAsync(string auth0Id, TaskAttribute attribute, double increment)
        {
            var payload = new UpdateProgressRequest
            {
                Attribute = attribute,
                Increment = increment
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"https://habbit-api1-cgafgqa6c3cfdvhj.polandcentral-01.azurewebsites.net/api/users/{auth0Id}/update-progress", content);

            if (response.IsSuccessStatusCode)
            {
                return true; // Успішне оновлення
            }
            else
            {
                var errorDetails = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error updating progress: {errorDetails}");
                return false;
            }
        }


        // Оновлення прогресу Intelligence
        public async Task<bool> UpdateIntelligenceProgressAsync(string auth0Id, double intelligenceProgress)
        {
            var userStats = new
            {
                Stats = new
                {
                    CurrentProgressIntelligence = intelligenceProgress
                }
            };

            var json = JsonSerializer.Serialize(userStats);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"https://habbit-api1-cgafgqa6c3cfdvhj.polandcentral-01.azurewebsites.net/api/users/{auth0Id}/intelligence", content);

            return response.IsSuccessStatusCode;
        }

        // Оновлення прогресу Charisma
        public async Task<bool> UpdateCharismaProgressAsync(string auth0Id, double charismaProgress)
        {
            var userStats = new
            {
                Stats = new
                {
                    CurrentProgressCharisma = charismaProgress
                }
            };

            var json = JsonSerializer.Serialize(userStats);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"https://habbit-api1-cgafgqa6c3cfdvhj.polandcentral-01.azurewebsites.net/api/users/{auth0Id}/charisma", content);

            return response.IsSuccessStatusCode;
        }


    }
}
