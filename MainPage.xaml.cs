using Auth0.ManagementApi.Models;
using Auth0.OidcClient;
using Habbit.Resources.Pages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Habbit
{
    public partial class MainPage : ContentPage
    {
        private readonly Auth0Client auth0Client;

        public MainPage(Auth0Client client)
        {
            InitializeComponent();
            auth0Client = client;
        }



        private async void OnLoginClicked(object sender, EventArgs e)
        {
            try
            {
                // Виконуємо логін через Auth0
                var loginResult = await auth0Client.LoginAsync();
                if (!loginResult.IsError)
                {
                    // Отримуємо токен доступу
                    var accessToken = loginResult.AccessToken;

                    // Виконуємо запит до Auth0 UserInfo API
                    var userInfoEndpoint = "https://dev-b5nb4y3005k58q3j.us.auth0.com/userinfo"; // Замість <your-auth0-domain> вставте свій домен
                    var httpClient = new HttpClient();
                    httpClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", accessToken);

                    var response = await httpClient.GetAsync(userInfoEndpoint);

                    if (response.IsSuccessStatusCode)
                    {
                        var userInfoJson = await response.Content.ReadAsStringAsync();
                        var userInfo = JsonSerializer.Deserialize<Dictionary<string, object>>(userInfoJson);

                        await Navigation.PushAsync(new StaticsPage());

                        // Показуємо аватарку та ім'я користувача
                        //if (userInfo.TryGetValue("picture", out var pictureUrl))
                        //{
                        //    UserAvatar.Source = pictureUrl.ToString();
                        //}

                       // if (userInfo.TryGetValue("name", out var userName))
                        //{
                        //    UserName.Text = userName.ToString();
                       // }
                    }
                    else
                    {
                        await DisplayAlert("Error", "Failed to fetch user info", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Error", loginResult.ErrorDescription, "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An unexpected error occurred: {ex.Message}", "OK");
            }
        }

    }
}
