using System;
using System.Collections.Generic;
using System.Text;
using TheTeamTracker.Mobile.DataLayer.Models;

namespace TheTeamTracker.Mobile.Classes
{
	public class Lookups
	{
		public List<Shift> Shifts { get; set; }
		public List<Task> Tasks { get; set; }
		public List<Status> Statuses { get; set; }
		public List<User> Users { get; set; }
		public List<Position> Positions { get; set; }
		public List<PayRate> PayRates { get; set; }
		public List<AttendanceReason> AttendanceReasons { get; set; }
		public List<ProgressChecklist> ProgressChecklists { get; set; }
	}
}
