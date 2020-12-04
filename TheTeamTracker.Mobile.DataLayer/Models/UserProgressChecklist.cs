using TheTeamTracker.Mobile.DataLayer.Models.Base;

namespace TheTeamTracker.Mobile.DataLayer.Models
{				
	public class UserProgressChecklist : UserProgressChecklistBase
	{
		public string StartDateFormatted
		{
			get { return StartDateValue.ToShortDateString(); }
		}

		public string CompletedDateFormatted
		{
			get { return CompletedDate == null ? string.Empty : CompletedDateValue.ToShortDateString(); }
		}
	}
}
