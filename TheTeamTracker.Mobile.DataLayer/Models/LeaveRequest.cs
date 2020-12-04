using System;
using TheTeamTracker.Mobile.DataLayer.Models.Base;

namespace TheTeamTracker.Mobile.DataLayer.Models
{				
	public class LeaveRequest : LeaveRequestBase
	{
		public DateTime RequestedDate { get; set; }
	}
}
