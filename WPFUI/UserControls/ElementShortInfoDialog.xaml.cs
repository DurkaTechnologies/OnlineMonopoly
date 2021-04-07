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
using BLL.DTO;
namespace WPFUI.UserControls
{
    /// <summary>
    /// Логика взаимодействия для SettingsControl.xaml
    /// </summary>
    public partial class ElementShortInfoDialog : UserControl , INotifyPropertyChanged
    {
       
        public ElementShortInfoDialog()
        {
            
            InitializeComponent();
            BranchDTO branch = new BranchDTO();

            branch.Name = "Sobaka";
            branch.Price = 300;
            branch.Pledge = 100;
            branch.Upgrade = 250;
            FName = branch.Name;
            Price = branch.Price;
            Pledge = branch.Pledge;
            Upgrade = branch.Upgrade;

        }
        private string name;

        public string FName
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        private int price;
        public int Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged();
            }
        }
        private int pledge;
        public int Pledge
        {
            get { return pledge; }
            set
            {
                pledge = value;
                OnPropertyChanged();
            }
        }
        private int upgrade;
        public int Upgrade
        {
            get { return upgrade; }
            set
            {
                upgrade = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
