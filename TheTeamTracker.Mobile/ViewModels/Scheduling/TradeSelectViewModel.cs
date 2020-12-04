using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.DataLayer.Models;
using TheTeamTracker.Mobile.DataLayer.Services;
using Xamarin.Forms;
using static TheTeamTracker.Mobile.Classes.Constants;

namespace TheTeamTracker.Mobile.ViewModels.Scheduling
{
    public class TradeSelectViewModel : BaseViewModel
	{
		public ObservableCollection<DisplayItem> Schedules { get; set; }
		public Command LoadSchedulesCommand { get; set; }
		public ScheduleTrade ScheduleTrade { get; }
		public const string SUCCESS = "success";

		public DateTime StartDate { get; set; }

		public TradeSelectViewModel(DateTime startDate, ScheduleTrade scheduleTrade)
		{
			ScheduleTrade = scheduleTrade;
			StartDate = startDate;
			Schedules = new ObservableCollection<DisplayItem>();
			LoadSchedulesCommand = new Command(async () => await ExecuteLoadSchedulesCommand());
		}

		public bool ScheduleConflicts(Schedule schedule)
		{
			var targetSchedule = ScheduleTrade.Schedule;
			var matches = Schedules.Select(s => s.Tag as Schedule)
				.Where(s => s != null && s.ScheduleId.Value != schedule.ScheduleId && s.ScheduleDateValue.Date == targetSchedule.ScheduleDateValue.Date);
			foreach (var match in matches)
			{
				int x1 = Convert.ToInt32(match.StartTimeTimezonedValue.ToString("HHmmss"));
				int x2 = Convert.ToInt32(match.EndTimeTimezonedValue.ToString("HHmmss"));
				int y1 = Convert.ToInt32(targetSchedule.StartTimeTimezonedValue.ToString("HHmmss"));
				int y2 = Convert.ToInt32(targetSchedule.EndTimeTimezonedValue.ToString("HHmmss"));

				if (Math.Max(x1, y1) <= Math.Min(x2, y2))
				{
					return true;
				}
			}
			return false;
		}

		public async System.Threading.Tasks.Task TradeSchedule(Schedule selectedSchedule)
		{
			IsBusy = true;
			try
			{
				await DataService.PutItemAsync("requestTrade", ScheduleTrade.ScheduleTradeIdValue, new
				{
					tradeForScheduleId = selectedSchedule != null ? selectedSchedule.ScheduleId : null
				});
				IsBusy = false;
				MessagingCenter.Send<TradeSelectViewModel>(this, SUCCESS);
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

            IsRefreshing = false;
            IsBusy = true;

			try
			{
				Schedules.Clear();
				Schedules.Add(new DisplayItem()
				{
					Line1 = "Trade Only",
					Line2 = ""
				});
				var items = await DataService.GetItemsAsync<Schedule>($"schedules?start={StartDate.ToString("MM-dd-yyyy")}&end={StartDate.AddDays(6).ToString("MM-dd-yyyy")}&forUser=true");
				foreach (var item in items.Data.OrderBy(d => d.ScheduleDate.GetValueOrDefault()).ThenBy(s => s.StartTime.GetValueOrDefault().TimeOfDay))
				{
					Schedules.Add(new DisplayItem()
					{
						Line1 = $"{item.ScheduleDate.GetValueOrDefault().DayOfWeek} - {item.ScheduleDate.GetValueOrDefault().ToShortDateString()} {item.StartTimeTimezonedValue.ToShortTimeString()} - {item.EndTimeTimezonedValue.ToShortTimeString()}",
						Line2 = $"{item.Shift.ShiftName} - {item.Task.TaskName}",
						Tag = item,
					});
				}
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
