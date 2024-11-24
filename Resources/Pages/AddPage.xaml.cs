namespace Habbit.Resources.Pages;

public partial class AddPage : ContentPage
{
	public AddPage()
	{
		InitializeComponent();
	}

    private async void OnHabbitsButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HabbitsPage());
    }

    private async void OnGoalsButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new GoalsPage());
    }

    private async void OnStaticsButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new StaticsPage());
    }

    private async void OnSettingsButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SettingsPage());
    }
}