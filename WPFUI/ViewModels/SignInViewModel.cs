using CommandsClassLibrary;
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
		private string login;
		private string password;
		private bool isLoginCorrect;
		private bool isPasswordCorrect;
		private string errorText;
		CancellationTokenSource cancelTokenSource;

		private Command signInCommand;
		private Command goMainMenuCommand;

		public SignInViewModel()
		{
			ParseConfig();
			ConnectClient();

			IsLoginCorrect = false;
			IsPasswordCorrect = false;
			InitializeCommands();
			InitializePropertyChanged();
		}

		private void InitializeCommands()
		{
			signInCommand = new DelegateCommand(SignIn, SignInCanExecute);
			goMainMenuCommand = new DelegateCommand(GoToMainPage, () => true);
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
			//BinaryFormatter formatter = new BinaryFormatter();
			//formatter.Serialize(client.GetStream(), new ClientUserDataCommand(Login, UserServiceDapper.ComputeSha256Hash(Password)));

			cancelTokenSource = new CancellationTokenSource();
			CancellationToken token = cancelTokenSource.Token;
			Task.Run(() => Listen(token), token);
		}

		public void GoToMainPage()
		{
			Navigation.Navigation.Navigate(Navigation.Navigation.MainMenuAlias, null);
		}

		private void Listen(CancellationToken token)
		{

			while (true)
			{
				try
				{
					if (token.IsCancellationRequested)
						return;

					BinaryFormatter formatter = new BinaryFormatter();
					ServerUserDataCommand command = (ServerUserDataCommand)formatter.Deserialize(client.GetStream());

					if (!String.IsNullOrWhiteSpace(command.Login))
					{
						client.Close();
						//Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
						//		   new Action(() =>
						//		   Navigation.Navigation.Navigate(Navigation.Navigation.MainPageAlias,
						//		   new MainPageViewModel(command.Login))));
					}
					else
						Password = "";
					return;
				}
				catch (Exception)
				{
				}
			}
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
