using Grpc.Net.Client;
using System;
using System.Threading;
using System.Threading.Tasks;
using UIWPF.ViewModels;
using Chat;
using UIWPF.Commands;
using System.Windows.Input;
using Grpc.Core;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;
using WPFUI.UserControls;
using System.Collections.Generic;
using WPFUI.Views;

namespace WPFUI.ViewModels
{
	class GamePageViewModel : BaseViewModel
	{
		#region Fields

		private ICollection<ShortInfo> users;
		private int usersCount;

		#endregion

		#region Proporties

		public IEnumerable<ShortInfo> Users => users;

		public int UsersCount
		{
			get => usersCount;
			set
			{
				usersCount = value;
				OnPropertyChanged();
			}
		}

		#endregion

		#region Methods

		public void AddUser(ShortInfo user)
		{
			user.DefBackground = ColorGradientGenerator.GetColorBrushFromLib();

			users.Add(user);
			UsersCount++;

			user.PlayerColor = ColorGradientGenerator.GetColorBrushFromLib(UsersCount);
			user.CurrentUser = false;
		}

		#endregion

		public GamePageViewModel()
		{
			users = new ObservableCollection<ShortInfo>();

			AddUser(new ShortInfo() { UserName = "Pozhilou", UserMoney = 666, ImageSource = "https://pdacdn.com/photo/1570603604764.jpg"});
			AddUser(new ShortInfo() { UserName = "KazzModan", UserMoney = 999, ImageSource = "https://cdn.discordapp.com/attachments/821379755743903764/829448089928597516/38bb8d4c3816590e.png" });
			AddUser(new ShortInfo() { UserName = "deathCodeDevelop", UserMoney = 1200, ImageSource = "https://cdn.discordapp.com/attachments/821379755743903764/829441923084320810/sticker.png" });
			AddUser(new ShortInfo() { UserName = "Yulia", UserMoney = 1333, ImageSource = "https://cdn.discordapp.com/attachments/821379755743903764/829308495093432430/unknown.png" });
			AddUser(new ShortInfo() { UserName = "Programist", UserMoney = 1422, ImageSource = "https://cdn.discordapp.com/attachments/821379755743903764/829297626120716308/F56E508D-C792-4715-A94A-DD4C878BD73B.jpg" });
		}

		public GamePageViewModel(object obj)
		{
			InitializeCommands();
			InitializePropertyChanged();

			channel = GrpcChannel.ForAddress("https://localhost:5001");
			client = new ChatRoom.ChatRoomClient(channel);
			chat = client.join();

			MessageText = "";
			Messages = new ObservableCollection<string>();
		}

		#region TestFields

		private string messageText;
		private Command sendCommand;
		string userName = new Random().Next(0, 100).ToString();
		GrpcChannel channel;
		ChatRoom.ChatRoomClient client;
		AsyncDuplexStreamingCall<Message, Message> chat;
		ObservableCollection<string> messages;
		bool p = true;

		#endregion

		#region TestProporties

		public ICommand SendCommand => sendCommand;

		public string MessageText
		{
			get => messageText;
			set
			{
				messageText = value;
				OnPropertyChanged();
			}
		}

		public ObservableCollection<string> Messages
		{
			get => messages;
			set
			{
				messages = value;
				OnPropertyChanged();
			}
		}

		#endregion

		#region TestMethods

		private async Task Start()
		{
			await Task.Run(async () =>
			{
				while (await chat.ResponseStream.MoveNext(cancellationToken: CancellationToken.None))
				{
					var response = chat.ResponseStream.Current;

					Application.Current.Dispatcher.Invoke(DispatcherPriority.Background,
							   new Action(() => Messages.Add($"[{response.User}]: {response.Text}")));
				}
			});
		}

		private async void SendMessage()
		{
			await chat.RequestStream.WriteAsync(new Message { User = userName, Text = MessageText });
		}

		#endregion

		#region Initialize

		private void InitializeCommands()
		{
			sendCommand = new DelegateCommand(SendMessage, () => !String.IsNullOrWhiteSpace(MessageText));
		}

		private void InitializePropertyChanged()
		{
			PropertyChanged += async (sender, args) =>
			{
				if (args.PropertyName.Equals(nameof(MessageText)))
				{
					if (p)
					{
						p = false;

						//await Start();
					}

					sendCommand.RaiseCanExecuteChanged();
				}
			};
		}

		#endregion
	}
}
