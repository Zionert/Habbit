using Microsoft.Maui.Storage;

namespace Habbit
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            RedirectIfLoggedOut();
        }

        protected override void OnResume()
        {
            RedirectIfLoggedOut();
        }

        private async void RedirectIfLoggedOut()
        {
            // Check login state
            if (!Preferences.Get("IsLoggedIn", false))
            {
                // Ensure we are on the MainPage
                await Shell.Current.GoToAsync("//MainPage");
            }
        }
    }
}
