
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using CoreLocation;
//using Foundation;
//using TheTeamTracker.Mobile.Classes;
//using TheTeamTracker.Mobile.iOS;
//using UIKit;
//using Xamarin.Forms;

//[assembly: Dependency(typeof(GeoLocationHelper))]
//namespace TheTeamTracker.Mobile.iOS
//{
//	public class GeoLocationHelper : IGeoLocationHelper
//	{
//		private static object _lockObj = new object();
//		private TaskCompletionSource<GeoLocation> _oltcs;
//		private CLLocationManager _manager;
//		public Task<GeoLocation> ObtainLocation(double timeoutMilliseconds = 7000)
//		{
//			_oltcs = new TaskCompletionSource<GeoLocation>();
//			_manager = new CLLocationManager();
//			_manager.RequestWhenInUseAuthorization();
//			_manager.DesiredAccuracy = 1;
//			_manager.AuthorizationChanged += (object sender, CLAuthorizationChangedEventArgs e) =>
//			{
//				if (e.Status == CLAuthorizationStatus.Denied || e.Status == CLAuthorizationStatus.NotDetermined || e.Status == CLAuthorizationStatus.Restricted)
//				{
//					//_manager.Dispose();
//					//_manager = null;
//					//_oltcs.SetResult(null);
//				}
//			};
//			_manager.LocationsUpdated += (object sender, CLLocationsUpdatedEventArgs e) =>
//				{
//					lock (_lockObj)
//					{
//						_manager.StopUpdatingLocation();
//						if (_oltcs.Task.IsCompleted)
//							return;
//						CLLocation lastLocation = null;
//						foreach (CLLocation location in e.Locations)
//						{
//							lastLocation = location;
//						}
//						_manager.Dispose();
//						_manager = null;
//						_oltcs.SetResult(new GeoLocation(lastLocation.Coordinate.Latitude, lastLocation.Coordinate.Longitude, lastLocation.HorizontalAccuracy));
//					}
//				};
//			_manager.StartUpdatingLocation();
//			return _oltcs.Task;
//		}
//	}
//}