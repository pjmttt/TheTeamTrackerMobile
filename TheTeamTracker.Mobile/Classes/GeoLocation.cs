using System;
using System.Collections.Generic;
using System.Text;

namespace TheTeamTracker.Mobile.Classes
{
 //   public interface IGeoLocationHelper
 //   {
	//	System.Threading.Tasks.Task<GeoLocation> ObtainLocation(double timeoutMilliseconds = 7000);
	//}

	public class GeoLocation
	{
		public double Latitude { get; }
		public double Longitude { get; }
		public double Accuracy { get; }
		public GeoLocation(double latitude, double longitude, double accuracy)
		{
			this.Latitude = latitude;
			this.Longitude = longitude;
			this.Accuracy = accuracy;
		}
	}
}
