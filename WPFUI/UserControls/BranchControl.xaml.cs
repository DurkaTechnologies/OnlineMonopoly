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

namespace WPFUI.UserControls
{
	/// <summary>
	/// Interaction logic for BranchControl.xaml
	/// </summary>
	public partial class BranchControl : UserControl, INotifyPropertyChanged
	{
		#region Fields

		private BranchDTO branch;
		private int price;

		/*Color Fields*/

		private Brush backColor;

		/*Rotate Fields*/

		private double controlRotate;
		private double imageRotate;
		private double textRotate;
		private Dock rotate;

		/*Image Fields*/

		private string imageSource;
		private ImageSource image;

		#endregion

		#region Proporties

		public BranchDTO Branch 
		{
			get => branch;
			set 
			{
				branch = value;
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

		/*Rotate Properties*/

		public Dock Rotate
		{
			get => rotate;
			set
			{
				rotate = value;
				OnPropertyChanged();
			}
		}

		public double ControlRotate
		{
			get => controlRotate;
			set
			{
				controlRotate = value;
				OnPropertyChanged();
			}
		}

		public double ImageRotate
		{
			get => imageRotate;
			set
			{
				imageRotate = value;
				OnPropertyChanged();
			}
		}

		public double TextRotate
		{
			get => textRotate;
			set
			{
				textRotate = value;
				OnPropertyChanged();
			}
		}

		/*Color Properties*/

		public Brush BackColor
		{
			get => backColor;
			set
			{
				backColor = value;
				OnPropertyChanged();
			}
		}

		/*Image Propeties*/

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

		#region Methods

		#endregion

		public BranchControl()
		{
			InitializeComponent();
			InitializePropertyChanged();

			BackColor = new SolidColorBrush(Color.FromRgb(255, 255, 255));
			Rotate = Dock.Left;
		}
		public BranchControl(BranchDTO branch)
		{
			InitializeComponent();
			InitializePropertyChanged();
			
			BackColor = new SolidColorBrush(Color.FromRgb(255, 255, 255));
			Rotate = Dock.Left;

			Branch = branch;
		}

		#region INotify

		private void InitializePropertyChanged()
		{
			PropertyChanged += (sender, args) =>
			{
				if (args.PropertyName.Equals(nameof(Rotate)))
				{
					switch (Rotate)
					{
						case Dock.Top:
							ControlRotate = 90.0;
							ImageRotate = -180.0;
							TextRotate = -90.0;
							break;
						case Dock.Right:
							ControlRotate = 180.0;
							ImageRotate = -180;
							TextRotate = -90.0;
							break;
						case Dock.Bottom:
							ControlRotate = -90.0;
							ImageRotate = 0.0;
							TextRotate = 90.0;
							break;
						default:
							ControlRotate = 0.0;
							ImageRotate = 0.0;
							TextRotate = -90.0;
							break;
					}
				}

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

				if (args.PropertyName.Equals(nameof(Branch))) 
				{
					ImageSource = Branch.Image;
					Price = Branch.Price;
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
