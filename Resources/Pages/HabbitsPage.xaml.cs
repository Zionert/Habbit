using CommunityToolkit.Mvvm.Messaging;
using Habbit.Resources.Models;
using Habbit.Services;
using Habbit.Resources.Pages;

namespace Habbit.Resources.Pages;

public partial class HabbitsPage : ContentPage
{
    private readonly TaskService _taskService;
    private readonly HabitService _habitService;
    public HabbitsPage(TaskService taskService, HabitService habitService)
	{
		InitializeComponent();
        _taskService = taskService;
        _habitService = habitService;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        UpdateHabitsList();
    }

    public record ProgressUpdatedMessage1(double ProgressDelta1);
    public record ProgressUpdatedMessage2(double ProgressDelta2);
    public record ProgressUpdatedMessage3(double ProgressDelta3);


    private async void UpdateHabitsList()
    {
        HabitsLayout.Children.Clear();

        
        var userId = Preferences.Get("Auth0Id", null);
        if (string.IsNullOrEmpty(userId))
        {
            await DisplayAlert("Error", "User is not logged in.", "OK");
            return;
        }
        var tasks = await _taskService.GetByUserIdAsync(userId);

        
        var habits = tasks.Where(t => t.Type == TaskType.Habbit).ToList();

        foreach (var habit in habits)
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
            bool isReadyForUpdate = habit.CompletionDate == null ||
                                (DateTime.UtcNow - habit.CompletionDate.Value).TotalHours >= 24;

            var textColor = Colors.Black; 
            if (habit.Attribute == TaskAttribute.Strength)
            {
                textColor = Color.FromArgb("#FF6347"); 
            }
            else if (habit.Attribute == TaskAttribute.Intelligence)
            {
                textColor = Color.FromArgb("#8EC1F3"); 
            }
            else if (habit.Attribute == TaskAttribute.Charisma)
            {
                textColor = Color.FromArgb("#FFD700"); 
            }


          
            var label = new Label
            {
                Text = habit.Title,
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
                HeightRequest = 40,
                IsEnabled = isReadyForUpdate
            }; 

            completeButton.Clicked += async(s, e) =>
            {
                await _taskService.MarkTaskAsCompletedAsync(habit.Id);
                DisplayAlert("Success", $"{habit.Title} completed!", "OK");
                UpdateHabitsList(); 
                var success = await _habitService.UpdateAttributeProgressAsync(userId, habit.Attribute, habit.Score / 100);
          
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
                _taskService.DeleteAsync(habit.Id);
                UpdateHabitsList(); 
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
                    await Navigation.PushModalAsync(new EditTaskPage(habit, taskService));
                    UpdateHabitsList(); 
                })
            });


           
            HabitsLayout.Children.Add(taskFrame);
        }
    }


}