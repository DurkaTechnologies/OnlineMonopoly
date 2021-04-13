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
using WPFUI.Views;

namespace WPFUI.UserControls
{
	/// <summary>
	/// Interaction logic for ShortInfo.xaml
	/// </summary>
	public partial class ShortInfo : UserControl, INotifyPropertyChanged
	{
		#region Fields
		// Color logic

		private SolidColorBrush playerColor;
		private Brush currentBackground;
		private LinearGradientBrush userBackground;
		private SolidColorBrush defBackground;
		private SolidColorBrush timerBackground;
		private ImageSource image;
		private string imageSource;

		// Size logic

		private double imageSize;

		// User information

		private UserDTO user;
		private bool currentUser;
		private string userName;
		private int userMoney;
		private int userTime;

		#endregion

		public ShortInfo()
		{
			InitializeComponent();
			InitializePropertyChanged();
			CurrentUser = false;
		}

		public ShortInfo(UserDTO user)
		{
			InitializeComponent();
			InitializePropertyChanged();
			CurrentUser = false;
			User = user;
		}

		#region Properties

		public UserDTO User
		{
			get => user;
			set
			{
				user = value;
				OnPropertyChanged();
			}
		}

		public bool CurrentUser
		{
			get => currentUser;
			set
			{
				currentUser = value;
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

		public int UserMoney
		{
			get => userMoney;
			set
			{
				userMoney = value;
				OnPropertyChanged();
			}
		}

		public int UserTime
		{
			get => userTime;
			set
			{
				userTime = value;
				OnPropertyChanged();
			}
		}

		// Color Properties

		public SolidColorBrush PlayerColor
		{
			get => playerColor;
			set
			{
				playerColor = value;
				OnPropertyChanged();
			}
		}

		public LinearGradientBrush UserBackground
		{
			get => userBackground;
			set
			{
				userBackground = value;
				OnPropertyChanged();
			}
		}

		public SolidColorBrush TimerBackground
		{
			get => timerBackground;
			set
			{
				timerBackground = value;
				OnPropertyChanged();
			}
		}

		public Brush CurrentBackground
		{
			get => currentBackground;
			set
			{
				currentBackground = value;
				OnPropertyChanged();
			}
		}

		public SolidColorBrush DefBackground
		{
			get => defBackground;
			set
			{
				defBackground = value;
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

		public Visibility ShowTimer => CurrentUser ? Visibility.Visible : Visibility.Hidden;

		// Size Properties

		public double ImageSize
		{
			get => imageSize;
			set
			{
				imageSize = value;
				OnPropertyChanged();
			}
		}

		#endregion

		#region Methods

		private void UpdateCurrentUser()
		{
			if (CurrentUser)
				CurrentBackground = UserBackground;
			else
				CurrentBackground = DefBackground;
		}

		private void InitializePropertyChanged()
		{
			PropertyChanged += (sender, args) =>
			{
				if (args.PropertyName.Equals(nameof(CurrentUser)))
				{
					UpdateCurrentUser();
					OnPropertyChanged(nameof(ShowTimer));
				}
				if (args.PropertyName.Equals(nameof(PlayerColor)))
				{
					UserBackground = ColorManager.GenerateGradient(PlayerColor.Color, -45);
					TimerBackground = ColorManager.GenerateDarkerColor(PlayerColor.Color);
				}
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

		#endregion

		#region INotify

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		#endregion

		#region Events

		private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			CurrentUser = !CurrentUser;
		}

		private void imageBlock_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			ImageSize = (sender as Border).ActualHeight;
		}

		#endregion
	}
}

