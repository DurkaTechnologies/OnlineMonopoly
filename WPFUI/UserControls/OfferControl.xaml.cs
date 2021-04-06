using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WPFUI.UserControls
{
	/// <summary>
	/// Interaction logic for OfferControl.xaml
	/// </summary>
	public partial class OfferControl : UserControl, INotifyPropertyChanged
	{
		private ICollection<object> userElements = new ObservableCollection<object>();

		public IEnumerable<object> UserElements => userElements;

		public OfferControl()
		{
			InitializeComponent();


			for (int i = 0; i < 5; i++)
			{
				userElements.Add(new object());
			}
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
