using CommandsClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using UIWPF.ViewModels;
using WPFUI.Views;

namespace WPFUI.ViewModels
{
	class GamePageViewModel : BaseTCPViewModel
	{
		private SenderToServer sender;

		public GamePageViewModel()
		{
			InitializeTCP();

			sender = new SenderToServer();
			InitializeCommands();
			InitializePropertyChanged();
		}

		private void InitializeTCP()
		{
			ParseConfig();
			ConnectClient();
		}

		private void InitializeCommands()
		{
		}

		private void InitializePropertyChanged()
		{
			PropertyChanged += (sender, args) =>
			{

			};
		}

		
	}
}
