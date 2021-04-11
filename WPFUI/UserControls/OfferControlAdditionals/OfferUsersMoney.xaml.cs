using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFUI.UserControls.OfferControlAdditionals
{
	/// <summary>
	/// Interaction logic for OfferUsersMoney.xaml
	/// </summary>
	public partial class OfferUsersMoney : UserControl, INotifyPropertyChanged
	{
		#region Fields

		public static readonly DependencyProperty UserMoneyProperty ;
		private int userMoney;
		private bool vis;

		#endregion

		#region Proporties

		public bool Vis
		{
			get => vis;
			set
			{
				vis = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(Hidden));
			}
		}

		public string Hidden => (Vis) ? "Visible" : "Hidden";

		public string UserMoney
		{
			get => (string)GetValue(UserMoneyProperty);
			set
			{
				if (CheckInt(value))
				{
					SetValue(UserMoneyProperty, value);
					OnPropertyChanged();
				}
			}
		}

		#endregion

		static OfferUsersMoney()
		{
			UserMoneyProperty = DependencyProperty.Register("UserMoney", typeof(string), typeof(OfferUsersMoney),
					new FrameworkPropertyMetadata("0"));
		}

		public OfferUsersMoney()
		{
			InitializeComponent();
			InitializePropertyChanged();

			LayoutRoot.DataContext = this;
			Vis = false;
		}

		#region Methods

		private void RoundButton_Click(object sender, RoutedEventArgs e)
		{
			Vis = true;
		}

		private void CustomTextBox_MouseLeave(object sender, MouseEventArgs e)
		{
			Vis = false;
		}

		#endregion

		#region INotify

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		private void InitializePropertyChanged()
		{
			PropertyChanged += (sender, args) =>
			{
			};
		}

		bool CheckInt(string value)
		{
			if (value.Length > 5)
				return false;

				int temp;
			return int.TryParse(value, out temp);
		}

		#endregion
	}
}
