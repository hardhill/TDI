using TDA.ViewModels;

namespace TDA;

public partial class MainPage : ContentPage
{
	

	public MainPage()
	{
		InitializeComponent();
		BindingContext = new MainViewModel();
	}

	
}

