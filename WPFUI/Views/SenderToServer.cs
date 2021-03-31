using CommandsClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI.Views
{
	public class SenderToServer
	{
		private int userId;

		public SenderToServer()
		{
		}

		public SenderToServer(int userId, TcpClient client)
		{
			this.userId = userId;
			Client = client;
		}

		private Task<ServerGameCommand> ReceiveCommand()
		{
			return Task.Run(() =>
			{
				BinaryFormatter formatter = new BinaryFormatter();
				return (ServerGameCommand)formatter.Deserialize(Client.GetStream());
			});
		}

		private Task SendCommand(IClientCommand command)
		{
			return Task.Run(() =>
			{
				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize(Client.GetStream(), command);
			});
		}

		public TcpClient Client { get; set; }
	}
}
