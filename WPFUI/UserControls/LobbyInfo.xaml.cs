using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFUI.UserControls
{
	/// <summary>
	/// Interaction logic for LobbyInfo.xaml
	/// </summary>
	public partial class LobbyInfo : UserControl, INotifyPropertyChanged
	{
		#region Fields

		private ObservableCollection<PlayerIcon> users = new ObservableCollection<PlayerIcon>();
		private int maxUsers;
		private int currentUsers;
		private ImageSource image;

		#endregion

		#region Proporties

		public IEnumerable<PlayerIcon> Users => users;
		
		public int CurrentUsers 
		{
			get => currentUsers;
			set 
			{
				currentUsers = value;
				OnPropertyChanged();
			}
		}
		
		public int MaxUsers 
		{
			get => maxUsers;
			set 
			{
				maxUsers = value;
				OnPropertyChanged();
			}
		}

		public ImageSource Image 
		{
			get => image;
			set 
			{
				image = value;
				OnPropertyChanged();
			}
		}

		#endregion

		public LobbyInfo()
		{
			InitializeComponent();

			/*Test Code*/

			MaxUsers = 4;

			AddUser(new PlayerIcon("Valntuna", "https://cdn.discordapp.com/attachments/821379755743903764/827188066694463529/unknown.png"));
			AddUser(new PlayerIcon("Vlad", "https://cdn.discordapp.com/attachments/821379755743903764/827188443335098398/unknown.png"));
			AddUser(new PlayerIcon("Sobaka Vlada", "https://cdn.discordapp.com/attachments/821379755743903764/827188612172218388/unknown.png"));
			AddUser(new PlayerIcon("kot Vlada", "https://cdn.discordapp.com/attachments/689145103582888033/827213236600176701/unknown.png"));
		}

		#region Methods

		public void AddUser(PlayerIcon player) 
		{
			if (CurrentUsers < MaxUsers)
			{
				users.Add(player);
				CurrentUsers++;
			}
		}

		#endregion

		#region INotify

		private void InitializePropertyChanged()
		{
			PropertyChanged += (sender, args) =>
			{
				//if (args.PropertyName.Equals(nameof()))
				//{
					
				//}
			};
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		#endregion
	}
}
