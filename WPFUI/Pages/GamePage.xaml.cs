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
	public partial class GamePage : Page
	{
		Random rand;

		public GamePage()
		{
			InitializeComponent();
			rand = new Random();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			dice.FirstValue = rand.Next(0, 6);
			dice.SecondValue = rand.Next(0, 6);
		}
	}
}