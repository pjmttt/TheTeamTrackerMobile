using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.DataLayer.Models;
using TheTeamTracker.Mobile.DataLayer.Services;
using Xamarin.Forms;

namespace TheTeamTracker.Mobile.ViewModels.Scheduling
{
	public class UserAvailabilityListViewModel : BaseViewModel
	{
		public ObservableCollection<DisplayItem> UserAvailability { get; set; }
		public Command RefreshAvailabilityCommand { get; }
		public bool ForUser { get; }
		public User SelectedUser { get; set; }
		public ObservableCollection<User> Users { get; set; }
		public Command ApproveCommand { get; }
		public Command DenyCommand { get; }
		public Command DeleteCommand { get; }
		public bool ShowEmployee { get { return !ForUser; } }
		public bool ShowPickEmployee { get; set; }
		public bool NoAvailability { get; set; }

		public UserAvailabilityListViewModel(bool forUser)
		{
			UserAvailability = new ObservableCollection<DisplayItem>();
			Users = new ObservableCollection<User>();
			ForUser = forUser;
			RefreshAvailabilityCommand = new Command(async () => await ExecuteLoadAvailabilityCommand());
			ApproveCommand = new Command(async (availability) => await ExecuteApproveDenyCommand((UserAvailability)availability, true));
			DenyCommand = new Command(async (availability) => await ExecuteApproveDenyCommand((UserAvailability)availability, false));
			DeleteCommand = new Command(async (availability) => await ExecuteDeleteCommand((UserAvailability)availability));
			ShowPickEmployee = !forUser;
		}

		async System.Threading.Tasks.Task ExecuteLoadAvailabilityCommand()
		{
			if (!ForUser && Users.Count < 1)
			{
				IsBusy = true;
				try
				{
					var lookups = await DataService.GetLookups(1000);
					lookups.Users.ForEach(u => Users.Add(u));
				}
				catch (Exception ex)
				{
					IsBusy = false;
					ExceptionHelper.ShowException(ex);
					return;
				}
				finally
				{
					IsBusy = false;
				}
			}

			if (!ForUser && SelectedUser == null)
				return;

			if (IsBusy)
				return;

			if (!IsRefreshing)
				IsBusy = true;

			try
			{
				ShowPickEmployee = false;
				OnPropertyChanged("ShowPickEmployee");
				UserAvailability.Clear();
				var userAvailability = (await DataService.GetItemsAsync<UserAvailability>("userAvailability" + (ForUser ? "?forUser=true" : ""))).Data;
				foreach (var item in userAvailability.OrderBy(d => d.DayOfWeek))
				{
					if (SelectedUser != null && item.User.UserIdValue != SelectedUser.UserIdValue) continue;
					var usr = ForUser ? null : item.User.DisplayName;
					var color = Color.Green;
					switch (item.StatusValue)
					{
						case 1:
							color = Color.Blue;
							break;
						case 2:
							color = Color.Red;
							break;
					}
					UserAvailability.Add(new DisplayItem()
					{
						Line1 = ((DayOfWeek)item.DayOfWeekValue).ToString() + ": " +
							(item.AllDayValue ? "All Day" : item.StartTimeTimezonedValue.ToShortTimeString() +
							" - " + item.EndTimeTimezonedValue.ToShortTimeString()),
						Tag = item,
						Tag2 = color,
						Tag3 = !ForUser && item.StatusValue == 0,
					});
				}
				NoAvailability = !UserAvailability.Any();
				OnPropertyChanged("NoAvailability");

			}
			catch (Exception ex)
			{
				IsBusy = false;
				IsRefreshing = false;
				ExceptionHelper.ShowException(ex);
			}
			finally
			{
				IsBusy = false;
				IsRefreshing = false;
			}
		}

		public async System.Threading.Tasks.Task ExecuteApproveDenyCommand(UserAvailability availability, bool approve)
		{
			try
			{
				availability.StatusValue = approve ? 1 : 2;
				var usr = AuthService.UserToken.User;
				availability.ApprovedDeniedById = usr.UserId;
				availability.ApprovedDeniedDateValue = DateTime.Now;
				IsBusy = true;
				await DataService.PutItemAsync<UserAvailability>("userAvailability", availability.UserAvailabilityIdValue, availability);
				IsBusy = false;
				await ExecuteLoadAvailabilityCommand();
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

		public async System.Threading.Tasks.Task ExecuteDeleteCommand(UserAvailability availability)
		{
			if (await runTask(async() => await DataService.DeleteItemAsync("userAvailability", availability.UserAvailabilityIdValue),
					"Are you sure you want to delete this availability?"))
				await ExecuteLoadAvailabilityCommand();
		}
	}
}
