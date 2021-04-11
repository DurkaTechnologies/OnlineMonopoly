using Grpc.Net.Client;
using SignUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using UIWPF.Commands;
using UIWPF.ViewModels;
using WPFUI.Navigation;

namespace WPFUI.ViewModels
{
	public class SignUpViewModel : BaseViewModel
	{
		#region Fields
		private IPasswordSupplier suppliear;

		private string login;
		private string password;
		private string email;

		private bool isLoginCorrect;
		private bool isPasswordCorrect;
		private bool isEmailCorrect;
		private bool registerDone;

		private Command signUpCommand;
		private Command goMainMenuCommand;

		GrpcChannel channel;
		Register.RegisterClient client;
		#endregion

		#region Ctors
		public SignUpViewModel()
		{
			channel = GrpcChannel.ForAddress("https://localhost:5001");
			client = new Register.RegisterClient(channel);

			InitializeCommands();
			InitializePropertyChanged();
			IsLoginCorrect = false;
			IsPasswordCorrect = false;
			IsEmailCorrect = false;
			RegisterDone = false;
		}

		public SignUpViewModel(IPasswordSupplier suppliear) : this()
		{
			this.suppliear = suppliear;
		}
		#endregion

		#region Initialize
		private void InitializeCommands()
		{
			signUpCommand = new DelegateCommand(SignUpAsync, SignUpCanExecute);
			goMainMenuCommand = new DelegateCommand(GoToMainMenu, () => true);
		}

		private void InitializePropertyChanged()
		{
			PropertyChanged += async (sender, args) =>
			{
				if (args.PropertyName.Equals(nameof(Login)))
				{
					IsLoginCorrect = (await client.CheckLoginAsync(new Login() { Login_ = login })).Correct_;
					signUpCommand.RaiseCanExecuteChanged();
				}

				if (args.PropertyName.Equals(nameof(Password)))
				{
					IsPasswordCorrect = Password.Length >= 8;
					signUpCommand.RaiseCanExecuteChanged();
				}

				if (args.PropertyName.Equals(nameof(Email)))
				{
					IsEmailCorrect = !String.IsNullOrWhiteSpace(Email);
					signUpCommand.RaiseCanExecuteChanged();
				}
			};
		}
		#endregion

		#region ICommands
		public ICommand SignUpCommand => signUpCommand;
		public ICommand GoMainMenuCommand => goMainMenuCommand;
		#endregion

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

		public string Email
		{
			get => email;
			set
			{
				email = value;
				OnPropertyChanged();
			}
		}

		#endregion

		#region Methods
		private async void SignUpAsync()
		{
			bool result = (await client.SendDataAsync(new RegInfo()
			{
				Login = Login,
				Password = ComputeSha256Hash(Password),
				Email = Email,
			})).Correct_;

			if (result)
			{
				RegisterDone = true;
				await Task.Delay(1500);
				GoToMainMenu();
			}
		}

		public void UpdatePassword()
		{
			Password = suppliear.GetPassword();
		}

		public void GoToMainMenu()
		{
			Navigation.Navigation.Navigate(Navigation.Navigation.MainMenuAlias, null);
		}

		private bool SignUpCanExecute() => IsLoginCorrect && IsPasswordCorrect && IsEmailCorrect;

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

		public bool IsEmailCorrect
		{
			get => isEmailCorrect;
			set
			{
				isEmailCorrect = value;
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

		#region Other
		public static string ComputeSha256Hash(string data)
		{
			using (SHA256 sha256Hash = SHA256.Create())
			{
				byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(data));
				StringBuilder builder = new StringBuilder();

				for (int i = 0; i < bytes.Length; i++)
					builder.Append(bytes[i].ToString("x2"));
				return builder.ToString();
			}
		}
		#endregion
	}
}
