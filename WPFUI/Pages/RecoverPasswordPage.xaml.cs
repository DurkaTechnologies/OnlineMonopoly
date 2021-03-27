using System;
using System.Collections.Generic;
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
using WPFUI.ViewModels;
namespace WPFUI.Pages
{
    /// <summary>
    /// Interaction logic for RecoverPasswordPage.xaml
    /// </summary>
    public partial class RecoverPasswordPage : Page
    {
        public RecoverPasswordPage()
        {
            InitializeComponent();

        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Navigation.Navigation.Service = NavigationService.GetNavigationService(this);
            this.DataContext = new RecoverViewModel();
        }

    }
}
