using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WPFUI.ViewModels;

namespace WPFUI.Pages
{
	/// <summary>
	/// Interaction logic for GamePage.xaml
	/// </summary>
	public partial class GamePage : Page, IGridSuppliear
	{
		public GamePage()
		{
			InitializeComponent();
		}

		public Grid GetGrid()
		{
			return GeneralGrid;
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			DataContext = new GamePageViewModel(this);
		}
	}

	public interface IGridSuppliear 
	{
		public Grid GetGrid();
	}
}