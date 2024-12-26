namespace Habbit.Resources.Pages;
using System.ComponentModel;
using System.Xml.Linq;


[QueryProperty(nameof(AvatarUrl), "AvatarUrl")]
[QueryProperty(nameof(Name), "Name")]
public partial class StaticsPage : ContentPage, INotifyPropertyChanged
{

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
    }

    




}