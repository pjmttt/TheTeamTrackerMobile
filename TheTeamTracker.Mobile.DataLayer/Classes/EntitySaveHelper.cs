using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TheTeamTracker.Mobile.DataLayer.Models;

namespace TheTeamTracker.Mobile.DataLayer.Classes
{
	public class EntitySaveHelper
	{
		public static Entry GetEntryForSave(Entry entry)
		{
			Entry forSave = entry.GetForSave<DataLayer.Models.Entry>();
			forSave.EntrySubtasks = new ObservableCollection<EntrySubtask>();
			if (entry.EntrySubtasks != null)
			{
				foreach (var rawes in entry.EntrySubtasks.Where(es => es.Status != null))
				{
					forSave.EntrySubtasks.Add(rawes.GetForSave<EntrySubtask>());
				}
			}

			return forSave;
		}

		public static LeaveRequest GetLeaveRequestForSave(LeaveRequest leaveRequest)
		{
			LeaveRequest forSave = Common.Clone<LeaveRequest>(leaveRequest);
			if (forSave.User != null) forSave.UserId = forSave.User.UserId;
			if (forSave.ApprovedDeniedBy != null) forSave.ApprovedDeniedById = forSave.ApprovedDeniedBy.UserId;

			forSave.User = null;
			forSave.ApprovedDeniedBy = null;
			return forSave;
		}

		public static UserClock GetUserClockForSave(UserClock userClock)
		{
			UserClock forSave = Common.Clone<UserClock>(userClock);
			if (forSave.User != null) forSave.UserId = forSave.User.UserId;

			forSave.User = null;
			return forSave;
		}
	}
}
