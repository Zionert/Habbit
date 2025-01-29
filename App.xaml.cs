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
            
            if (!Preferences.Get("IsLoggedIn", false))
            {
                
                await Shell.Current.GoToAsync("//MainPage");
            }
        }
    }
}
