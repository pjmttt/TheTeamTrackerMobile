using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.DataLayer.Classes;
using TheTeamTracker.Mobile.DataLayer.Models;
using Xamarin.Forms;

namespace TheTeamTracker.Mobile.ViewModels.Attendance
{
    public class AttendanceEditViewModel : BaseViewModel
	{
		public DataLayer.Models.Attendance Attendance { get; private set; }
		public ObservableCollection<AttendanceReason> Reasons { get; private set; }
		public ObservableCollection<User> Users { get; private set; }
		public const string SUCCESS = "success";

		public Command SaveCommand { get; }

		public AttendanceEditViewModel(List<User> users, List<AttendanceReason> reasons, DataLayer.Models.Attendance attendance)
		{
			Attendance = attendance ?? new DataLayer.Models.Attendance() { AttendanceDateValue = DateTime.Now };
			Users = new ObservableCollection<User>(users);
			Reasons = new ObservableCollection<AttendanceReason>(reasons);

			this.SaveCommand = new Command(async () => await this.saveAttendance());
		}

		private async System.Threading.Tasks.Task saveAttendance()
		{
			if (Attendance.AttendanceReason == null || Attendance.User == null)
			{
				MessageHelper.ShowMessage("Reason and user are required!", "Validation");
				return;
			}

			IsBusy = true;
			try
			{
				var toSave = Common.Clone<DataLayer.Models.Attendance>(Attendance);
				toSave.UserIdValue = Attendance.User.UserIdValue;
				toSave.AttendanceReasonIdValue = Attendance.AttendanceReason.AttendanceReasonIdValue;
				toSave.User = null;
				toSave.AttendanceReason = null;
				if (toSave.AttendanceId != null)
					await DataService.PutItemAsync<DataLayer.Models.Attendance>("attendance", toSave.AttendanceId.Value, toSave);
				else
					await DataService.PostItemAsync<DataLayer.Models.Attendance>("attendance", toSave);
				MessagingCenter.Send<AttendanceEditViewModel>(this, SUCCESS);
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
	}
}
