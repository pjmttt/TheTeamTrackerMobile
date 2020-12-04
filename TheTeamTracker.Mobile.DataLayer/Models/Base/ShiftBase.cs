// !!!! DON'T MAKE CHANGES HERE, THEY WILL BE OVERWRITTEN !!!!
using Newtonsoft.Json;
using System;
using TheTeamTracker.Mobile.DataLayer.Classes;
namespace TheTeamTracker.Mobile.DataLayer.Models.Base
{				
	public abstract class ShiftBase : EntityBase
	{
		
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
			
		
		private float? _LunchDuration;
		[JsonProperty(PropertyName = "lunchDuration", NullValueHandling = NullValueHandling.Include)]
		public virtual float? LunchDuration 
		{ 
			get { return _LunchDuration; }
			set 
			{
				_LunchDuration = value;
				OnPropertyChanged("LunchDuration");
			}
		}	
		[JsonIgnore]
		public virtual float LunchDurationValue
		{
			get { return LunchDuration.GetValueOrDefault(); }
			set
			{ 
				LunchDuration = value;
				OnPropertyChanged("LunchDurationValue");
			}
		}
		
		private System.Guid? _ShiftId;
		[JsonProperty(PropertyName = "shiftId", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Guid? ShiftId 
		{ 
			get { return _ShiftId; }
			set 
			{
				_ShiftId = value;
				OnPropertyChanged("ShiftId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid ShiftIdValue
		{
			get { return ShiftId.GetValueOrDefault(); }
			set
			{ 
				ShiftId = value;
				OnPropertyChanged("ShiftIdValue");
			}
		}
		
		private string _ShiftName;
		[JsonProperty(PropertyName = "shiftName", NullValueHandling = NullValueHandling.Include)]
		public virtual string ShiftName 
		{ 
			get { return _ShiftName; }
			set 
			{
				_ShiftName = value;
				OnPropertyChanged("ShiftName");
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
		[JsonProperty(PropertyName = "entrys", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<Entry> Entrys { get; set; }
		[JsonProperty(PropertyName = "schedules", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<Schedule> Schedules { get; set; }
		
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
					
		internal override void resetParentsChildren()
		{
			// TODO: for parents null through
			if (Entrys != null)
			{
				foreach (var child in Entrys)	
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
		
			_Company = null;
					
		}
	}
}
