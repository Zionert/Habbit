using Habbit.Resources.Models;

namespace Habbit.Resources.Pages;

public partial class AddPage : ContentPage
{
	public AddPage()
	{
		InitializeComponent();
	}

    private TaskType? selectedType = null;      // ��� "Habit" ��� "Goal"
    private TaskAttribute? selectedAttribute = null; // ��� "Strength", "Intelligence", "Charisma"


    private void OnHabitButtonClicked(object sender, EventArgs e)
    {
        

            selectedType = TaskType.Habbit;
            HabitButton.BackgroundColor = Color.FromArgb("#8EC1F3");
            HabitButton.TextColor = Colors.White;

            GoalButton.BackgroundColor = Color.FromArgb("#B0B0B0");
            GoalButton.TextColor = Colors.Black;

    }

    // Goal RadioButton CheckedChanged Handler
    private void OnGoalButtonClicked(object sender, EventArgs e)
    {


            selectedType = TaskType.Goal;
            GoalButton.BackgroundColor = Color.FromArgb("#8EC1F3");
            GoalButton.TextColor = Colors.White;

            HabitButton.BackgroundColor = Color.FromArgb("#B0B0B0");
            HabitButton.TextColor = Colors.Black;

    }


    // RadioButton CheckedChanged Handlers
    private void OnStrengthButtonClicked(object sender, EventArgs e)
    {
        selectedAttribute = TaskAttribute.Strength;
        StrengthButton.BackgroundColor = Color.FromArgb("#8EC1F3");
        StrengthButton.TextColor = Colors.White;

        IntelligenceButton.BackgroundColor = Color.FromArgb("#B0B0B0");
        IntelligenceButton.TextColor = Colors.Black;

        CharismaButton.BackgroundColor = Color.FromArgb("#B0B0B0");
        CharismaButton.TextColor = Colors.Black;
    }

    private void OnIntelligenceButtonClicked(object sender, EventArgs e)
    {
        selectedAttribute = TaskAttribute.Intelligence;
        IntelligenceButton.BackgroundColor = Color.FromArgb("#8EC1F3");
        IntelligenceButton.TextColor = Colors.White;

        StrengthButton.BackgroundColor = Color.FromArgb("#B0B0B0");
        StrengthButton.TextColor = Colors.Black;

        CharismaButton.BackgroundColor = Color.FromArgb("#B0B0B0");
        CharismaButton.TextColor = Colors.Black;
    }

    private void OnCharismaButtonClicked(object sender, EventArgs e)
    {
        selectedAttribute = TaskAttribute.Charisma;
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
        var difficulty = SliderDifficulty.Value;

        // �������� �������� �����
        if (type != TaskType.Habbit && type != TaskType.Goal)
        {
            DisplayAlert("Error", "Task Type is required!", "OK");
            return;
        }
        if(attribute != TaskAttribute.Strength &&  attribute != TaskAttribute.Charisma && attribute != TaskAttribute.Intelligence)
        {
            DisplayAlert("Error", "Task Attribure is required!", "OK");
            return;
        }
        if (title == null) {
            DisplayAlert("Error", "Task Title is required!", "OK");
            return;
        }

        // ��������� ���� ������
        var newTask = new TaskItem
        {
            Title = title,
            Type = type,
            Attribute = attribute,
            IsCompleted = false,
            Difficulty = difficulty
        };

        // ������ � ����������
        TaskRepository.Tasks.Add(newTask);

        // ��������� �����������
        DisplayAlert("Success", "Task added successfully!", "OK");

        // ����������� �� ��������� �������
        Navigation.PopAsync();

    }

}