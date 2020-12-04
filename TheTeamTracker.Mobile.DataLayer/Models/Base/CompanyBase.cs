// !!!! DON'T MAKE CHANGES HERE, THEY WILL BE OVERWRITTEN !!!!
using Newtonsoft.Json;
using System;
using TheTeamTracker.Mobile.DataLayer.Classes;
namespace TheTeamTracker.Mobile.DataLayer.Models.Base
{				
	public abstract class CompanyBase : EntityBase
	{
		
		private string _City;
		[JsonProperty(PropertyName = "city", NullValueHandling = NullValueHandling.Include)]
		public virtual string City 
		{ 
			get { return _City; }
			set 
			{
				_City = value;
				OnPropertyChanged("City");
			}
		}
		
		private System.Guid? _CompanyId;
		[JsonProperty(PropertyName = "companyId", NullValueHandling = NullValueHandling.Ignore)]
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
		
		private string _CompanyName;
		[JsonProperty(PropertyName = "companyName", NullValueHandling = NullValueHandling.Include)]
		public virtual string CompanyName 
		{ 
			get { return _CompanyName; }
			set 
			{
				_CompanyName = value;
				OnPropertyChanged("CompanyName");
			}
		}
		
		private string _Country;
		[JsonProperty(PropertyName = "country", NullValueHandling = NullValueHandling.Include)]
		public virtual string Country 
		{ 
			get { return _Country; }
			set 
			{
				_Country = value;
				OnPropertyChanged("Country");
			}
		}
		
		private System.DateTime? _ExpirationDate;
		[JsonProperty(PropertyName = "expirationDate", NullValueHandling = NullValueHandling.Include)]
		public virtual System.DateTime? ExpirationDate 
		{ 
			get { return _ExpirationDate; }
			set 
			{
				_ExpirationDate = value;
				OnPropertyChanged("ExpirationDate");
			}
		}	
		[JsonIgnore]
		public virtual System.DateTime ExpirationDateValue
		{
			get { return ExpirationDate.GetValueOrDefault(); }
			set
			{ 
				ExpirationDate = value;
				OnPropertyChanged("ExpirationDateValue");
			}
		}
		[JsonIgnore]
		public virtual System.DateTime? ExpirationDateTimezoned
		{
			get { return ExpirationDate == null ? (System.DateTime?)null : TimeZoneInfo.ConvertTimeFromUtc(DateTime.SpecifyKind(ExpirationDate.Value, DateTimeKind.Unspecified), Globalization.TimeZoneInfo); }
			set
			{ 
				ExpirationDate = value == null ? (System.DateTime?)null : TimeZoneInfo.ConvertTimeToUtc(DateTime.SpecifyKind(value.Value, DateTimeKind.Unspecified), Globalization.TimeZoneInfo);
				OnPropertyChanged("ExpirationDateTimezoned");
			}
		}
		[JsonIgnore]
		public virtual System.DateTime ExpirationDateTimezonedValue
		{
			get { return ExpirationDateTimezoned.GetValueOrDefault(); }
			set
			{ 
				ExpirationDateTimezoned = value;
				OnPropertyChanged("ExpirationDateTimezonedValue");
			}
		}
			
		
		private string _GeoLocation;
		[JsonProperty(PropertyName = "geoLocation", NullValueHandling = NullValueHandling.Include)]
		public virtual string GeoLocation 
		{ 
			get { return _GeoLocation; }
			set 
			{
				_GeoLocation = value;
				OnPropertyChanged("GeoLocation");
			}
		}
		
		private string _IpAddress;
		[JsonProperty(PropertyName = "ipAddress", NullValueHandling = NullValueHandling.Include)]
		public virtual string IpAddress 
		{ 
			get { return _IpAddress; }
			set 
			{
				_IpAddress = value;
				OnPropertyChanged("IpAddress");
			}
		}
		
		private int? _MinClockDistance;
		[JsonProperty(PropertyName = "minClockDistance", NullValueHandling = NullValueHandling.Include)]
		public virtual int? MinClockDistance 
		{ 
			get { return _MinClockDistance; }
			set 
			{
				_MinClockDistance = value;
				OnPropertyChanged("MinClockDistance");
			}
		}	
		[JsonIgnore]
		public virtual int MinClockDistanceValue
		{
			get { return MinClockDistance.GetValueOrDefault(); }
			set
			{ 
				MinClockDistance = value;
				OnPropertyChanged("MinClockDistanceValue");
			}
		}
		
		private int? _MinutesBeforeLate;
		[JsonProperty(PropertyName = "minutesBeforeLate", NullValueHandling = NullValueHandling.Include)]
		public virtual int? MinutesBeforeLate 
		{ 
			get { return _MinutesBeforeLate; }
			set 
			{
				_MinutesBeforeLate = value;
				OnPropertyChanged("MinutesBeforeLate");
			}
		}	
		[JsonIgnore]
		public virtual int MinutesBeforeLateValue
		{
			get { return MinutesBeforeLate.GetValueOrDefault(); }
			set
			{ 
				MinutesBeforeLate = value;
				OnPropertyChanged("MinutesBeforeLateValue");
			}
		}
		
