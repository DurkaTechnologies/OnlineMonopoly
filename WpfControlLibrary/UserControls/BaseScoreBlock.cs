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
	public class BaseScoreBlock : UserControl
	{
		public static DependencyProperty ScoreTextProperty;
		public static DependencyProperty CornerRadiusProperty;
		public static DependencyProperty BlockThicknessProperty;
		public static DependencyProperty BorderStyleProperty;
		public static DependencyProperty TextStyleProperty;
		public static DependencyProperty InfoPaddingProperty;
		public static DependencyProperty NumberPaddingProperty;


		static BaseScoreBlock()
		{
			BlockThicknessProperty = DependencyProperty.Register("BlockThickness", typeof(Thickness), typeof(BaseScoreBlock),
				new FrameworkPropertyMetadata(new Thickness(1, 1, 1, 8)));
			ScoreTextProperty = DependencyProperty.Register("ScoreText", typeof(String), typeof(BaseScoreBlock),
				new FrameworkPropertyMetadata(null));
			BorderStyleProperty = DependencyProperty.Register("BorderStyle", typeof(Style), typeof(BaseScoreBlock),
				new FrameworkPropertyMetadata(new Style(typeof(Border))));
			TextStyleProperty = DependencyProperty.Register("TextStyle", typeof(Style), typeof(BaseScoreBlock),
				new FrameworkPropertyMetadata(new Style(typeof(Label))));
			CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(BaseScoreBlock),
				new FrameworkPropertyMetadata(new CornerRadius(8)));
			InfoPaddingProperty = DependencyProperty.Register("InfoPadding", typeof(Thickness), typeof(BaseScoreBlock),
				new FrameworkPropertyMetadata(new Thickness(0, 5, 0, 0)));
			NumberPaddingProperty = DependencyProperty.Register("NumberPadding", typeof(Thickness), typeof(BaseScoreBlock),
				new FrameworkPropertyMetadata(new Thickness(0, 0, 0, 5)));

			DefaultStyleKeyProperty.OverrideMetadata(typeof(BaseScoreBlock), new FrameworkPropertyMetadata(typeof(BaseScoreBlock)));
		}
		//public BaseScoreBlock()
		//{
		//	Style style = new Style(typeof(Label));
		//	style.Setters.Add(new Setter(Label.FontSizeProperty, 25.0));
		//	style.Setters.Add(new Setter(Label.FontWeightProperty, FontWeights.Bold));
		//	style.Setters.Add(new Setter(Label.ForegroundProperty, new SolidColorBrush(Colors.Black)));
		//	style.Setters.Add(new Setter(Label.VerticalAlignmentProperty, VerticalAlignment.Center));
		//	TextStyle = style;
		//}

		public Thickness BlockThickness
		{
			get => (Thickness)GetValue(BlockThicknessProperty);
			set => SetValue(BlockThicknessProperty, value);
		}

		public Style BorderStyle
		{
			get => (Style)GetValue(BorderStyleProperty);
			set => SetValue(BorderStyleProperty, value);
		}

		public Style TextStyle
		{
			get => (Style)GetValue(TextStyleProperty);
			set => SetValue(TextStyleProperty, value);
		}

		public String ScoreText
		{
			get => (String)GetValue(ScoreTextProperty);
			set => SetValue(ScoreTextProperty, value);
		}

		public CornerRadius CornerRadius
		{
			get => (CornerRadius)GetValue(CornerRadiusProperty);
			set => SetValue(CornerRadiusProperty, value);
		}

		public Thickness InfoPadding
		{
			get => (Thickness)GetValue(InfoPaddingProperty);
			set => SetValue(InfoPaddingProperty, value);
		}

		public Thickness NumberPadding
		{
			get => (Thickness)GetValue(NumberPaddingProperty);
			set => SetValue(NumberPaddingProperty, value);
		}
	}
}
