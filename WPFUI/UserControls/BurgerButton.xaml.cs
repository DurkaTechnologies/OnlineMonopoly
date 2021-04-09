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
	/// Interaction logic for BurgerButton.xaml
	/// </summary>
	public partial class BurgerButton : UserControl, INotifyPropertyChanged
	{
		#region Fields

		private bool activated;
		private double topAngle;
		private double downAngle;

		#endregion

		public BurgerButton()
		{
			InitializeComponent();
			InitializePropertyChanged();
		}

		#region Proporties

		public double DownAngle
		{
			get => downAngle;
			set
			{
				downAngle = value;
				OnPropertyChanged();
			}
		}

		public double TopAngle
		{
			get => topAngle;
			set
			{
				topAngle = value;
				OnPropertyChanged();
			}
		}

		public bool Activated
		{
			get => activated;
			set
			{
				activated = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(MiddleVisible));
			}
		}

		public string MiddleVisible => (Activated) ? "Hidden" : "Visible"; 

		#endregion

		#region Notify

		private void InitializePropertyChanged()
		{
			PropertyChanged += (sender, args) =>
			{
				if (args.PropertyName.Equals(nameof(Activated)))
				{
					if (Activated)
					{
						TopAngle = 45.0;
						DownAngle = -45.0;
					}
					else 
					{
						TopAngle = 0.0;
						DownAngle = 0.0;
					}
				}
			};
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		#endregion

		#region Methods

		private void BurgerClick(object sender, MouseButtonEventArgs e)
		{
			Activated = !activated;
		}

		private void RoundButton_Click(object sender, RoutedEventArgs e)
		{
			Activated = !activated;
		}

		#endregion
	}
}
