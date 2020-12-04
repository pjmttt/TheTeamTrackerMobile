// !!!! DON'T MAKE CHANGES HERE, THEY WILL BE OVERWRITTEN !!!!
using Newtonsoft.Json;
using System;
using TheTeamTracker.Mobile.DataLayer.Classes;
namespace TheTeamTracker.Mobile.DataLayer.Models.Base
{				
	public abstract class ScheduleBase : EntityBase
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
		
		private bool? _Published;
		[JsonProperty(PropertyName = "published", NullValueHandling = NullValueHandling.Include)]
		public virtual bool? Published 
		{ 
			get { return _Published; }
			set 
			{
				_Published = value;
				OnPropertyChanged("Published");
			}
		}	
		[JsonIgnore]
		public virtual bool PublishedValue
		{
			get { return Published.GetValueOrDefault(); }
			set
			{ 
				Published = value;
				OnPropertyChanged("PublishedValue");
			}
		}
		
		private System.DateTime? _ScheduleDate;
		[JsonProperty(PropertyName = "scheduleDate", NullValueHandling = NullValueHandling.Include)]
		public virtual System.DateTime? ScheduleDate 
		{ 
			get { return _ScheduleDate; }
			set 
			{
				_ScheduleDate = value;
				OnPropertyChanged("ScheduleDate");
			}
		}	
		[JsonIgnore]
		public virtual System.DateTime ScheduleDateValue
		{
			get { return ScheduleDate.GetValueOrDefault(); }
			set
			{ 
				ScheduleDate = value;
				OnPropertyChanged("ScheduleDateValue");
			}
		}
		
		private System.Guid? _ScheduleId;
		[JsonProperty(PropertyName = "scheduleId", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Guid? ScheduleId 
		{ 
			get { return _ScheduleId; }
			set 
			{
				_ScheduleId = value;
				OnPropertyChanged("ScheduleId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid ScheduleIdValue
		{
			get { return ScheduleId.GetValueOrDefault(); }
			set
			{ 
				ScheduleId = value;
				OnPropertyChanged("ScheduleIdValue");
			}
		}
		
		private System.Guid? _ScheduleTemplateId;
		[JsonProperty(PropertyName = "scheduleTemplateId", NullValueHandling = NullValueHandling.Include)]
		public virtual System.Guid? ScheduleTemplateId 
		{ 
			get { return _ScheduleTemplateId; }
			set 
			{
				_ScheduleTemplateId = value;
				OnPropertyChanged("ScheduleTemplateId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid ScheduleTemplateIdValue
		{
			get { return ScheduleTemplateId.GetValueOrDefault(); }
			set
			{ 
				ScheduleTemplateId = value;
				OnPropertyChanged("ScheduleTemplateIdValue");
			}
		}
		
		private System.Guid? _ShiftId;
		[JsonProperty(PropertyName = "shiftId", NullValueHandling = NullValueHandling.Include)]
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
			
		
		private System.Guid? _TaskId;
		[JsonProperty(PropertyName = "taskId", NullValueHandling = NullValueHandling.Include)]
		public virtual System.Guid? TaskId 
		{ 
			get { return _TaskId; }
			set 
			{
				_TaskId = value;
				OnPropertyChanged("TaskId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid TaskIdValue
		{
			get { return TaskId.GetValueOrDefault(); }
			set
			{ 
				TaskId = value;
				OnPropertyChanged("TaskIdValue");
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
		[JsonProperty(PropertyName = "scheduleTrades", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<ScheduleTrade> ScheduleTrades { get; set; }
		[JsonProperty(PropertyName = "tradeForScheduleScheduleTrades", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<ScheduleTrade> TradeForScheduleScheduleTrades { get; set; }
		
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
		
		private ScheduleTemplate _ScheduleTemplate;
		[JsonProperty(PropertyName = "scheduleTemplate", NullValueHandling = NullValueHandling.Ignore)]
		public virtual ScheduleTemplate ScheduleTemplate
		{ 
			get { return _ScheduleTemplate; }
			set
			{
				_ScheduleTemplate = value;
				if (_ScheduleTemplate != null)
				{
					this._ScheduleTemplateId = _ScheduleTemplate.ScheduleTemplateId;
				}
				OnPropertyChanged("ScheduleTemplate");
			}
		}
		
		private Shift _Shift;
		[JsonProperty(PropertyName = "shift", NullValueHandling = NullValueHandling.Ignore)]
		public virtual Shift Shift
		{ 
			get { return _Shift; }
			set
			{
				_Shift = value;
				if (_Shift != null)
				{
					this._ShiftId = _Shift.ShiftId;
				}
				OnPropertyChanged("Shift");
			}
		}
		
		private Task _Task;
		[JsonProperty(PropertyName = "task", NullValueHandling = NullValueHandling.Ignore)]
		public virtual Task Task
		{ 
			get { return _Task; }
			set
			{
				_Task = value;
				if (_Task != null)
				{
					this._TaskId = _Task.TaskId;
				}
				OnPropertyChanged("Task");
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
			if (ScheduleTrades != null)
			{
				foreach (var child in ScheduleTrades)	
				{
					child.resetParentsChildren();
				}
			}
		
			if (TradeForScheduleScheduleTrades != null)
			{
				foreach (var child in TradeForScheduleScheduleTrades)	
				{
					child.resetParentsChildren();
				}
			}
		
			_Company = null;
			_ScheduleTemplate = null;
			_Shift = null;
			_Task = null;
			_User = null;
					
		}
	}
}
