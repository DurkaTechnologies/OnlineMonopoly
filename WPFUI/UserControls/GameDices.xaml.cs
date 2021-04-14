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
	/// Interaction logic for GameDices.xaml
	/// </summary>
	public partial class GameDices : UserControl, INotifyPropertyChanged
	{
		#region Fields

		private int firstValue;
		private int secondValue;

		#endregion

		public GameDices()
		{
			InitializeComponent();
			InitializePropertyChanged();
			
			LayoutRoot.DataContext = this;
		}

		#region Proporties

		public int FirstValue
		{
			get => firstValue;
			set
			{
				firstValue = value;
				OnPropertyChanged();
			}
		}

		public int SecondValue
		{
			get => secondValue;
			set
			{
				secondValue = value;
				OnPropertyChanged();
			}
		}

		#endregion

		#region INotify

		private void InitializePropertyChanged()
		{
			PropertyChanged += (sender, args) =>
			{
				if (args.PropertyName.Equals(nameof(FirstValue)))
				{
					firstDice.DiceValue = FirstValue;
				}

				if (args.PropertyName.Equals(nameof(FirstValue)))
				{
					secondDice.DiceValue = SecondValue;
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
