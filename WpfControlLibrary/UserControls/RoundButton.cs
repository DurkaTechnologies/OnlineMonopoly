using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UIWPF.UserControls
{
	public enum PressType
	{
		None,
		Correct,
		Incorrect,
	}

	public class RoundButton : Button, INotifyPropertyChanged
	{
		public static DependencyProperty ButtonThicknessProperty;
		public static DependencyProperty BorderStyleProperty;
		public static DependencyProperty BorderMarginProperty;
		public static DependencyProperty ButtonRadiusProperty;
		public static DependencyProperty IsCorrectPressProperty;
		public static DependencyProperty SecondBackgroundProperty;
		public static DependencyProperty SecondBorderBrushProperty;

		static RoundButton()
		{
			ButtonThicknessProperty = DependencyProperty.Register("ButtonThickness", typeof(Thickness), typeof(RoundButton),
				new FrameworkPropertyMetadata(new Thickness(2,2,2,4)));

			BorderMarginProperty = DependencyProperty.Register("BorderMargin", typeof(Thickness), typeof(RoundButton),
				new FrameworkPropertyMetadata(new Thickness(0)));

			ButtonRadiusProperty = DependencyProperty.Register("ButtonRadius", typeof(CornerRadius), typeof(RoundButton),
				new FrameworkPropertyMetadata(new CornerRadius(8)));

			BorderStyleProperty = DependencyProperty.Register("BorderStyle", typeof(Style), typeof(RoundButton),
				new FrameworkPropertyMetadata(new Style(typeof(Border))));

			IsCorrectPressProperty = DependencyProperty.Register("IsCorrectPress", typeof(PressType), typeof(RoundButton),
				new FrameworkPropertyMetadata(PressType.None));

			SecondBackgroundProperty = DependencyProperty.Register("SecondBackground", typeof(SolidColorBrush), typeof(RoundButton),
				new FrameworkPropertyMetadata(null));

			SecondBorderBrushProperty = DependencyProperty.Register("SecondBorderBrush", typeof(Brush), typeof(RoundButton),
				new FrameworkPropertyMetadata(null));

			DefaultStyleKeyProperty.OverrideMetadata(typeof(RoundButton), new FrameworkPropertyMetadata(typeof(RoundButton)));
		}

		public Thickness ButtonThickness
		{
			get => (Thickness)GetValue(BorderThicknessProperty);
			set => SetValue(BorderThicknessProperty, value);
		}

		public Style BorderStyle
		{
			get => (Style)GetValue(BorderStyleProperty);
			set => SetValue(BorderStyleProperty, value);
		}

		public Thickness BorderMargin
		{
			get => (Thickness)GetValue(BorderMarginProperty);
			set => SetValue(BorderMarginProperty, value);
		}

		public CornerRadius ButtonRadius
		{
			get => (CornerRadius)GetValue(ButtonRadiusProperty);
			set => SetValue(ButtonRadiusProperty, value);
		}

		public PressType? IsCorrectPress
		{
			get => (PressType)GetValue(IsCorrectPressProperty);
			set
			{
				SetValue(IsCorrectPressProperty, value);
				OnPropertyChanged();
			}
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

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}
}
