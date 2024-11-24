using Habbit.Resources.Pages;
namespace Habbit


{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(StaticsPage), typeof(Habbit.Resources.Pages.StaticsPage));
            Routing.RegisterRoute(nameof(HabbitsPage), typeof(Habbit.Resources.Pages.HabbitsPage));
            Routing.RegisterRoute(nameof(GoalsPage), typeof(Habbit.Resources.Pages.GoalsPage));
            Routing.RegisterRoute(nameof(AddPage), typeof(Habbit.Resources.Pages.AddPage));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(Habbit.Resources.Pages.SettingsPage));
        }
    }
}
