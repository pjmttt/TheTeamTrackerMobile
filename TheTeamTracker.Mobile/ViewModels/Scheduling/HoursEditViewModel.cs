using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.DataLayer.Classes;
using TheTeamTracker.Mobile.DataLayer.Models;
using Xamarin.Forms;

namespace TheTeamTracker.Mobile.ViewModels.Scheduling
{
	public class HoursEditViewModel : BaseViewModel
	{
		public const string SUCCESS = "success";

		public ObservableCollection<User> Users { get; }
		public Command SaveCommand { get; }
		public UserClock Hours { get; }
		public bool IsForUser { get; }
		public bool IsNotForUser
		{
			get
			{
				return !IsForUser;
			}
		}

		public DateTime ClockInDate
		{
			get { return Hours.ClockInDateTimezonedValue.Date; }
			set { Hours.ClockInDateTimezonedValue = value.Date + Hours.ClockInDateTimezonedValue.TimeOfDay; }
		}

		public TimeSpan ClockInTime
		{
			get { return Hours.ClockInDateTimezonedValue.TimeOfDay; }
			set { Hours.ClockInDateTimezonedValue = Hours.ClockInDateTimezonedValue.Date + value; }
		}

		public TimeSpan? ClockOutTime
		{
			get { return Hours.ClockOutDate == null ? null : (TimeSpan?)Hours.ClockOutDateTimezonedValue.TimeOfDay; }
			set { Hours.ClockOutDateTimezoned = value == null ? (DateTime?)null : (ClockInDate.Date + value.Value); }
		}

		public HoursEditViewModel(UserClock hours, bool isForUser, List<User> users)
		{
			Users = new ObservableCollection<User>(users);
			IsForUser = isForUser;
			var user = LoginHelper.GetLoggedInUser().User;
			Hours = hours ?? new UserClock() { UserId = user.UserId, ClockInDateTimezoned = DateTime.Now };
			Hours.User = Users.FirstOrDefault(u => u.UserIdValue == Hours.UserIdValue);
			this.SaveCommand = new Command(async () => await saveHours());
		}

		private async System.Threading.Tasks.Task saveHours()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				var hours = EntitySaveHelper.GetUserClockForSave(Hours);
				if (hours.UserClockId == null)
					await DataService.PostItemAsync<UserClock>("userClocks", hours);
				else
					await DataService.PutItemAsync<UserClock>("userClocks", hours.UserClockIdValue, Hours);

				MessagingCenter.Send<HoursEditViewModel>(this, SUCCESS);
				IsBusy = false;
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

	}
}
