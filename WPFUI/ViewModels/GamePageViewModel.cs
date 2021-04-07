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

namespace WPFUI.ViewModels
{
	class GamePageViewModel : BaseViewModel
	{
		private string messageText;
		private Command sendCommand;
		string userName = new Random().Next(0,100).ToString();
		GrpcChannel channel;
		ChatRoom.ChatRoomClient client;
		AsyncDuplexStreamingCall<Message, Message> chat;
		ObservableCollection<string> messages;

		bool p = true;


		public GamePageViewModel()
		{
			InitializeCommands();
			InitializePropertyChanged();


			channel = GrpcChannel.ForAddress("https://localhost:5001");
			client = new ChatRoom.ChatRoomClient(channel);
			chat = client.join();

			MessageText = "";
			Messages = new ObservableCollection<string>();
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

						await Start();
					}

					sendCommand.RaiseCanExecuteChanged();
				}
			};
		}

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
	}
}
