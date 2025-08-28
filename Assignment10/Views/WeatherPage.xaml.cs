using Assignment10.ViewModel;
using Assignment10.Models;	


namespace Assignment10.Views;

public partial class WeatherPage : ContentPage
{
	public WeatherPage()
	{
		InitializeComponent();
		BindingContext = new WeatherViewModel();

    }
}