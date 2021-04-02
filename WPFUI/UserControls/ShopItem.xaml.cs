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
	/// Interaction logic for ShopItem.xaml
	/// </summary>
	public partial class ShopItem : UserControl, INotifyPropertyChanged
	{

		#region Fields

		private string title;
		private int price;
		private ImageSource image;
		private string imageSource;

		#endregion

		public ShopItem()
		{
			InitializeComponent();
			InitializePropertyChanged();

			Title = "Valentuna's Dices";
			Price = 666;
			ImageSource = "https://png.pngtree.com/png-vector/20201112/ourmid/pngtree-two-realistic-white-dices-vector-png-image_2423974.jpg";
		}

		#region Proporties

		public string Title 
		{
			get => title;
			set 
			{
				title = value;
				OnPropertyChanged();
			}
		}

		public int Price 
		{
			get => price;
			set 
			{
				price = value;
				OnPropertyChanged();
			}
		}

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
