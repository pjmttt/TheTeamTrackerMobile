// !!!! DON'T MAKE CHANGES HERE, THEY WILL BE OVERWRITTEN !!!!
using Newtonsoft.Json;
using System;
using TheTeamTracker.Mobile.DataLayer.Classes;
namespace TheTeamTracker.Mobile.DataLayer.Models.Base
{				
	public abstract class UserBase : EntityBase
	{
		
		private System.Guid? _CellPhoneCarrierId;
		[JsonProperty(PropertyName = "cellPhoneCarrierId", NullValueHandling = NullValueHandling.Include)]
		public virtual System.Guid? CellPhoneCarrierId 
		{ 
			get { return _CellPhoneCarrierId; }
			set 
			{
				_CellPhoneCarrierId = value;
				OnPropertyChanged("CellPhoneCarrierId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid CellPhoneCarrierIdValue
		{
			get { return CellPhoneCarrierId.GetValueOrDefault(); }
			set
			{ 
				CellPhoneCarrierId = value;
				OnPropertyChanged("CellPhoneCarrierIdValue");
			}
		}
		
		private bool? _ClockedIn;
		[JsonProperty(PropertyName = "clockedIn", NullValueHandling = NullValueHandling.Include)]
		public virtual bool? ClockedIn 
		{ 
			get { return _ClockedIn; }
			set 
			{
				_ClockedIn = value;
				OnPropertyChanged("ClockedIn");
			}
		}	
		[JsonIgnore]
		public virtual bool ClockedInValue
		{
			get { return ClockedIn.GetValueOrDefault(); }
			set
			{ 
				ClockedIn = value;
				OnPropertyChanged("ClockedInValue");
			}
		}
		
		private System.Guid? _CompanyId;
		[JsonProperty(PropertyName = "companyId", NullValueHandling = NullValueHandling.Include)]
		public virtual System.Guid? CompanyId 
		{ 
			get { return _CompanyId; }
			set 
			{
				_CompanyId = value;
				OnPropertyChanged("CompanyId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid CompanyIdValue
		{
			get { return CompanyId.GetValueOrDefault(); }
			set
			{ 
				CompanyId = value;
				OnPropertyChanged("CompanyIdValue");
			}
		}
		
		private string _Email;
		[JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Include)]
		public virtual string Email 
		{ 
			get { return _Email; }
			set 
			{
				_Email = value;
				OnPropertyChanged("Email");
			}
		}
		
		private System.Collections.Generic.List<int> _EmailNotifications;
		[JsonProperty(PropertyName = "emailNotifications", NullValueHandling = NullValueHandling.Include)]
		public virtual System.Collections.Generic.List<int> EmailNotifications 
		{ 
			get { return _EmailNotifications; }
			set 
			{
				_EmailNotifications = value;
				OnPropertyChanged("EmailNotifications");
			}
		}
		
		private bool? _EnableEmailNotifications;
		[JsonProperty(PropertyName = "enableEmailNotifications", NullValueHandling = NullValueHandling.Include)]
		public virtual bool? EnableEmailNotifications 
		{ 
			get { return _EnableEmailNotifications; }
			set 
			{
				_EnableEmailNotifications = value;
				OnPropertyChanged("EnableEmailNotifications");
			}
		}	
		[JsonIgnore]
		public virtual bool EnableEmailNotificationsValue
		{
			get { return EnableEmailNotifications.GetValueOrDefault(); }
			set
			{ 
				EnableEmailNotifications = value;
				OnPropertyChanged("EnableEmailNotificationsValue");
			}
		}
		
		private bool? _EnableTextNotifications;
		[JsonProperty(PropertyName = "enableTextNotifications", NullValueHandling = NullValueHandling.Include)]
		public virtual bool? EnableTextNotifications 
		{ 
			get { return _EnableTextNotifications; }
			set 
			{
				_EnableTextNotifications = value;
				OnPropertyChanged("EnableTextNotifications");
			}
		}	
		[JsonIgnore]
		public virtual bool EnableTextNotificationsValue
		{
			get { return EnableTextNotifications.GetValueOrDefault(); }
			set
			{ 
				EnableTextNotifications = value;
				OnPropertyChanged("EnableTextNotificationsValue");
			}
		}
		
		private string _FirstName;
		[JsonProperty(PropertyName = "firstName", NullValueHandling = NullValueHandling.Include)]
		public virtual string FirstName 
		{ 
			get { return _FirstName; }
			set 
			{
				_FirstName = value;
				OnPropertyChanged("FirstName");
			}
		}
		
		private string _ForgotPassword;
		[JsonProperty(PropertyName = "forgotPassword", NullValueHandling = NullValueHandling.Include)]
		public virtual string ForgotPassword 
		{ 
			get { return _ForgotPassword; }
			set 
			{
				_ForgotPassword = value;
				OnPropertyChanged("ForgotPassword");
			}
		}
		
		private System.DateTime? _HireDate;
		[JsonProperty(PropertyName = "hireDate", NullValueHandling = NullValueHandling.Include)]
		public virtual System.DateTime? HireDate 
		{ 
			get { return _HireDate; }
			set 
			{
				_HireDate = value;
				OnPropertyChanged("HireDate");
			}
		}	
		[JsonIgnore]
		public virtual System.DateTime HireDateValue
		{
			get { return HireDate.GetValueOrDefault(); }
			set
			{ 
				HireDate = value;
				OnPropertyChanged("HireDateValue");
			}
		}
		
		private bool? _IsFired;
		[JsonProperty(PropertyName = "isFired", NullValueHandling = NullValueHandling.Include)]
		public virtual bool? IsFired 
		{ 
			get { return _IsFired; }
			set 
			{
				_IsFired = value;
				OnPropertyChanged("IsFired");
			}
		}	
		[JsonIgnore]
		public virtual bool IsFiredValue
		{
			get { return IsFired.GetValueOrDefault(); }
			set
			{ 
				IsFired = value;
				OnPropertyChanged("IsFiredValue");
			}
		}
		
		private System.DateTime? _LastActivity;
		[JsonProperty(PropertyName = "lastActivity", NullValueHandling = NullValueHandling.Include)]
		public virtual System.DateTime? LastActivity 
		{ 
			get { return _LastActivity; }
			set 
			{
				_LastActivity = value;
				OnPropertyChanged("LastActivity");
			}
		}	
		[JsonIgnore]
		public virtual System.DateTime LastActivityValue
		{
			get { return LastActivity.GetValueOrDefault(); }
			set
			{ 
				LastActivity = value;
				OnPropertyChanged("LastActivityValue");
			}
		}
		[JsonIgnore]
		public virtual System.DateTime? LastActivityTimezoned
		{
			get { return LastActivity == null ? (System.DateTime?)null : TimeZoneInfo.ConvertTimeFromUtc(DateTime.SpecifyKind(LastActivity.Value, DateTimeKind.Unspecified), Globalization.TimeZoneInfo); }
			set
			{ 
				LastActivity = value == null ? (System.DateTime?)null : TimeZoneInfo.ConvertTimeToUtc(DateTime.SpecifyKind(value.Value, DateTimeKind.Unspecified), Globalization.TimeZoneInfo);
				OnPropertyChanged("LastActivityTimezoned");
			}
		}
		[JsonIgnore]
		public virtual System.DateTime LastActivityTimezonedValue
		{
			get { return LastActivityTimezoned.GetValueOrDefault(); }
			set
			{ 
				LastActivityTimezoned = value;
				OnPropertyChanged("LastActivityTimezonedValue");
			}
		}
			
		
		private string _LastName;
		[JsonProperty(PropertyName = "lastName", NullValueHandling = NullValueHandling.Include)]
		public virtual string LastName 
		{ 
			get { return _LastName; }
			set 
			{
				_LastName = value;
				OnPropertyChanged("LastName");
			}
		}
		
		private System.DateTime? _LastRaiseDate;
		[JsonProperty(PropertyName = "lastRaiseDate", NullValueHandling = NullValueHandling.Include)]
		public virtual System.DateTime? LastRaiseDate 
		{ 
			get { return _LastRaiseDate; }
			set 
			{
				_LastRaiseDate = value;
				OnPropertyChanged("LastRaiseDate");
			}
		}	
		[JsonIgnore]
		public virtual System.DateTime LastRaiseDateValue
		{
			get { return LastRaiseDate.GetValueOrDefault(); }
			set
			{ 
				LastRaiseDate = value;
				OnPropertyChanged("LastRaiseDateValue");
			}
		}
		
		private System.DateTime? _LastReviewDate;
		[JsonProperty(PropertyName = "lastReviewDate", NullValueHandling = NullValueHandling.Include)]
		public virtual System.DateTime? LastReviewDate 
		{ 
			get { return _LastReviewDate; }
			set 
			{
				_LastReviewDate = value;
				OnPropertyChanged("LastReviewDate");
			}
		}	
		[JsonIgnore]
		public virtual System.DateTime LastReviewDateValue
		{
			get { return LastReviewDate.GetValueOrDefault(); }
			set
			{ 
				LastReviewDate = value;
				OnPropertyChanged("LastReviewDateValue");
			}
		}
		
		private string _MiddleName;
		[JsonProperty(PropertyName = "middleName", NullValueHandling = NullValueHandling.Include)]
		public virtual string MiddleName 
		{ 
			get { return _MiddleName; }
			set 
			{
				_MiddleName = value;
				OnPropertyChanged("MiddleName");
			}
		}
		
		private string _Notes;
		[JsonProperty(PropertyName = "notes", NullValueHandling = NullValueHandling.Include)]
		public virtual string Notes 
		{ 
			get { return _Notes; }
			set 
			{
				_Notes = value;
				OnPropertyChanged("Notes");
			}
		}
		
		private string _Password;
		[JsonProperty(PropertyName = "password", NullValueHandling = NullValueHandling.Include)]
		public virtual string Password 
		{ 
			get { return _Password; }
			set 
			{
				_Password = value;
				OnPropertyChanged("Password");
			}
		}
		
		private System.Guid? _PayRateId;
		[JsonProperty(PropertyName = "payRateId", NullValueHandling = NullValueHandling.Include)]
		public virtual System.Guid? PayRateId 
		{ 
			get { return _PayRateId; }
			set 
			{
				_PayRateId = value;
				OnPropertyChanged("PayRateId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid PayRateIdValue
		{
			get { return PayRateId.GetValueOrDefault(); }
			set
			{ 
				PayRateId = value;
				OnPropertyChanged("PayRateIdValue");
			}
		}
		
		private string _PhoneNumber;
		[JsonProperty(PropertyName = "phoneNumber", NullValueHandling = NullValueHandling.Include)]
		public virtual string PhoneNumber 
		{ 
			get { return _PhoneNumber; }
			set 
			{
				_PhoneNumber = value;
				OnPropertyChanged("PhoneNumber");
			}
		}
		
		private System.Guid? _PositionId;
		[JsonProperty(PropertyName = "positionId", NullValueHandling = NullValueHandling.Include)]
		public virtual System.Guid? PositionId 
		{ 
			get { return _PositionId; }
			set 
			{
				_PositionId = value;
				OnPropertyChanged("PositionId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid PositionIdValue
		{
			get { return PositionId.GetValueOrDefault(); }
			set
			{ 
				PositionId = value;
				OnPropertyChanged("PositionIdValue");
			}
		}
		
		private System.Collections.Generic.List<int> _Roles;
		[JsonProperty(PropertyName = "roles", NullValueHandling = NullValueHandling.Include)]
		public virtual System.Collections.Generic.List<int> Roles 
		{ 
			get { return _Roles; }
			set 
			{
				_Roles = value;
				OnPropertyChanged("Roles");
			}
		}
		
		private int? _RunningScore;
		[JsonProperty(PropertyName = "runningScore", NullValueHandling = NullValueHandling.Include)]
		public virtual int? RunningScore 
		{ 
			get { return _RunningScore; }
			set 
			{
				_RunningScore = value;
				OnPropertyChanged("RunningScore");
			}
		}	
		[JsonIgnore]
		public virtual int RunningScoreValue
		{
			get { return RunningScore.GetValueOrDefault(); }
			set
			{ 
				RunningScore = value;
				OnPropertyChanged("RunningScoreValue");
			}
		}
		
		private bool? _ShowOnSchedule;
		[JsonProperty(PropertyName = "showOnSchedule", NullValueHandling = NullValueHandling.Include)]
		public virtual bool? ShowOnSchedule 
		{ 
			get { return _ShowOnSchedule; }
			set 
			{
				_ShowOnSchedule = value;
				OnPropertyChanged("ShowOnSchedule");
			}
		}	
		[JsonIgnore]
		public virtual bool ShowOnScheduleValue
		{
			get { return ShowOnSchedule.GetValueOrDefault(); }
			set
			{ 
				ShowOnSchedule = value;
				OnPropertyChanged("ShowOnScheduleValue");
			}
		}
		
		private System.Collections.Generic.List<int> _TextNotifications;
		[JsonProperty(PropertyName = "textNotifications", NullValueHandling = NullValueHandling.Include)]
		public virtual System.Collections.Generic.List<int> TextNotifications 
		{ 
			get { return _TextNotifications; }
			set 
			{
				_TextNotifications = value;
				OnPropertyChanged("TextNotifications");
			}
		}
		
		private string _UpdatedBy;
		[JsonProperty(PropertyName = "updatedBy", NullValueHandling = NullValueHandling.Include)]
		public virtual string UpdatedBy 
		{ 
			get { return _UpdatedBy; }
			set 
			{
				_UpdatedBy = value;
				OnPropertyChanged("UpdatedBy");
			}
		}
		
		private System.Guid? _UserId;
		[JsonProperty(PropertyName = "userId", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Guid? UserId 
		{ 
			get { return _UserId; }
			set 
			{
				_UserId = value;
				OnPropertyChanged("UserId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid UserIdValue
		{
			get { return UserId.GetValueOrDefault(); }
			set
			{ 
				UserId = value;
				OnPropertyChanged("UserIdValue");
			}
		}
		
		private string _UserName;
		[JsonProperty(PropertyName = "userName", NullValueHandling = NullValueHandling.Include)]
		public virtual string UserName 
		{ 
			get { return _UserName; }
			set 
			{
				_UserName = value;
				OnPropertyChanged("UserName");
			}
		}
		
		private float? _Wage;
		[JsonProperty(PropertyName = "wage", NullValueHandling = NullValueHandling.Include)]
		public virtual float? Wage 
		{ 
			get { return _Wage; }
			set 
			{
				_Wage = value;
				OnPropertyChanged("Wage");
			}
		}	
		[JsonIgnore]
		public virtual float WageValue
		{
			get { return Wage.GetValueOrDefault(); }
			set
			{ 
				Wage = value;
				OnPropertyChanged("WageValue");
			}
		}
		[JsonProperty(PropertyName = "attendances", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<Attendance> Attendances { get; set; }
		[JsonProperty(PropertyName = "entrys", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<Entry> Entrys { get; set; }
		[JsonProperty(PropertyName = "enteredByEntrys", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<Entry> EnteredByEntrys { get; set; }
		[JsonProperty(PropertyName = "enteredByEntrySubtasks", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<EntrySubtask> EnteredByEntrySubtasks { get; set; }
		[JsonProperty(PropertyName = "enteredByInventoryTransactions", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<InventoryTransaction> EnteredByInventoryTransactions { get; set; }
		[JsonProperty(PropertyName = "approvedDeniedByLeaveRequests", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<LeaveRequest> ApprovedDeniedByLeaveRequests { get; set; }
		[JsonProperty(PropertyName = "leaveRequests", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<LeaveRequest> LeaveRequests { get; set; }
		[JsonProperty(PropertyName = "assignedToMaintenanceRequests", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<MaintenanceRequest> AssignedToMaintenanceRequests { get; set; }
		[JsonProperty(PropertyName = "requestedByMaintenanceRequests", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<MaintenanceRequest> RequestedByMaintenanceRequests { get; set; }
		[JsonProperty(PropertyName = "schedules", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<Schedule> Schedules { get; set; }
		[JsonProperty(PropertyName = "tradeUserScheduleTrades", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<ScheduleTrade> TradeUserScheduleTrades { get; set; }
		[JsonProperty(PropertyName = "approvedDeniedByUserAvailabilitys", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<UserAvailability> ApprovedDeniedByUserAvailabilitys { get; set; }
		[JsonProperty(PropertyName = "userAvailabilitys", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<UserAvailability> UserAvailabilitys { get; set; }
		[JsonProperty(PropertyName = "userClocks", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<UserClock> UserClocks { get; set; }
		[JsonProperty(PropertyName = "userComments", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<UserComment> UserComments { get; set; }
		[JsonProperty(PropertyName = "userNotes", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<UserNote> UserNotes { get; set; }
		[JsonProperty(PropertyName = "userProgressChecklists", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<UserProgressChecklist> UserProgressChecklists { get; set; }
		[JsonProperty(PropertyName = "managerUserProgressChecklists", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<UserProgressChecklist> ManagerUserProgressChecklists { get; set; }
		[JsonProperty(PropertyName = "completedByUserProgressItems", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<UserProgressItem> CompletedByUserProgressItems { get; set; }
		[JsonProperty(PropertyName = "userTaskQueues", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<UserTaskQueue> UserTaskQueues { get; set; }
		
		private CellPhoneCarrier _CellPhoneCarrier;
		[JsonProperty(PropertyName = "cellPhoneCarrier", NullValueHandling = NullValueHandling.Ignore)]
		public virtual CellPhoneCarrier CellPhoneCarrier
		{ 
			get { return _CellPhoneCarrier; }
			set
			{
				_CellPhoneCarrier = value;
				if (_CellPhoneCarrier != null)
				{
					this._CellPhoneCarrierId = _CellPhoneCarrier.CellPhoneCarrierId;
				}
				OnPropertyChanged("CellPhoneCarrier");
			}
		}
		
		private Company _Company;
		[JsonProperty(PropertyName = "company", NullValueHandling = NullValueHandling.Ignore)]
		public virtual Company Company
		{ 
			get { return _Company; }
			set
			{
				_Company = value;
				if (_Company != null)
				{
					this._CompanyId = _Company.CompanyId;
				}
				OnPropertyChanged("Company");
			}
		}
		
		private PayRate _PayRate;
		[JsonProperty(PropertyName = "payRate", NullValueHandling = NullValueHandling.Ignore)]
		public virtual PayRate PayRate
		{ 
			get { return _PayRate; }
			set
			{
				_PayRate = value;
				if (_PayRate != null)
				{
					this._PayRateId = _PayRate.PayRateId;
				}
				OnPropertyChanged("PayRate");
			}
		}
		
		private Position _Position;
		[JsonProperty(PropertyName = "position", NullValueHandling = NullValueHandling.Ignore)]
		public virtual Position Position
		{ 
			get { return _Position; }
			set
			{
				_Position = value;
				if (_Position != null)
				{
					this._PositionId = _Position.PositionId;
				}
				OnPropertyChanged("Position");
			}
		}
					
		internal override void resetParentsChildren()
		{
			// TODO: for parents null through
			if (Attendances != null)
			{
				foreach (var child in Attendances)	
				{
					child.resetParentsChildren();
				}
			}
		
			if (Entrys != null)
			{
				foreach (var child in Entrys)	
				{
					child.resetParentsChildren();
				}
			}
		
			if (EnteredByEntrys != null)
			{
				foreach (var child in EnteredByEntrys)	
				{
					child.resetParentsChildren();
				}
			}
		
			if (EnteredByEntrySubtasks != null)
			{
				foreach (var child in EnteredByEntrySubtasks)	
				{
					child.resetParentsChildren();
				}
			}
		
			if (EnteredByInventoryTransactions != null)
			{
				foreach (var child in EnteredByInventoryTransactions)	
				{
					child.resetParentsChildren();
				}
			}
		
			if (ApprovedDeniedByLeaveRequests != null)
			{
				foreach (var child in ApprovedDeniedByLeaveRequests)	
				{
					child.resetParentsChildren();
				}
			}
		
			if (LeaveRequests != null)
			{
				foreach (var child in LeaveRequests)	
				{
					child.resetParentsChildren();
				}
			}
		
			if (AssignedToMaintenanceRequests != null)
			{
				foreach (var child in AssignedToMaintenanceRequests)	
				{
					child.resetParentsChildren();
				}
			}
		
			if (RequestedByMaintenanceRequests != null)
			{
				foreach (var child in RequestedByMaintenanceRequests)	
				{
					child.resetParentsChildren();
				}
			}
		
			if (Schedules != null)
			{
				foreach (var child in Schedules)	
				{
					child.resetParentsChildren();
				}
			}
		
			if (TradeUserScheduleTrades != null)
			{
				foreach (var child in TradeUserScheduleTrades)	
				{
					child.resetParentsChildren();
				}
			}
		
			if (ApprovedDeniedByUserAvailabilitys != null)
			{
				foreach (var child in ApprovedDeniedByUserAvailabilitys)	
				{
					child.resetParentsChildren();
				}
			}
		
			if (UserAvailabilitys != null)
			{
				foreach (var child in UserAvailabilitys)	
				{
					child.resetParentsChildren();
				}
			}
		
			if (UserClocks != null)
			{
				foreach (var child in UserClocks)	
				{
					child.resetParentsChildren();
				}
			}
		
			if (UserComments != null)
			{
				foreach (var child in UserComments)	
				{
					child.resetParentsChildren();
				}
			}
		
			if (UserNotes != null)
			{
				foreach (var child in UserNotes)	
				{
					child.resetParentsChildren();
				}
			}
		
			if (UserProgressChecklists != null)
			{
				foreach (var child in UserProgressChecklists)	
				{
					child.resetParentsChildren();
				}
			}
		
			if (ManagerUserProgressChecklists != null)
			{
				foreach (var child in ManagerUserProgressChecklists)	
				{
					child.resetParentsChildren();
				}
			}
		
			if (CompletedByUserProgressItems != null)
			{
				foreach (var child in CompletedByUserProgressItems)	
				{
					child.resetParentsChildren();
				}
			}
		
			if (UserTaskQueues != null)
			{
				foreach (var child in UserTaskQueues)	
				{
					child.resetParentsChildren();
				}
			}
		
			_CellPhoneCarrier = null;
			_Company = null;
			_PayRate = null;
			_Position = null;
					
		}
	}
}
