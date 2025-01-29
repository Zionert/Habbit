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

        // Витягуємо задачі типу "Habit"
        var userId = Preferences.Get("Auth0Id", null);
        if (string.IsNullOrEmpty(userId))
        {
            await DisplayAlert("Error", "User is not logged in.", "OK");
            return;
        }
        var tasks = await _taskService.GetByUserIdAsync(userId);

        // Вибираємо задачі типу "Goal"
        var habits = tasks.Where(t => t.Type == TaskType.Habbit).ToList();

        foreach (var habit in habits)
        {
            // Головна рамка для задачі
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
                         new ColumnDefinition { Width = GridLength.Star }, // Назва задачі
                         new ColumnDefinition { Width = GridLength.Auto }  // Кнопки
                    }
                }
            };
            bool isReadyForUpdate = habit.CompletionDate == null ||
                                (DateTime.UtcNow - habit.CompletionDate.Value).TotalHours >= 24;

            var textColor = Colors.Black; // Колір за замовчуванням
            if (habit.Attribute == TaskAttribute.Strength)
            {
                textColor = Color.FromArgb("#FF6347"); // Червоний колір для Strength
            }
            else if (habit.Attribute == TaskAttribute.Intelligence)
            {
                textColor = Color.FromArgb("#8EC1F3"); // Блакитний колір для Intelligence
            }
            else if (habit.Attribute == TaskAttribute.Charisma)
            {
                textColor = Color.FromArgb("#FFD700"); // Золотий колір для Charisma
            }


            // Назва задачі
            var label = new Label
            {
                Text = habit.Title,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                TextColor = textColor,
                FontSize = 16
            };

            // Кнопка завершення задачі
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
                UpdateHabitsList(); // Оновлюємо список
                var success = await _habitService.UpdateAttributeProgressAsync(userId, habit.Attribute, habit.Score / 100);
          
            };

            // Кнопка видалення задачі
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
                UpdateHabitsList(); // Оновлюємо список
            };

            // Горизонтальний стек для кнопок
            var buttonsStack = new HorizontalStackLayout
            {
                Spacing = 5,
                Children = { completeButton, deleteButton },
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center
            };  

            // Додавання елементів до сітки
            var grid = (Grid)taskFrame.Content;
            grid.Add(label, 0, 0);         // Назва задачі в перший стовпець
            grid.Add(buttonsStack, 1, 0); // Кнопки в другий стовпець

            taskFrame.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    var taskService = App.Current.Handler.MauiContext.Services.GetService<TaskService>();
                    await Navigation.PushModalAsync(new EditTaskPage(habit, taskService));
                    UpdateHabitsList(); // Оновлюємо список після повернення
                })
            });


            // Додавання рамки до контейнера
            HabitsLayout.Children.Add(taskFrame);
        }
    }


}