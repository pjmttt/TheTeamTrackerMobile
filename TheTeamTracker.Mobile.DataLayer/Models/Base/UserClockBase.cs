// !!!! DON'T MAKE CHANGES HERE, THEY WILL BE OVERWRITTEN !!!!
using Newtonsoft.Json;
using System;
using TheTeamTracker.Mobile.DataLayer.Classes;
namespace TheTeamTracker.Mobile.DataLayer.Models.Base
{				
	public abstract class UserClockBase : EntityBase
	{
		
		private System.DateTime? _ClockInDate;
		[JsonProperty(PropertyName = "clockInDate", NullValueHandling = NullValueHandling.Include)]
		public virtual System.DateTime? ClockInDate 
		{ 
			get { return _ClockInDate; }
			set 
			{
				_ClockInDate = value;
				OnPropertyChanged("ClockInDate");
			}
		}	
		[JsonIgnore]
		public virtual System.DateTime ClockInDateValue
		{
			get { return ClockInDate.GetValueOrDefault(); }
			set
			{ 
				ClockInDate = value;
				OnPropertyChanged("ClockInDateValue");
			}
		}
		[JsonIgnore]
		public virtual System.DateTime? ClockInDateTimezoned
		{
			get { return ClockInDate == null ? (System.DateTime?)null : TimeZoneInfo.ConvertTimeFromUtc(DateTime.SpecifyKind(ClockInDate.Value, DateTimeKind.Unspecified), Globalization.TimeZoneInfo); }
			set
			{ 
				ClockInDate = value == null ? (System.DateTime?)null : TimeZoneInfo.ConvertTimeToUtc(DateTime.SpecifyKind(value.Value, DateTimeKind.Unspecified), Globalization.TimeZoneInfo);
				OnPropertyChanged("ClockInDateTimezoned");
			}
		}
		[JsonIgnore]
		public virtual System.DateTime ClockInDateTimezonedValue
		{
			get { return ClockInDateTimezoned.GetValueOrDefault(); }
			set
			{ 
				ClockInDateTimezoned = value;
				OnPropertyChanged("ClockInDateTimezonedValue");
			}
		}
			
		
		private System.DateTime? _ClockOutDate;
		[JsonProperty(PropertyName = "clockOutDate", NullValueHandling = NullValueHandling.Include)]
		public virtual System.DateTime? ClockOutDate 
		{ 
			get { return _ClockOutDate; }
			set 
			{
				_ClockOutDate = value;
				OnPropertyChanged("ClockOutDate");
			}
		}	
		[JsonIgnore]
		public virtual System.DateTime ClockOutDateValue
		{
			get { return ClockOutDate.GetValueOrDefault(); }
			set
			{ 
				ClockOutDate = value;
				OnPropertyChanged("ClockOutDateValue");
			}
		}
		[JsonIgnore]
		public virtual System.DateTime? ClockOutDateTimezoned
		{
			get { return ClockOutDate == null ? (System.DateTime?)null : TimeZoneInfo.ConvertTimeFromUtc(DateTime.SpecifyKind(ClockOutDate.Value, DateTimeKind.Unspecified), Globalization.TimeZoneInfo); }
			set
			{ 
				ClockOutDate = value == null ? (System.DateTime?)null : TimeZoneInfo.ConvertTimeToUtc(DateTime.SpecifyKind(value.Value, DateTimeKind.Unspecified), Globalization.TimeZoneInfo);
				OnPropertyChanged("ClockOutDateTimezoned");
			}
		}
		[JsonIgnore]
		public virtual System.DateTime ClockOutDateTimezonedValue
		{
			get { return ClockOutDateTimezoned.GetValueOrDefault(); }
			set
			{ 
				ClockOutDateTimezoned = value;
				OnPropertyChanged("ClockOutDateTimezonedValue");
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
		
		private System.Guid? _UserClockId;
		[JsonProperty(PropertyName = "userClockId", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Guid? UserClockId 
		{ 
			get { return _UserClockId; }
			set 
			{
				_UserClockId = value;
				OnPropertyChanged("UserClockId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid UserClockIdValue
		{
			get { return UserClockId.GetValueOrDefault(); }
			set
			{ 
				UserClockId = value;
				OnPropertyChanged("UserClockIdValue");
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
					
		internal override void resetParentsChildren()
		{
			// TODO: for parents null through
			_User = null;
					
		}
	}
}
