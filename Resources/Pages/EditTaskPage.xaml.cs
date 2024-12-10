using Habbit.Resources.Models;

namespace Habbit.Resources.Pages;

public partial class EditTaskPage : ContentPage
{
    private TaskItem _task;
    public EditTaskPage(TaskItem task)
	{
		InitializeComponent();
        _task = task;

        // Заповнення полів даними задачі
        taskTitleEntry.Text = _task.Title;
        HabitButton.BackgroundColor = _task.Type == "Habit" ? Color.FromArgb("#8EC1F3") : Color.FromArgb("#B0B0B0");
        GoalButton.BackgroundColor = _task.Type == "Goal" ? Color.FromArgb("#8EC1F3") : Color.FromArgb("#B0B0B0");

        StrengthButton.BackgroundColor = _task.Attribute == "Strength" ? Color.FromArgb("#8EC1F3") : Color.FromArgb("#B0B0B0");
        IntelligenceButton.BackgroundColor = _task.Attribute == "Intelligence" ? Color.FromArgb("#8EC1F3") : Color.FromArgb("#B0B0B0");
        CharismaButton.BackgroundColor = _task.Attribute == "Charisma" ? Color.FromArgb("#8EC1F3") : Color.FromArgb("#B0B0B0");
    }

    

    private void OnHabitButtonClicked(object sender, EventArgs e)
    {
            HabitButton.BackgroundColor = Color.FromArgb("#8EC1F3");
            HabitButton.TextColor = Colors.White;

            GoalButton.BackgroundColor = Color.FromArgb("#B0B0B0");
            GoalButton.TextColor = Colors.Black;

            _task.Type = "Habit";
    }

    // Goal RadioButton CheckedChanged Handler
    private void OnGoalButtonClicked(object sender, EventArgs e)
    {
            GoalButton.BackgroundColor = Color.FromArgb("#8EC1F3");
            GoalButton.TextColor = Colors.White;

            HabitButton.BackgroundColor = Color.FromArgb("#B0B0B0");
            HabitButton.TextColor = Colors.Black;

            _task.Type = "Goal";
    }


    // RadioButton CheckedChanged Handlers
    private void OnStrengthButtonClicked(object sender, EventArgs e)
    {
        _task.Attribute = "Strength";
        StrengthButton.BackgroundColor = Color.FromArgb("#8EC1F3");
        StrengthButton.TextColor = Colors.White;

        IntelligenceButton.BackgroundColor = Color.FromArgb("#B0B0B0");
        IntelligenceButton.TextColor = Colors.Black;

        CharismaButton.BackgroundColor = Color.FromArgb("#B0B0B0");
        CharismaButton.TextColor = Colors.Black;
    }

    private void OnIntelligenceButtonClicked(object sender, EventArgs e)
    {
        _task.Attribute = "Intelligence";
        IntelligenceButton.BackgroundColor = Color.FromArgb("#8EC1F3");
        IntelligenceButton.TextColor = Colors.White;

        StrengthButton.BackgroundColor = Color.FromArgb("#B0B0B0");
        StrengthButton.TextColor = Colors.Black;

        CharismaButton.BackgroundColor = Color.FromArgb("#B0B0B0");
        CharismaButton.TextColor = Colors.Black;
    }

    private void OnCharismaButtonClicked(object sender, EventArgs e)
    {
        _task.Attribute = "Charisma";
        CharismaButton.BackgroundColor = Color.FromArgb("#8EC1F3");
        CharismaButton.TextColor = Colors.White;

        StrengthButton.BackgroundColor = Color.FromArgb("#B0B0B0");
        StrengthButton.TextColor = Colors.Black;

        IntelligenceButton.BackgroundColor = Color.FromArgb("#B0B0B0");
        IntelligenceButton.TextColor = Colors.Black;
    }

    private void SaveChanges(object sender, EventArgs e)
    {
        // Оновлення даних задачі
        _task.Title = taskTitleEntry.Text;
        


        // Повернення до попередньої сторінки
        Navigation.PopModalAsync();
    }
}