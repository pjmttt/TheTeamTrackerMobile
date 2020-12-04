using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TheTeamTracker.Mobile.DataLayer.Classes;
using TheTeamTracker.Mobile.DataLayer.Models;

namespace TheTeamTracker.Mobile.DataLayer.Services
{
	public class AuthService
	{
		private Configuration _configuration;
		public static UserToken UserToken { get; set; }

		public AuthService(Configuration configuration)
		{
			this._configuration = configuration;
		}

		public async Task<UserToken> Login(string email, string password)
		{
			try
			{
				UserToken = await HttpService.Post<object, UserToken>($"{this._configuration.ApiUrl}/login", new { email, password, rememberMe = true }, string.Empty);
				return UserToken;
			}
			catch (WebException ex)
			{
				var response = ex.Response as HttpWebResponse;
				if (response != null && (response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Forbidden))
					throw new Exception("Invalid username or password!");
				throw new Exception(ex.Message);
			}
		}
	}

	public class UserToken
	{
		public string AccessToken { get; set; }
		public User User { get; set; }
	}
}
