using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WPFUI.Resources.Materials.DiceTextures;

namespace WPFUI.UserControls
{
	/// <summary>
	/// Interaction logic for GameDice.xaml
	/// </summary>
	public partial class GameDice : UserControl, INotifyPropertyChanged
	{
		#region Fields

		private List<ImageSource> images;

		#endregion

		static GameDice()
		{
			DiceValueProperty = DependencyProperty.Register("DiceValue", typeof(int), typeof(GameDice),
					new FrameworkPropertyMetadata(0));
		}

		public GameDice()
		{
			InitializeComponent();
			InitializePropertyChanged();

			DiceValue = 0;
			images = new List<ImageSource>();
			ResourceSet resourceSet = DiceResources.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);

			var items = resourceSet
					   .Cast<DictionaryEntry>()
					   .Where(x => x.Value.GetType() == typeof(Bitmap))
					   .Select(x => new ImageContainer() { Name = x.Key.ToString(), Image = x.Value as Bitmap }).ToList().OrderBy(x => x.Name);

			foreach (var item in items)
				images.Add(GetImageSource(item.Image));

		}

		#region Proporties

		public static DependencyProperty DiceValueProperty;

		public int DiceValue
		{
			get => (int)GetValue(DiceValueProperty);
			set
			{
				SetValue(DiceValueProperty, value);
				OnPropertyChanged();
			}
		}

		#endregion

		#region Methods

		public void Play()
		{
			Task.Run(() =>
			{
				GameDiceLogic logic = new GameDiceLogic();
				logic.Action();

				System.Threading.Thread.Sleep(300);

				Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
								   new Action(() => DiceImage.Source = images[logic.FirstValue]));

				Task.Delay(2000);
				System.Threading.Thread.Sleep(300);

				Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
								   new Action(() => DiceImage.Source = images[logic.SecondValue]));

				System.Threading.Thread.Sleep(300);
				Task.Delay(2000);

				Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
								   new Action(() => DiceImage.Source = images[DiceValue]));
			});
		}

		private void InitializePropertyChanged()
		{
			PropertyChanged += (sender, args) =>
			{
				if (args.PropertyName.Equals(nameof(DiceValue)))
				{
					Play();
				}
			};
		}

		[DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool DeleteObject([In] IntPtr hObject);

		private ImageSource GetImageSource(Bitmap bmp)
		{
			IntPtr ptr = bmp.GetHbitmap();

			try
			{
				return Imaging.CreateBitmapSourceFromHBitmap(
						  ptr, IntPtr.Zero, Int32Rect.Empty,
						  BitmapSizeOptions.FromEmptyOptions());
			}
			catch (Exception)
			{
				return null;
			}
			finally
			{
				DeleteObject(ptr);
			}
		}


		#endregion

		#region INotify

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		#endregion
	}

	public struct ImageContainer
	{
		public string Name { get; set; }
		public Bitmap Image { get; set; }
	}
}
