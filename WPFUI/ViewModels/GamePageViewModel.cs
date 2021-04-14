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
	static class GameLoader 
	{
		static HttpClientHandler httpHandler;
		static GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:5001");
		static Loader.LoaderClient client;
		static Mapper mapper;

		static GameLoader()
		{
			httpHandler = new HttpClientHandler();

			httpHandler.ServerCertificateCustomValidationCallback =
					HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

			IConfigurationProvider configuration = new MapperConfiguration(
			
			cfg =>
			{
				cfg.CreateMap<RentSetting, RentSettingDTO>();
				cfg.CreateMap<BranchType, BranchTypeDTO>();
				cfg.CreateMap<Branch, BranchDTO>();

				cfg.CreateMap<RentSettingDTO, RentSetting>();
				cfg.CreateMap<BranchTypeDTO, BranchType>();
				cfg.CreateMap<BranchDTO, Branch>();
			});

			mapper = new Mapper(configuration);
		}

		public static ICollection<BranchDTO> GetBranches() 
		{
			Connect();

			Branches branches = client.LoadBranches(new Empty());
			ICollection<BranchDTO> collection = new Collection<BranchDTO>();

			foreach (var item in branches.Collection)
				collection.Add(mapper.Map<Branch, BranchDTO>(item));

			Close();

			return collection;
		}

		private static void Connect() 
		{
			channel = GrpcChannel.ForAddress("https://localhost:5001",
					new GrpcChannelOptions { HttpHandler = httpHandler });

			client = new Loader.LoaderClient(channel);
		}

		private static void Close()
		{
			channel.ShutdownAsync();
			client = null;
		}
	}

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

		private const int TIME = 1;

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
				time = 0;

				users[CurrentUser].CurrentUser = false;
				CurrentUser++;

				if (CurrentUser == UsersCount)
					CurrentUser = 0;

				users[CurrentUser].CurrentUser = true;
			}
			users[CurrentUser].UserTime = TIME - time++;

			FirstDiceValue = 3;
		}

		private void StartTimer()
		{
			timer.Tick += TimerTick;
			timer.Interval = new TimeSpan(0, 0, 1);
			timer.Start();
		}

		#endregion

		#endregion

		#region GameBoardLogid

		#region Fields

		GrpcChannel boardChannel;
		Loader.LoaderClient loaderClient;
		private int firstDiceValue;
		private Random rand = new Random();
		Grid grid;

		#endregion

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

		#region Properties

		public int FirstDiceValue
		{
			get => firstDiceValue;
			set
			{
				firstDiceValue = value;
				OnPropertyChanged();
			}
		}

		#endregion

		#endregion

		public GamePageViewModel(IGridSuppliear grid)
		{
			this.grid = grid.GetGrid();

			users = new ObservableCollection<ShortInfo>();
			InitializeCommands();
			InitializePropertyChanged();

			InitializeCommands();
			InitializePropertyChanged();

			try
			{
				client = new ChatRoom.ChatRoomClient(channel);
				chat = client.join();

				MessageText = "";
				Messages = new ObservableCollection<string>();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message); ;
			}

			SetBranches(GameLoader.GetBranches());

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

	public static class GamePageTransfer
	{
		public static TransferCoord GetGameCoord(int position)
		{
			TransferCoord coord = new TransferCoord();

			if (position < 0 || position > 40)
				return coord;

			if (position <= 10)
			{
				coord.X = position;
				coord.Rotate = Dock.Top;
			}
			else if (position > 10 && position <= 20)
			{
				coord.X = 10;
				coord.Y = position - 10;
				coord.Rotate = Dock.Right;
			}
			else if (position > 20 && position <= 30)
			{
				coord.X = 30 - position;
				coord.Y = 10;
				coord.Rotate = Dock.Bottom;
			}
			else if (position > 30 && position < 40)
			{
				coord.X = 0;
				coord.Y = 40 - position;
				coord.Rotate = Dock.Left;
			}
			else
			{
				coord.X = 10;
				coord.Y = 0;
			}

			return coord;
		}
	}

	public struct TransferCoord
	{
		public int X { get; set; }
		public int Y { get; set; }
		public Dock Rotate { get; set; }
	}
}
