using System;

using Xamarin.Forms;
using XLabs.Ioc;
using XLabs.Platform.Mvvm;
using System.Diagnostics;
using XLabs.Forms.Mvvm;

namespace pdia
{
	public class App : Application
	{
		public static App Instance{get;private set;}
		public App ()
		{
			Instance = this;
			Init ();
			MainPage = GetMainPage ();
		}
		/// <summary>
		/// Initializes the application.
		/// </summary>
		public static void Init()
		{

			var app = Resolver.Resolve<IXFormsApp>();
			if (app == null)
			{
				return;
			}

			app.Closing += (o, e) => Debug.WriteLine("Application Closing");
			app.Error += (o, e) => Debug.WriteLine("Application Error");
			app.Initialize += (o, e) => Debug.WriteLine("Application Initialized");
			app.Resumed += (o, e) => Debug.WriteLine("Application Resumed");
			app.Rotation += (o, e) => Debug.WriteLine("Application Rotated");
			app.Startup += (o, e) => Debug.WriteLine("Application Startup");
			app.Suspended += (o, e) => Debug.WriteLine("Application Suspended");
		}
		public static Page GetMainPage()
		{
			/*
			// Register our views with our view models
			ViewFactory.Register<MvvmSamplePage, MvvmSampleViewModel>();
			ViewFactory.Register<NewPageView, NewPageViewModel>();
			ViewFactory.Register<GeolocatorPage, GeolocatorViewModel>();
			ViewFactory.Register<CameraPage, CameraViewModel>();
			ViewFactory.Register<CacheServicePage, CacheServiceViewModel>();
			ViewFactory.Register<SoundPage, SoundServiceViewModel>();
			ViewFactory.Register<RepeaterViewPage, RepeaterViewViewModel>();
			ViewFactory.Register<WaveRecorderPage, WaveRecorderViewModel>();

			var mainTab = new ExtendedTabbedPage()
			{
				Title = "Xamarin Forms Labs",
				SwipeEnabled = true,
				TintColor = Color.White,
				BarTintColor = Color.Blue,
				Badges = { "1", "2", "3" },
				TabBarBackgroundImage = "ToolbarGradient2.png",
				TabBarSelectedImage = "blackbackground.png",
			};

			var mainPage = new NavigationPage(mainTab);

			Resolver.Resolve<IDependencyContainer>()
				.Register<INavigationService>(t => new NavigationService(mainPage.Navigation));

			mainTab.CurrentPageChanged += () => Debug.WriteLine("ExtendedTabbedPage CurrentPageChanged {0}", mainTab.CurrentPage.Title);

			var controls = GetControlsPage(mainPage);
			var services = GetServicesPage(mainPage);
			var charts = GetChartingPage(mainPage);
			var samples = GetSamplesPage(mainPage);

			var mvvm = ViewFactory.CreatePage<MvvmSampleViewModel, Page>();

			mainTab.Children.Add(controls);
			mainTab.Children.Add(services);
			mainTab.Children.Add(charts);
			mainTab.Children.Add(mvvm as Page);
			mainTab.Children.Add(samples);

			return mainPage;*/
			return new NavigationPage( new LoginPage ());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

