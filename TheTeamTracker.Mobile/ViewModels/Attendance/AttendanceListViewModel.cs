using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.DataLayer.Models;
using Xamarin.Forms;
using Xamarin.Forms.Extended;

namespace TheTeamTracker.Mobile.ViewModels.Attendance
{
	public class AttendanceListViewModel : ListViewModel<DataLayer.Models.Attendance>
	{
		public ObservableCollection<User> Users { get; set; }
		public ObservableCollection<AttendanceReason> Reasons { get; set; }
		public bool ForUser { get; set; }
		public bool NotForUser { get { return !ForUser; } }

		public Command DeleteCommand { get; }

		protected override string endpoint => "attendance";

		public AttendanceListViewModel(bool forUser)
		{
			Users = new ObservableCollection<User>();
			Reasons = new ObservableCollection<AttendanceReason>();
			DeleteCommand = new Command(async (parameter) => await ExecuteDeleteCommand(parameter as DisplayItem));
			ForUser = forUser;
			this.sortQueue.Add(new Tuple<string, bool>("attendanceDate", true));
			this.sortFields.Add("Date", "attendanceDate");
			if (!forUser)
				this.sortFields.Add("Employee", "user");
		}

		protected override string getPostParams()
		{
			if (this.ForUser)
				return "&forUser=true";
			return base.getPostParams();
		}

		public async System.Threading.Tasks.Task ExecuteDeleteCommand(DisplayItem attendanceItem)
		{
			if (await runTask(async () => await DataService.DeleteItemAsync(endpoint, (attendanceItem.Tag as DataLayer.Models.Attendance).AttendanceIdValue),
				"Are you sure you want to delete this item?"))
			{
				this.DataSource.Remove(attendanceItem);
			}
		}

		protected override void populateDisplayItem(DisplayItem item, DataLayer.Models.Attendance attendance)
		{
			attendance.AttendanceReason = Reasons.FirstOrDefault(r => r.AttendanceReasonId == attendance.AttendanceReasonId) ?? attendance.AttendanceReason;
			attendance.User = Users.FirstOrDefault(u => u.UserId == attendance.UserId) ?? attendance.User;
			item.Line1 = attendance.AttendanceDateValue.ToShortDateString() + (ForUser ? "" : " - " + (attendance.User == null ? string.Empty : attendance.User.DisplayName));
			item.Line2 = attendance.AttendanceReason.ReasonName;
			item.Line3 = attendance.Comments;
			item.Tag = attendance;
			item.Tag2 = string.IsNullOrEmpty(attendance.AttendanceReason.BackgroundColor) ? Color.White : Color.FromHex(attendance.AttendanceReason.BackgroundColor);
			item.Tag3 = !string.IsNullOrEmpty(attendance.Comments);
		}

		protected override async System.Threading.Tasks.Task onInit()
		{
			if (Reasons.Count < 1)
			{
				var lookups = await DataService.GetLookups(3);
				lookups.AttendanceReasons.ForEach(ar => Reasons.Add(ar));
				lookups.Users.ForEach(u => Users.Add(u));
			}
		}
	}
}
