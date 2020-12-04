using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.DataLayer.Models;
using Xamarin.Forms;

namespace TheTeamTracker.Mobile.ViewModels.Scheduling
{
	public class BulkScheduleViewModel : BaseViewModel
	{
		public const string SUCCESS = "success";

		public ObservableCollection<Shift> Shifts { get; }
		public ObservableCollection<Task> Tasks { get; }
		public ObservableCollection<User> Users { get; }

		public Task Task { get; set; }

		private Shift _shift;
		public Shift Shift
		{
			get { return _shift; }
			set
			{
				_shift = value;
				if (_startTime == null && value != null)
					StartTime = value.StartTimeTimezonedValue.TimeOfDay;

				if (_endTime == null && value != null)
					EndTime = value.EndTimeTimezonedValue.TimeOfDay;
			}
		}

		private TimeSpan? _startTime;
		public TimeSpan StartTime
		{
			get { return _startTime.GetValueOrDefault(); }
			set
			{
				_startTime = value;
				OnPropertyChanged("StartTime");
			}
		}

		private TimeSpan? _endTime;
		public TimeSpan EndTime
		{
			get { return _endTime.GetValueOrDefault(); }
			set
			{
				_endTime = value;
				OnPropertyChanged("EndTime");
			}
		}

		public User User { get; set; }

		public bool Monday { get; set; }
		public bool Tuesday { get; set; }
		public bool Wednesday { get; set; }
		public bool Thursday { get; set; }
		public bool Friday { get; set; }
		public bool Saturday { get; set; }
		public bool Sunday { get; set; }

		public Command LoadLookupsCommand { get; set; }
		public Command UpdateScheduleWeekCommand { get; set; }
		public ICommand SaveCommand { get; set; }

		public DateTime ScheduleWeek { get; set; }
		public string ScheduleWeekFormatted { get; set; }
		public bool WeekIsPublished { get; set; }

		public BulkScheduleViewModel(bool weekIsPublished)
		{
			WeekIsPublished = weekIsPublished;
			Shifts = new ObservableCollection<Shift>();
			Tasks = new ObservableCollection<Task>();
			Users = new ObservableCollection<User>();

			this.LoadLookupsCommand = new Command(async () => await loadLookups());
			this.SaveCommand = new Command(async () => await saveSchedules());
			this.UpdateScheduleWeekCommand = new Command(() =>
			{
				updateScheduleWeek();
			});
			ScheduleWeek = DateTime.Now;
			var user = LoginHelper.GetLoggedInUser().User;
			while ((int)ScheduleWeek.DayOfWeek != user.Company.WeekStartValue)
			{
				ScheduleWeek = ScheduleWeek.AddDays(-1);
			}
			updateScheduleWeek();
		}

		private void updateScheduleWeek()
		{
			ScheduleWeekFormatted = ScheduleWeek.ToShortDateString();
			OnPropertyChanged("ScheduleWeekFormatted");
		}

		private async System.Threading.Tasks.Task loadLookups()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				var lookups = await DataService.GetLookups(4);
				lookups.Shifts.ForEach(s => Shifts.Add(s));
				lookups.Tasks.ForEach(t => Tasks.Add(t));
				lookups.Users.ForEach(u => Users.Add(u));
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

		private async System.Threading.Tasks.Task saveSchedules()
		{
			if (Shift == null || Task == null || (!Monday && !Tuesday && !Wednesday && !Thursday && !Friday && !Saturday && !Sunday))
			{
				MessageHelper.ShowMessage("Please fill in required fields and select at least one day!", "Validation");
				return;
			}
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				var schedules = new List<Schedule>();
				var user = LoginHelper.GetLoggedInUser().User;

				var daysOfWeek = new List<int>();
				if (Monday) daysOfWeek.Add((int)DayOfWeek.Monday);
				if (Tuesday) daysOfWeek.Add((int)DayOfWeek.Tuesday);
				if (Wednesday) daysOfWeek.Add((int)DayOfWeek.Wednesday);
				if (Thursday) daysOfWeek.Add((int)DayOfWeek.Thursday);
				if (Friday) daysOfWeek.Add((int)DayOfWeek.Friday);
				if (Saturday) daysOfWeek.Add((int)DayOfWeek.Saturday);
				if (Sunday) daysOfWeek.Add((int)DayOfWeek.Sunday);

				foreach (var d in daysOfWeek)
				{
					var schedule = new Schedule();
					schedule.CompanyId = user.CompanyId;
					schedule.DayOfWeek = d;
					schedule.StartTimeTimezonedValue = (new DateTime(2000, 1, 1) + StartTime);
					schedule.EndTimeTimezoned = (new DateTime(2000, 1, 1) + EndTime);
					schedule.ShiftId = Shift.ShiftId;
					schedule.TaskId = Task.TaskId;
					if (User != null) schedule.UserId = User.UserId;
					var dow = d - user.Company.WeekStartValue;
					if (dow < 0)
						dow += 7;
					schedule.ScheduleDate = ScheduleWeek.AddDays(dow);
					if (User != null)
						schedule.Published = WeekIsPublished;
					else
						schedule.Published = false;
					schedules.Add(schedule);
				}

				await DataService.PostItemAsync<Schedule[]>("schedules", schedules.ToArray());

				MessagingCenter.Send<BulkScheduleViewModel>(this, SUCCESS);
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
