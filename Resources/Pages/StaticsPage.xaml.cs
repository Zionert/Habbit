namespace Habbit.Resources.Pages;

public partial class StaticsPage : ContentPage
{
	public StaticsPage()
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

    private async void OnAddButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddPage());
    }

    private async void OnSettingsButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SettingsPage());
    }
}