using Habbit.Resources.Models;

namespace Habbit.Resources.Pages;

public partial class AddPage : ContentPage
{
	public AddPage()
	{
		InitializeComponent();
	}

    // Habit RadioButton CheckedChanged Handler
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

    private void AddTask(object sender, EventArgs e)
    {
        // Витягуємо дані з полів вводу та радіокнопок
        var title = taskTitleEntry.Text; // Витягуємо з Entry
        var type = HabitRadioButton.IsChecked ? "Habit" : "Goal";
        var attribute = StrengthRadioButton.IsChecked ? "Strength" :
                        IntelligenceRadioButton.IsChecked ? "Intelligence" :
                        CharismaRadioButton.IsChecked ? "Charisma" : null;

        // Перевірка введених даних
        if (string.IsNullOrWhiteSpace(title))
        {
            DisplayAlert("Error", "Task Title is required!", "OK");
            return;
        }

        // Створюємо нову задачу
        var newTask = new TaskItem
        {
            Title = title,
            Type = type,
            Attribute = attribute,
            IsCompleted = false
        };

        // Додаємо в репозиторій
        TaskRepository.Tasks.Add(newTask);

        // Сповіщення користувача
        DisplayAlert("Success", "Task added successfully!", "OK");

        // Повертаємося на попередню сторінку
        Navigation.PopAsync();

    }

}