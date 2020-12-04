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

namespace TheTeamTracker.Mobile.ViewModels.Scheduling
{
	public abstract class ScheduleViewModelBase : BaseViewModel
	{
		public bool WeekUnpublished { get; set; }
		public Command DeleteCommand { get; }
		public Command AssignCommand { get; }
		public Command UnassignCommand { get; }
		public ObservableCollection<User> Users { get; }

		public ScheduleViewModelBase()
		{
			DeleteCommand = new Command(async (parameter) => await deleteSchedule(parameter as Schedule));
			AssignCommand = new Command(async (parameter) => await assign(parameter as DisplayItem));
			UnassignCommand = new Command(async (parameter) => await unassign(parameter as DisplayItem));
			Users = new ObservableCollection<User>();
		}

		public abstract System.Threading.Tasks.Task ExecuteLoadSchedulesCommand();
		protected abstract void scheduleToDisplayItem(DisplayItem displayItem, Schedule schedule);

		private async System.Threading.Tasks.Task deleteSchedule(Schedule schedule)
		{
			if (await runTask(async () => await DataService.DeleteItemAsync("schedules", schedule.ScheduleIdValue),
				"Are you sure you want to delete this schedule?"))
				await ExecuteLoadSchedulesCommand();
		}

		protected virtual List<User> getRealUsers()
		{
			return this.Users.ToList();
		}

		private async System.Threading.Tasks.Task assign(DisplayItem displayItem)
		{
			var schedule = displayItem.Tag as DataLayer.Models.Schedule;
			string selected = await UserDialogs.Instance.ActionSheetAsync("Select Employee", "Cancel", "", null, getRealUsers().Select(u => u.DisplayName).ToArray());
			var usr = Users.FirstOrDefault(u => u.DisplayName == selected);
			if (usr == null) return;

			schedule.UserIdValue = usr.UserIdValue;
			schedule.User = usr;
			schedule.Published = !this.WeekUnpublished;

			var toSave = Common.Clone<Schedule>(schedule);
			toSave.ScheduleTrades = null;
			toSave.Shift = null;
			toSave.Task = null;
			toSave.User = null;

			await runTask(async () => await DataService.PutItemAsync<Schedule>("schedules", toSave.ScheduleIdValue, toSave));
			scheduleToDisplayItem(displayItem, schedule);
			displayItem.Refresh();
		}

		private async System.Threading.Tasks.Task unassign(DisplayItem displayItem)
		{
			var schedule = displayItem.Tag as DataLayer.Models.Schedule;
			schedule.User = null;
			schedule.UserId = null;
			var toSave = Common.Clone<Schedule>(schedule);
			toSave.Shift = null;
			toSave.Task = null;
			toSave.ScheduleTrades = null;

			var result = await runTask<Schedule>(async () => await DataService.PutItemAsync<Schedule>("schedules", toSave.ScheduleIdValue, toSave));
			scheduleToDisplayItem(displayItem, schedule);
			displayItem.Refresh();
		}


	}
}
