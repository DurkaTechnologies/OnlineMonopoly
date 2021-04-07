using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using UIWPF.Commands;
using UIWPF.ViewModels;
using WPFUI.Navigation;

namespace WPFUI.ViewModels
{
	class SignInViewModel : BaseTCPViewModel
	{
		#region Fields

		private IPasswordSupplier suppliear;
		private string login;
		private string password;
		private bool isLoginCorrect;
		private bool isPasswordCorrect;
		private string errorText;

		private Command signInCommand;
		private Command goMainMenuCommand;
		private Command goRecoverCommand;

		#endregion

		public SignInViewModel(IPasswordSupplier suppliear)
		{
			this.suppliear = suppliear;
			ParseConfig();

			IsLoginCorrect = false;
			IsPasswordCorrect = false;

			InitializeCommands();
			InitializePropertyChanged();
		}

		#region Initialize

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
					else if (!IsLoginCorrect && IsPasswordCorrect)
						ErrorText = "Password and Login incorrect";
				}
			};
		}

		#endregion

		#region Proporties

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

		#endregion

		#region Methods

		public void UpdatePassword()
		{
			Password = suppliear.GetPassword();
		}

		private void SignIn()
		{
			/*get password method from passwordbox*/
			ErrorText = suppliear.GetPassword();
		}

		public void GoToMainPage()
		{
			Navigation.Navigation.Navigate(Navigation.Navigation.MainMenuAlias, null);
		}
		public void GoToRecoverPage()
		{
			Navigation.Navigation.Navigate(Navigation.Navigation.RecoverPageAlies, null);
		}

		#endregion

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

		private bool SignInCanExecute() => IsLoginCorrect && IsPasswordCorrect;
	}

	public interface IPasswordSupplier
	{
		string GetPassword();
	}
}
