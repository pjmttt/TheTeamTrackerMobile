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
	public class UserAvailabilityEditViewModel : BaseViewModel
	{
		public const string SUCCESS = "success";
		public const string APPROVE = "Approved";
		public const string DENY = "Denied";

		public event EventHandler AvailabilitySaved;
		public ObservableCollection<User> Users { get; }
		public Command SaveCommand { get; }
		public bool ForUser { get; }
		public UserAvailability UserAvailability { get; }
		public bool ShowUserPicker
		{
			get { return !ForUser && IsNew; }
		}
		public bool ShowUserLabel
		{
			get { return !ForUser && !IsNew; }
		}

		public bool Monday { get; set; }
		public bool Tuesday { get; set; }
		public bool Wednesday { get; set; }
		public bool Thursday { get; set; }
		public bool Friday { get; set; }
		public bool Saturday { get; set; }
		public bool Sunday { get; set; }

		public string DayName
		{
			get
			{
				if (UserAvailability == null) return string.Empty;
				return ((DayOfWeek)UserAvailability.DayOfWeekValue).ToString();
			}
		}

		public bool AllDay
		{
			get { return UserAvailability.AllDayValue; }
			set
			{
				UserAvailability.AllDayValue = value;
				OnPropertyChanged("AllDay");
				OnPropertyChanged("NotAllDay");
			}
		}

		public bool NotAllDay { get { return !UserAvailability.AllDayValue; } }
		public bool NotForUser { get { return !ForUser; } }
		public bool IsNew { get { return UserAvailability.UserAvailabilityId == null; } }
		public bool IsNotNew { get { return UserAvailability.UserAvailabilityId != null; } }

		public TimeSpan StartTime
		{
			get { return UserAvailability.StartTimeTimezonedValue.TimeOfDay; }
			set { UserAvailability.StartTimeTimezonedValue = new DateTime(1900, 1, 1) + value; }
		}

		public TimeSpan EndTime
		{
			get { return UserAvailability.EndTimeTimezonedValue.TimeOfDay; }
			set { UserAvailability.EndTimeTimezonedValue = new DateTime(1900, 1, 1) + value; }
		}

		public UserAvailabilityEditViewModel(bool forUser, UserAvailability userAvailability, List<User> users, User selectedUser)
		{
			Users = new ObservableCollection<User>(forUser ? new List<User>() : users);
			ForUser = forUser;
			var user = LoginHelper.GetLoggedInUser().User;
			if (userAvailability != null)
			{
				UserAvailability = userAvailability;
				UserAvailability.User = forUser ? user : Users.FirstOrDefault(u => u.UserId == UserAvailability.UserId);
			}
			else
			{
				UserAvailability = userAvailability ?? new UserAvailability() { UserId = user.UserId, Status = ForUser ? 0 : 1, StartTimeTimezonedValue = new DateTime(2000, 1, 1), EndTimeTimezonedValue = new DateTime(2000, 1, 1) };
				UserAvailability.User = forUser ? user : Users.FirstOrDefault(u => u.UserId == (selectedUser == null ? user.UserId : selectedUser.UserId));
			}
			if (UserAvailability.UserAvailabilityId != null)
			{
				var day = (DayOfWeek)UserAvailability.DayOfWeekValue;
				var boolDay = this.GetType().GetProperty(day.ToString(), System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
				boolDay.SetValue(this, true);
			}
			this.SaveCommand = new Command(async (status) => await saveAvailability(Convert.ToInt16(status)));
		}

		private async System.Threading.Tasks.Task saveAvailability(int status)
		{
			IsBusy = true;

			if (status == 0 && NotForUser) status = 1;

			try
			{
				UserAvailability.StatusValue = status;
				if (status > 0)
				{
					var user = LoginHelper.GetLoggedInUser().User;
					UserAvailability.ApprovedDeniedById = user.UserId;
					UserAvailability.ApprovedDeniedDate = DateTime.Now;
				}

				if (UserAvailability.UserAvailabilityId != null)
				{
					await DataService.PutItemAsync<UserAvailability>("userAvailability", UserAvailability.UserAvailabilityIdValue, UserAvailability);
				}
				else
				{
					var userAvailabilities = new List<UserAvailability>();
					var daysOfWeek = new List<int>();
					if (Monday) daysOfWeek.Add((int)DayOfWeek.Monday);
					if (Tuesday) daysOfWeek.Add((int)DayOfWeek.Tuesday);
					if (Wednesday) daysOfWeek.Add((int)DayOfWeek.Wednesday);
					if (Thursday) daysOfWeek.Add((int)DayOfWeek.Thursday);
					if (Friday) daysOfWeek.Add((int)DayOfWeek.Friday);
					if (Saturday) daysOfWeek.Add((int)DayOfWeek.Saturday);
					if (Sunday) daysOfWeek.Add((int)DayOfWeek.Sunday);

					foreach (var dow in daysOfWeek)
					{
						var availability = new UserAvailability();
						availability.AllDayValue = UserAvailability.AllDayValue;
						availability.ApprovedDeniedById = UserAvailability.ApprovedDeniedById;
						availability.ApprovedDeniedDateValue = UserAvailability.ApprovedDeniedDateValue;
						availability.DayOfWeekValue = dow;
						availability.EndTime = UserAvailability.EndTime;
						availability.StartTime = UserAvailability.StartTime;
						availability.Status = UserAvailability.Status;
						availability.UserId = UserAvailability.User.UserId;
						userAvailabilities.Add(availability);
					}

					await DataService.PostItemAsync<UserAvailability[]>("userAvailability", userAvailabilities.ToArray());
				}
				AvailabilitySaved?.Invoke(this, new EventArgs());

				MessagingCenter.Send<UserAvailabilityEditViewModel>(this, SUCCESS);
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
