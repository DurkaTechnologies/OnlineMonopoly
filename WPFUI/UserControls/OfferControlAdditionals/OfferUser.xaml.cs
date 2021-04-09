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
using WPFUI.Views;

namespace WPFUI.UserControls.OfferControlAdditionals
{
	/// <summary>
	/// Interaction logic for OfferUser.xaml
	/// </summary>
	public partial class OfferUser : UserControl, INotifyPropertyChanged
	{
		#region Fields

		private bool userType;
		private string userName;
		private string imageSource;
		private ImageSource image;

		#endregion

		#region Proporties

		public bool UserType
		{
			get => userType;
			set 
			{
				userType = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(UserTypeText));
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

		public string UserTypeText => (UserType)? "Proposes" : "Gives";

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

		#endregion

		public OfferUser()
		{
			InitializeComponent();
			InitializePropertyChanged();
		}

		#region INotify

		private void InitializePropertyChanged()
		{
			PropertyChanged += (sender, args) =>
			{
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
