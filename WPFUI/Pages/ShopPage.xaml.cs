using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
using WPFUI.UserControls;

namespace WPFUI.Pages
{
	/// <summary>
	/// Interaction logic for ShopPage.xaml
	/// </summary>
	public partial class ShopPage : Page
	{
		private ObservableCollection<ShopItem> items = new ObservableCollection<ShopItem>();

		public IEnumerable<ShopItem> Items => items;

		public ShopPage()
		{
			InitializeComponent();

			items.Add(new ShopItem());
			items.Add(new ShopItem());
			items.Add(new ShopItem());
			items.Add(new ShopItem());
			items.Add(new ShopItem());
			items.Add(new ShopItem());
			items.Add(new ShopItem());
			items.Add(new ShopItem());
		}
	}
}
