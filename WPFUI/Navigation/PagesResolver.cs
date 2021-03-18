using System;
using System.Collections.Generic;
using System.Windows.Controls;
using WPFUI.Pages;

namespace WPFUI.Navigation
{
	public class PagesResolver
	{

		private readonly Dictionary<string, Func<Page>> pagesResolvers = new Dictionary<string, Func<Page>>();

		public PagesResolver()
		{
			pagesResolvers.Add(Navigation.MainMenuAlias, () => new MainMenu());
			//pagesResolvers.Add(Navigation.GamePageAlias, () => new GamePage());
			pagesResolvers.Add(Navigation.SignInPageAlias, () => new SignInPage());
			pagesResolvers.Add(Navigation.SignUpPageAlias, () => new SignUpPage());
		}

		public Page GetPageInstance(string alias)
		{
			if (pagesResolvers.ContainsKey(alias))
				return pagesResolvers[alias]();

			return pagesResolvers[Navigation.MainMenuAlias]();
		}
	}
}
