using System;
using System.Collections.Generic;
using System.Text;

namespace TheTeamTracker.Mobile.DataLayer.Classes
{
	public class Globalization
	{
		private static TimeZoneInfo _timezoneInfo;
		public static TimeZoneInfo TimeZoneInfo
		{
			get { return _timezoneInfo ?? TimeZoneInfo.Local; }
			set { _timezoneInfo = value; }
		}

	}
}
