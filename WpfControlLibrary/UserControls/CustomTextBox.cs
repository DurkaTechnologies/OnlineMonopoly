using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UIWPF.UserControls
{

	public class CustomTextBox : TextBox
	{
		public static DependencyProperty SecondBackgroundProperty;
		public static DependencyProperty SecondBorderBrushProperty;

		static CustomTextBox()
		{
			SecondBackgroundProperty = DependencyProperty.Register("SecondBackground", typeof(SolidColorBrush), typeof(CustomTextBox),
				new FrameworkPropertyMetadata(null));

			SecondBorderBrushProperty = DependencyProperty.Register("SecondBorderBrush", typeof(Brush), typeof(CustomTextBox),
				new FrameworkPropertyMetadata(null));

			DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomTextBox), new FrameworkPropertyMetadata(typeof(CustomTextBox)));
		}

		public SolidColorBrush SecondBackground
		{
			get => (SolidColorBrush)GetValue(SecondBackgroundProperty);
			set => SetValue(SecondBackgroundProperty, value);
		}

		public Brush SecondBorderBrush
		{
			get => (Brush)GetValue(SecondBorderBrushProperty);
			set => SetValue(SecondBorderBrushProperty, value);
		}
	}
}
