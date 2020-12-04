using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace TheTeamTracker.Mobile.Classes
{
	public class ExceptionHelper
	{
		public static void ShowException(Exception ex)
		{
			var msg = ex.Message;
			if (ex is WebException)
			{
				var response = (ex as WebException).Response as HttpWebResponse;
				try
				{
					var resp = new StreamReader(response.GetResponseStream()).ReadToEnd();
					try
					{
						var json = (JObject)JsonConvert.DeserializeObject(resp);
						var token = json.GetValue("message");
						if (token != null)
							msg = token.ToString();
						else
							msg = resp;
					}
					catch
					{
						msg = resp;
					}
				}
				catch
				{
					if (response != null && !string.IsNullOrEmpty(response.StatusDescription))
						msg = response.StatusDescription;
				}
			}
			if (msg.Contains("Connection refused") || msg.Contains("ConnectionFailure") || msg.Contains("NameResolutionFailure"))
				msg = "Failed to connect, please ensure connection and try again.";
			Acr.UserDialogs.UserDialogs.Instance.Alert(msg, "Warning");
		}
	}

	public class MessageHelper
	{
		public static void ShowMessage(string message, string title = null)
		{
			Acr.UserDialogs.UserDialogs.Instance.Alert(message, title);
		}
	}
}
