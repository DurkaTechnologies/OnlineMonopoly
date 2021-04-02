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
		private LinearGradientBrush currentBackground;
		private LinearGradientBrush userBackground;
		private LinearGradientBrush defBackground;
		private SolidColorBrush timerBackground;

		// User information

		private bool currentUser;
		private string userName;
		private int userMoney;
		private int userTime;

		#endregion

		public ShortInfo()
		{
			InitializeComponent();
			InitializePropertyChanged();

			defBackground = ColorGradientGenerator.GenerateGradient(Color.FromArgb(255, 15, 2, 25), -45);
			CurrentUser = false;
		}

		#region Methods
		private void UpdateCurrentUser()
		{
			if (CurrentUser)
				CurrentBackground = UserBackground;
			else
				CurrentBackground = defBackground;
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
					UserBackground = ColorGradientGenerator.GenerateGradient(PlayerColor.Color, -45);
					TimerBackground = ColorGradientGenerator.GenerateDarkerColor(PlayerColor.Color);
				}
			};
		}
		#endregion

		#region Properties

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

		public LinearGradientBrush CurrentBackground
		{

			get => currentBackground;
			set
			{
				currentBackground = value;
				OnPropertyChanged();
			}
		}

		public Visibility ShowTimer => CurrentUser ? Visibility.Visible : Visibility.Hidden;

		#endregion

		#region INotify
		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
		#endregion
	}
}
