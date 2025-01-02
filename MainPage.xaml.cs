using Auth0.ManagementApi.Models;
using Auth0.OidcClient;
using Habbit.Resources.Pages;
using System.Net.Http.Headers;
using System.Text.Json;
using Habbit.Services;
using IdentityModel.OidcClient;
using Habbit.Services;

namespace Habbit
{
    public partial class MainPage : ContentPage
    {
        private readonly Auth0Client auth0Client;
        private readonly HabitService habitService;

        public MainPage(Auth0Client client, HabitService userService)
        {
            InitializeComponent();
            auth0Client = client;
            this.habitService = userService;
        }

        

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            try
            {
                // Show loading page
                await Shell.Current.GoToAsync("//LoadingPage");

                var loginResult = await auth0Client.LoginAsync();
                if (!loginResult.IsError)
                {
                    Preferences.Set("IsLoggedIn", true);

                    // Отримуємо дані користувача
                    var user = loginResult.User;
                    var auth0Id = user.Claims.FirstOrDefault(c => c.Type == "sub")?.Value ?? throw new Exception("Auth0Id is missing.");
                    var avatarUrl = user.Claims.FirstOrDefault(c => c.Type == "picture")?.Value ?? string.Empty;
                    var username = user.Claims.FirstOrDefault(c => c.Type == "nickname")?.Value ?? "DefaultUsername";
                    var name = user.Claims.FirstOrDefault(c => c.Type == "name")?.Value ?? "User";
                    var email = user.Claims.FirstOrDefault(c => c.Type == "email")?.Value ?? "no-email@example.com";
                    var isSuccess = await habitService.EnsureUserExistsAsync(auth0Id, username, name, email, avatarUrl, "light");
                    if (isSuccess)
                    {
                        // Якщо все добре, переходимо на наступну сторінку
                        var route = $"//StaticsPage?AvatarUrl={Uri.EscapeDataString(avatarUrl)}&Name={Uri.EscapeDataString(name)}";
                        await Shell.Current.GoToAsync(route);
                    }
                    else
                    {
                        await DisplayAlert("Error", "Failed to create user.", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Error", loginResult.ErrorDescription, "OK");

                    // Navigate back to MainPage if login fails
                    await Shell.Current.GoToAsync("//MainPage");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An unexpected error occurred: {ex.Message}", "OK");

                // Navigate back to MainPage if an error occurs
                await Shell.Current.GoToAsync("//MainPage");
            }
        }

    }
}
