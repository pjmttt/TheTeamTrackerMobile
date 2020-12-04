using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.DataLayer.Models;
using Xamarin.Forms;
using static TheTeamTracker.Mobile.Classes.Constants;
namespace TheTeamTracker.Mobile.ViewModels.Scheduling
{
	public class HoursViewModel : ListViewModel<UserClock>
	{
		const string DELETE = "Delete";
		private DateTime _startDate;
		public DateTime StartDate
		{
			get { return _startDate; }
			set
			{
				_startDate = value;
				OnPropertyChanged("StartDate");
			}
		}
		private DateTime _endDate;
		public DateTime EndDate
		{
			get { return _endDate; }
			set
			{
				_endDate = value;
				OnPropertyChanged("EndDate");
			}
		}
		public ObservableCollection<User> Users { get; set; }
		public User SelectedUser { get; set; }
		public Command ApproveCommand { get; }
		public Command DenyCommand { get; }
		public Command DeleteCommand { get; }
		public bool ForUser { get; }
		public bool ShowEmployee
		{
			get { return !ForUser; }
		}
		public bool CanChangeEmployee
		{
			get { return !ForUser; }
		}
		private double _totalMinutes { get; set; }
		public string TotalHours
		{
			get { return $"{Math.Round(_totalMinutes / 60, 2)} Hours"; }
		}
		public bool ShowFilter { get; set; }

		protected override string endpoint => "userClocks";

		private Guid _allGID;
		public HoursViewModel(bool forUser)
		{
			_allGID = Guid.NewGuid();
			ShowFilter = true;
			Users = new ObservableCollection<User>();
			ForUser = forUser;
			StartDate = DateTime.Now;
			EndDate = DateTime.Now;
			var usr = LoginHelper.GetLoggedInUser();
			if (ForUser)
			{
				while ((int)StartDate.DayOfWeek > usr.User.Company.WeekStartValue)
				{
					StartDate = StartDate.AddDays(-1);
				}
				EndDate = StartDate.AddDays(6);
			}
			DeleteCommand = new Command(async (parameter) => await ExecuteDeleteCommand(parameter as DisplayItem));
			ApproveCommand = new Command(async (parameter) => await approveDenyHours(parameter as UserClock, true));
			DenyCommand = new Command(async (parameter) => await approveDenyHours(parameter as UserClock, false));
			this.sortQueue.Add(new Tuple<string, bool>("clockInDate", true));
			this.sortFields.Add("Date", "clockInDate");
			if (!forUser)
				this.sortFields.Add("Employee", "user");
		}
		private TimeSpan getMinutes(DateTime dt1)
		{
			var tod = dt1.TimeOfDay;
			return new TimeSpan(tod.Hours, tod.Minutes, 0);
		}
		public void ToggleFilter()
		{
			this.ShowFilter = !this.ShowFilter;
			OnPropertyChanged("ShowFilter");
		}
		protected override async System.Threading.Tasks.Task onInit()
		{
			if (!ForUser && Users.Count < 1)
			{
				Users.Add(new User() { UserIdValue = _allGID, FirstName = "(All)" });
				var lookups = await DataService.GetLookups(1000);
				lookups.Users.ForEach(u => Users.Add(u));
			}
		}

		protected override string getPostParams()
		{
			var postParams = string.Empty;
			if (ForUser) postParams += "&forUser=true";
			postParams += "&where=" + getDateRangeWhere("clockInDate", this.StartDate, this.EndDate);
			if (SelectedUser != null && SelectedUser.UserIdValue != _allGID)
				postParams += ",userId eq " + SelectedUser.UserIdValue.ToString();
			return postParams;
		}

		public IEnumerable<User> GetUsers()
		{
			return this.Users.Where(u => u.UserIdValue != _allGID);
		}
		public async System.Threading.Tasks.Task ExecuteDeleteCommand(DisplayItem displayItem)
		{
			var userClock = displayItem.Tag as UserClock;
			if (await runTask(async () => await DataService.DeleteItemAsync("UserClocks", userClock.UserClockIdValue),
				"Are you sure you want to delete these hours?"))
			{
				DataSource.Remove(displayItem);
			}
		}
		private async System.Threading.Tasks.Task approveDenyHours(UserClock userClock, bool approve)
		{
			this.IsBusy = true;
			try
			{
				if (approve)
				{
					userClock.Status = 0;
					await DataService.PutItemAsync<UserClock>("userClocks", userClock.UserClockIdValue, userClock);
				}
				else
				{
					await DataService.DeleteItemAsync("userClocks", userClock.UserClockIdValue);
				}
				this.IsBusy = false;
				LoadDataCommand.Execute(null);
			}
			catch (Exception ex)
			{
				this.IsBusy = false;
				ExceptionHelper.ShowException(ex);
			}
		}

		protected override void populateDisplayItem(DisplayItem item, UserClock entity)
		{
			var usr = ForUser ? null : entity.User.DisplayName;
			var timeDiffString = string.Empty;
			double duration = 0;
			if (entity.ClockOutDate != null)
			{
				duration = (getMinutes(entity.ClockOutDateValue) - getMinutes(entity.ClockInDateValue)).TotalMinutes;
				if (duration < 0)
				{
					duration += (24 * 60);
				}
				timeDiffString = $"{Math.Round(duration / 60, 2)}";
			}
			var color = Color.LightGray;
			if (entity.StatusValue != 0)
			{
				color = Color.Red;
			}
			else if (entity.ClockOutDate == null)
			{
				color = Color.Blue;
			}
			item.Line1 = ForUser ? "" : usr;
			item.Line2 = entity.ClockInDateTimezonedValue.ToShortDateString();
			item.Line3 = $"In: {entity.ClockInDateTimezonedValue.ToShortTimeString()}";
			item.Line4 = entity.ClockOutDate == null ? "" : $"Out: {entity.ClockOutDateTimezonedValue.ToShortTimeString()}";
			item.Tag = entity;
			item.Tag2 = color;
			item.Tag3 = entity.ClockOutDate == null ? "" : $"{timeDiffString} Hours";
			item.Tag4 = !string.IsNullOrEmpty(entity.Notes);
			item.Tag5 = !ForUser && entity.StatusValue != 0;
		}
	}
}
