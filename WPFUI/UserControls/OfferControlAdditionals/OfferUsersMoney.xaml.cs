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

		public static readonly DependencyProperty UserMoneyProperty;
		private string userMoneyStr;
		private Visibility visibility;

		#endregion

		static OfferUsersMoney()
		{
			UserMoneyProperty = DependencyProperty.Register("UserMoney", typeof(int), typeof(OfferUsersMoney),
					new FrameworkPropertyMetadata(0));
		}

		public OfferUsersMoney()
		{
			InitializeComponent();
			InitializePropertyChanged();

			LayoutRoot.DataContext = this;
			UserMoneyStr = "0";

			TextBoxVisibility = Visibility.Hidden;
		}

		#region Proporties
		public Visibility TextBoxVisibility
		{
			get => visibility;
			set
			{
				visibility = value;
				OnPropertyChanged();
			}
		}

		public string UserMoneyStr
		{
			get => userMoneyStr;
			set
			{
				if (CheckInt(value))
				{
					userMoneyStr = value;
					OnPropertyChanged();
				}
			}
		}

		public int UserMoney
		{
			get => (int)GetValue(UserMoneyProperty);
			set
			{
				SetValue(UserMoneyProperty, value);
				OnPropertyChanged();
			}
		}
		#endregion

		#region Methods

		private void RoundButton_Click(object sender, RoutedEventArgs e)
		{
			TextBoxVisibility = Visibility.Visible;
		}

		private void CustomTextBox_MouseLeave(object sender, MouseEventArgs e)
		{
			TextBoxVisibility = Visibility.Hidden;
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
				if (args.PropertyName.Equals(nameof(UserMoneyStr)))
					UserMoney = int.Parse(UserMoneyStr);
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
