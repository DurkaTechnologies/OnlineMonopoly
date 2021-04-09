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

namespace WPFUI.UserControls
{
	/// <summary>
	/// Interaction logic for GamePiece.xaml
	/// </summary>
	public partial class GamePiece : UserControl
	{
		#region Fields

		public static DependencyProperty PieceBackgroundProperty;
		public static DependencyProperty PieceBorderBrushProperty;

		#endregion

		static GamePiece()
		{
			PieceBackgroundProperty = DependencyProperty.Register("PieceBackground", typeof(Brush), typeof(GamePiece),
			new FrameworkPropertyMetadata(null));

			PieceBorderBrushProperty = DependencyProperty.Register("PieceBorderBrush", typeof(Brush), typeof(GamePiece),
				new FrameworkPropertyMetadata(null));
		}

		#region Methods

		public GamePiece()
		{
			InitializeComponent();
		}

		public Brush PieceBackground
		{
			get => (Brush)GetValue(PieceBackgroundProperty);
			set => SetValue(PieceBackgroundProperty, value);
		}

		public Brush PieceBorderBrush
		{
			get => (Brush)GetValue(PieceBorderBrushProperty);
			set => SetValue(PieceBorderBrushProperty, value);
		}

		#endregion
	}
}
