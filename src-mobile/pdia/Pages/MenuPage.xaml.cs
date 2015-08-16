using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace pdia
{
	public partial class MenuPage : ContentPage
	{
		public MenuPage ()
		{
			InitializeComponent (); 
		}

		void NotificationsTapped (object sender, EventArgs e)
		{
			var page = App.Instance.MainPage as MasterDetailPage;
			page.Detail =new NavigationPage (new NotificationPage (){ }){ BarBackgroundColor = Color.FromHex ("#1ecaa7") };
			page.IsPresented = false;
		}
	}
}

