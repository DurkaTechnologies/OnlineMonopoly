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
		private Command goGeneralPageCommand;

		public MainMenuViewModel()
		{
			InitializeCommands();
		}

		private void InitializeCommands()
		{
			goSignInCommand = new DelegateCommand(GoToSignInPage, () => true);
			goSignUpCommand = new DelegateCommand(GoToSignUpPage, () => true);
			goMainPageCommand = new DelegateCommand(GoToGamePage, () => true);
			goGeneralPageCommand = new DelegateCommand(GoToGeneralPage, () => true);
		}

		public ICommand GoSignInCommand => goSignInCommand;
		public ICommand GoSignUpCommand => goSignUpCommand;
		public ICommand GoMainPageCommand => goMainPageCommand;
		public ICommand GoGeneralPageCommand => goGeneralPageCommand;

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

		public void GoToGeneralPage()
		{
			Navigation.Navigation.Navigate(Navigation.Navigation.GeneralPageAlias, null);
		}
	}
}
