using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WPFUI.ViewModels;

namespace WPFUI.Pages
{
	/// <summary>
	/// Interaction logic for SignInPage.xaml
	/// </summary>
	public partial class SignInPage : Page, IPasswordSupplier
	{
		public SignInPage()
		{
			InitializeComponent();
		}

		public string GetPassword()
		{
			return passwordBox.Password;
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			Navigation.Navigation.Service = NavigationService.GetNavigationService(this);
			this.DataContext = new SignInViewModel(this);
		}

		private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
		{
			(this.DataContext as SignInViewModel).UpdatePassword();
		}
	}
}
