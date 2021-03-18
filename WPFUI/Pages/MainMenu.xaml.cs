using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WPFUI.ViewModels;

namespace WPFUI.Pages
{
	/// <summary>
	/// Interaction logic for MainMenu.xaml
	/// </summary>
	public partial class MainMenu : Page
	{
		public MainMenu()
		{
			InitializeComponent();
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			Navigation.Navigation.Service = NavigationService.GetNavigationService(this);

			this.DataContext = new MainMenuViewModel();
		}
	}
}
