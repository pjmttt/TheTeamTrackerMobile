// !!!! DON'T MAKE CHANGES HERE, THEY WILL BE OVERWRITTEN !!!!
using Newtonsoft.Json;
using System;
using TheTeamTracker.Mobile.DataLayer.Classes;
namespace TheTeamTracker.Mobile.DataLayer.Models.Base
{				
	public abstract class LeaveRequestBase : EntityBase
	{
		
		private System.Guid? _ApprovedDeniedById;
		[JsonProperty(PropertyName = "approvedDeniedById", NullValueHandling = NullValueHandling.Include)]
		public virtual System.Guid? ApprovedDeniedById 
		{ 
			get { return _ApprovedDeniedById; }
			set 
			{
				_ApprovedDeniedById = value;
				OnPropertyChanged("ApprovedDeniedById");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid ApprovedDeniedByIdValue
		{
			get { return ApprovedDeniedById.GetValueOrDefault(); }
			set
			{ 
				ApprovedDeniedById = value;
				OnPropertyChanged("ApprovedDeniedByIdValue");
			}
		}
		
		private System.DateTime? _ApprovedDeniedDate;
		[JsonProperty(PropertyName = "approvedDeniedDate", NullValueHandling = NullValueHandling.Include)]
		public virtual System.DateTime? ApprovedDeniedDate 
		{ 
			get { return _ApprovedDeniedDate; }
			set 
			{
				_ApprovedDeniedDate = value;
				OnPropertyChanged("ApprovedDeniedDate");
			}
		}	
		[JsonIgnore]
		public virtual System.DateTime ApprovedDeniedDateValue
		{
			get { return ApprovedDeniedDate.GetValueOrDefault(); }
			set
			{ 
				ApprovedDeniedDate = value;
				OnPropertyChanged("ApprovedDeniedDateValue");
			}
		}
		
		private System.DateTime? _EndDate;
		[JsonProperty(PropertyName = "endDate", NullValueHandling = NullValueHandling.Include)]
		public virtual System.DateTime? EndDate 
		{ 
			get { return _EndDate; }
			set 
			{
				_EndDate = value;
				OnPropertyChanged("EndDate");
			}
		}	
		[JsonIgnore]
		public virtual System.DateTime EndDateValue
		{
			get { return EndDate.GetValueOrDefault(); }
			set
			{ 
				EndDate = value;
				OnPropertyChanged("EndDateValue");
			}
		}
		[JsonIgnore]
		public virtual System.DateTime? EndDateTimezoned
		{
			get { return EndDate == null ? (System.DateTime?)null : TimeZoneInfo.ConvertTimeFromUtc(DateTime.SpecifyKind(EndDate.Value, DateTimeKind.Unspecified), Globalization.TimeZoneInfo); }
			set
			{ 
				EndDate = value == null ? (System.DateTime?)null : TimeZoneInfo.ConvertTimeToUtc(DateTime.SpecifyKind(value.Value, DateTimeKind.Unspecified), Globalization.TimeZoneInfo);
				OnPropertyChanged("EndDateTimezoned");
			}
		}
		[JsonIgnore]
		public virtual System.DateTime EndDateTimezonedValue
		{
			get { return EndDateTimezoned.GetValueOrDefault(); }
			set
			{ 
				EndDateTimezoned = value;
				OnPropertyChanged("EndDateTimezonedValue");
			}
		}
			
		
		private System.Guid? _LeaveRequestId;
		[JsonProperty(PropertyName = "leaveRequestId", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Guid? LeaveRequestId 
		{ 
			get { return _LeaveRequestId; }
			set 
			{
				_LeaveRequestId = value;
				OnPropertyChanged("LeaveRequestId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid LeaveRequestIdValue
		{
			get { return LeaveRequestId.GetValueOrDefault(); }
			set
			{ 
				LeaveRequestId = value;
				OnPropertyChanged("LeaveRequestIdValue");
			}
		}
		
		private string _Reason;
		[JsonProperty(PropertyName = "reason", NullValueHandling = NullValueHandling.Include)]
		public virtual string Reason 
		{ 
			get { return _Reason; }
			set 
			{
				_Reason = value;
				OnPropertyChanged("Reason");
			}
		}
		
		private System.DateTime? _StartDate;
		[JsonProperty(PropertyName = "startDate", NullValueHandling = NullValueHandling.Include)]
		public virtual System.DateTime? StartDate 
		{ 
			get { return _StartDate; }
			set 
			{
				_StartDate = value;
				OnPropertyChanged("StartDate");
			}
		}	
		[JsonIgnore]
		public virtual System.DateTime StartDateValue
		{
			get { return StartDate.GetValueOrDefault(); }
			set
			{ 
				StartDate = value;
				OnPropertyChanged("StartDateValue");
			}
		}
		[JsonIgnore]
		public virtual System.DateTime? StartDateTimezoned
		{
			get { return StartDate == null ? (System.DateTime?)null : TimeZoneInfo.ConvertTimeFromUtc(DateTime.SpecifyKind(StartDate.Value, DateTimeKind.Unspecified), Globalization.TimeZoneInfo); }
			set
			{ 
				StartDate = value == null ? (System.DateTime?)null : TimeZoneInfo.ConvertTimeToUtc(DateTime.SpecifyKind(value.Value, DateTimeKind.Unspecified), Globalization.TimeZoneInfo);
				OnPropertyChanged("StartDateTimezoned");
			}
		}
		[JsonIgnore]
		public virtual System.DateTime StartDateTimezonedValue
		{
			get { return StartDateTimezoned.GetValueOrDefault(); }
			set
			{ 
				StartDateTimezoned = value;
				OnPropertyChanged("StartDateTimezonedValue");
			}
		}
			
		
		private int? _Status;
		[JsonProperty(PropertyName = "status", NullValueHandling = NullValueHandling.Include)]
		public virtual int? Status 
		{ 
			get { return _Status; }
			set 
			{
				_Status = value;
				OnPropertyChanged("Status");
			}
		}	
		[JsonIgnore]
		public virtual int StatusValue
		{
			get { return Status.GetValueOrDefault(); }
			set
			{ 
				Status = value;
				OnPropertyChanged("StatusValue");
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
		[JsonProperty(PropertyName = "userId", NullValueHandling = NullValueHandling.Include)]
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
		
		private User _User;
		[JsonProperty(PropertyName = "user", NullValueHandling = NullValueHandling.Ignore)]
		public virtual User User
		{ 
			get { return _User; }
			set
			{
				_User = value;
				if (_User != null)
				{
					this._UserId = _User.UserId;
				}
				OnPropertyChanged("User");
			}
		}
		
		private User _ApprovedDeniedBy;
		[JsonProperty(PropertyName = "approvedDeniedBy", NullValueHandling = NullValueHandling.Ignore)]
		public virtual User ApprovedDeniedBy
		{ 
			get { return _ApprovedDeniedBy; }
			set
			{
				_ApprovedDeniedBy = value;
				if (_ApprovedDeniedBy != null)
				{
					this._ApprovedDeniedById = _ApprovedDeniedBy.UserId;
				}
				OnPropertyChanged("ApprovedDeniedBy");
			}
		}
					
		internal override void resetParentsChildren()
		{
			// TODO: for parents null through
			_User = null;
			_ApprovedDeniedBy = null;
					
		}
	}
}
