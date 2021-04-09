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

		Grid grid;

		public void AddBranch(BranchControl control, int position)
		{
			TransferCoord coord = GamePageTransfer.GetGameCoord(position);

			grid.Children.Add(control);
			Grid.SetColumn(control, coord.X);
			Grid.SetRow(control, coord.Y);

			control.Rotate = coord.Rotate;
		}

		#endregion

		public GamePageViewModel(IGridSuppliear grid)
		{
			this.grid = grid.GetGrid();

			users = new ObservableCollection<ShortInfo>();

			AddBranch(new BranchControl() { PriceColor = ColorManager.GetSecondBrushFromLib(0), ImageSource = "http://durkaftpserver.cf/Resources/Study/water.png"}, 1);
			AddBranch(new BranchControl() { PriceColor = ColorManager.GetSecondBrushFromLib(0), ImageSource = "http://durkaftpserver.cf/Resources/Study/gym.png" }, 3);
			AddBranch(new BranchControl() { PriceColor = ColorManager.GetSecondBrushFromLib(1), ImageSource = "http://durkaftpserver.cf/Resources/Cars/bmw.png" }, 5);
			AddBranch(new BranchControl() { PriceColor = ColorManager.GetSecondBrushFromLib(2), ImageSource = "http://durkaftpserver.cf/Resources/Clothes/econom.png" }, 6);
			AddBranch(new BranchControl() { PriceColor = ColorManager.GetSecondBrushFromLib(2), ImageSource = "http://durkaftpserver.cf/Resources/Clothes/abibas.png" }, 8);
			AddBranch(new BranchControl() { PriceColor = ColorManager.GetSecondBrushFromLib(2), ImageSource = "http://durkaftpserver.cf/Resources/Clothes/waikiki.png" }, 9);

			AddBranch(new BranchControl() { PriceColor = ColorManager.GetSecondBrushFromLib(3), ImageSource = "http://durkaftpserver.cf/Resources/IT/step.png" }, 11);
			AddBranch(new BranchControl() { PriceColor = ColorManager.GetSecondBrushFromLib(4), ImageSource = "http://durkaftpserver.cf/Resources/Studios/shiza.png" }, 12);
			AddBranch(new BranchControl() { PriceColor = ColorManager.GetSecondBrushFromLib(3), ImageSource = "http://durkaftpserver.cf/Resources/IT/softserve.png" }, 13);
			AddBranch(new BranchControl() { PriceColor = ColorManager.GetSecondBrushFromLib(3), ImageSource = "http://durkaftpserver.cf/Resources/IT/softgroup.png" }, 14);
			AddBranch(new BranchControl() { PriceColor = ColorManager.GetSecondBrushFromLib(1), ImageSource = "http://durkaftpserver.cf/Resources/Cars/bogdan.png" }, 15);
			AddBranch(new BranchControl() { PriceColor = ColorManager.GetSecondBrushFromLib(5), ImageSource = "http://durkaftpserver.cf/Resources/Drinks/pepsi.png" }, 16);
			AddBranch(new BranchControl() { PriceColor = ColorManager.GetSecondBrushFromLib(5), ImageSource = "http://durkaftpserver.cf/Resources/Drinks/riven.png" }, 18);
			AddBranch(new BranchControl() { PriceColor = ColorManager.GetSecondBrushFromLib(5), ImageSource = "http://durkaftpserver.cf/Resources/Drinks/kaluna.png" }, 19);

			AddBranch(new BranchControl() { PriceColor = ColorManager.GetSecondBrushFromLib(6), ImageSource = "http://durkaftpserver.cf/Resources/Builders/renome.png" }, 21);
			AddBranch(new BranchControl() { PriceColor = ColorManager.GetSecondBrushFromLib(6), ImageSource = "http://durkaftpserver.cf/Resources/Builders/stograd.png" }, 23);
			AddBranch(new BranchControl() { PriceColor = ColorManager.GetSecondBrushFromLib(6), ImageSource = "http://durkaftpserver.cf/Resources/Builders/smartgroup.png" }, 24);
			AddBranch(new BranchControl() { PriceColor = ColorManager.GetSecondBrushFromLib(1), ImageSource = "http://durkaftpserver.cf/Resources/Cars/Skoda.png" }, 25);
			AddBranch(new BranchControl() { PriceColor = ColorManager.GetSecondBrushFromLib(7), ImageSource = "http://durkaftpserver.cf/Resources/Eat/mrcat.png" }, 26);
			AddBranch(new BranchControl() { PriceColor = ColorManager.GetSecondBrushFromLib(7), ImageSource = "http://durkaftpserver.cf/Resources/Eat/matsuri.png" }, 27);
			AddBranch(new BranchControl() { PriceColor = ColorManager.GetSecondBrushFromLib(4), ImageSource = "http://durkaftpserver.cf/Resources/Studios/zagrava.png" }, 28);
			AddBranch(new BranchControl() { PriceColor = ColorManager.GetSecondBrushFromLib(7), ImageSource = "http://durkaftpserver.cf/Resources/Eat/father.png" }, 29);

			AddBranch(new BranchControl() { PriceColor = ColorManager.GetSecondBrushFromLib(8), ImageSource = "http://durkaftpserver.cf/Resources/ChainStores/silpo.png" }, 31);
			AddBranch(new BranchControl() { PriceColor = ColorManager.GetSecondBrushFromLib(8), ImageSource = "http://durkaftpserver.cf/Resources/ChainStores/atb.png" }, 32);
			AddBranch(new BranchControl() { PriceColor = ColorManager.GetSecondBrushFromLib(8), ImageSource = "http://durkaftpserver.cf/Resources/ChainStores/sim.png" }, 34);
			AddBranch(new BranchControl() { PriceColor = ColorManager.GetSecondBrushFromLib(1), ImageSource = "http://durkaftpserver.cf/Resources/Cars/tata.png" }, 35);
			AddBranch(new BranchControl() { PriceColor = ColorManager.GetSecondBrushFromLib(9), ImageSource = "http://durkaftpserver.cf/Resources/Delivers/glovo.png" }, 37);
			AddBranch(new BranchControl() { PriceColor = ColorManager.GetSecondBrushFromLib(9), ImageSource = "http://durkaftpserver.cf/Resources/Delivers/oregano.png" }, 39);

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
