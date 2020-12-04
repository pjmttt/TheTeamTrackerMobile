using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.DataLayer.Models;
using Xamarin.Forms;
namespace TheTeamTracker.Mobile.ViewModels.Scheduling
{
	public class LeaveRequestsViewModel : ListViewModel<LeaveRequest>
	{
		const string DELETE = "Delete";
		public ObservableCollection<User> Users { get; }
		public Command DeleteCommand { get; }
		public Command ApproveCommand { get; }
		public Command DenyCommand { get; }
		public bool ForUser { get; }
		public List<string> MenuItems { get; }
		public bool NotForUser { get { return !ForUser; } }
		public bool PendingOnly { get; set; }
		public bool ShowFilter { get; set; }
		public User SelectedUser { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }

		protected override string endpoint => "leaveRequests";

		public LeaveRequestsViewModel(bool forUser)
		{
			Users = new ObservableCollection<User>();
			ForUser = forUser;
			DeleteCommand = new Command(async (parameter) => await ExecuteDeleteCommand(parameter as DisplayItem));
			ApproveCommand = new Command(async (parameter) => await ExecuteApproveDenyCommand(parameter as DisplayItem, true));
			DenyCommand = new Command(async (parameter) => await ExecuteApproveDenyCommand(parameter as DisplayItem, false));
			MenuItems = new List<string>() { DELETE };

			if (!forUser)
				this.sortFields.Add("Employee", "user");

			this.sortFields.Add("Start Date", "startDate");
			this.sortFields.Add("Requested Date", "created_at");
			this.sortQueue.Add(new Tuple<string, bool>("startDate", true));
		}
		protected override string getPostParams()
		{
			var postParams = "";
			if (this.ForUser)
				postParams += "&forUser=true";
			else
			{
				var whereParts = new List<string>();

				if (StartDate != null)
				{
					whereParts.Add($"startDate gte {this.StartDate.Value.ToString("MM-dd-yyyy")}");
				}
				if (EndDate != null)
				{
					var endDt = EndDate.Value.AddDays(1);
					whereParts.Add($"startDate lt {endDt.ToString("MM-dd-yyyy")}");
				}
				if (PendingOnly)
				{
					whereParts.Add("status eq 0");
				}
				if (SelectedUser != null && SelectedUser.UserIdValue != Guid.Empty)
				{
					whereParts.Add($"userId eq {SelectedUser.UserIdValue.ToString()}");
				}

				if (whereParts.Any())
				{
					postParams += "&where=" + string.Join(",", whereParts.ToArray());
				}
			}
			return postParams;
		}
		private bool assumeZero(DateTime dt)
		{
			return dt.Hour == 0 && dt.Minute == 0;
		}
		protected override void populateDisplayItem(DisplayItem item, LeaveRequest request)
		{
			var usr = ForUser ? null : request.User.DisplayName;
			var startDt = request.StartDateTimezonedValue;
			var endDt = request.EndDate == null ? (DateTime?)null : request.EndDateTimezonedValue;
			var append = request.ApprovedDeniedDate != null ? " on " + request.ApprovedDeniedDateValue.ToShortDateString() : "";
			if (request.ApprovedDeniedById != null)
				append += " by " + request.ApprovedDeniedBy.DisplayName;
			var status = "Pending";
			var color = Color.Green;
			switch (request.StatusValue)
			{
				case 1:
					status = $"Approved{append}";
					color = Color.Blue;
					break;
				case 2:
					status = $"Denied{append}";
					color = Color.Red;
					break;
			}
			item.Line1 = ForUser ? "" : usr;
			item.Line2 = startDt.ToShortDateString() + (assumeZero(startDt) ? "" : " " + startDt.ToShortTimeString()) +
						(endDt == null ? "" : " - " + (assumeZero(endDt.Value) ? endDt.Value.ToShortDateString() : "") + (assumeZero(endDt.Value) ? "" : endDt.Value.ToShortTimeString()));
			item.Line3 = $"Request on {request.RequestedDate.ToShortDateString()}";
			item.Line4 = request.Reason;
			item.Tag = request;
			item.Tag2 = color;
			item.Tag3 = !string.IsNullOrEmpty(request.Reason);
			item.Tag4 = !ForUser && request.StatusValue < 1;
		}

		public void ToggleFilter()
		{
			this.ShowFilter = !this.ShowFilter;
			OnPropertyChanged("ShowFilter");
		}
		public async System.Threading.Tasks.Task ExecuteDeleteCommand(DisplayItem displayItem)
		{
			var leaveRequest = displayItem.Tag as LeaveRequest;
			if (await runTask(async () => await DataService.DeleteItemAsync("leaveRequests", leaveRequest.LeaveRequestIdValue),
				"Are you sure you want to delete this request?"))
			{
				DataSource.Remove(displayItem);
			}
		}
		async System.Threading.Tasks.Task ExecuteApproveDenyCommand(DisplayItem displayItem, bool approve)
		{
			var leaveRequest = displayItem.Tag as LeaveRequest;
			if (await runTask(async () =>
			{
				var user = LoginHelper.GetLoggedInUser().User;
				leaveRequest.StatusValue = approve ? 1 : 2;
				leaveRequest.ApprovedDeniedDate = DateTime.Now;
				leaveRequest.ApprovedDeniedById = user.UserId;
				leaveRequest.ApprovedDeniedBy = user;
				await DataService.PutItemAsync("leaveRequests", leaveRequest.LeaveRequestIdValue, leaveRequest);
			}, $"Are you sure you want to {(approve ? "approve" : "deny")} this request?"))
			{
				populateDisplayItem(displayItem, leaveRequest);
				displayItem.Refresh();
			}
		}

		protected override async System.Threading.Tasks.Task onInit()
		{
			if (!Users.Any())
			{
				var usr = new User() { UserId = Guid.Empty, LastName = "(All)" };
				Users.Add(usr);
				var lookups = await DataService.GetLookups(1000);
				lookups.Users.ForEach(u => Users.Add(u));
			}
		}
	}
}
