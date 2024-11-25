using Auth0.OidcClient;
using System;
using Microsoft.Maui.Controls;

namespace Habbit.Resources.Pages;

public partial class SettingsPage : ContentPage
{
    private readonly Auth0Client _auth0Client;

    public SettingsPage(Auth0Client auth0Client)
    {
        InitializeComponent();
        _auth0Client = auth0Client;
    }

    private async void LogOut(object sender, EventArgs e)
    {
        try
        {
            await _auth0Client.LogoutAsync();

            // Clear login state
            Preferences.Remove("IsLoggedIn");

            // Navigate to MainPage
            await Shell.Current.GoToAsync("//MainPage");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Logout failed: {ex.Message}", "OK");
        }
    }
}

