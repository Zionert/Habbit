namespace Habbit.Resources.Pages;

public partial class HabbitsPage : ContentPage
{
	public HabbitsPage()
	{
		InitializeComponent();
	}

    private async void OnGoalsButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new GoalsPage());
    }

    private async void OnStaticsButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new StaticsPage());
    }

    private async void OnAddButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddPage());
    }

    private async void OnSettingsButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SettingsPage());
    }
}