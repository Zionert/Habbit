namespace Habbit.Resources.Pages;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
	}

    private async void OnHabbitsButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HabbitsPage());
    }

    private async void OnStaticsButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new StaticsPage());
    }

    private async void OnAddButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddPage());
    }

    private async void OnGoalsButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new GoalsPage());
    }
}