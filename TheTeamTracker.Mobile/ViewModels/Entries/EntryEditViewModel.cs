using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.DataLayer.Classes;
using TheTeamTracker.Mobile.DataLayer.Models;
using TheTeamTracker.Mobile.DataLayer.Services;
using TheTeamTracker.Mobile.Views.Entries;
using Xamarin.Forms;

namespace TheTeamTracker.Mobile.ViewModels.Entries
{
	public class EntryEditViewModel : BaseViewModel
	{
		public const string SUCCESS = "success";
		public DataLayer.Models.Entry Entry { get; set; }
		public int EntryType { get; set; }
		public bool ForUser { get; set; }
		public bool IsEditingSubtask { get; set; }
		public EntrySubtask EditingSubtask { get; set; }
		public ObservableCollection<Shift> Shifts { get; set; }
		public ObservableCollection<Status> Statuses { get; set; }
		public ObservableCollection<Task> Tasks { get; set; }
		public ObservableCollection<User> Users { get; set; }
		public ObservableCollection<EntrySubtaskItem> EntrySubtasks { get; set; }
		public Command LoadScreenCommand;
		public Command SaveCommand { get; }
		public bool IsNotForUser { get { return !ForUser; } }
		public event EventHandler EntrySubtasksLoaded;
		public event EventHandler EntrySaved;

		private Task _task;
		public Task Task
		{
			get { return _task; }
			set
			{
				_task = value;
				Entry.Task = value;
				OnPropertyChanged("Task");
			}
		}

		private Shift _shift;
		public Shift Shift
		{
			get { return _shift; }
			set
			{
				_shift = value;
				Entry.Shift = value;
				OnPropertyChanged("Shift");
			}
		}

		private User _user;
		public User User
		{
			get { return _user; }
			set
			{
				_user = value;
				Entry.User = value;
				OnPropertyChanged("User");
			}
		}

		public bool CanChangeAll
		{
			get { return EntryType == 0 && IsNotForUser; }
		}

		public bool IsGeneral
		{
			get { return EntryType == 1; }
		}

		public bool ShowRatingEdit
		{
			get { return EntryType == 1 && IsNotForUser; }
		}

		public bool ShowRatingDisplay
		{
			get { return EntryType == 1 && ForUser; }
		}

		public int Score
		{
			get { return Entry.Task == null ? 0 : Entry.Task.DifficultyValue * Entry.RatingValue; }
		}

		public string EnteredBy
		{
			get
			{
				return Entry.EnteredBy == null ? string.Empty : Entry.EnteredBy.DisplayName;
			}
		}

		public string EntryDateFormatted
		{
			get { return Entry.EntryDateValue.ToShortDateString(); }
		}


		public string SubtaskEnteredBy
		{
			get
			{
				if (EditingSubtask == null || EditingSubtask.EnteredBy == null) return string.Empty;
				return EditingSubtask.EnteredBy.DisplayName;
			}
		}

		public EntryEditViewModel(DataLayer.Models.Entry entry, int entryType, bool forUser, DateTime entryDate)
		{
			EntryType = entryType;
			ForUser = forUser;
			Entry = entry ?? new DataLayer.Models.Entry()
			{
				EntryType = entryType,
				EntrySubtasks = new ObservableCollection<EntrySubtask>(),
				EntryDateValue = entryDate,
				CompanyIdValue = LoginHelper.GetLoggedInUser().User.CompanyIdValue
			};
			LoadScreenCommand = new Command(async () => await ExecuteLoadScreen());
			Shifts = new ObservableCollection<Shift>();
			Statuses = new ObservableCollection<Status>();
			Tasks = new ObservableCollection<Task>();
			Users = new ObservableCollection<User>();
			EntrySubtasks = new ObservableCollection<EntrySubtaskItem>();
			EditingSubtask = new EntrySubtask() { Subtask = new Subtask(), Status = new Status() };
			this.SaveCommand = new Command(async () => await this.saveEntry());
		}

