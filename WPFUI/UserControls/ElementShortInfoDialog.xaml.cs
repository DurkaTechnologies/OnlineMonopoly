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
    public partial class ElementShortInfoDialog : UserControl, INotifyPropertyChanged
    {

        public ElementShortInfoDialog()
        {
            InitializeComponent();
            BranchDTO branch = new BranchDTO();
            branch.Name = "Sobaka";
            branch.Price = 300;
            branch.Pledge = 100;
            branch.Upgrade = 250;
            branch.Buyout = 200;
            FName = branch.Name;
            Price = branch.Price;
            Pledge = branch.Pledge;
            Upgrade = branch.Upgrade;
            Buyout = branch.Buyout;
            FillBox();

        }
        private string name;
        private Data data = new Data();
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

        private int buyout;
        public int Buyout
        {
            get { return buyout; }
            set
            {
                buyout = value;
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
        private int firstValue;
        public int FirstValue
        {
            get { return firstValue; }
            set
            {
                firstValue = value;
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

        private void FillBox()
        {
            for (int i = 0; i < 6; i++)
            {
                data.Quantity = i;
                if (i == 5)
                    data.Brush = new SolidColorBrush(Color.FromRgb(244, 252, 21));
                else
                    data.Brush = new SolidColorBrush(Color.FromRgb(204, 204, 204));
                lBox.Items.Add(data);
                data = new Data();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
    public class Data
    {
        public Data()
        {
            quantity = 0;
            Symb = arr[0];
            FontSize = 10;         
        }

        public string[] arr = { "Base price:", "✯", "✯✯", "✯✯✯", "✯✯✯✯", "✯" };
        public int[] numbs = new int[] { 1, 2, 3, 4, 5, 6 };
        public int Money { get; set; }
        private string symb;
        private int numb;

        private int fontSize;
        private int quantity;
        private SolidColorBrush brush;

        public SolidColorBrush Brush
        {
            get { return brush; }
            set
            {
                brush = value;
            }
        }
        public int Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                if (quantity == 5)
                    FontSize = 15;
                Symb = arr[quantity];
                Numb = numbs[quantity];
            }
        }
        public int FontSize
        {
            get { return fontSize; }
            set
            {
                fontSize = value;
            }
        }
  
        public string Symb
        {
            get { return symb; }
            set { symb = value; }
        }

        public int Numb
        {
            get { return numb; }
            set { numb = value; }
        }
    }
}

