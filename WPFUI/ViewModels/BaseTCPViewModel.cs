using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFUI.ViewModels
{
	class BaseTCPViewModel : UIWPF.ViewModels.BaseViewModel
	{
		protected string serverIp;
		protected int serverPort;
		protected TcpClient client;

		protected void ParseConfig()
		{
			IPAddress ipAddress = null;
			try
			{
				serverIp = ConfigurationManager.AppSettings["ServerIP"];
				string port = ConfigurationManager.AppSettings["ServerPort"];

				if (!IPAddress.TryParse(serverIp, out ipAddress))
					ShowError("Config Error", "Incorrect server ip adress confing");
				else if (!int.TryParse(port, out serverPort))
					ShowError("Config Error", "Incorrect server port confing");
			}
			catch (Exception)
			{
				ShowError("Config Error", "Configuration Error");
			}
		}

		protected void ConnectClient()
		{
			try
			{
				client = new TcpClient(new IPEndPoint(0, 0));
				client.Connect(serverIp, serverPort);
			}
			catch (Exception)
			{
				ShowError("Connection Error", "Connection Error");
				Application.Current.Shutdown();
			}
		}

		protected void ShowError(string caption, string message)
		{
			MessageBox.Show(message, caption,
						MessageBoxButton.OK, MessageBoxImage.Error);
			Application.Current.Shutdown();
		}
	}
}
