using BLL.DTO;
using System;
using System.Collections.Generic;
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
	/// Interaction logic for PlayerIcon.xaml
	/// </summary>
	public partial class PlayerIcon : UserControl, INotifyPropertyChanged
	{
		#region Fields

		private UserDTO user;
		private string userName;
		private ImageSource image;
		private string imageSource;

		#endregion

		#region Proporties

		public UserDTO User
		{
			get => user;
			set
			{
				user = value;
				OnPropertyChanged();
			}
		}

		public string UserName 
		{
			get => userName;
			set 
			{
				userName = value;
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

		public string ImageSource 
		{
			get => imageSource;
			set 
			{
				imageSource = value;
				OnPropertyChanged();
			}
		}

		#endregion

		public PlayerIcon()
		{
			InitializeComponent();
			InitializePropertyChanged();

			ImageSource = "https://d1nhio0ox7pgb.cloudfront.net/_img/o_collection_png/green_dark_grey/256x256/plain/user.png";
			UserName = "Unknown";
		}

		public PlayerIcon(UserDTO user)
		{
			InitializeComponent();
			InitializePropertyChanged();
		}

		public PlayerIcon(string UserName, string ImageSource) 
		{
			InitializeComponent();
			InitializePropertyChanged();

			this.UserName = UserName;
			this.ImageSource = ImageSource;
		}

		#region INotify

		private void InitializePropertyChanged()
		{
			PropertyChanged += (sender, args) =>
			{
				if (args.PropertyName.Equals(nameof(ImageSource)))
				{
					try
					{
						Image = new BitmapImage(new Uri(ImageSource));
					}
					catch (Exception e)
					{
						Console.WriteLine(e.Message);
					}
				}

				if (args.PropertyName.Equals(nameof(User))) 
				{
					UserName = User.Login;
					ImageSource = User.Image;
				}
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
