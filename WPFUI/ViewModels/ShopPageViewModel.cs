using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UIWPF.Commands;
using UIWPF.ViewModels;
using WPFUI.UserControls;

namespace WPFUI.ViewModels
{
	class ShopPageViewModel : BaseViewModel
	{
		#region Fields

		private ObservableCollection<ShopItem> items;


		private ICommand openGeneralCommmand;

		#endregion

		public ShopPageViewModel()
		{
			InitializePropertyChanged();

			openGeneralCommmand = new DelegateCommand(OpenGeneralPage, () => true);
			items = new ObservableCollection<ShopItem>();

			for (int i = 0; i < 10; i++)
				items.Add(new ShopItem());
		}

		private void OpenGeneralPage()
		{
			Navigation.Navigation.Navigate(Navigation.Navigation.GeneralPageAlias, new GeneralPageViewModel());
		}

		#region Proporties

		public IEnumerable<ShopItem> Items => items;

		public ICommand OpenGeneralCommand => openGeneralCommmand;

		#endregion

		#region Notify

		private void InitializePropertyChanged()
		{
			PropertyChanged += (sender, args) =>
			{

			};
		}

		#endregion
	}
}
