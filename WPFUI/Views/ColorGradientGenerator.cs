using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WPFUI.Views
{
	static class ColorGradientGenerator
	{
		public static int ColorMax = 255;
		private static List<Color> colors;

		static ColorGradientGenerator()
		{
			colors = new List<Color>();
			colors.Add(Color.FromRgb(22, 26, 27));
			colors.Add(Color.FromRgb(205, 56, 71));
			colors.Add(Color.FromRgb(36,148, 226));
			colors.Add(Color.FromRgb(139, 203, 90));
			colors.Add(Color.FromRgb(143, 89, 181));
			colors.Add(Color.FromRgb(191, 132, 232));
		}

		public static Color GetColorFromLib(int id = 0) 
		{
			if (id < 0 || id > colors.Count - 1)
				return colors[0];

			return colors[id];
		}

		public static SolidColorBrush GetColorBrushFromLib(int id = 0)
		{
			return new SolidColorBrush(GetColorFromLib(id));
		}

		public static SolidColorBrush GenerateDarkerColor(Color color)
		{
			Color newColor = new Color();

			newColor.A = (byte)(ColorMax);
			newColor.R = (byte)(color.R - 10);
			newColor.G = (byte)(color.G - 10);
			newColor.B = (byte)(color.B - 10);

			return new SolidColorBrush(newColor);
		}

		public static LinearGradientBrush GenerateGradient(Color color, double angle)
		{
			GradientStop first = new GradientStop() { Offset = 0 };
			GradientStop second = new GradientStop() { Offset = 1 };

			Color firstColor = color;
			Color secondColor = new Color();

			secondColor.A = (byte)(255);
			secondColor.R = (byte)(color.R + 35);
			secondColor.G = (byte)(color.G + 48);
			secondColor.B = (byte)(color.B + 25);

			first.Color = firstColor;
			second.Color = secondColor;

			return new LinearGradientBrush(
						   new GradientStopCollection(
						   new List<GradientStop>()
						   { first, second }), angle);
		}
	}
}
