using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.DataLayer.Classes;
using TheTeamTracker.Mobile.DataLayer.Models;
using Xamarin.Forms;
using static TheTeamTracker.Mobile.Classes.Constants;

namespace TheTeamTracker.Mobile.ViewModels.Scheduling
{
	public class ManageScheduleViewModel : ScheduleViewModelBase
	{
		public ObservableCollection<DisplayItem> Schedules { get; set; }
		public ObservableCollection<Shift> Shifts { get; set; }
		public Shift SelectedShift { get; set; }
		public ObservableCollection<Task> Tasks { get; set; }
		public Task SelectedTask { get; set; }
		public Command LoadSchedulesCommand { get; set; }
		public Command ToggleFilterCommand { get; }

		#region TOOLBAR
		public Command PublishCommand { get; set; }
		public Command DeleteSchedulesCommand { get; }
		public Command EmailCommand { get; }
		public Command TextMessageCommand { get; }
		public Command SaveTemplateCommand { get; }
		public Command LoadTemplateCommand { get; }
		public Command CopyPreviousCommand { get; }
		public Command TradeCommand { get; }
		#endregion

		public DateTime ScheduleDate { get; set; }
		public bool NoSchedules { get; set; }
		public User SelectedUser { get; set; }
		public bool ShowFilter { get; set; }
		private List<Schedule> _weekSchedules = new List<Schedule>();

		private Guid _allGuid;
		private Guid _unassignedGuid;
		private Guid _assignedGuid;

		public ManageScheduleViewModel()
		{
			Schedules = new ObservableCollection<DisplayItem>();
			Shifts = new ObservableCollection<Shift>();
			Tasks = new ObservableCollection<Task>();
			LoadSchedulesCommand = new Command(async () => await ExecuteLoadSchedulesCommand());
			DeleteSchedulesCommand = new Command(async (parameter) => await deleteSchedules((bool)parameter));
			SaveTemplateCommand = new Command(async () => await saveTemplate());
			LoadTemplateCommand = new Command(async () => await loadTemplate());
			CopyPreviousCommand = new Command(async () => await copyPrevious());
			EmailCommand = new Command(async (parameter) => await sendSchedules((bool)parameter));
			TradeCommand = new Command(async (parameter) => await tradeSchedule(parameter as DisplayItem));
			PublishCommand = new Command(async () => await publishSchedules());
			ToggleFilterCommand = new Command(() =>
			{
				this.ShowFilter = !this.ShowFilter;
				this.OnPropertyChanged("ShowFilter");
			});
			ScheduleDate = DateTime.Now;
			setWeekStart();
			_allGuid = Guid.NewGuid();
			_unassignedGuid = Guid.NewGuid();
			_assignedGuid = Guid.NewGuid();
		}

		private void setWeekStart()
		{
			var usr = LoginHelper.GetLoggedInUser().User;
			while (ScheduleDate.DayOfWeek != (DayOfWeek)usr.Company.WeekStart.GetValueOrDefault())
			{
				ScheduleDate = ScheduleDate.AddDays(-1);
			}
		}

		protected override void scheduleToDisplayItem(DisplayItem displayItem, Schedule schedule)
		{
			displayItem.Line1 = schedule.User == null ? "Unassigned" : schedule.User.DisplayName;
			displayItem.Line2 = $"{CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedDayNames[schedule.DayOfWeekValue]}, {schedule.ScheduleDateValue.ToString("MM/dd")} - {schedule.StartTimeTimezonedValue.ToShortTimeString()} - {schedule.EndTimeTimezonedValue.ToShortTimeString()}";
			displayItem.Line3 = $"{schedule.Shift.ShiftName} - {schedule.Task.TaskName}";
			displayItem.Tag = schedule;
			displayItem.Tag2 = string.IsNullOrEmpty(schedule.Task.TextColor) ? Color.LightGray : Color.FromHex(schedule.Task.TextColor);
			displayItem.Tag3 = schedule.User == null ? Color.Maroon : Color.Black;
			displayItem.Tag4 = schedule.User == null;
			displayItem.Tag5 = schedule.User != null;
			displayItem.Tag6 = schedule.User == null && !schedule.ScheduleTrades.Any();
		}

		public override async System.Threading.Tasks.Task ExecuteLoadSchedulesCommand()
		{
			await runTask(async () =>
			{
				if (!Users.Any())
				{
					Users.Add(new User() { FirstName = "(All)", UserId = _allGuid });
					Users.Add(new User() { FirstName = "(Unassigned)", UserId = _unassignedGuid });
					Users.Add(new User() { FirstName = "(Assigned)", UserId = _assignedGuid });
					var lookups = await DataService.GetLookups(4);
					lookups.Users.ForEach(u => Users.Add(u));

					Shifts.Add(new Shift() { ShiftName = "(All)", ShiftId = _allGuid });
					lookups.Shifts.ForEach(s => Shifts.Add(s));

					Tasks.Add(new Task() { TaskName = "(All)", TaskId = _allGuid });
					lookups.Tasks.ForEach(t => Tasks.Add(t));
				}

				Schedules.Clear();
				var items = await DataService.GetItemsAsync<Schedule>($"schedules?start={ScheduleDate.ToString("MM-dd-yyyy")}&end={ScheduleDate.AddDays(6).ToString("MM-dd-yyyy")}");
				NoSchedules = !items.Data.Any();
				this.WeekUnpublished = false;
				foreach (var item in items.Data.OrderBy(d => d.ScheduleDateValue).ThenBy(d => d.StartTimeValue).ThenBy(d => d.User == null ? "aaaaaaa" : d.User.FirstName))
				{
					if (item.UserId != null && !item.PublishedValue) this.WeekUnpublished = true;
					_weekSchedules = items.Data;
					if (SelectedUser != null && SelectedUser.UserIdValue != _allGuid)
					{
						if (SelectedUser.UserIdValue == _unassignedGuid && item.User != null) continue;
						if (SelectedUser.UserIdValue == _assignedGuid && item.User == null) continue;
						if (SelectedUser.UserIdValue != _assignedGuid && SelectedUser.UserIdValue != _unassignedGuid && SelectedUser.UserIdValue != item.UserIdValue) continue;
					}

					if (SelectedShift != null && SelectedShift.ShiftIdValue != _allGuid && SelectedShift.ShiftIdValue != item.ShiftIdValue)
						continue;

					if (SelectedTask != null && SelectedTask.TaskIdValue != _allGuid && SelectedTask.TaskIdValue != item.TaskIdValue)
						continue;

					var displayItem = new DisplayItem();
					scheduleToDisplayItem(displayItem, item);
					Schedules.Add(displayItem);
				}
				OnPropertyChanged("WeekUnpublished");

				return items;
			});

			OnPropertyChanged("NoSchedules");
		}

