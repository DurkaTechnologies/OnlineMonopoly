using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WPFUI.UserControls
{
	/// <summary>
	/// Interaction logic for OfferControl.xaml
	/// </summary>
	public partial class OfferControl : UserControl, INotifyPropertyChanged
	{

		public int LeftUserMoney
		{
			get => leftUserMoney;
			set
			{
				leftUserMoney = value;
				OnPropertyChanged();
			}
		}

		#region Fields

		private ICollection<UserElement> leftUserElements = new ObservableCollection<UserElement>();
		private ICollection<UserElement> rightUserElements = new ObservableCollection<UserElement>();

		private int leftUserMoney;
		private int rightUserMoney;

		private int allRightMoney;
		private int allLeftMoney;

		#endregion


		public OfferControl()
		{
			InitializeComponent();
			InitializePropertyChanged();

			LayoutRoot.DataContext = this;
			AddLeftUserElement(new UserElement("SoftServe", 666, "pack://application:,,,/Resources/IT/softserve.png"));
			AddLeftUserElement(new UserElement("Step", 666, "pack://application:,,,/Resources/IT/step.png"));

			AddRightUserElement(new UserElement("Riven", 666, "pack://application:,,,/Resources/Drinks/riven.png"));
		}

		#region Methods

		public void AddLeftUserElement(UserElement element) 
		{
			leftUserElements.Add(element);
			UpdateAllLeftMoney();
		}

		public void AddRightUserElement(UserElement element)
		{
			rightUserElements.Add(element);
			UpdateAllRightMoney();
		}

		#endregion

		#region Proporties

		public IEnumerable<UserElement> LeftUserElements => leftUserElements;
		public IEnumerable<UserElement> RightUserElements => rightUserElements;


		public int RightUserMoney
		{
			get => rightUserMoney;
			set
			{
				rightUserMoney = value;
				OnPropertyChanged();
			}
		}


		public int AllLeftMoney
		{
			get => allLeftMoney;
			set
			{
				allLeftMoney = value;
				OnPropertyChanged();
			}
		}

		public int AllRightMoney
		{
			get => allRightMoney;
			set
			{
				allRightMoney = value;
				OnPropertyChanged();
			}
		}

		#endregion

		#region INotify

		private void InitializePropertyChanged()
		{
			PropertyChanged += (sender, args) =>
			{
				if (args.PropertyName.Equals(nameof(LeftUserMoney)))
					UpdateAllLeftMoney();
				
				if (args.PropertyName.Equals(nameof(RightUserMoney)))
					UpdateAllRightMoney();
			};
		}

		private void UpdateAllLeftMoney()
		{
			AllLeftMoney = LeftUserMoney;

			foreach (var item in leftUserElements)
				AllLeftMoney += item.Price;
		}

		private void UpdateAllRightMoney() 
		{
			AllRightMoney = RightUserMoney;

			foreach (var item in rightUserElements)
				AllRightMoney += item.Price;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		#endregion
	}

	public class UserElement : INotifyPropertyChanged
	{
		#region Fields

		private string title;
		private int price;
		private ImageSource image;
		private string imageSource;

		#endregion

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
				OnPropertyChanged(nameof(Image));
			}
		}

		#endregion

		public UserElement()
		{
			InitializePropertyChanged();
		}

		public UserElement(string title, int price, string image)
		{
			InitializePropertyChanged();


			Title = title;
			Price = price;
			ImageSource = image;
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

