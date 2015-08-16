using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using XLabs.Forms;
using XLabs.Ioc;
using XLabs.Platform.Mvvm;
using Xamarin.Forms;
using System.IO;
using XLabs.Platform.Device;
using XLabs.Forms.Services;
using XLabs.Serialization;
using XLabs.Platform.Services.Media;
using XLabs.Platform.Services;

namespace pdia.Droid
{
	[Activity (Label = "pdia.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : XFormsApplicationDroid
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate(bundle);

			if (!Resolver.IsSet)
			{
				this.SetIoc();
			}
			else
			{
				var app = Resolver.Resolve<IXFormsApp>() as IXFormsApp<XFormsApplicationDroid>;
				app.AppContext = this;
			}

			Forms.Init(this, bundle);

			App.Init();

			Forms.ViewInitialized += (sender, e) =>
			{
				if (!string.IsNullOrWhiteSpace(e.View.StyleId))
				{
					e.NativeView.ContentDescription = e.View.StyleId;
				}
			};


			this.LoadApplication (new App ());
		}

		/// <summary>
		/// Sets the IoC.
		/// </summary>
		private void SetIoc()
		{
			var resolverContainer = new SimpleContainer();

			var app = new XFormsAppDroid();

			app.Init(this);

			var documents = app.AppDataDirectory;
			var pathToDatabase = Path.Combine(documents, "xforms.db");

			resolverContainer.Register<IDevice> (t => AndroidDevice.CurrentDevice)
				.Register<IDisplay> (t => t.Resolve<IDevice> ().Display)
				.Register<IFontManager> (t => new FontManager (t.Resolve<IDisplay> ()))
			//.Register<IJsonSerializer, Services.Serialization.JsonNET.JsonSerializer>()
			//.Register<IJsonSerializer, JsonSerializer>()
			//.Register<IEmailService, EmailService>()
				.Register<IMediaPicker, MediaPicker> ()
			//.Register<ITextToSpeechService, TextToSpeechService>()
				.Register<IDependencyContainer> (resolverContainer)
				.Register<IXFormsApp> (app)
				.Register<ISecureStorage> (t => new KeyVaultStorage (t.Resolve<IDevice> ().Id.ToCharArray ()));
				/*.Register<ISimpleCache>(
					t => new SQLiteSimpleCache(new SQLitePlatformAndroid(),
						new SQLiteConnectionString(pathToDatabase, true), t.Resolve<IJsonSerializer>()));*/


			Resolver.SetResolver(resolverContainer.GetResolver());
		}
	}
}

