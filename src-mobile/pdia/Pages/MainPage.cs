using System;

using Xamarin.Forms;

namespace pdia
{
	public class MainPage : MasterDetailPage
	{
		public MainPage ()
		{
			Master = new MenuPage ();
			Detail = new NavigationPage (new NewsFeedPage (){ }){ BarBackgroundColor = Color.FromHex ("#1ecaa7") };
		}
	}
}