		private string _PostalCode;
		[JsonProperty(PropertyName = "postalCode", NullValueHandling = NullValueHandling.Include)]
		public virtual string PostalCode 
		{ 
			get { return _PostalCode; }
			set 
			{
				_PostalCode = value;
				OnPropertyChanged("PostalCode");
			}
		}
		
		private string _PromoCode;
		[JsonProperty(PropertyName = "promoCode", NullValueHandling = NullValueHandling.Include)]
		public virtual string PromoCode 
		{ 
			get { return _PromoCode; }
			set 
			{
				_PromoCode = value;
				OnPropertyChanged("PromoCode");
			}
		}
		
		private string _StateProvince;
		[JsonProperty(PropertyName = "stateProvince", NullValueHandling = NullValueHandling.Include)]
		public virtual string StateProvince 
		{ 
			get { return _StateProvince; }
			set 
			{
				_StateProvince = value;
				OnPropertyChanged("StateProvince");
			}
		}
		
		private string _StreetAddress1;
		[JsonProperty(PropertyName = "streetAddress1", NullValueHandling = NullValueHandling.Include)]
		public virtual string StreetAddress1 
		{ 
			get { return _StreetAddress1; }
			set 
			{
				_StreetAddress1 = value;
				OnPropertyChanged("StreetAddress1");
			}
		}
		
		private string _StreetAddress2;
		[JsonProperty(PropertyName = "streetAddress2", NullValueHandling = NullValueHandling.Include)]
		public virtual string StreetAddress2 
		{ 
			get { return _StreetAddress2; }
			set 
			{
				_StreetAddress2 = value;
				OnPropertyChanged("StreetAddress2");
			}
		}
		
		private int? _SubscriptionDuration;
		[JsonProperty(PropertyName = "subscriptionDuration", NullValueHandling = NullValueHandling.Include)]
		public virtual int? SubscriptionDuration 
		{ 
			get { return _SubscriptionDuration; }
			set 
			{
				_SubscriptionDuration = value;
				OnPropertyChanged("SubscriptionDuration");
			}
		}	
		[JsonIgnore]
		public virtual int SubscriptionDurationValue
		{
			get { return SubscriptionDuration.GetValueOrDefault(); }
			set
			{ 
				SubscriptionDuration = value;
				OnPropertyChanged("SubscriptionDurationValue");
			}
		}
		
