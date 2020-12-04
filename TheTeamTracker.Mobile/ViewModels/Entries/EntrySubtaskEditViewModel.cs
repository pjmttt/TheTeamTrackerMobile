using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.DataLayer.Classes;
using TheTeamTracker.Mobile.DataLayer.Models;

namespace TheTeamTracker.Mobile.ViewModels.Entries
{
	public class EntrySubtaskEditViewModel : BaseViewModel
	{
		public const string SUCCESS = "success";
		public Entry Entry { get; private set; }
		public EntrySubtask EntrySubtask { get; private set; }
		public ObservableCollection<Status> Statuses { get; private set; }
		public Xamarin.Forms.Command SaveCommand { get; }

		public Status Status { get; set; }
		public bool NotIsForAll { get { return EntrySubtask != null; } }
		public string EnteredBy { get; }
		public bool Addressed { get; set; }

		public string Header => (Entry.User == null ? "" : Entry.User.DisplayName + ": ")
			+ (Entry.Shift == null ? string.Empty : Entry.Shift.ShiftName + " - ") 
			+ Entry.Task.TaskName;

		public string SubtaskName => EntrySubtask == null ? string.Empty : EntrySubtask.Subtask.SubtaskName;

		public string Comments
		{
			get { return EntrySubtask == null ? string.Empty : EntrySubtask.Comments; }
			set { if (EntrySubtask != null) EntrySubtask.Comments = value; }
		}

		
		public EntrySubtaskEditViewModel(Entry entry, EntrySubtask entrySubtask, ObservableCollection<Status> statuses)
		{
			Entry = entry;
			EntrySubtask = entrySubtask;
			Statuses = statuses;
			if (EntrySubtask != null)
			{
				if (EntrySubtask.StatusId != null) Status = Statuses.FirstOrDefault(s => s.StatusId == EntrySubtask.StatusId);
				if (EntrySubtask.EnteredById == null)
				{
					var user = LoginHelper.GetLoggedInUser();
					EntrySubtask.EnteredById = user.User.UserId;
					EntrySubtask.EnteredBy = user.User;
				}
				EnteredBy = EntrySubtask.EnteredBy.DisplayName;
				Addressed = EntrySubtask.AddressedValue;
			}

			this.SaveCommand = new Xamarin.Forms.Command(() =>
			{
				if (Status == null)
				{
					MessageHelper.ShowMessage("Status required!", "Validation");
					return;
				}
				var user = LoginHelper.GetLoggedInUser();
				if (EntrySubtask != null)
				{
					EntrySubtask.Status = Status;
					EntrySubtask.StatusId = Status.StatusId;
					EntrySubtask.Addressed = Addressed;
					EntrySubtask.EnteredBy = user.User;
					EntrySubtask.EnteredById = user.User.UserId;
					if (!Entry.EntrySubtasks.Any(es => es.SubtaskId == EntrySubtask.SubtaskId))
						Entry.EntrySubtasks.Add(EntrySubtask);
				}
				else
				{
					foreach (var st in Entry.Task.Subtasks)
					{
						var curr = Entry.EntrySubtasks.FirstOrDefault(es => es.SubtaskId == st.SubtaskId);
						if (curr == null)
						{
							curr = new EntrySubtask();
							Entry.EntrySubtasks.Add(curr);
						}
						curr.Status = Status;
						curr.StatusId = Status.StatusId;
						curr.Addressed = Addressed;
						curr.EnteredBy = user.User;
						curr.EnteredById = user.User.UserId;
					}
				}
				Xamarin.Forms.MessagingCenter.Send<EntrySubtaskEditViewModel>(this, SUCCESS);
			});
		}
	}
}
