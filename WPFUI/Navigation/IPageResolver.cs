using System.Windows.Controls;

namespace WPFUI.Navigation
{
	public interface IPageResolver
	{
		Page GetPageInstance(string alias);
	}
}