		private System.Guid? _SubscriptionRequestNumber;
		[JsonProperty(PropertyName = "subscriptionRequestNumber", NullValueHandling = NullValueHandling.Include)]
		public virtual System.Guid? SubscriptionRequestNumber 
		{ 
			get { return _SubscriptionRequestNumber; }
			set 
			{
				_SubscriptionRequestNumber = value;
				OnPropertyChanged("SubscriptionRequestNumber");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid SubscriptionRequestNumberValue
		{
			get { return SubscriptionRequestNumber.GetValueOrDefault(); }
			set
			{ 
				SubscriptionRequestNumber = value;
				OnPropertyChanged("SubscriptionRequestNumberValue");
			}
		}
		
		private int? _SubscriptionTier;
		[JsonProperty(PropertyName = "subscriptionTier", NullValueHandling = NullValueHandling.Include)]
		public virtual int? SubscriptionTier 
		{ 
			get { return _SubscriptionTier; }
			set 
			{
				_SubscriptionTier = value;
				OnPropertyChanged("SubscriptionTier");
			}
		}	
		[JsonIgnore]
		public virtual int SubscriptionTierValue
		{
			get { return SubscriptionTier.GetValueOrDefault(); }
			set
			{ 
				SubscriptionTier = value;
				OnPropertyChanged("SubscriptionTierValue");
			}
		}
		
		private string _SubscriptionTransactionNumber;
		[JsonProperty(PropertyName = "subscriptionTransactionNumber", NullValueHandling = NullValueHandling.Include)]
		public virtual string SubscriptionTransactionNumber 
		{ 
			get { return _SubscriptionTransactionNumber; }
			set 
			{
				_SubscriptionTransactionNumber = value;
				OnPropertyChanged("SubscriptionTransactionNumber");
			}
		}
		
		private string _Timezone;
		[JsonProperty(PropertyName = "timezone", NullValueHandling = NullValueHandling.Include)]
		public virtual string Timezone 
		{ 
			get { return _Timezone; }
			set 
			{
				_Timezone = value;
				OnPropertyChanged("Timezone");
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
		
		private int? _WeekStart;
		[JsonProperty(PropertyName = "weekStart", NullValueHandling = NullValueHandling.Include)]
		public virtual int? WeekStart 
		{ 
			get { return _WeekStart; }
			set 
			{
				_WeekStart = value;
				OnPropertyChanged("WeekStart");
			}
		}	
		[JsonIgnore]
		public virtual int WeekStartValue
		{
			get { return WeekStart.GetValueOrDefault(); }
			set
			{ 
				WeekStart = value;
				OnPropertyChanged("WeekStartValue");
			}
		}
		[JsonProperty(PropertyName = "attendances", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<Attendance> Attendances { get; set; }
		[JsonProperty(PropertyName = "attendanceReasons", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<AttendanceReason> AttendanceReasons { get; set; }
		[JsonProperty(PropertyName = "cellPhoneCarriers", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<CellPhoneCarrier> CellPhoneCarriers { get; set; }
		[JsonProperty(PropertyName = "documents", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<Document> Documents { get; set; }
		[JsonProperty(PropertyName = "emailTemplates", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<EmailTemplate> EmailTemplates { get; set; }
		[JsonProperty(PropertyName = "entrys", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<Entry> Entrys { get; set; }
		[JsonProperty(PropertyName = "inventoryCategorys", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<InventoryCategory> InventoryCategorys { get; set; }
		[JsonProperty(PropertyName = "inventoryItems", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<InventoryItem> InventoryItems { get; set; }
		[JsonProperty(PropertyName = "maintenanceRequests", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<MaintenanceRequest> MaintenanceRequests { get; set; }
		[JsonProperty(PropertyName = "payRates", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<PayRate> PayRates { get; set; }
		[JsonProperty(PropertyName = "positions", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<Position> Positions { get; set; }
		[JsonProperty(PropertyName = "progressChecklists", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<ProgressChecklist> ProgressChecklists { get; set; }
		[JsonProperty(PropertyName = "schedules", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<Schedule> Schedules { get; set; }
		[JsonProperty(PropertyName = "scheduleTemplates", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<ScheduleTemplate> ScheduleTemplates { get; set; }
		[JsonProperty(PropertyName = "shifts", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<Shift> Shifts { get; set; }
		[JsonProperty(PropertyName = "statuss", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<Status> Statuss { get; set; }
		[JsonProperty(PropertyName = "tasks", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<Task> Tasks { get; set; }
		[JsonProperty(PropertyName = "users", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<User> Users { get; set; }
		[JsonProperty(PropertyName = "userComments", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<UserComment> UserComments { get; set; }
		[JsonProperty(PropertyName = "userProgressChecklists", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<UserProgressChecklist> UserProgressChecklists { get; set; }
					
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
		
			if (AttendanceReasons != null)
			{
				foreach (var child in AttendanceReasons)	
				{
					child.resetParentsChildren();
				}
			}
		
			if (CellPhoneCarriers != null)
			{
				foreach (var child in CellPhoneCarriers)	
				{
					child.resetParentsChildren();
				}
			}
		
			if (Documents != null)
			{
				foreach (var child in Documents)	
				{
					child.resetParentsChildren();
				}
			}
		
			if (EmailTemplates != null)
			{
				foreach (var child in EmailTemplates)	
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
		
			if (InventoryCategorys != null)
			{
				foreach (var child in InventoryCategorys)	
				{
					child.resetParentsChildren();
				}
			}
		
			if (InventoryItems != null)
			{
				foreach (var child in InventoryItems)	
				{
					child.resetParentsChildren();
				}
			}
		
			if (MaintenanceRequests != null)
			{
				foreach (var child in MaintenanceRequests)	
				{
					child.resetParentsChildren();
				}
			}
		
			if (PayRates != null)
			{
				foreach (var child in PayRates)	
				{
					child.resetParentsChildren();
				}
			}
		
			if (Positions != null)
			{
				foreach (var child in Positions)	
				{
					child.resetParentsChildren();
				}
			}
		
			if (ProgressChecklists != null)
			{
				foreach (var child in ProgressChecklists)	
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
		
			if (ScheduleTemplates != null)
			{
				foreach (var child in ScheduleTemplates)	
				{
					child.resetParentsChildren();
				}
			}
		
			if (Shifts != null)
			{
				foreach (var child in Shifts)	
				{
					child.resetParentsChildren();
				}
			}
		
			if (Statuss != null)
			{
				foreach (var child in Statuss)	
				{
					child.resetParentsChildren();
				}
			}
		
			if (Tasks != null)
			{
				foreach (var child in Tasks)	
				{
					child.resetParentsChildren();
				}
			}
		
			if (Users != null)
			{
				foreach (var child in Users)	
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
		
			if (UserProgressChecklists != null)
			{
				foreach (var child in UserProgressChecklists)	
				{
					child.resetParentsChildren();
				}
			}
		
					
		}
	}
}
