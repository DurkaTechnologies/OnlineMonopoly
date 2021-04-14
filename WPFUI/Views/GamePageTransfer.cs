using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace WPFUI.Views
{
	public static class GamePageTransfer
	{
		public static TransferCoord GetGameCoord(int position)
		{
			TransferCoord coord = new TransferCoord();

			if (position < 0 || position > 40)
				return coord;

			if (position <= 10)
			{
				coord.X = position;
				coord.Rotate = Dock.Top;
			}
			else if (position > 10 && position <= 20)
			{
				coord.X = 10;
				coord.Y = position - 10;
				coord.Rotate = Dock.Right;
			}
			else if (position > 20 && position <= 30)
			{
				coord.X = 30 - position;
				coord.Y = 10;
				coord.Rotate = Dock.Bottom;
			}
			else if (position > 30 && position < 40)
			{
				coord.X = 0;
				coord.Y = 40 - position;
				coord.Rotate = Dock.Left;
			}
			else
			{
				coord.X = 10;
				coord.Y = 0;
			}

			return coord;
		}
	}

	public struct TransferCoord
	{
		public int X { get; set; }
		public int Y { get; set; }
		public Dock Rotate { get; set; }
	}
}
