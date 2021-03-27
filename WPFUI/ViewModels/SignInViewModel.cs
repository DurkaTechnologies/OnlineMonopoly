using System;
using System.Threading;
using System.Windows.Input;
using UIWPF.Commands;
using UIWPF.ViewModels;
using WPFUI.Navigation;

namespace WPFUI.ViewModels
{
	class SignInViewModel : BaseViewModel
	{
		private string login;
		private string password;
		private bool isLoginCorrect;
		private bool isPasswordCorrect;
		private string errorText;

		private Command signInCommand;
		private Command goMainMenuCommand;
		private Command goRecoverCommand;


		public SignInViewModel()
		{
			IsLoginCorrect = false;
			IsPasswordCorrect = false;
			InitializeCommands();
			InitializePropertyChanged();
		}

		private void InitializeCommands()
		{
			signInCommand = new DelegateCommand(SignIn, SignInCanExecute);
			goMainMenuCommand = new DelegateCommand(GoToMainPage, () => true);
			goRecoverCommand = new DelegateCommand(GoToRecoverPage, () => true);

		}

		private void InitializePropertyChanged()
		{
			PropertyChanged += (sender, args) =>
			{
				if (args.PropertyName.Equals(nameof(Login)))
				{
					signInCommand.RaiseCanExecuteChanged();
					IsLoginCorrect = !String.IsNullOrWhiteSpace(Login);
				}

				if (args.PropertyName.Equals(nameof(Password)))
				{
					signInCommand.RaiseCanExecuteChanged();
					IsPasswordCorrect = !String.IsNullOrWhiteSpace(Password);
				}

				if (args.PropertyName.Equals(nameof(IsLoginCorrect)) ||
					args.PropertyName.Equals(nameof(IsPasswordCorrect)))
				{
					if (IsLoginCorrect && IsPasswordCorrect)
						ErrorText = "";
					else if (IsLoginCorrect && !IsPasswordCorrect)
						ErrorText = "Password lenght not correct";
					else if (!IsLoginCorrect && !IsPasswordCorrect)
						ErrorText = "Password and Login incorrect";
				}
			};
		}

		public ICommand SignInCommand => signInCommand;
		public ICommand GoMainMenuCommand => goMainMenuCommand;
		public ICommand GoRecoverCommand => goRecoverCommand;


		public string Login
		{
			get => login;
			set
			{
				login = value;
				OnPropertyChanged();
			}
		}

		public string Password
		{
			get => password;
			set
			{
				password = value;
				OnPropertyChanged();
			}
		}

		public string ErrorText
		{
			get => errorText;
			set
			{
				errorText = value;
				OnPropertyChanged();
			}

		}

		private void SignIn()
		{
		
		}

		public void GoToMainPage()
		{
			Navigation.Navigation.Navigate(Navigation.Navigation.MainMenuAlias, null);
		}
		public void GoToRecoverPage()
		{
			Navigation.Navigation.Navigate(Navigation.Navigation.RecoverPageAlies, null);
		}

		private bool SignInCanExecute() => IsLoginCorrect && IsPasswordCorrect;

		#region IsCorrect
		public bool IsLoginCorrect
		{
			get => isLoginCorrect;
			set
			{
				isLoginCorrect = value;
				OnPropertyChanged();
			}
		}

		public bool IsPasswordCorrect
		{
			get => isPasswordCorrect;
			set
			{
				isPasswordCorrect = value;
				OnPropertyChanged();
			}
		}

		#endregion

	}
}
