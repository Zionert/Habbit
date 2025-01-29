namespace Habbit.Resources.Pages;
using System.ComponentModel;
using System.Xml.Linq;
using CommunityToolkit.Mvvm.Messaging;
using Habbit.Services;
using static Habbit.Resources.Pages.GoalsPage;
using static Habbit.Resources.Pages.HabbitsPage;

[QueryProperty(nameof(AvatarUrl), "AvatarUrl")]
[QueryProperty(nameof(Name), "Name")]
public partial class StaticsPage : ContentPage, INotifyPropertyChanged
{
    private readonly HabitService _habitService;
    private double _currentProgressStrength;
    private double _currentProgressIntelligence;
    private double _currentProgressCharisma;

    private int _levelStrength;
    private int _levelIntelligence;
    private int _levelCharisma;

    private string avatarUrl;
    public string AvatarUrl
    {
        get => avatarUrl;
        set
        {
            avatarUrl = value;
            Console.WriteLine($"AvatarUrl set to: {avatarUrl}");
            OnPropertyChanged(nameof(AvatarUrl));
        }
    }

    private string name;
    public string Name
    {
        get => name;
        set
        {
            name = value;
            OnPropertyChanged(nameof(Name));
        }
    }
    public StaticsPage(HabitService habitService)
    {
        InitializeComponent();
        BindingContext = this; // Прив'язка контексту
        _habitService = habitService;
        Console.WriteLine($"Initializing StaticsPage with AvatarUrl: {AvatarUrl}");
        Task.Run(async () => await LoadStatsAsync());
    }

    private async Task LoadStatsAsync()
    {
        try
        {
            var userId = Preferences.Get("Auth0Id", null);
            if (string.IsNullOrEmpty(userId))
            {
                Console.WriteLine("User ID not found in preferences.");
                return;
            }

            // Отримуємо прогрес
            var strengthExp = await _habitService.GetStrengthProgressAsync(userId);
            var intelligenceExp = await _habitService.GetIntelligenceProgressAsync(userId);
            var charismaExp = await _habitService.GetCharismaProgressAsync(userId);

            Console.WriteLine($"Strength: {strengthExp}, Intelligence: {intelligenceExp}, Charisma: {charismaExp}");

            // Оновлюємо прогрес і рівні
            _currentProgressStrength = CalculateExp(strengthExp ?? 0);
            _currentProgressIntelligence = CalculateExp(intelligenceExp ?? 0);
            _currentProgressCharisma = CalculateExp(charismaExp ?? 0);

            _levelStrength = CalculateLevel(strengthExp ?? 0);
            _levelIntelligence = CalculateLevel(intelligenceExp ?? 0);
            _levelCharisma = CalculateLevel(charismaExp ?? 0);

            // Оновлення UI
            MainThread.BeginInvokeOnMainThread(() =>
            {
                UpdateLabels();
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading stats: {ex.Message}");
        }
    }


    private double CalculateExp(double exp)
    {
        int level = 0;
        while (exp >= 1.0)
        {
            exp -= 1.0;
            level++;
        }
        return exp;
    }

    private int CalculateLevel(double exp)
    {
        int level = 0;
        while (exp >= 1.0)
        {
            exp -= 1.0;
            level++;
        }
        return level;
    }

    private void UpdateLabels()
    {
        labelStrengh.Text = $"LVL{_levelStrength}";
        progressBarStrengh.Progress = _currentProgressStrength;
        labelIntelligence.Text = $"LVL{_levelIntelligence}";
        progressBarIntelligence.Progress = _currentProgressIntelligence;
        labelCharisma.Text = $"LVL{_levelCharisma}";
        progressBarCharisma.Progress = _currentProgressCharisma;
        labelMain.Text = $"lvl {Math.Round((_levelStrength + _levelIntelligence + _levelCharisma) / 3.0)}";
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Task.Run(async () => await LoadStatsAsync());
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        // Автоматично очищається в WeakReferenceMessenger, ручне відписування не потрібне
    }
}