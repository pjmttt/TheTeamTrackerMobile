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
    public class MyScheduleViewModel : BaseViewModel
	{
		public ObservableCollection<DisplayItem> Schedules { get; set; }
		public Command LoadSchedulesCommand { get; set; }

		public DateTime StartDate { get; set; }
		public bool NoSchedules { get; set; }

		public MyScheduleViewModel()
		{
			Schedules = new ObservableCollection<DisplayItem>();
			LoadSchedulesCommand = new Command(async () => await ExecuteLoadSchedulesCommand());
			StartDate = DateTime.Now;
			setWeekStart();
		}

		private void setWeekStart()
		{
			var usr = LoginHelper.GetLoggedInUser().User;
			while (StartDate.DayOfWeek != (DayOfWeek)usr.Company.WeekStart.GetValueOrDefault())
			{
				StartDate = StartDate.AddDays(-1);
			}
		}

		public async System.Threading.Tasks.Task TradeSchedule(Schedule schedule)
		{
			IsBusy = true;
			try
			{
				await DataService.PostItemAsync("postTrade", new
				{
					scheduleId = schedule.ScheduleId
				});
				IsBusy = false;
				await this.ExecuteLoadSchedulesCommand();
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

		private async System.Threading.Tasks.Task ExecuteLoadSchedulesCommand()
		{
			if (IsBusy)
				return;

			if (!IsRefreshing)
				IsBusy = true;

			try
			{
				//StartDateFormatted = StartDate.ToShortDateString();
				//OnPropertyChanged("StartDateFormatted");

				Schedules.Clear();
				var items = await DataService.GetItemsAsync<Schedule>($"schedules?start={StartDate.ToString("MM-dd-yyyy")}&end={StartDate.AddDays(6).ToString("MM-dd-yyyy")}&forUser=true");
				NoSchedules = !items.Data.Any();
				OnPropertyChanged("NoSchedules");
				foreach (var item in items.Data.OrderBy(d => d.ScheduleDate.GetValueOrDefault()).ThenBy(s => s.StartTime.GetValueOrDefault().TimeOfDay))
				{
					Schedules.Add(new DisplayItem()
					{
						Line1 = $"{item.ScheduleDateValue.DayOfWeek} - {item.ScheduleDateValue.ToShortDateString()}",
						Line2 = $"{item.StartTimeTimezonedValue.ToShortTimeString()} - {item.EndTimeTimezonedValue.ToShortTimeString()}",
						Line3 = $"{item.Shift.ShiftName} - {item.Task.TaskName}" + 
							(item.ScheduleTrades.Any(t => t.TradeStatusValue < (int)TRADE_STATUS.APPROVED) ? " *" : ""),
						Tag = item,
						Tag2 = string.IsNullOrEmpty(item.Task.TextColor) ? Color.LightGray : Color.FromHex(item.Task.TextColor),
					});
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
