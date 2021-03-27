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

		static ColorGradientGenerator()
		{

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

			//color first - 205 56 71, second - 240 104 96, third - 36, 148, 226, timer - 52, 150, 199

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
