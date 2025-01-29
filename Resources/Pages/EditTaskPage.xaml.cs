
using Habbit.Resources.Models;
using Habbit.Services; 
using System;

namespace Habbit.Resources.Pages;

public partial class EditTaskPage : ContentPage
{
    private readonly Habbit.Resources.Models.Task _task;
    private readonly TaskService _taskService;

    public EditTaskPage(Habbit.Resources.Models.Task task, TaskService taskService)
    {
        InitializeComponent();
        _task = task;
        _taskService = taskService;

        
        taskTitleEntry.Text = _task.Title;
        HabitButton.BackgroundColor = _task.Type == TaskType.Habbit ? Color.FromArgb("#8EC1F3") : Color.FromArgb("#B0B0B0");
        GoalButton.BackgroundColor = _task.Type == TaskType.Goal ? Color.FromArgb("#8EC1F3") : Color.FromArgb("#B0B0B0");

        StrengthButton.BackgroundColor = _task.Attribute == TaskAttribute.Strength ? Color.FromArgb("#8EC1F3") : Color.FromArgb("#B0B0B0");
        IntelligenceButton.BackgroundColor = _task.Attribute == TaskAttribute.Intelligence ? Color.FromArgb("#8EC1F3") : Color.FromArgb("#B0B0B0");
        CharismaButton.BackgroundColor = _task.Attribute == TaskAttribute.Charisma ? Color.FromArgb("#8EC1F3") : Color.FromArgb("#B0B0B0");
        SliderDifficulty.Value = _task.Score;
    }

    private void OnHabitButtonClicked(object sender, EventArgs e)
    {
        HabitButton.BackgroundColor = Color.FromArgb("#8EC1F3");
        HabitButton.TextColor = Colors.White;

        GoalButton.BackgroundColor = Color.FromArgb("#B0B0B0");
        GoalButton.TextColor = Colors.Black;

        _task.Type = TaskType.Habbit;
    }

    private void OnGoalButtonClicked(object sender, EventArgs e)
    {
        GoalButton.BackgroundColor = Color.FromArgb("#8EC1F3");
        GoalButton.TextColor = Colors.White;

        HabitButton.BackgroundColor = Color.FromArgb("#B0B0B0");
        HabitButton.TextColor = Colors.Black;

        _task.Type = TaskType.Goal;
    }

    private void OnStrengthButtonClicked(object sender, EventArgs e)
    {
        _task.Attribute = TaskAttribute.Strength;
        StrengthButton.BackgroundColor = Color.FromArgb("#8EC1F3");
        StrengthButton.TextColor = Colors.White;

        IntelligenceButton.BackgroundColor = Color.FromArgb("#B0B0B0");
        IntelligenceButton.TextColor = Colors.Black;

        CharismaButton.BackgroundColor = Color.FromArgb("#B0B0B0");
        CharismaButton.TextColor = Colors.Black;
    }

    private void OnIntelligenceButtonClicked(object sender, EventArgs e)
    {
        _task.Attribute = TaskAttribute.Intelligence;
        IntelligenceButton.BackgroundColor = Color.FromArgb("#8EC1F3");
        IntelligenceButton.TextColor = Colors.White;

        StrengthButton.BackgroundColor = Color.FromArgb("#B0B0B0");
        StrengthButton.TextColor = Colors.Black;

        CharismaButton.BackgroundColor = Color.FromArgb("#B0B0B0");
        CharismaButton.TextColor = Colors.Black;
    }

    private void OnCharismaButtonClicked(object sender, EventArgs e)
    {
        _task.Attribute = TaskAttribute.Charisma;
        CharismaButton.BackgroundColor = Color.FromArgb("#8EC1F3");
        CharismaButton.TextColor = Colors.White;

        StrengthButton.BackgroundColor = Color.FromArgb("#B0B0B0");
        StrengthButton.TextColor = Colors.Black;

        IntelligenceButton.BackgroundColor = Color.FromArgb("#B0B0B0");
        IntelligenceButton.TextColor = Colors.Black;
    }

    private void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
    {
        _task.Score = e.NewValue;
    }

    private async void SaveChanges(object sender, EventArgs e)
    {
        
        _task.Title = taskTitleEntry.Text;

        try
        {
            
            bool isSuccess = await _taskService.UpdateTaskAsync(_task);

            if (isSuccess)
            {
                
                await DisplayAlert("Success", "Changes saved successfully!", "OK");
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Error", "Could not save changes. Please try again.", "OK");
            }
        }
        catch (Exception ex)
        {
            
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }
}
