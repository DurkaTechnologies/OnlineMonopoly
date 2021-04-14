﻿using Grpc.Net.Client;
using System;
using SignIn;
using System.Windows.Input;
using UIWPF.Commands;
using UIWPF.ViewModels;
using System.Security.Cryptography;
using System.Text;
using BLL.DTO;
using AutoMapper;

namespace WPFUI.ViewModels
{
	class SignInViewModel : BaseViewModel
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

		GrpcChannel channel;
		Loginer.LoginerClient client;
		#endregion

		#region Ctors

		public SignInViewModel()
		{
			channel = GrpcChannel.ForAddress("https://localhost:5001");
			client = new Loginer.LoginerClient(channel);

			IsLoginCorrect = false;
			IsPasswordCorrect = false;

			InitializeCommands();
			InitializePropertyChanged();
		}

		public SignInViewModel(IPasswordSupplier suppliear) : this()
		{
			this.suppliear = suppliear;
		}
		#endregion

		#region Initialize

		private void InitializeCommands()
		{
			signInCommand = new DelegateCommand(SignInAsync, SignInCanExecute);
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

		#region ICommands 
		public ICommand SignInCommand => signInCommand;
		public ICommand GoMainMenuCommand => goMainMenuCommand;
		public ICommand GoRecoverCommand => goRecoverCommand;
		#endregion

		#region Proporties
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

		private async void SignInAsync()
		{
			try
			{
				User user = await client.LoginAsync(new LoginInfo()
				{
					Login = Login,
					Password = ComputeSha256Hash(Password),
				});

				if (user == null)
				{
					ShowIncorrect();
				}
				else
				{
					if (String.IsNullOrEmpty(user.Login))
						ShowIncorrect();
					else
					{
						await channel.ShutdownAsync();

						Mapper mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()));
						UserDTO userDTO = mapper.Map<User, UserDTO>(user);

						Navigation.Navigation.Navigate(
							Navigation.Navigation.GeneralPageAlias,
							new GeneralPageViewModel(userDTO));
					}
				}
			}
			catch (Exception)
			{
				ShowIncorrect();
			}
		}

		private void ShowIncorrect()
		{
			ErrorText = "Password or Login incorrect";
			Password = "";
		}

		public void UpdatePassword()
		{
			Password = suppliear.GetPassword();
		}

		private string ComputeSha256Hash(string data)
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
		private bool SignInCanExecute() => IsLoginCorrect && IsPasswordCorrect;
		#endregion
	}

	public interface IPasswordSupplier
	{
		string GetPassword();
	}
}
