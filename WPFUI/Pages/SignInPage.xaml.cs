using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WPFUI.ViewModels;

namespace WPFUI.Pages
{
	/// <summary>
	/// Interaction logic for SignInPage.xaml
	/// </summary>
	public partial class SignInPage : Page
	{
		public SignInPage()
		{
			InitializeComponent();
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			Navigation.Navigation.Service = NavigationService.GetNavigationService(this);
			this.DataContext = new SignInViewModel();
		}
	}
}
