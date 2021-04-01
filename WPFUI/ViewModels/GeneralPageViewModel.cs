using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UIWPF.ViewModels;
using WPFUI.UserControls;

namespace WPFUI.ViewModels
{
	class GeneralPageViewModel : BaseViewModel
	{
		#region Fields

		private ObservableCollection<PlayerIcon> friends;
		private ObservableCollection<LobbyInfo> lobbies;

		#endregion

		#region Proporties

		public IEnumerable<PlayerIcon> Friends => friends;
		public IEnumerable<LobbyInfo> Lobbies => lobbies;

		#endregion

		public GeneralPageViewModel()
		{
			friends = new ObservableCollection<PlayerIcon>();
			lobbies = new ObservableCollection<LobbyInfo>();
			InitializePropertyChanged();

			for (int i = 0; i < 5; i++)
			{
				friends.Add(new PlayerIcon("Valntuna", "https://cdn.discordapp.com/attachments/821379755743903764/827188066694463529/unknown.png"));
				friends.Add(new PlayerIcon("Vlad", "https://cdn.discordapp.com/attachments/821379755743903764/827188443335098398/unknown.png"));
				friends.Add(new PlayerIcon("Sobaka Vlada", "https://cdn.discordapp.com/attachments/821379755743903764/827188612172218388/unknown.png"));
				friends.Add(new PlayerIcon("kot Vlada", "https://cdn.discordapp.com/attachments/689145103582888033/827213236600176701/unknown.png"));
			}

			for (int i = 0; i < 5; i++)
				lobbies.Add(new LobbyInfo());
		}

		private void InitializePropertyChanged()
		{
			PropertyChanged += (sender, args) =>
			{

			};
		}
	}
}
