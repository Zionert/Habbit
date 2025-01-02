namespace Habbit.Resources.Pages;
using System.ComponentModel;
using System.Xml.Linq;
using CommunityToolkit.Mvvm.Messaging;
using static Habbit.Resources.Pages.GoalsPage;
using static Habbit.Resources.Pages.HabbitsPage;

[QueryProperty(nameof(AvatarUrl), "AvatarUrl")]
[QueryProperty(nameof(Name), "Name")]
public partial class StaticsPage : ContentPage, INotifyPropertyChanged
{

    private double _currentProgressStrengh = 0;
    private double _currentProgressIntelligence = 0;
    private double _currentProgressCharisma = 0;

    private int _levelStrengh = 1;
    private int _levelIntelligence = 1;
    private int _levelCharisma = 1;

    private string avatarUrl;
    public string AvatarUrl
    {
        get => avatarUrl;
        set
        {
            avatarUrl = value;
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
    public StaticsPage()
	{
		InitializeComponent();
        BindingContext = this; // Прив'язка контексту

        WeakReferenceMessenger.Default.Register<GoalsPage.ProgressUpdatedMessage1>(this, (r, message) =>
        {
            _currentProgressStrengh += message.ProgressDelta1;
            while (_currentProgressStrengh >= 1.0)
            {
                _currentProgressStrengh -= 1.0;
                _levelStrengh++;
            }
            progressBarStrengh.Progress = _currentProgressStrengh;
            UpdateLabels();
        });

        WeakReferenceMessenger.Default.Register<HabbitsPage.ProgressUpdatedMessage1>(this, (r, message) =>
        {
            _currentProgressStrengh += message.ProgressDelta1;
            while (_currentProgressStrengh >= 1.0)
            {
                _currentProgressStrengh -= 1.0;
                _levelStrengh++;
            }
            progressBarStrengh.Progress = _currentProgressStrengh;
            UpdateLabels();
        });

        WeakReferenceMessenger.Default.Register<GoalsPage.ProgressUpdatedMessage2>(this, (r, message) =>
        {
            _currentProgressIntelligence += message.ProgressDelta2;
            while (_currentProgressIntelligence >= 1.0)
            {
                _currentProgressIntelligence -= 1.0;
                _levelIntelligence++;
            }
            progressBarIntelligence.Progress = _currentProgressIntelligence;
            UpdateLabels();
        });

        WeakReferenceMessenger.Default.Register<HabbitsPage.ProgressUpdatedMessage2>(this, (r, message) =>
        {
            _currentProgressIntelligence += message.ProgressDelta2;
            while (_currentProgressIntelligence >= 1.0)
            {
                _currentProgressIntelligence -= 1.0;
                _levelIntelligence++;
            }
            progressBarIntelligence.Progress = _currentProgressIntelligence;
            UpdateLabels();
        });

        WeakReferenceMessenger.Default.Register<GoalsPage.ProgressUpdatedMessage3>(this, (r, message) =>
        {
            _currentProgressCharisma += message.ProgressDelta3;
            while (_currentProgressCharisma >= 1.0)
            {
                _currentProgressCharisma -= 1.0;
                _levelCharisma++;
            }
            progressBarCharisma.Progress = _currentProgressCharisma;
            UpdateLabels();
        });

        WeakReferenceMessenger.Default.Register<HabbitsPage.ProgressUpdatedMessage3>(this, (r, message) =>
        {
            _currentProgressCharisma += message.ProgressDelta3;
            while (_currentProgressCharisma >= 1.0)
            {
                _currentProgressCharisma -= 1.0;
                _levelCharisma++;
            }
            progressBarCharisma.Progress = _currentProgressCharisma;
            UpdateLabels();
        });
    }

    private void UpdateLabels()
    {
        labelStrengh.Text = $"LVL{_levelStrengh}";
        labelIntelligence.Text = $"LVL{_levelIntelligence}";
        labelCharisma.Text = $"LVL{_levelCharisma}";
        labelMain.Text = $"lvl {Math.Round((_levelStrengh + _levelIntelligence + _levelCharisma) / 3.0)}";
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        // Автоматично очищається в WeakReferenceMessenger, ручне відписування не потрібне
    }






}