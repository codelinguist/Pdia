using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using XLabs.Forms;
using Xamarin.Forms;
using XLabs.Ioc;
using System.IO;
using XLabs.Platform.Device;
using XLabs.Forms.Services;
using XLabs.Serialization;
using XLabs.Platform.Services.Media;
using XLabs.Platform.Mvvm;
using XLabs.Platform.Services;

namespace pdia.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : XFormsApplicationDelegate
	{

		private UIWindow _window;
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			this.SetIoc();
			Forms.Init();

			var formsApp = new App();

			LoadApplication (formsApp);

			//			this._window = new UIWindow(UIScreen.MainScreen.Bounds)
			//			{
			//				RootViewController = App.GetMainPage().CreateViewController()
			//			};

			Forms.ViewInitialized += (sender, e) =>
			{
				if (!string.IsNullOrWhiteSpace(e.View.StyleId))
				{
					e.NativeView.AccessibilityIdentifier = e.View.StyleId;
				}
			};


			base.FinishedLaunching(app, options);

			return true;
		}

		/// <summary>
		/// Sets the IoC.
		/// </summary>
		private void SetIoc()
		{
			var resolverContainer = new SimpleContainer();

			var app = new XFormsAppiOS();
			app.Init(this);

			var documents = app.AppDataDirectory;
			var pathToDatabase = Path.Combine(documents, "xforms.db");

			resolverContainer.Register<IDevice>(t => AppleDevice.CurrentDevice)
				.Register<IDisplay>(t => t.Resolve<IDevice>().Display)
				.Register<IFontManager>(t => new FontManager(t.Resolve<IDisplay>()))
				//.Register<IJsonSerializer, XLabs.Serialization.ServiceStack.JsonSerializer>()
				//.Register<IJsonSerializer, Services.Serialization.SystemJsonSerializer>()
				//.Register<ITextToSpeechService, TextToSpeechService>()
				//.Register<IEmailService, EmailService>()
				.Register<IMediaPicker, MediaPicker>()
				.Register<IXFormsApp>(app)
				.Register<ISecureStorage, SecureStorage>()
				.Register<IDependencyContainer>(t => resolverContainer)
				/*.Register<ISimpleCache>(
					t => new SQLiteSimpleCache(new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS(),
						new SQLite.Net.SQLiteConnectionString(pathToDatabase, true), t.Resolve<IJsonSerializer>()))*/;

			Resolver.SetResolver(resolverContainer.GetResolver());
		}
	}

}

