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
        HabitRadioButton.IsChecked = _task.Type == "Habit";
        GoalRadioButton.IsChecked = _task.Type == "Goal";
        StrengthRadioButton.IsChecked = _task.Attribute == "Strength";
        IntelligenceRadioButton.IsChecked = _task.Attribute == "Intelligence";
        CharismaRadioButton.IsChecked = _task.Attribute == "Charisma";
    }

    

    private void OnHabitRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value) // When selected
        {
            HabitRadioButton.BackgroundColor = Color.FromArgb("#8EC1F3");
            HabitRadioButton.TextColor = Colors.White;

            GoalRadioButton.BackgroundColor = Color.FromArgb("#B0B0B0");
            GoalRadioButton.TextColor = Colors.Black;
        }
    }

    // Goal RadioButton CheckedChanged Handler
    private void OnGoalRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value) // When selected
        {
            GoalRadioButton.BackgroundColor = Color.FromArgb("#8EC1F3");
            GoalRadioButton.TextColor = Colors.White;

            HabitRadioButton.BackgroundColor = Color.FromArgb("#B0B0B0");
            HabitRadioButton.TextColor = Colors.Black;
        }
    }


    // RadioButton CheckedChanged Handlers
    private void OnStrengthRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value) // When selected
        {
            StrengthRadioButton.BackgroundColor = Color.FromArgb("#8EC1F3");
            StrengthRadioButton.TextColor = Colors.White;
        }
        else // When deselected
        {
            StrengthRadioButton.BackgroundColor = Color.FromArgb("#B0B0B0");
            StrengthRadioButton.TextColor = Colors.Black;
        }
    }

    private void OnIntelligenceRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value) // When selected
        {
            IntelligenceRadioButton.BackgroundColor = Color.FromArgb("#8EC1F3");
            IntelligenceRadioButton.TextColor = Colors.White;
        }
        else // When deselected
        {
            IntelligenceRadioButton.BackgroundColor = Color.FromArgb("#B0B0B0");
            IntelligenceRadioButton.TextColor = Colors.Black;
        }
    }

    private void OnCharismaRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value) // When selected
        {
            CharismaRadioButton.BackgroundColor = Color.FromArgb("#8EC1F3");
            CharismaRadioButton.TextColor = Colors.White;
        }
        else // When deselected
        {
            CharismaRadioButton.BackgroundColor = Color.FromArgb("#B0B0B0");
            CharismaRadioButton.TextColor = Colors.Black;
        }
    }

    private void SaveChanges(object sender, EventArgs e)
    {
        // Оновлення даних задачі
        _task.Title = taskTitleEntry.Text;
        _task.Type = HabitRadioButton.IsChecked ? "Habit" : "Goal";
        _task.Attribute = StrengthRadioButton.IsChecked ? "Strength" :
                          IntelligenceRadioButton.IsChecked ? "Intelligence" :
                          CharismaRadioButton.IsChecked ? "Charisma" : null;


        // Повернення до попередньої сторінки
        Navigation.PopModalAsync();
    }
}