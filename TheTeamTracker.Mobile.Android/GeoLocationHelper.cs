//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Android.App;
//using Android.Content;
//using Android.Locations;
//using Android.OS;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//using TheTeamTracker.Mobile.Classes;
//using TheTeamTracker.Mobile.Droid;

//[assembly: Xamarin.Forms.Dependency(typeof(GeoLocationHelper))]
//namespace TheTeamTracker.Mobile.Droid
//{
//	public class GeoLocationHelper : Java.Lang.Object, IGeoLocationHelper, ILocationListener
//	{
//		private static object _lockObj = new object();
//		private TaskCompletionSource<GeoLocation> _oltcs;
//		private LocationManager _manager;
//		public Task<GeoLocation> ObtainLocation(double timeoutMilliseconds = 7000)
//		{
//			_manager = (LocationManager)Application.Context.GetSystemService(Context.LocationService);
//			_oltcs = new TaskCompletionSource<GeoLocation>();
//			if (!_manager.IsProviderEnabled(LocationManager.GpsProvider))
//			{
//				_oltcs.SetResult(null);
//			}
//			else
//			{
//				var crit = new Criteria();
//				crit.Accuracy = Accuracy.Fine;
//				_manager.RequestLocationUpdates()
//			}

//			return _oltcs.Task;
//		}

//		public void OnLocationChanged(Location location)
//		{
//			lock (_lockObj)
//			{
//				if (location != null && _manager != null)
//				{
//					_manager.Dispose();
//					_manager = null;
//					if (_oltcs.Task.IsCompleted) return;
//					_oltcs.SetResult(new GeoLocation(location.Latitude, location.Longitude, location.Accuracy));
//				}
//			}
//		}

//		public void OnProviderDisabled(string provider)
//		{
//		}

//		public void OnProviderEnabled(string provider)
//		{
//		}

//		public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
//		{
//		}
//	}
//}