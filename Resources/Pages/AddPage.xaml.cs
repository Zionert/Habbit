using Habbit.Resources.Models;

namespace Habbit.Resources.Pages;

public partial class AddPage : ContentPage
{
	public AddPage()
	{
		InitializeComponent();
	}

    private string selectedType = null;      // ��� "Habit" ��� "Goal"
    private string selectedAttribute = null; // ��� "Strength", "Intelligence", "Charisma"


    private async void OnSwipeRight(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//GoalsPage");  
    }

    // Swipe Left Handler: ������� ������
    private async void OnSwipeLeft(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//StaticsPage");  
    }
    private void OnHabitButtonClicked(object sender, EventArgs e)
    {
        

            selectedType = "Habit";
            HabitButton.BackgroundColor = Color.FromArgb("#8EC1F3");
            HabitButton.TextColor = Colors.White;

            GoalButton.BackgroundColor = Color.FromArgb("#B0B0B0");
            GoalButton.TextColor = Colors.Black;

    }

    // Goal RadioButton CheckedChanged Handler
    private void OnGoalButtonClicked(object sender, EventArgs e)
    {


            selectedType = "Goal";
            GoalButton.BackgroundColor = Color.FromArgb("#8EC1F3");
            GoalButton.TextColor = Colors.White;

            HabitButton.BackgroundColor = Color.FromArgb("#B0B0B0");
            HabitButton.TextColor = Colors.Black;

    }


    // RadioButton CheckedChanged Handlers
    private void OnStrengthButtonClicked(object sender, EventArgs e)
    {
        selectedAttribute = "Strength";
        StrengthButton.BackgroundColor = Color.FromArgb("#8EC1F3");
        StrengthButton.TextColor = Colors.White;

        IntelligenceButton.BackgroundColor = Color.FromArgb("#B0B0B0");
        IntelligenceButton.TextColor = Colors.Black;

        CharismaButton.BackgroundColor = Color.FromArgb("#B0B0B0");
        CharismaButton.TextColor = Colors.Black;
    }

    private void OnIntelligenceButtonClicked(object sender, EventArgs e)
    {
        selectedAttribute = "Intelligence";
        IntelligenceButton.BackgroundColor = Color.FromArgb("#8EC1F3");
        IntelligenceButton.TextColor = Colors.White;

        StrengthButton.BackgroundColor = Color.FromArgb("#B0B0B0");
        StrengthButton.TextColor = Colors.Black;

        CharismaButton.BackgroundColor = Color.FromArgb("#B0B0B0");
        CharismaButton.TextColor = Colors.Black;
    }

    private void OnCharismaButtonClicked(object sender, EventArgs e)
    {
        selectedAttribute = "Charisma";
        CharismaButton.BackgroundColor = Color.FromArgb("#8EC1F3");
        CharismaButton.TextColor = Colors.White;

        StrengthButton.BackgroundColor = Color.FromArgb("#B0B0B0");
        StrengthButton.TextColor = Colors.Black;

        IntelligenceButton.BackgroundColor = Color.FromArgb("#B0B0B0");
        IntelligenceButton.TextColor = Colors.Black;
    }

    private void AddTask(object sender, EventArgs e)
    {
        // �������� ��� � ���� ����� �� ����������
        var title = taskTitleEntry.Text; // �������� � Entry
        var type = selectedType;        // �������� �������� ��� ("Habit" ��� "Goal")
        var attribute = selectedAttribute;

        // �������� �������� �����
        if (string.IsNullOrEmpty(type) || string.IsNullOrEmpty(attribute))
        {
            DisplayAlert("Error", "Task Title is required!", "OK");
            return;
        }

        // ��������� ���� ������
        var newTask = new TaskItem
        {
            Title = title,
            Type = type,
            Attribute = attribute,
            IsCompleted = false
        };

        // ������ � ����������
        TaskRepository.Tasks.Add(newTask);

        // ��������� �����������
        DisplayAlert("Success", "Task added successfully!", "OK");

        // ����������� �� ��������� �������
        Navigation.PopAsync();

    }

}