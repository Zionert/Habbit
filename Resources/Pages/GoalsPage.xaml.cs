namespace Habbit.Resources.Pages;

public partial class GoalsPage : ContentPage
{
	public GoalsPage()
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

    private async void OnSettingsButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SettingsPage());
    }
}