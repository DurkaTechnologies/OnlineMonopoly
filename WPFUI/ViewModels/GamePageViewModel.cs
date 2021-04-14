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
using WPFUI.Pages;
using System.Windows.Controls;
using GameLoader;
using Google.Protobuf.WellKnownTypes;
using AutoMapper;
using BLL.DTO;
using System.Net.Http;
using static GameLoader.Branch.Types;

namespace WPFUI.ViewModels
{
	class GamePageViewModel : BaseViewModel
	{
		#region UsersLogic

		#region Fields

		private ObservableCollection<ShortInfo> users;
		private int usersCount;
		private DispatcherTimer timer = new DispatcherTimer();
		private int time;
		private int currentUser = 0;

		/*def rotate time for test*/

		private const int TIME = 3;

		#endregion

		#region Proporties

		public int CurrentUser
		{
			get => currentUser;
			set
			{
				currentUser = value;
				OnPropertyChanged();
			}
		}

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
			user.DefBackground = ColorManager.GetColorBrushFromLib();

			users.Add(user);
			UsersCount++;

			user.PlayerColor = ColorManager.GetColorBrushFromLib(UsersCount);
			user.CurrentUser = false;
		}

		private void TimerTick(object sender, EventArgs e)
		{
			if (time == TIME)
			{
				dices.FirstValue = 0;
				dices.FirstValue = 5;

				time = 0;

				users[CurrentUser].CurrentUser = false;
				CurrentUser++;

				if (CurrentUser == UsersCount)
					CurrentUser = 0;

				users[CurrentUser].CurrentUser = true;
			}
			users[CurrentUser].UserTime = TIME - time++;
		}

		private void StartTimer()
		{
			timer.Tick += TimerTick;
			timer.Interval = new TimeSpan(0, 0, 1);
			timer.Start();
		}

		#endregion

		#endregion

		#region GameBoardLogic

		#region Fields

		private GameDices dices;
		private Random rand = new Random();
		Grid grid;

		#endregion

		#region Properties

		#endregion

		#region Methods

		public void AddBranch(BranchDTO branch)
		{
			TransferCoord coord = GamePageTransfer.GetGameCoord(branch.Position);
			BranchControl control = new BranchControl(branch);

			grid.Children.Add(control);
			Grid.SetColumn(control, coord.X);
			Grid.SetRow(control, coord.Y);

			control.Rotate = coord.Rotate;
		}

		public void SetBranches(ICollection<BranchDTO> branches)
		{
			foreach (var item in branches)
				AddBranch(item);
		}

		#endregion

		#endregion

		public GamePageViewModel(IGridSuppliear grid)
		{
			Initialize(grid);

			SetBranches(GamePageLoader.GetBranches());

			AddUser(new ShortInfo() { UserName = "Pozhilou", UserMoney = 666, ImageSource = "https://pdacdn.com/photo/1570603604764.jpg" });
			AddUser(new ShortInfo() { UserName = "KazzModan", UserMoney = 999, ImageSource = "https://cdn.discordapp.com/attachments/821379755743903764/829448089928597516/38bb8d4c3816590e.png" });
			AddUser(new ShortInfo() { UserName = "deathCodeDevelop", UserMoney = 1200, ImageSource = "https://cdn.discordapp.com/attachments/821379755743903764/829441923084320810/sticker.png" });
			AddUser(new ShortInfo() { UserName = "Yulia", UserMoney = 1333, ImageSource = "https://cdn.discordapp.com/attachments/821379755743903764/829308495093432430/unknown.png" });
			AddUser(new ShortInfo() { UserName = "Programist", UserMoney = 1422, ImageSource = "https://cdn.discordapp.com/attachments/821379755743903764/829297626120716308/F56E508D-C792-4715-A94A-DD4C878BD73B.jpg" });

			/*Test Rotate*/

			users[0].CurrentUser = true;
			StartTimer();
		}

		public GamePageViewModel(object obj)
		{
			InitializeCommands();
			InitializePropertyChanged();
      
			InitializeCommands();
			InitializePropertyChanged();

			channel = GrpcChannel.ForAddress("https://localhost:5001");
			client = new ChatRoom.ChatRoomClient(channel);
			chat = client.join();

			MessageText = "";
			Messages = new ObservableCollection<string>();
		}

		#region Chat

		#region Fields

		private string messageText;
		private Command sendCommand;
		string userName = new Random().Next(0, 100).ToString();
		GrpcChannel channel;
		ChatRoom.ChatRoomClient client;
		AsyncDuplexStreamingCall<Message, Message> chat;
		ObservableCollection<string> messages;
		bool p = true;
		#endregion

		#region Proporties

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

		#region Methods

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

		#endregion

		#region Initialize

		private void Initialize(IGridSuppliear grid) 
		{
			this.grid = grid.GetGrid();

			InitializeCommands();
			InitializePropertyChanged();
			InitializeDices();

			users = new ObservableCollection<ShortInfo>();
		}

		private void InitializeDices() 
		{
			dices = new GameDices();

			this.grid.Children.Add(dices);
			Grid.SetColumn(dices, 3);
			Grid.SetRow(dices, 3);
			Grid.SetColumnSpan(dices, 5);
			Grid.SetRowSpan(dices, 5);
			Grid.SetZIndex(dices, 10);
		}

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
