using Plugin.Geolocator;
using Plugin.Permissions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TheTeamTracker.Mobile.DataLayer.Services;
using Xamarin.Forms;

namespace TheTeamTracker.Mobile.Classes
{
	public class ClockInOut
	{
		public static async Task<bool> ClockIn()
		{
			return await clockInOut(true);
		}

		public static async Task<bool> ClockOut()
		{
			return await clockInOut(false);
		}

		private static double getDistanceFromLatLonInKm(double lat1, double lon1, double lat2, double lon2)
		{
			var R = 6371; // Radius of the earth in km
			var dLat = deg2rad(lat2 - lat1);  // deg2rad below
			var dLon = deg2rad(lon2 - lon1);
			var a =
			  Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
			  Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) *
			  Math.Sin(dLon / 2) * Math.Sin(dLon / 2)
			  ;
			var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
			var d = R * c; // Distance in km
			return d;
		}

		private static double deg2rad(double deg)
		{
			return deg * (Math.PI / 180);
		}

		private static async Task<bool> clockInOut(bool clockIn)
		{
			var dlg = Acr.UserDialogs.UserDialogs.Instance.Loading("Loading");
			var usr = AuthService.UserToken.User;
			object geoLocation = null;

			var hasPermission = await Utils.CheckPermissions(Plugin.Permissions.Abstractions.Permission.Location, dlg);
			if (hasPermission)
			{
				var locator = CrossGeolocator.Current;
				// locator.DesiredAccuracy = 10000;

				var position = await locator.GetLastKnownLocationAsync();

				// if (!string.IsNullOrEmpty(usr.Company.GeoLocation))
				{
					//var locator = DependencyService.Get<IGeoLocationHelper>();
					//var position = await locator.ObtainLocation();
					if (position != null)
					{
						//	MessagingCenter.Send<ExceptionHelper, Exception>(ExceptionHelper.Instance, "Pos", new Exception(position.Accuracy.ToString()));
						geoLocation = new
						{
							latitude = position.Latitude,
							longitude = position.Longitude,
							accuracy = position.Accuracy,
						};
						//	//var geoParts = usr.Company.GeoLocation.Split(',');
						//	//if (geoParts.Length == 2)
						//	//{
						//	//	double lat = 0;
						//	//	double lon = 0;
						//	//	double.TryParse(geoParts[0], out lat);
						//	//	double.TryParse(geoParts[1], out lon);
						//	//	double dist = getDistanceFromLatLonInKm(lat, lon, position.Latitude, position.Longitude);
						//	//	dlg.Hide();
						//	//	MessagingCenter.Send<ExceptionHelper, Exception>(ExceptionHelper.Instance, ExceptionHelper.EXCEPTION, new Exception($"{dist} ({position.Latitude},{position.Longitude})"));
						//	//	return;
						//	//}
					}
				}
			}

			var postItem = new
			{
				userId = usr.UserIdValue,
				clockIn,
				geoLocation,
			};

			try
			{
				await DataService.PostItemAsync("clockInOutById", postItem);
				dlg.Hide();
				return true;
			}
			catch (Exception ex)
			{
				dlg.Hide();
				ExceptionHelper.ShowException(ex);
				return false;
			}
		}
	}
}
