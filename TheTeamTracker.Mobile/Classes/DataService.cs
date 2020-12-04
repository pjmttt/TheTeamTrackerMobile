using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TheTeamTracker.Mobile.DataLayer.Services;
using Xamarin.Forms;

namespace TheTeamTracker.Mobile.Classes
{
	public class DataService
	{
		private static Dictionary<string, string> getCustomHeader()
		{
			var headers = new Dictionary<string, string>();
			var user = LoginHelper.GetLoggedInUser();
			if (user != null)
			{
				headers.Add("access-token", user.AccessToken);
			}
			return headers;
		}

		private static string _userAgent = string.Empty;
		private static string userAgent
		{
			get
			{
				if (string.IsNullOrEmpty(_userAgent))
				{
					if (Device.RuntimePlatform == Device.iOS)
					{
						_userAgent = "iOS";
					}
					else if (Device.RuntimePlatform == Device.Android)
					{
						_userAgent = "Android";
					}
					if (Device.Idiom == TargetIdiom.Tablet)
					{
						_userAgent += "_Tablet";
					}
				}
				return _userAgent;
			}
		}

		public static async Task<Items<TItem>> GetItemsAsync<TItem>(string url)
		{
			return await HttpService.Get<Items<TItem>>($"{Config.Instance.ApiUrl}/{url}", userAgent, getCustomHeader());
		}

		public static async Task<TItem> GetItemAsync<TItem>(string url)
			where TItem : class
		{
			return await HttpService.Get<TItem>($"{Config.Instance.ApiUrl}/{url}", userAgent, getCustomHeader());
		}

		public static async Task<Stream> GetAsync(string url, string contentType = null)
		{
			return await HttpService.Get($"{Config.Instance.ApiUrl}/{url}", userAgent, getCustomHeader(), contentType);
		}

		public static async Task<Lookups> GetLookups(int lookupType)
		{
			return await HttpService.Get<Lookups>($"{Config.Instance.ApiUrl}/lookups?lookupType={lookupType}", userAgent, getCustomHeader());
		}

		public static async Task<TItem> PostItemAsync<TItem>(string url, TItem item)
			where TItem : class
		{
			return await HttpService.Post<TItem, TItem>($"{Config.Instance.ApiUrl}/{url}", item, userAgent, getCustomHeader());
		}

		public static async Task<TOut> PostAsync<TIn, TOut>(string url, TIn item)
			where TOut : class
		{
			return await HttpService.Post<TIn, TOut>($"{Config.Instance.ApiUrl}/{url}", item, userAgent, getCustomHeader());
		}

		public static async Task<TItem> PutItemAsync<TItem>(string url, Guid id, TItem item)
			where TItem : class
		{
			return await HttpService.Put<TItem, TItem>($"{Config.Instance.ApiUrl}/{url}/{id}", item, userAgent, getCustomHeader());
		}

		public static async Task DeleteItemAsync(string url, Guid id)
		{
			await HttpService.Delete($"{Config.Instance.ApiUrl}/{url}/{id}", userAgent, getCustomHeader());
		}

		public static async Task<TObject> SendFiles<TObject>(string url, List<FileToSend> filesToSend, bool put)
		{
			return await HttpService.SendFiles<TObject>($"{Config.Instance.ApiUrl}/{url}", filesToSend, put, getCustomHeader());
		}
	}

	public class Items<TItem>
	{
		public List<TItem> Data { get; set; }
		public int Count { get; set; }
	}
}