using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using Plugin.Permissions;

namespace TheTeamTracker.Mobile.Droid
{
	[Activity(Label = "The Team Tracker", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			Xam.Plugin.Droid.PopupEffect.Init();

			base.OnCreate(bundle);

			var context = Application.Context;
			string config = string.Empty;
#if DebugQA
			using (var asset = context.Assets.Open("config.qa.json"))
#elif DebugPROD
			using (var asset = context.Assets.Open("config.json"))
#elif DEBUG
			using (var asset = context.Assets.Open("config.dev.json"))
#else
			using (var asset = context.Assets.Open("config.json"))
#endif
			using (var streamReader = new StreamReader(asset))
			{
				config = streamReader.ReadToEnd();
			}

			Config.LoadConfig(config);

			global::Xamarin.Forms.Forms.Init(this, bundle);
			Acr.UserDialogs.UserDialogs.Init(this);

			System.Net.ServicePointManager.ServerCertificateValidationCallback += (o, cert, chain, errors) => true;
			LoadApplication(new App());
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
		{
			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
			PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}
	}
}

