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
	public class TradeBoardViewModel : BaseViewModel
	{
		public ObservableCollection<DisplayItem> Schedules { get; set; }
		public Command LoadSchedulesCommand { get; set; }
		public Command DeleteCommand { get; set; }
		public Command TradeOnlyCommand { get; set; }
		public bool NoTrades { get; set; }
		public bool CanManageSchedule { get; set; }

		public DateTime StartDate { get; set; }
		public string StartDateFormatted { get; set; }

		public TradeBoardViewModel()
		{
			Schedules = new ObservableCollection<DisplayItem>();
			LoadSchedulesCommand = new Command(async () => await ExecuteLoadSchedulesCommand());
			DeleteCommand = new Command(async (parameter) => await deleteTrade(parameter as DisplayItem));
			TradeOnlyCommand = new Command(async (parameter) => await tradeOnly(parameter as DisplayItem));
			StartDate = DateTime.Now;
			setWeekStart();
			CanManageSchedule = LoginHelper.GetLoggedInUser().User.Roles.IndexOf((int)ROLE.SCHEDULING) >= 0;
		}

		private void setWeekStart()
		{
			var usr = LoginHelper.GetLoggedInUser().User;
			while (StartDate.DayOfWeek != (DayOfWeek)usr.Company.WeekStart.GetValueOrDefault())
			{
				StartDate = StartDate.AddDays(-1);
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
				StartDateFormatted = StartDate.ToShortDateString();
				OnPropertyChanged("StartDateFormatted");

				var user = AuthService.UserToken.User;
				Schedules.Clear();
				var items = await DataService.GetItemsAsync<ScheduleTrade>($"scheduleTrades?start={StartDate.ToString("MM-dd-yyyy")}&end={StartDate.AddDays(6).ToString("MM-dd-yyyy")}&status=0");
				NoTrades = !items.Data.Any();
				OnPropertyChanged("NoTrades");
				foreach (var item in items.Data.Where(d => d.Schedule.UserIdValue != user.UserIdValue).OrderBy(d => d.Schedule.ScheduleDate.GetValueOrDefault()).ThenBy(s => s.Schedule.StartTime.GetValueOrDefault().TimeOfDay))
				{
					var sched = item.Schedule;
					Schedules.Add(new DisplayItem()
					{
						Line1 = $"{sched.ScheduleDateValue.DayOfWeek} - {sched.ScheduleDateValue.ToShortDateString()}",
						Line2 = $"{sched.StartTimeTimezonedValue.ToShortTimeString()} - {sched.EndTimeTimezonedValue.ToShortTimeString()}",
						Line3 = $"{sched.Shift.ShiftName} - {sched.Task.TaskName}",
						Line4 = sched.User == null ? "" : sched.User.DisplayName,
						Tag = item,
						Tag2 = sched.User != null
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

		private async System.Threading.Tasks.Task deleteTrade(DisplayItem displayItem)
		{
			await runTask(async () => await DataService.DeleteItemAsync("scheduleTrades", (displayItem.Tag as ScheduleTrade).ScheduleTradeIdValue),
				"Are you sure you want to delete this trade?");
			this.Schedules.Remove(displayItem);
		}

		private async System.Threading.Tasks.Task tradeOnly(DisplayItem displayItem)
		{
			if (await runTask(async () =>
			{
				await DataService.PutItemAsync("requestTrade", (displayItem.Tag as ScheduleTrade).ScheduleTradeIdValue, new { });
			}, "Are you sure you want to trade this schedule?"))
				this.Schedules.Remove(displayItem);
		}

	}
}
