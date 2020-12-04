using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace TheTeamTracker.Mobile.Classes
{
    public class TTTPicker : Xamarin.Forms.Picker
    {
		public TTTPicker() : base()
		{
			this.On<iOS>().SetUpdateMode(UpdateMode.WhenFinished);
		}
    }
}
