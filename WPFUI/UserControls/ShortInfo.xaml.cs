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
		public ShortInfo()
		{
			InitializeComponent();
			InitializePropertyChanged();

			defBackground = ColorGradientGenerator.GenerateGradient(Color.FromArgb(255, 15, 2, 25), -45);
			CurrentUser = false;
		}

		/*Color Logic*/

		private SolidColorBrush playerColor;
		private LinearGradientBrush currentBackground;
		private LinearGradientBrush userBackground;
		private LinearGradientBrush defBackground;
		private SolidColorBrush timerBackground;
		private bool currentUser;

		public bool CurrentUser
		{
			get => currentUser;
			set
			{
				currentUser = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(ShowTimer));
			}
		}

		public string ShowTimer
		{
			get 
			{
				if (CurrentUser)
					return "Visible";
				else
					return "Hidden";
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

		private void UpdateCurrentUser()
		{
			if (CurrentUser)
				CurrentBackground = UserBackground;
			else
				CurrentBackground = defBackground;
		}

		public SolidColorBrush PlayerColor
		{
			get => playerColor;
			set
			{
				playerColor = value;
				OnPropertyChanged();

				UserBackground = ColorGradientGenerator.GenerateGradient(value.Color, -45);
				TimerBackground = ColorGradientGenerator.GenerateDarkerColor(value.Color);
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

		public object Value { get; set; }

		private void InitializePropertyChanged()
		{
			PropertyChanged += (sender, args) =>
			{
				if (args.PropertyName.Equals(nameof(CurrentUser)))
				{
					UpdateCurrentUser();
				}
			};
		}

		/*USER INFORMATION*/

		private string userName;
		private int userMoney;
		private int userTime;

		/*USER INF PROPORTIES*/

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

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}
}
