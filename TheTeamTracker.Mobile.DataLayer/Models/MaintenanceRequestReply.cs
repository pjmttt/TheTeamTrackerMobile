using Newtonsoft.Json;
using TheTeamTracker.Mobile.DataLayer.Models.Base;

namespace TheTeamTracker.Mobile.DataLayer.Models
{				
	public class MaintenanceRequestReply : MaintenanceRequestReplyBase
	{
		[JsonIgnore]
		public string ReplyDateFormatted
		{
			get { return ReplyDateValue.ToShortDateString(); }
		}
	}
}
