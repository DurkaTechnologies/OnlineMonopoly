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
		private List<ImageSource> images;
		private int diceValue;

		public int DiceValue 
		{
			get => diceValue;
			set 
			{
				diceValue = value;
				OnPropertyChanged();
			}
		}

		public GameDice()
		{
			InitializeComponent();

			images = new List<ImageSource>();
			ResourceSet resourceSet = DiceResources.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);

			var zalypa = resourceSet
					   .Cast<DictionaryEntry>()
					   .Where(x => x.Value.GetType() == typeof(Bitmap))
					   .Select(x => new Kolhoz(){ Name = x.Key.ToString(), Image = x.Value as Bitmap}).ToList().OrderBy(x => x.Name);

			resourceSet.Close();

			foreach (var item in zalypa)
				images.Add(GetImageSource(item.Image));
		}

		public void Play(int number)
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
								   new Action(() => DiceImage.Source = images[number - 1]));
			});
		}

		private void InitializePropertyChanged()
		{
			PropertyChanged += (sender, args) =>
			{
				if (args.PropertyName.Equals(nameof(DiceValue)))
				{
					
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

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}

	public struct Kolhoz
	{
		public string Name { get; set; }
		public Bitmap Image { get; set; }
	}
}