		protected override List<User> getRealUsers()
		{
			return this.Users
				.Where(u => u.UserIdValue != _allGuid && u.UserIdValue != _assignedGuid && u.UserIdValue != _unassignedGuid)
				.ToList();
		}

		private async System.Threading.Tasks.Task saveTemplate()
		{
			var result = await UserDialogs.Instance.PromptAsync("Save Template");
			if (!result.Ok || string.IsNullOrEmpty(result.Text)) return;

			var schedules = Schedules.Select(i => i.Tag as Schedule);
			var scheduleTemplate = new ScheduleTemplate() { TemplateName = result.Text };
			scheduleTemplate.Schedules = new ObservableCollection<Schedule>();
			foreach (var s in schedules)
			{
				var toSave = Common.Clone<Schedule>(s);
				toSave.ScheduleId = null;
				toSave.Shift = null;
				toSave.Task = null;
				toSave.DayOfWeek = (int)toSave.ScheduleDateValue.DayOfWeek;
				toSave.ScheduleDate = null;
				toSave.ScheduleTrades = null;
				toSave.Published = false;
				scheduleTemplate.Schedules.Add(toSave);
			}
			await runTask(async () => await DataService.PostItemAsync<ScheduleTemplate>("scheduleTemplates", scheduleTemplate));
		}

		private async System.Threading.Tasks.Task deleteSchedules(bool unscheduled)
		{
			var dt = ScheduleDate.Date;

			if (await runTask(async () =>
			{
				for (int i = 0; i < 7; i++)
				{
					await DataService.PostItemAsync("deleteSchedules", new
					{
						forDate = dt,
						unscheduled
					});
					dt = dt.AddDays(1);
				}
			}, $"Are you sure you want to delete all {(unscheduled ? "uscheduled" : "schedules?")}"))
				await ExecuteLoadSchedulesCommand();
		}

		private async System.Threading.Tasks.Task tradeSchedule(DisplayItem displayItem)
		{
			var schedule = displayItem.Tag as DataLayer.Models.Schedule;
			var result = await runTask(async () => await DataService.PostItemAsync("postTrade", new { scheduleId = schedule.ScheduleId }));
			schedule.ScheduleTrades.Add(new ScheduleTrade());
			scheduleToDisplayItem(displayItem, schedule);
			displayItem.Refresh();
		}


		private async System.Threading.Tasks.Task sendSchedules(bool isText)
		{
			var scheduleIds = this.Schedules.Select(s => s.Tag as Schedule).Where(s => s.UserId != null).Select(s => s.ScheduleIdValue);
			await runTask(async () => await DataService.PostItemAsync("sendSchedules", new
			{
				scheduleIds,
				isText
			}), "Are you sure you want to send all schedules?");
		}

		private async System.Threading.Tasks.Task loadTemplate()
		{
			var items = await runTask<Items<ScheduleTemplate>>(async () =>
				await DataService.GetItemsAsync<ScheduleTemplate>("scheduleTemplates"));
			if (items == null) return;
			var templates = items.Data;

			var selected = await UserDialogs.Instance.ActionSheetAsync("Select Template", "Cancel", "", null, templates.Select(t => t.TemplateName).ToArray());
			var template = templates.FirstOrDefault(t => t.TemplateName == selected);
			if (template == null) return;

			await runTask(async () => await DataService.PostItemAsync($"schedulesFromTemplate/{template.ScheduleTemplateIdValue}", new
			{
				forDate = ScheduleDate.Date
			}), "Are you sure you want to load template for this week?");

			await ExecuteLoadSchedulesCommand();
		}

		private async System.Threading.Tasks.Task copyPrevious()
		{
			await runTask(async () => await DataService.PostItemAsync("schedulesFromPrevious", new { forDate = ScheduleDate.Date }),
				"Are you sure you want to copy the previous week's schedule?");
			await ExecuteLoadSchedulesCommand();
		}

		private async System.Threading.Tasks.Task publishSchedules()
		{
			var scheduleIds = _weekSchedules.Where(s => s.UserId != null && !s.PublishedValue).Select(s => s.ScheduleIdValue).ToArray();
			await runTask(async () => await DataService.PostItemAsync("publishSchedules", new { scheduleIds }),
			   "Are you sure you want to publish this week's schedule?");
			await ExecuteLoadSchedulesCommand();
		}
	}
}
