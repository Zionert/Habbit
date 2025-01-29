using Habbit.Resources.Models;
using Habbit.Services;



namespace Habbit.Resources.Pages;

public partial class AddPage : ContentPage
{
    private readonly TaskService _taskService;
    public AddPage(TaskService taskService)
	{
		InitializeComponent();
        _taskService = taskService;
    }

    private TaskType? selectedType = null;     
    private TaskAttribute? selectedAttribute = null; 


    private void OnHabitButtonClicked(object sender, EventArgs e)
    {
        

            selectedType = TaskType.Habbit;
            HabitButton.BackgroundColor = Color.FromArgb("#8EC1F3");
            HabitButton.TextColor = Colors.White;

            GoalButton.BackgroundColor = Color.FromArgb("#B0B0B0");
            GoalButton.TextColor = Colors.Black;

    }


    private void OnGoalButtonClicked(object sender, EventArgs e)
    {


            selectedType = TaskType.Goal;
            GoalButton.BackgroundColor = Color.FromArgb("#8EC1F3");
            GoalButton.TextColor = Colors.White;

            HabitButton.BackgroundColor = Color.FromArgb("#B0B0B0");
            HabitButton.TextColor = Colors.Black;

    }


   
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

    private async void AddTask(object sender, EventArgs e)
    {
       
        var title = taskTitleEntry.Text; 
        var type = selectedType;        
        var attribute = selectedAttribute;
        var difficulty = SliderDifficulty.Value;

        
        if (type == null || attribute == null)
        {
            await DisplayAlert("Error", "Task Type and Attribute are required!", "OK");
            return;
        }
        if (string.IsNullOrEmpty(title))
        {
            await DisplayAlert("Error", "Task Title is required!", "OK");
            return;
        }

        var userId = Preferences.Get("Auth0Id", null);
        if (string.IsNullOrEmpty(userId))
        {
            await DisplayAlert("Error", "User is not logged in.", "OK");
            return;
        }
       
        var newTask = new Habbit.Resources.Models.Task
        {
            Title = title,
            UserId = userId,
            Type = type.Value,
            Attribute = attribute.Value,
            CompletionDate = null,
            Description = "",
            Score = difficulty
        };

        
        var createdTask = await _taskService.CreateAsync(newTask);

        
        if (createdTask != null)
        {
            await DisplayAlert("Success", "Task added successfully!", "OK");
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Error", "Failed to create task.", "OK");
        }

    }

}