using Habbit.Resources.Models;

namespace Habbit.Resources.Pages;

public partial class HabbitsPage : ContentPage
{
	public HabbitsPage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        UpdateHabitsList();
    }

    private void UpdateHabitsList()
    {
        HabitsLayout.Children.Clear();

        // �������� ������ ���� "Habit"
        var habits = TaskRepository.Tasks.Where(t => t.Type == "Habit");

        foreach (var habit in habits)
        {
            // ������� ����� ��� ������
            var taskFrame = new Frame
            {
                BackgroundColor = Colors.White,
                CornerRadius = 8,
                Padding = 10,
                Margin = new Thickness(0, 5),
                HasShadow = true,
                Content = new Grid
                {
                    ColumnDefinitions =
            {
                new ColumnDefinition { Width = GridLength.Star }, // ����� ������
                new ColumnDefinition { Width = GridLength.Auto }  // ������
            }
                }
            };

            // ����� ������
            var label = new Label
            {
                Text = habit.Title,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                TextColor = Colors.Black,
                FontSize = 16
            };

            // ������ ���������� ������
            var completeButton = new Button
            {
                Text = "Yes",
                BackgroundColor = Colors.LightGreen,
                TextColor = Colors.White,
                CornerRadius = 20,
                WidthRequest = 50,
                HeightRequest = 40
            };

            completeButton.Clicked += (s, e) =>
            {
                habit.IsCompleted = true;
                DisplayAlert("Success", $"{habit.Title} completed!", "OK");
                UpdateHabitsList(); // ��������� ������
            };

            // ������ ��������� ������
            var deleteButton = new Button
            {
                Text = "No",
                BackgroundColor = Colors.Red,
                TextColor = Colors.White,
                CornerRadius = 20,
                WidthRequest = 50,
                HeightRequest = 40
            };

            deleteButton.Clicked += (s, e) =>
            {
                TaskRepository.Tasks.Remove(habit);
                UpdateHabitsList(); // ��������� ������
            };

            // �������������� ���� ��� ������
            var buttonsStack = new HorizontalStackLayout
            {
                Spacing = 5,
                Children = { completeButton, deleteButton },
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center
            };

            // ��������� �������� �� ����
            var grid = (Grid)taskFrame.Content;
            grid.Add(label, 0, 0);         // ����� ������ � ������ ��������
            grid.Add(buttonsStack, 1, 0); // ������ � ������ ��������

            // ��������� ����� �� ����������
            HabitsLayout.Children.Add(taskFrame);
        }
    }


}