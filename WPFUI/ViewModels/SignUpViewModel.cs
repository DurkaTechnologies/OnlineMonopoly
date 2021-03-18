using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using UIWPF.Commands;
using UIWPF.ViewModels;
using WPFUI.Navigation;

namespace WPFUI.ViewModels
{
	class SignUpViewModel : BaseViewModel
	{
		private string login;
		private string password;
		private string phoneNumber;
	
		private bool isLoginCorrect;
		private bool isPasswordCorrect;
		private bool isPhoneNumberCorrect;

		private Command signUpCommand;
		private Command goMainMenuCommand;
		private bool registerDone;

		public SignUpViewModel()
		{
			InitializeCommands();
			InitializePropertyChanged();
			IsLoginCorrect = false;
			IsPasswordCorrect = false;
			IsPhoneNumberCorrect = false;
			RegisterDone = false;
		}

		private void InitializeCommands()
		{
			signUpCommand = new DelegateCommand(SignUpAsync, SignUpCanExecute);
			goMainMenuCommand = new DelegateCommand(GoToMainMenu, () => true);
		}

		private void InitializePropertyChanged()
		{
			PropertyChanged += (sender, args) =>
			{
				if (args.PropertyName.Equals(nameof(Login)))
				{
					//object result = null;
					//Thread thread = new System.Threading.Thread(() =>
					//{
					//	result = !userDapper.GetLogins().Contains(Login);
					//});
					//thread.Start();
					//thread.Join();

					//IsLoginCorrect = (bool)result;
					//signUpCommand.RaiseCanExecuteChanged();
				}

				if (args.PropertyName.Equals(nameof(Password)))
				{
					IsPasswordCorrect = Password.Length >= 8;
					signUpCommand.RaiseCanExecuteChanged();
				}

				if (args.PropertyName.Equals(nameof(PhoneNumber)))
				{
					//Thread thread = new System.Threading.Thread(() =>
					//{
					//	IsPhoneNumberCorrect = UserService.CheckUserNumber(PhoneNumber);
					//	if (IsPhoneNumberCorrect)
					//		IsPhoneNumberCorrect = !userDapper.GetPhoneNumbers().Contains(PhoneNumber);
					//});
					//thread.Start();
					//thread.Join();

					signUpCommand.RaiseCanExecuteChanged();
				}
			};
		}

		public ICommand SignUpCommand => signUpCommand;
		public ICommand GoMainMenuCommand => goMainMenuCommand;

		private async void SignUpAsync()
		{

			//bool result = false;
			//Thread thread = new System.Threading.Thread(() =>
			//{
			//	result = userDapper.RegisterUser( new User()
			//	{
			//		Login = Login,
			//		Password = UserDapper.ComputeSha256Hash(Password),
			//		PhoneNumber = PhoneNumber,
			//		Balance = 0,
			//		SpentSum = 0			
			//	});
			//});
			//thread.Start();
			//thread.Join();

			//if (result)
			//{
			//	RegisterDone = true;
			//	await Task.Delay(1500);
			//	GoToMainMenu();
			//}
		}

		public void GoToMainMenu()
		{
			Navigation.Navigation.Navigate(Navigation.Navigation.MainMenuAlias, null);
		}

		private bool SignUpCanExecute() => IsLoginCorrect && IsPasswordCorrect && IsPhoneNumberCorrect;

		#region Properties
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

		public string PhoneNumber
		{
			get => phoneNumber;
			set
			{
				if (value.Length > 12)
					return;

				if (value.Length < phoneNumber?.Length)
				{
					if (phoneNumber.Length == 4)
						value = value.Remove(2);
					if (phoneNumber.Length == 8)
						value = value.Remove(6);
				}
				else if (value.Length == 3)
					value += '-';
				else if (value.Length == 7)
					value += '-';
				phoneNumber = value;
				OnPropertyChanged();
			}
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

		public bool IsPhoneNumberCorrect
		{
			get => isPhoneNumberCorrect;
			set
			{
				isPhoneNumberCorrect = value;
				OnPropertyChanged();
			}
		}

		public bool RegisterDone
		{
			get => registerDone;
			set
			{
				registerDone = value;
				OnPropertyChanged();
			}
		}
		#endregion
	}
}
