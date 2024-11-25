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
                // Show loading page
                await Shell.Current.GoToAsync("//LoadingPage");

                var loginResult = await auth0Client.LoginAsync();
                if (!loginResult.IsError)
                {
                    Preferences.Set("IsLoggedIn", true);

                    // Navigate to the main application page after login
                    await Shell.Current.GoToAsync("//StaticsPage");
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
