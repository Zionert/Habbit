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

        
        public async Task<bool> CheckUserExistsAsync(string auth0Id)
        {
            var response = await _httpClient.GetAsync($"https://habbit-api-dbesdvgkhefgdwdh.polandcentral-01.azurewebsites.net/api/users/{auth0Id}");
            if (response.IsSuccessStatusCode)
            {
                return true; 
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return false;
            }
            else
            {
                throw new Exception($"Error checking user existence: {response.StatusCode}");
            }
        }

        
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

            var response = await _httpClient.PostAsync("https://habbit-api-dbesdvgkhefgdwdh.polandcentral-01.azurewebsites.net/api/users", content);
            if (!response.IsSuccessStatusCode)
            {
                var errorDetails = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {errorDetails}");
            }

            return response.IsSuccessStatusCode;
        }

        
        public async Task<bool> EnsureUserExistsAsync(string auth0Id, string username, string name, string email, string avatarUrl, string theme)
        {
            var userExists = await CheckUserExistsAsync(auth0Id);

            if (!userExists)
            {
                Console.WriteLine("User does not exist. Creating new user...");
                return await CreateUserAsync(auth0Id, username, name, email, avatarUrl, theme);
            }

            Console.WriteLine("User already exists.");
            return true; 
        }

       
       
        public async Task<double?> GetStrengthProgressAsync(string auth0Id)
        {
            var response = await _httpClient.GetAsync($"https://habbit-api-dbesdvgkhefgdwdh.polandcentral-01.azurewebsites.net/api/users/{auth0Id}/strength");
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response JSON: {responseContent}");

            if (response.IsSuccessStatusCode)
            {
                using var document = JsonDocument.Parse(responseContent);
                if (document.RootElement.TryGetProperty("strength", out var strength))
                {
                    Console.WriteLine($"Parsed Strength: {strength}");
                    return strength.GetDouble();
                }
                else
                {
                    Console.WriteLine("Field 'strength' not found.");
                    return null;
                }
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("User not found.");
                return null;
            }
            else
            {
                Console.WriteLine($"Unexpected status code: {response.StatusCode}");
                throw new Exception($"Error fetching Strength progress: {response.StatusCode}");
            }
        }


       
        public async Task<double?> GetIntelligenceProgressAsync(string auth0Id)
        {
            var response = await _httpClient.GetAsync($"https://habbit-api-dbesdvgkhefgdwdh.polandcentral-01.azurewebsites.net/api/users/{auth0Id}/intelligence");
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response JSON: {responseContent}");

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    using var document = JsonDocument.Parse(responseContent);
                    if (document.RootElement.TryGetProperty("intelligence", out var intelligence))
                    {
                        Console.WriteLine($"Parsed Intelligence: {intelligence}");
                        return intelligence.GetDouble();
                    }
                    else
                    {
                        Console.WriteLine("Field 'intelligence' not found.");
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error parsing JSON: {ex.Message}");
                    return null;
                }
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("User not found.");
                return null;
            }
            else
            {
                Console.WriteLine($"Unexpected status code: {response.StatusCode}");
                throw new Exception($"Error fetching Intelligence progress: {response.StatusCode}");
            }
        }


        public async Task<double?> GetCharismaProgressAsync(string auth0Id)
        {
            var response = await _httpClient.GetAsync($"https://habbit-api-dbesdvgkhefgdwdh.polandcentral-01.azurewebsites.net/api/users/{auth0Id}/charisma");
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response JSON: {responseContent}");

            if (response.IsSuccessStatusCode)
            {
                using var document = JsonDocument.Parse(responseContent);
                if (document.RootElement.TryGetProperty("charisma", out var charisma))
                {
                    Console.WriteLine($"Parsed Charisma: {charisma}");
                    return charisma.GetDouble();
                }
                else
                {
                    Console.WriteLine("Field 'charisma' not found.");
                    return null;
                }
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("User not found.");
                return null;
            }
            else
            {
                Console.WriteLine($"Unexpected status code: {response.StatusCode}");
                throw new Exception($"Error fetching Charisma progress: {response.StatusCode}");
            }
        }



        public class StrengthResponse
        {
            public double? Strength { get; set; }
        }

        public class IntelligenceResponse
        {
            public double? Intelligence { get; set; }
        }

        public class CharismaResponse
        {
            public double? Charisma { get; set; }
        }

       
        public async Task<bool> UpdateAttributeProgressAsync(string auth0Id, TaskAttribute attribute, double increment)
        {
            var payload = new UpdateProgressRequest
            {
                Attribute = attribute,
                Increment = increment
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"https://habbit-api-dbesdvgkhefgdwdh.polandcentral-01.azurewebsites.net/api/users/{auth0Id}/update-progress", content);

            if (response.IsSuccessStatusCode)
            {
                return true; 
            }
            else
            {
                var errorDetails = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error updating progress: {errorDetails}");
                return false;
            }
        }


        
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

            var response = await _httpClient.PutAsync($"https://habbit-api-dbesdvgkhefgdwdh.polandcentral-01.azurewebsites.net/api/users/{auth0Id}/intelligence", content);

            return response.IsSuccessStatusCode;
        }

       
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

            var response = await _httpClient.PutAsync($"https://habbit-api-dbesdvgkhefgdwdh.polandcentral-01.azurewebsites.net/api/users/{auth0Id}/charisma", content);

            return response.IsSuccessStatusCode;
        }


    }
}
