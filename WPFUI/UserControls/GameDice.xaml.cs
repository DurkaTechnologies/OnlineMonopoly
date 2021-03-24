using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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

			ResourceSet resourceSet = DiceResources.ResourceManager.GetResourceSet(CultureInfo.CurrentCulture, true, true);
			
			foreach (var item in resourceSet)
				images.Add(GetImageSource(item as Bitmap));
		}

		public void Play(int number) 
		{
			DiceImage.Source = images[number];
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
}
