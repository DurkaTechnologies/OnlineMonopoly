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

namespace UIWPF.UserControls
{
	public class ImageButton : RoundButton
	{
		public static DependencyProperty EnterImageProperty;
		public static DependencyProperty LeaveImageProperty;

		static ImageButton()
		{
			EnterImageProperty = DependencyProperty.Register("EnterImage", typeof(ImageSource), typeof(ImageButton),
				new FrameworkPropertyMetadata(null));

			LeaveImageProperty = DependencyProperty.Register("LeaveImage", typeof(ImageSource), typeof(ImageButton),
				new FrameworkPropertyMetadata(null));

			DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageButton), new FrameworkPropertyMetadata(typeof(ImageButton)));
		}

		public ImageSource EnterImage
		{
			get => (ImageSource)GetValue(EnterImageProperty);
			set => SetValue(EnterImageProperty, value);
		}
	
		public ImageSource LeaveImage
		{
			get => (ImageSource)GetValue(LeaveImageProperty);
			set => SetValue(LeaveImageProperty, value);
		}
	}
}
