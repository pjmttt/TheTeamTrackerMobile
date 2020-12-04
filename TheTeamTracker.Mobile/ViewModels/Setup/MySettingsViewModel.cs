using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.DataLayer.Classes;
using TheTeamTracker.Mobile.DataLayer.Models;
using Xamarin.Forms;

namespace TheTeamTracker.Mobile.ViewModels.Setup
{
	public class MySettingsViewModel : BaseViewModel
	{
		public User User { get; set; }
		public string PasswordConfirm { get; set; }
		public ObservableCollection<CellPhoneCarrier> CellPhoneCarriers { get; set; }
		public Command ExecuteLoadCommand { get; set; }
		public event EventHandler SettingsLoaded;
		public Command NotificationChangedCommand { get; set; }
		public Command SaveCommand { get; set; }

		public MySettingsViewModel()
		{
			this.CellPhoneCarriers = new ObservableCollection<CellPhoneCarrier>();
			ExecuteLoadCommand = new Command(async () => await loadSettings());
			NotificationChangedCommand = new Command((args) => setNotification((Tuple<int, bool, bool>)args));
			SaveCommand = new Command(async () => await saveSettings());
		}

		private async System.Threading.Tasks.Task loadSettings()
		{
			this.IsBusy = true;
			try
			{
				if (CellPhoneCarriers.Count < 1)
				{
					var carriers = await DataService.GetItemsAsync<CellPhoneCarrier>("cellPhoneCarriers");
					carriers.Data.ForEach(d => this.CellPhoneCarriers.Add(d));
				}

				User = await DataService.GetItemAsync<User>("mySettings");
				User.CellPhoneCarrier = CellPhoneCarriers.FirstOrDefault(c => c.CellPhoneCarrierIdValue == User.CellPhoneCarrierIdValue);
				OnPropertyChanged("User");
				SettingsLoaded?.Invoke(this, new EventArgs());
			}
			catch (Exception ex)
			{
				IsBusy = false;
				ExceptionHelper.ShowException(ex);
			}
			finally
			{
				IsBusy = false;
			}
		}

		private async System.Threading.Tasks.Task saveSettings()
		{
			if (!string.IsNullOrEmpty(this.User.Password))
			{
				if (this.User.Password != this.PasswordConfirm)
				{
					MessageHelper.ShowMessage("Passwords do not match!", "Validation");
					return;
				}
			}


			this.IsBusy = true;
			try
			{
				var forSave = Common.Clone<User>(User);
				forSave.CellPhoneCarrier = null;
				User = await DataService.PutItemAsync<User>("mySettings", User.UserIdValue, forSave);
				User.CellPhoneCarrier = CellPhoneCarriers.FirstOrDefault(c => c.CellPhoneCarrierIdValue == User.CellPhoneCarrierIdValue);
				LoginHelper.GetLoggedInUser().User = Common.Clone<User>(User);
				PasswordConfirm = string.Empty;
				OnPropertyChanged("User");
				OnPropertyChanged("PasswordConfirm");
				UserDialogs.Instance.Alert("Settings have been saved.");
			}
			catch (Exception ex)
			{
				IsBusy = false;
				ExceptionHelper.ShowException(ex);
			}
			finally
			{
				IsBusy = false;
			}
		}

		private void setNotification(Tuple<int, bool, bool> notification)
		{
			var notifications = notification.Item2 ? User.EmailNotifications : User.TextNotifications;
			if (notification.Item3 && !notifications.Contains(notification.Item1))
				notifications.Add(notification.Item1);
			else if (!notification.Item3 && notifications.Contains(notification.Item1))
				notifications.Remove(notification.Item1);
		}
	}
}
