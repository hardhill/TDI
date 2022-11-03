using Plugin.Maui.Audio;
using TDA.ViewModels;

namespace TDA;

public partial class MainPage : ContentPage
{
	

	public MainPage(IAudioManager audioManager)
	{
		InitializeComponent();
		BindingContext = new MainViewModel(audioManager, myEllipse);
		
	}

	
}

