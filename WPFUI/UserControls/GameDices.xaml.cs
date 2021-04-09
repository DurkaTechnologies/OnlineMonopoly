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
		static GameDices()
		{
			FirstValueProperty = DependencyProperty.Register("FirstValue", typeof(int), typeof(GameDices),
					new FrameworkPropertyMetadata(0));
			SecondValueProperty = DependencyProperty.Register("SecondValue", typeof(int), typeof(GameDices),
					new FrameworkPropertyMetadata(0));
		}

		public GameDices()
		{
			InitializeComponent();
			InitializePropertyChanged();
		}

		#region Proporties

		public static DependencyProperty FirstValueProperty;
		public static DependencyProperty SecondValueProperty;

		public int FirstValue
		{
			get => (int)GetValue(FirstValueProperty);
			set
			{
				SetValue(FirstValueProperty, value);
				first.DiceValue = value;
				OnPropertyChanged();
			}
		}

		public int SecondValue
		{
			get => (int)GetValue(SecondValueProperty);
			set
			{
				SetValue(SecondValueProperty, value);
				second.DiceValue = value;
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
