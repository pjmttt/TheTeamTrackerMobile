using System;
using System.Collections.Generic;
using System.Text;

namespace TheTeamTracker.Mobile.Classes
{
	public class Constants
	{
		// copy from api
		public enum ROLE : int
		{
			MANAGER = 100,
			INVENTORY = 500,
			ADMIN = 600,
			MAINTENANCE_REQUESTS = 900,
			SCHEDULING = 1000,
		}

		public enum TRADE_STATUS : int
		{
			SUBMITTED,
			REQUESTED,
			PENDING_APPROVAL,
			APPROVED,
			DENIED,
		}

		public enum NOTIFICATION : int
		{
			DAILY_REPORT = 500,
			TRADE_REQUESTS = 600,
		}
	}
}
