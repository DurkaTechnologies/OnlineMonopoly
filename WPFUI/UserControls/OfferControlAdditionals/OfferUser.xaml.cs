using BLL.DTO;
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

		public static readonly DependencyProperty UserProperty;
		private bool userType;

		#endregion

		static OfferUser()
		{
			UserProperty = DependencyProperty.Register("User", typeof(UserDTO),
				typeof(OfferUser), new PropertyMetadata(null));
		}

		public OfferUser()
		{
			InitializeComponent();
			LayoutRoot.DataContext = this;
		}

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

		public int User
		{
			get { return (int)GetValue(UserProperty); }
			set { SetValue(UserProperty, value); }
		}

		public string UserTypeText => (UserType)? "Proposes" : "Gives";

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
