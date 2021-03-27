using System;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WPFUI.Navigation
{
	public sealed class Navigation
	{
		public static readonly string MainMenuAlias = "MainMenu";
		public static readonly string GamePageAlias = "GamePage";
		public static readonly string SignInPageAlias = "SignInPage";
		public static readonly string SignUpPageAlias = "SignUpPage";
		public static readonly string RecoverPageAlies = "RecoverPasswordPage";

		private NavigationService navService;
		private readonly PagesResolver resolver;

		public static NavigationService Service
		{
			get => Instance.navService;
			set
			{
				if (Instance.navService != null)
					Instance.navService.Navigated -= Instance.NavServiceNavigated;

				Instance.navService = value;
				Instance.navService.Navigated += Instance.NavServiceNavigated;
			}
		}

		public static void Navigate(Page page, object context)
		{
			if (Instance.navService == null || page == null)
				return;

			Instance.navService.Navigate(page, context);
		}

		public static void Navigate(Page page)
		{
			Navigate(page, null);
		}

		public static void Navigate(string uri, object context)
		{
			if (Instance.navService == null || String.IsNullOrEmpty(uri))
				return;

			var page = Instance.resolver.GetPageInstance(uri);

			Navigate(page, context);
		}

		public static void Navigate(string uri)
		{
			Navigate(uri, null);
		}

		void NavServiceNavigated(object sender, NavigationEventArgs e)
		{
			var page = e.Content as Page;
			if (page == null)
				return;

			if (e != null)
			{
				page.DataContext = e.ExtraData;
			}
		}

		// Використовуємо паттерн Singelton
		private static volatile Navigation instance;
		private static readonly object SyncRoot = new Object();

		private Navigation()
		{
			resolver = new PagesResolver();
		}

		private static Navigation Instance
		{
			get
			{
				if (instance == null)
				{
					lock (SyncRoot)
					{
						if (instance == null)
							instance = new Navigation();
					}
				}

				return instance;
			}
		}
	}
}
