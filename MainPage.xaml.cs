using Auth0.ManagementApi.Models;
using Auth0.OidcClient;

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

        private async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage()); //Switch the page
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var loginResult = await auth0Client.LoginAsync();
            if (!loginResult.IsError)
            {
                LoginView.IsVisible = false;
                HomeView.IsVisible = false;
            }
            else
            {
                await DisplayAlert("Error", loginResult.ErrorDescription, "OK");
            }
        }

    }
}
