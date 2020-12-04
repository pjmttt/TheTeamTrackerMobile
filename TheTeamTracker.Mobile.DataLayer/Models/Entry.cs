using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using TheTeamTracker.Mobile.DataLayer.Models.Base;

namespace TheTeamTracker.Mobile.DataLayer.Models
{
	public class Entry : EntryBase
	{
		[JsonIgnore]
		public string HeaderText
		{
			get
			{
				return User == null ? "" : User.DisplayName + ": " + (Shift == null ? string.Empty : Shift.ShiftName + " - ") + (Task == null ? string.Empty : Task.TaskName) +
				  (EntryTypeValue == 1 && Task != null ? $" ({(RatingValue * Task.DifficultyValue)})" : "");
			}
		}

		private Status getBlankStatus()
		{
			return new Status { Abbreviation = "-", BackgroundColor = "#f5f5f5" };
		}

		[JsonIgnore]
		public List<Status> DistinctStatuses
		{
			get
			{
				if (EntrySubtasks == null || EntrySubtasks.Count < 1)
				{
					return new List<Status>()
					{
						getBlankStatus()
					};
				}
				return EntrySubtasks
					  .Select(es => es.Status ?? getBlankStatus())
					  .OrderBy(s => s.StatusId != null ? s.Abbreviation : "ZZZZ")
					  .GroupBy(s => s.Abbreviation)
					  .Select(g => g.First())
					  .Distinct()
					  .ToList();
			}
		}
	}
}
