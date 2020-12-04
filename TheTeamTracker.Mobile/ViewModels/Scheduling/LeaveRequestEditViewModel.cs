using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.DataLayer.Classes;
using TheTeamTracker.Mobile.DataLayer.Models;
using Xamarin.Forms;

namespace TheTeamTracker.Mobile.ViewModels.Scheduling
{
	public class LeaveRequestEditViewModel : BaseViewModel
	{
		public const string SUCCESS = "success";
		public const string APPROVE = "Approved";
		public const string DENY = "Denied";

		public const string SINGLE_DAY = "Single Day";
		public const string MULTI_DAY = "Multi Day";
		public const string PARTIAL_DAY = "Partial Day";

		public ObservableCollection<User> Users { get; }
		public ObservableCollection<string> RequestTypeOptions { get; }
		public Command SaveCommand { get; }
		public Command LoadLookupsCommand { get; }
		public bool ForUser { get; }
		public LeaveRequest LeaveRequest { get; }

		public bool CanApproveDeny
		{
			get { return LeaveRequest.StatusValue == 0; }
		}

		private string _requestType;
		public string RequestType
		{
			get { return _requestType; }
			set
			{
				_requestType = value;
				OnPropertyChanged("IsMultiDay");
				OnPropertyChanged("IsPartialDay");
			}
		}

		public DateTime StartDate
		{
			get { return LeaveRequest.StartDateTimezonedValue.Date; }
			set { LeaveRequest.StartDateTimezonedValue = value.Date + LeaveRequest.StartDateTimezonedValue.TimeOfDay; }
		}

		public TimeSpan StartTime
		{
			get { return LeaveRequest.StartDateTimezonedValue.TimeOfDay; }
			set { LeaveRequest.StartDateTimezonedValue = LeaveRequest.StartDateTimezonedValue.Date + value; }
		}

		public DateTime EndDate
		{
			get { return LeaveRequest.EndDateTimezonedValue.Date; }
			set { LeaveRequest.EndDateTimezonedValue = value.Date + LeaveRequest.EndDateTimezonedValue.TimeOfDay; }
		}

		public TimeSpan EndTime
		{
			get { return LeaveRequest.EndDateTimezonedValue.TimeOfDay; }
			set { LeaveRequest.EndDateTimezonedValue = LeaveRequest.EndDateTimezonedValue.Date + value; }
		}

		public bool ReadOnly
		{
			get { return LeaveRequest.LeaveRequestId != null; }
		}

		public bool NotReadOnly
		{
			get { return LeaveRequest.LeaveRequestId == null; }
		}

		public bool NotForUser
		{
			get { return !ForUser; }
		}

		public bool IsMultiDay
		{
			get { return RequestType == MULTI_DAY; }
		}

		public bool IsPartialDay
		{
			get { return RequestType == PARTIAL_DAY; }
		}

		public LeaveRequestEditViewModel(bool forUser, LeaveRequest leaveRequest)
		{
			Users = new ObservableCollection<User>();
			RequestTypeOptions = new ObservableCollection<string>() { SINGLE_DAY, MULTI_DAY, PARTIAL_DAY };
			ForUser = forUser;
			var user = LoginHelper.GetLoggedInUser().User;
			LeaveRequest = leaveRequest ?? new LeaveRequest() { UserId = user.UserId, Status = 0, StartDate = DateTime.Now, EndDate = DateTime.Now };
			if (leaveRequest == null && (ForUser || user.Roles.IndexOf((int)Constants.ROLE.SCHEDULING) >= 0))
			{
				LeaveRequest.Status = 1;
				LeaveRequest.ApprovedDeniedById = user.UserIdValue;
				LeaveRequest.ApprovedDeniedBy = user;
				LeaveRequest.ApprovedDeniedDate = DateTime.Now;
			}

			if (LeaveRequest.LeaveRequestId == null)
			{
				RequestType = SINGLE_DAY;
			}
			else if (assumeZero(LeaveRequest.StartDateTimezonedValue))
			{
				RequestType = LeaveRequest.EndDate == null ? SINGLE_DAY : MULTI_DAY;
			}
			else
			{
				RequestType = PARTIAL_DAY;
			}

			this.LoadLookupsCommand = new Command(async () => await loadLookups());
			this.SaveCommand = new Command(async (status) => await saveLeaveRequest(Convert.ToInt16(status)));
		}

		private bool assumeZero(DateTime dt)
		{
			return dt.Hour == 0 && dt.Minute == 0;
		}

		private async System.Threading.Tasks.Task loadLookups()
		{
			if (ForUser) return;

			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				var lookups = await DataService.GetLookups(1000);
				lookups.Users.ForEach(u => Users.Add(u));
				LeaveRequest.User = Users.FirstOrDefault(u => u.UserId == this.LeaveRequest.UserId);
			}
			catch (Exception ex)
			{
				IsBusy = false;
				ExceptionHelper.ShowException(ex);
			}
			finally
			{
				IsBusy = false;
			}
		}

		private async System.Threading.Tasks.Task saveLeaveRequest(int status)
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				var leaveRequest = EntitySaveHelper.GetLeaveRequestForSave(LeaveRequest);
				if (RequestType == SINGLE_DAY)
				{
					leaveRequest.StartDateTimezoned = leaveRequest.StartDateTimezonedValue.Date;
					leaveRequest.EndDate = null;
				}
				else if (RequestType == MULTI_DAY)
				{
					leaveRequest.StartDateTimezoned = leaveRequest.StartDateTimezonedValue.Date;
					leaveRequest.EndDateTimezoned = leaveRequest.EndDateTimezonedValue.Date;
				}

				if (!ForUser && status > 0)
				{
					var user = LoginHelper.GetLoggedInUser().User;
					leaveRequest.ApprovedDeniedById = user.UserId;
					leaveRequest.ApprovedDeniedDate = DateTime.Now;
					leaveRequest.StatusValue = status;
				}

				if (leaveRequest.LeaveRequestId == null)
					await DataService.PostItemAsync<LeaveRequest>("leaveRequests", leaveRequest);
				else
					await DataService.PutItemAsync<LeaveRequest>("leaveRequests", leaveRequest.LeaveRequestIdValue, leaveRequest);

				MessagingCenter.Send<LeaveRequestEditViewModel>(this, SUCCESS);
				IsBusy = false;
			}
			catch (Exception ex)
			{
				IsBusy = false;
				ExceptionHelper.ShowException(ex);
			}
			finally
			{
				IsBusy = false;
			}
		}

	}
}
