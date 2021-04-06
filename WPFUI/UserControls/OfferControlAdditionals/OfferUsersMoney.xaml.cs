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
		private int userMoney;
		private bool vis;

		public int UserMoney 
		{
			get => userMoney;
			set 
			{
				userMoney = value;
				OnPropertyChanged();
			}
		}

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

		public OfferUsersMoney()
		{
			InitializeComponent();
			Vis = false;
		}

		private void RoundButton_Click(object sender, RoutedEventArgs e)
		{
			Vis = true;
		}

		private void CustomTextBox_MouseLeave(object sender, MouseEventArgs e)
		{
			Vis = false;
		}

		#region INotify

		private void InitializePropertyChanged()
		{
			PropertyChanged += (sender, args) =>
			{
				//if (args.PropertyName.Equals(nameof(ImageSource)))
				//{
				//	try
				//	{
				//		Image = new BitmapImage(new Uri(ImageSource));
				//	}
				//	catch (Exception e)
				//	{
				//		Console.WriteLine(e.Message);
				//	}
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
