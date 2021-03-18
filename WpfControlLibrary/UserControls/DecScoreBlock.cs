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
	public class DecScoreBlock : BaseScoreBlock
	{
		public static DependencyProperty QuantityProperty;

		static DecScoreBlock()
		{
			QuantityProperty = DependencyProperty.Register("Quantity", typeof(double), typeof(DecScoreBlock),
				new FrameworkPropertyMetadata(0d));

			DefaultStyleKeyProperty.OverrideMetadata(typeof(DecScoreBlock), new FrameworkPropertyMetadata(typeof(DecScoreBlock)));
		}
		//public DecScoreBlock() : base()
		//{
		//}

		public double Quantity
		{
			get => (double)GetValue(QuantityProperty);
			set => SetValue(QuantityProperty, value);
		}
	}
}
