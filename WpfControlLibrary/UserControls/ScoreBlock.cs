using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UIWPF.UserControls
{

	public class ScoreBlock : BaseScoreBlock
	{
		public static DependencyProperty QuantityProperty;

		static ScoreBlock()
		{
			QuantityProperty = DependencyProperty.Register("Quantity", typeof(int), typeof(ScoreBlock),
				new FrameworkPropertyMetadata(0));

			DefaultStyleKeyProperty.OverrideMetadata(typeof(ScoreBlock), new FrameworkPropertyMetadata(typeof(ScoreBlock)));
		}
		//public ScoreBlock() : base()
		//{
		//}

		public int Quantity
		{
			get => (int)GetValue(QuantityProperty);
			set => SetValue(QuantityProperty, value);
		}
	}
}
