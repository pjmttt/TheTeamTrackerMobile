// !!!! DON'T MAKE CHANGES HERE, THEY WILL BE OVERWRITTEN !!!!
using Newtonsoft.Json;
using System;
using TheTeamTracker.Mobile.DataLayer.Classes;
namespace TheTeamTracker.Mobile.DataLayer.Models.Base
{				
	public abstract class UserAvailabilityBase : EntityBase
	{
		
		private bool? _AllDay;
		[JsonProperty(PropertyName = "allDay", NullValueHandling = NullValueHandling.Include)]
		public virtual bool? AllDay 
		{ 
			get { return _AllDay; }
			set 
			{
				_AllDay = value;
				OnPropertyChanged("AllDay");
			}
		}	
		[JsonIgnore]
		public virtual bool AllDayValue
		{
			get { return AllDay.GetValueOrDefault(); }
			set
			{ 
				AllDay = value;
				OnPropertyChanged("AllDayValue");
			}
		}
		
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
		
		private int? _DayOfWeek;
		[JsonProperty(PropertyName = "dayOfWeek", NullValueHandling = NullValueHandling.Include)]
		public virtual int? DayOfWeek 
		{ 
			get { return _DayOfWeek; }
			set 
			{
				_DayOfWeek = value;
				OnPropertyChanged("DayOfWeek");
			}
		}	
		[JsonIgnore]
		public virtual int DayOfWeekValue
		{
			get { return DayOfWeek.GetValueOrDefault(); }
			set
			{ 
				DayOfWeek = value;
				OnPropertyChanged("DayOfWeekValue");
			}
		}
		
		private System.DateTime? _EndTime;
		[JsonProperty(PropertyName = "endTime", NullValueHandling = NullValueHandling.Include)]
		public virtual System.DateTime? EndTime 
		{ 
			get { return _EndTime; }
			set 
			{
				_EndTime = value;
				OnPropertyChanged("EndTime");
			}
		}	
		[JsonIgnore]
		public virtual System.DateTime EndTimeValue
		{
			get { return EndTime.GetValueOrDefault(); }
			set
			{ 
				EndTime = value;
				OnPropertyChanged("EndTimeValue");
			}
		}
		[JsonIgnore]
		public virtual System.DateTime? EndTimeTimezoned
		{
			get { return EndTime == null ? (System.DateTime?)null : TimeZoneInfo.ConvertTimeFromUtc(DateTime.SpecifyKind(EndTime.Value, DateTimeKind.Unspecified), Globalization.TimeZoneInfo); }
			set
			{ 
				EndTime = value == null ? (System.DateTime?)null : TimeZoneInfo.ConvertTimeToUtc(DateTime.SpecifyKind(value.Value, DateTimeKind.Unspecified), Globalization.TimeZoneInfo);
				OnPropertyChanged("EndTimeTimezoned");
			}
		}
		[JsonIgnore]
		public virtual System.DateTime EndTimeTimezonedValue
		{
			get { return EndTimeTimezoned.GetValueOrDefault(); }
			set
			{ 
				EndTimeTimezoned = value;
				OnPropertyChanged("EndTimeTimezonedValue");
			}
		}
			
		
		private System.DateTime? _StartTime;
		[JsonProperty(PropertyName = "startTime", NullValueHandling = NullValueHandling.Include)]
		public virtual System.DateTime? StartTime 
		{ 
			get { return _StartTime; }
			set 
			{
				_StartTime = value;
				OnPropertyChanged("StartTime");
			}
		}	
		[JsonIgnore]
		public virtual System.DateTime StartTimeValue
		{
			get { return StartTime.GetValueOrDefault(); }
			set
			{ 
				StartTime = value;
				OnPropertyChanged("StartTimeValue");
			}
		}
		[JsonIgnore]
		public virtual System.DateTime? StartTimeTimezoned
		{
			get { return StartTime == null ? (System.DateTime?)null : TimeZoneInfo.ConvertTimeFromUtc(DateTime.SpecifyKind(StartTime.Value, DateTimeKind.Unspecified), Globalization.TimeZoneInfo); }
			set
			{ 
				StartTime = value == null ? (System.DateTime?)null : TimeZoneInfo.ConvertTimeToUtc(DateTime.SpecifyKind(value.Value, DateTimeKind.Unspecified), Globalization.TimeZoneInfo);
				OnPropertyChanged("StartTimeTimezoned");
			}
		}
		[JsonIgnore]
		public virtual System.DateTime StartTimeTimezonedValue
		{
			get { return StartTimeTimezoned.GetValueOrDefault(); }
			set
			{ 
				StartTimeTimezoned = value;
				OnPropertyChanged("StartTimeTimezonedValue");
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
		
		private System.Guid? _UserAvailabilityId;
		[JsonProperty(PropertyName = "userAvailabilityId", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Guid? UserAvailabilityId 
		{ 
			get { return _UserAvailabilityId; }
			set 
			{
				_UserAvailabilityId = value;
				OnPropertyChanged("UserAvailabilityId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid UserAvailabilityIdValue
		{
			get { return UserAvailabilityId.GetValueOrDefault(); }
			set
			{ 
				UserAvailabilityId = value;
				OnPropertyChanged("UserAvailabilityIdValue");
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
