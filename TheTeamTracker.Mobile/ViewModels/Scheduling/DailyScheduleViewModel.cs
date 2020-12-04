using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.DataLayer.Models;
using Xamarin.Forms;
using static TheTeamTracker.Mobile.Classes.Constants;

namespace TheTeamTracker.Mobile.ViewModels.Scheduling
{
	public class DailyScheduleViewModel : ScheduleViewModelBase
	{
		public ObservableCollection<DisplayItem> Schedules { get; set; }
		public Command LoadSchedulesCommand { get; set; }

		public DateTime ScheduleDate { get; set; }
		public string ScheduleDateFormatted { get; set; }
		public bool NoSchedules { get; set; }
		public bool CanManageSchedule { get; set; }

		public DailyScheduleViewModel()
		{
			Schedules = new ObservableCollection<DisplayItem>();
			LoadSchedulesCommand = new Command(async () => await ExecuteLoadSchedulesCommand());
			ScheduleDate = DateTime.Now;
			CanManageSchedule = LoginHelper.GetLoggedInUser().User.Roles.IndexOf((int)ROLE.SCHEDULING) >= 0;
		}

		protected override void scheduleToDisplayItem(DisplayItem displayItem, Schedule schedule)
		{
			displayItem.Line1 = schedule.User == null ? "Unassigned" : schedule.User.DisplayName;
			displayItem.Line2 = $"{schedule.StartTimeTimezonedValue.ToShortTimeString()} - {schedule.EndTimeTimezonedValue.ToShortTimeString()}";
			displayItem.Line3 = $"{schedule.Shift.ShiftName} - {schedule.Task.TaskName}";
			displayItem.Tag = schedule;
			displayItem.Tag2 = string.IsNullOrEmpty(schedule.Task.TextColor) ? Color.LightGray : Color.FromHex(schedule.Task.TextColor);
			displayItem.Tag3 = schedule.UserId == null;
			displayItem.Tag4 = schedule.UserId != null;
		}

		public override async System.Threading.Tasks.Task ExecuteLoadSchedulesCommand()
		{
			if (IsBusy)
				return;

			if (!IsRefreshing)
				IsBusy = true;

			try
			{
				if (!Users.Any())
				{
					var lookups = await DataService.GetLookups(1000);
					lookups.Users.ForEach(u => Users.Add(u));
				}

				ScheduleDateFormatted = ScheduleDate.ToShortDateString();
				OnPropertyChanged("ScheduleDateFormatted");

				Schedules.Clear();
				var items = await DataService.GetItemsAsync<Schedule>($"schedules?start={ScheduleDate.ToString("MM-dd-yyyy")}&end={ScheduleDate.ToString("MM-dd-yyyy")}{(CanManageSchedule ? "" : "&assigned=true")}");
				NoSchedules = !items.Data.Any();
				OnPropertyChanged("NoSchedules");
				this.WeekUnpublished = false;
				foreach (var item in items.Data.OrderBy(d => d.User == null ? "zzzzzz" : d.User.FirstName))
				{
					if (item.UserId != null && !item.PublishedValue) this.WeekUnpublished = true;
					var displayItem = new DisplayItem();
					scheduleToDisplayItem(displayItem, item);
					Schedules.Add(displayItem);
				}
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
	}
}
