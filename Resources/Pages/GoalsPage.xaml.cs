using Habbit.Resources.Models;
using CommunityToolkit.Mvvm.Messaging;
using Habbit.Services;

namespace Habbit.Resources.Pages;

public partial class GoalsPage : ContentPage
{
    private readonly TaskService _taskService;
    private readonly HabitService _habitService;
    public GoalsPage(TaskService taskService, HabitService habitService)
	{
		InitializeComponent();
        _taskService = taskService;
        _habitService = habitService;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        UpdateGoalsList();
    }

    public record ProgressUpdatedMessage1(double ProgressDelta1);
    public record ProgressUpdatedMessage2(double ProgressDelta2);
    public record ProgressUpdatedMessage3(double ProgressDelta3);


    private async void UpdateGoalsList()
    {
        GoalsLayout.Children.Clear();

        var userId = Preferences.Get("Auth0Id", null);
        if (string.IsNullOrEmpty(userId))
        {
            await DisplayAlert("Error", "User is not logged in.", "OK");
            return;
        }
        var tasks = await _taskService.GetByUserIdAsync(userId);

        
        var goals = tasks.Where(t => t.Type == TaskType.Goal).ToList();

        foreach (var goal in goals)
        {
           
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
                         new ColumnDefinition { Width = GridLength.Star }, 
                         new ColumnDefinition { Width = GridLength.Auto }  
                    }
                }
            };

            var textColor = Colors.Black; 
            if (goal.Attribute == TaskAttribute.Strength)
            {
                textColor = Color.FromArgb("#FF6347"); 
            }
            else if (goal.Attribute == TaskAttribute.Intelligence)
            {
                textColor = Color.FromArgb("#8EC1F3"); 
            }
            else if (goal.Attribute == TaskAttribute.Charisma)
            {
                textColor = Color.FromArgb("#FFD700"); 
            }



            
            var label = new Label
            {
                Text = goal.Title,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                TextColor = textColor,
                FontSize = 16
            };

         
            var completeButton = new Button
            {
                Text = "Yes",
                BackgroundColor = Colors.LightGreen,
                TextColor = Colors.White,
                CornerRadius = 20,
                WidthRequest = 50,
                HeightRequest = 40
            };

            completeButton.Clicked += async (s, e) =>
            {
                await DisplayAlert("Success", $"{goal.Title} completed!", "OK");
                await _taskService.DeleteAsync(goal.Id);
                UpdateGoalsList(); 
                var success = await _habitService.UpdateAttributeProgressAsync(userId, goal.Attribute, goal.Score / 100);

                if (success)
                {
                    
                    await DisplayAlert("Success", "Progress updated successfully!", "OK");
                    if (goal.Attribute == TaskAttribute.Strength)
                    {
                        WeakReferenceMessenger.Default.Send(new ProgressUpdatedMessage1(goal.Score / 100));
                    }

                    if (goal.Attribute == TaskAttribute.Intelligence)
                    {
                        WeakReferenceMessenger.Default.Send(new ProgressUpdatedMessage2(goal.Score / 100));
                    }

                    if (goal.Attribute == TaskAttribute.Charisma)
                    {
                        WeakReferenceMessenger.Default.Send(new ProgressUpdatedMessage3(goal.Score / 100));
                    }
                }
                else
                {
                    await DisplayAlert("Error", "Failed to update progress via API.", "OK");
                }
            };

            
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
                _taskService.DeleteAsync(goal.Id);
                UpdateGoalsList(); 
            };

            
            var buttonsStack = new HorizontalStackLayout
            {
                Spacing = 5,
                Children = { completeButton, deleteButton },
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center
            };

            
            var grid = (Grid)taskFrame.Content;
            grid.Add(label, 0, 0);        
            grid.Add(buttonsStack, 1, 0); 

            taskFrame.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    
                    var taskService = App.Current.Handler.MauiContext.Services.GetService<TaskService>();
                    await Navigation.PushModalAsync(new EditTaskPage(goal, taskService));
                    UpdateGoalsList(); 
                })
            });

            
            GoalsLayout.Children.Add(taskFrame);
        }
    }


}