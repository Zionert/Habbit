using Microsoft.Maui.Controls.Compatibility;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace Habbit;
public partial class SignUpPage : ContentPage
{
    public SignUpPage()
    {
		InitializeComponent(); //writes an error, but everything works


    }

    private async void OnLogInButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage()); //Switch the page
    }
}