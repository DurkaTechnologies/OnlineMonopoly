using System.Windows.Input;
using UIWPF.Commands;
using UIWPF.ViewModels;
using WPFUI.Navigation;

namespace WPFUI.ViewModels
{
	class MainMenuViewModel : BaseViewModel
	{
		private Command goSignInCommand;
		private Command goSignUpCommand;
		private Command goMainPageCommand;

		public MainMenuViewModel()
		{
			InitializeCommands();
		}

		private void InitializeCommands()
		{
			goSignInCommand = new DelegateCommand(GoToSignInPage, () => true);
			goSignUpCommand = new DelegateCommand(GoToSignUpPage, () => true);
			goMainPageCommand = new DelegateCommand(GoToGamePage, () => true);
		}

		public ICommand GoSignInCommand => goSignInCommand;
		public ICommand GoSignUpCommand => goSignUpCommand;
		public ICommand GoMainPageCommand => goMainPageCommand;

		private void GoToSignInPage()
		{
			Navigation.Navigation.Navigate(Navigation.Navigation.SignInPageAlias, null);
		}

		private void GoToSignUpPage()
		{
			Navigation.Navigation.Navigate(Navigation.Navigation.SignUpPageAlias, null);
		}

		public void GoToGamePage()
		{
			Navigation.Navigation.Navigate(Navigation.Navigation.GamePageAlias, null);
		}
	}
}
