using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoreLocation;
// using CoreLocation;
using Foundation;
using UIKit;

namespace TheTeamTracker.Mobile.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
#if DebugQA
			var data = File.ReadAllText("config.qa.json");
#elif DebugPROD
			var data = File.ReadAllText("config.json");
#elif DEBUG
			var data = File.ReadAllText("config.dev.json");
#else
			var data = File.ReadAllText("config.json");
#endif
			Config.LoadConfig(data);

			global::Xamarin.Forms.Forms.Init();
			Xam.Plugin.iOS.PopupEffect.Init();
			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}
	}
}
