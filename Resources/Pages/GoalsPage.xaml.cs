using Habbit.Resources.Models;

namespace Habbit.Resources.Pages;

public partial class GoalsPage : ContentPage
{
	public GoalsPage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        UpdateGoalsList();
    }


    private void UpdateGoalsList()
    {
        GoalsLayout.Children.Clear();

        // Вибираємо задачі типу "Goal"
        var goals = TaskRepository.Tasks.Where(t => t.Type == TaskType.Goal);

        foreach (var goal in goals)
        {
            // Головна рамка для кожної цілі
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
                         new ColumnDefinition { Width = GridLength.Star }, // Назва цілі
                         new ColumnDefinition { Width = GridLength.Auto }  // Кнопки
                    }
                }
            };

            var textColor = Colors.Black; // Колір за замовчуванням
            if (goal.Attribute == TaskAttribute.Strength)
            {
                textColor = Color.FromArgb("#FF6347"); // Червоний для Strength
            }
            else if (goal.Attribute == TaskAttribute.Intelligence)
            {
                textColor = Color.FromArgb("#8EC1F3"); // Блакитний для Intelligence
            }
            else if (goal.Attribute == TaskAttribute.Charisma)
            {
                textColor = Color.FromArgb("#FFD700"); // Золотий для Charisma
            }



            // Назва цілі
            var label = new Label
            {
                Text = goal.Title,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                TextColor = textColor,
                FontSize = 16
            };

            // Кнопка завершення цілі
            var completeButton = new Button
            {
                Text = "Yes",
                BackgroundColor = Colors.LightGreen,
                TextColor = Colors.White,
                CornerRadius = 20,
                WidthRequest = 50,
                HeightRequest = 40
            };

            completeButton.Clicked += (s, e) =>
            {
                goal.IsCompleted = true;
                DisplayAlert("Success", $"{goal.Title} completed!", "OK");
                UpdateGoalsList(); // Оновлюємо список
            };

            // Кнопка видалення цілі
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
                TaskRepository.Tasks.Remove(goal);
                UpdateGoalsList(); // Оновлюємо список
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
            grid.Add(label, 0, 0);         // Назва цілі в перший стовпець
            grid.Add(buttonsStack, 1, 0); // Кнопки в другий стовпець

            taskFrame.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    await Navigation.PushModalAsync(new EditTaskPage(goal));
                    UpdateGoalsList(); // Оновлюємо список після повернення
                })
            });

            // Додавання рамки до контейнера
            GoalsLayout.Children.Add(taskFrame);
        }
    }


}