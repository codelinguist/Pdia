using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace pdia
{
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
			NavigationPage.SetHasNavigationBar (this, false);
		}

		void RegisterFbTapped (object sender, EventArgs e)
		{
			App.Instance.MainPage = new MainPage ();
		}

		void RegisterPediaTapped (object sender, EventArgs e)
		{
			App.Instance.MainPage.Navigation.PushAsync (new RegisterPediaPage ());
		}
	}
}

