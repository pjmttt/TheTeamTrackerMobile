using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTeamTracker.Mobile.DataLayer.Classes;
using TheTeamTracker.Mobile.DataLayer.Services;
using Xamarin.Auth;
using Xamarin.Forms;

namespace TheTeamTracker.Mobile.Classes
{
    public class LoginHelper
    {
        public const string USER_TOKEN = "userToken";
        public const string TTT = "TheTeamTracker";
        public static async Task<UserToken> Login(string userName, string pwd)
        {
            var authService = new AuthService(Config.Instance);
            var usrToken = await authService.Login(userName, pwd);
            Logout();
            Application.Current.Properties.Add(USER_TOKEN, usrToken);
            var account = new Account();
            account.Properties[USER_TOKEN] = JsonConvert.SerializeObject(usrToken);
            AccountStore.Create().Save(account, TTT);
			Globalization.TimeZoneInfo = TimeZoneInfo.GetSystemTimeZones().FirstOrDefault(tz => tz.Id == usrToken.User.Company.Timezone);
			return usrToken;
        }

        public static bool CheckExistingLogin()
        {
            var account = AccountStore.Create().FindAccountsForService(TTT).FirstOrDefault();
            if (account == null) return false;
            if (!account.Properties.ContainsKey(USER_TOKEN)) return false;
            try
            {
                AuthService.UserToken = JsonConvert.DeserializeObject<UserToken>(account.Properties[USER_TOKEN]);
                if (Application.Current.Properties.ContainsKey(USER_TOKEN))
                    Application.Current.Properties.Remove(USER_TOKEN);
                Application.Current.Properties.Add(USER_TOKEN, AuthService.UserToken);
				Globalization.TimeZoneInfo = TimeZoneInfo.GetSystemTimeZones().FirstOrDefault(tz => tz.Id == AuthService.UserToken.User.Company.Timezone);
				return true;
            }
            catch
            {
                return false;
            }
        }

        public static UserToken GetLoggedInUser()
        {
            if (Application.Current.Properties.ContainsKey(USER_TOKEN))
                return Application.Current.Properties[USER_TOKEN] as UserToken;
            return null;
        }

        public static void Logout()
        {
            if (Application.Current.Properties.ContainsKey(USER_TOKEN))
                Application.Current.Properties.Remove(USER_TOKEN);

            var account = AccountStore.Create().FindAccountsForService(TTT).FirstOrDefault();
            if (account != null)
                AccountStore.Create().Delete(account, TTT);

			Globalization.TimeZoneInfo = null;
        }
    }
}