		async System.Threading.Tasks.Task ExecuteLoadScreen()
		{
			if (Users.Count < 1)
			{
				if (IsBusy)
					return;

				IsBusy = true;

				try
				{
					var lookups = await DataService.GetLookups(this.EntryType);
					if (lookups.Shifts != null)
					{
						lookups.Shifts.ForEach(s => Shifts.Add(s));
						if (Entry.ShiftId != null) Shift = lookups.Shifts.FirstOrDefault(x => x.ShiftId == Entry.ShiftId);
					}
					if (lookups.Statuses != null)
					{
						lookups.Statuses.ForEach(s => Statuses.Add(s));
					}
					lookups.Tasks.ForEach(t => Tasks.Add(t));
					if (Entry.TaskId != null) Task = lookups.Tasks.FirstOrDefault(t => t.TaskId == Entry.TaskId);
					lookups.Users.ForEach(u => Users.Add(u));
					if (Entry.UserId != null) User = lookups.Users.FirstOrDefault(u => u.UserId == Entry.UserId);
					if (Entry.EnteredById == null)
					{
						var usr = AuthService.UserToken.User;
						Entry.EnteredBy = usr;
						Entry.EnteredById = usr.UserId;
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
			RefreshEntrySubtasks();

			OnPropertyChanged("Entry");
		}

		private async System.Threading.Tasks.Task saveEntry()
		{
			IsBusy = true;
			try
			{
				var forSave = EntitySaveHelper.GetEntryForSave(Entry);
				var url = EntryType == 1 ? "generalentries" : "entries";
				if (Entry.EntryId != null)
				{
					var saved = await DataService.PutItemAsync<DataLayer.Models.Entry>(url, forSave.EntryId.Value, forSave);
					Entry.EntryId = saved.EntryId;
				}
				else
				{
					var saved = await DataService.PostItemAsync<DataLayer.Models.Entry>(url, forSave);
					Entry.EntryId = saved.EntryId;
				}
				EntrySaved?.Invoke(Entry, new EventArgs());
				MessagingCenter.Send<EntryEditViewModel>(this, SUCCESS);
				IsBusy = false;
			}
			catch (Exception ex)
			{
				IsBusy = false;
				if (ex.InnerException != null)
				{
					ex = ex.InnerException;
				}
				ExceptionHelper.ShowException(ex);
			}
			finally
			{
				IsBusy = false;
			}
		}

		public void RefreshEntrySubtasks()
		{
			if (EntryType > 0) return;

			EntrySubtasks.Clear();
			if (Entry.TaskId == null) return;

			var task = Tasks.FirstOrDefault(t => t.TaskId == Entry.TaskId);
			if (task == null)
			{
				foreach (var est in Entry.EntrySubtasks)
				{
					EntrySubtasks.Add(new EntrySubtaskItem() { EntrySubtask = est });
				}
			}
			else
			{
				foreach (var st in task.Subtasks.OrderBy(st => st.Sequence))
				{
					var est = Entry.EntrySubtasks.FirstOrDefault(et => et.SubtaskId == st.SubtaskId);
					if (est == null)
					{
						est = new EntrySubtask();
						est.Subtask = st;
						est.SubtaskId = st.SubtaskId;
						Entry.EntrySubtasks.Add(est);
					}
					else if (est.EnteredBy == null && est.EnteredById != null)
					{
						est.EnteredBy = Users.FirstOrDefault(u => u.UserId == est.EnteredById);
					}
					EntrySubtasks.Add(new EntrySubtaskItem() { EntrySubtask = est });
				}
			}
			EntrySubtasksLoaded?.Invoke(this, new EventArgs());
		}

		public void RaiseScoreChanged()
		{
			if (Entry.RatingValue < 0)
			{
				Entry.RatingValue = 0;
				OnPropertyChanged("Entry");
			}
			if (Entry.RatingValue > 5)
			{
				Entry.RatingValue = 5;
				OnPropertyChanged("Entry");
			}
			OnPropertyChanged("Score");
		}
	}

	public class EntrySubtaskItem
	{
		public EntrySubtask EntrySubtask { get; set; }
		public string SubtaskNameStatus
		{
			get { return EntrySubtask.Subtask.SubtaskName + (EntrySubtask.Status != null ? " - " + EntrySubtask.Status.StatusName : ""); }
		}
		public Color BackgroundColor
		{
			get { return EntrySubtask.Status != null && !string.IsNullOrEmpty(EntrySubtask.Status.BackgroundColor) ? Color.FromHex(EntrySubtask.Status.BackgroundColor) : Color.White; }
		}
		public Color TextColor
		{
			get { return EntrySubtask.Status != null && !string.IsNullOrEmpty(EntrySubtask.Status.TextColor) ? Color.FromHex(EntrySubtask.Status.TextColor) : Color.Black; }
		}
	}
}
