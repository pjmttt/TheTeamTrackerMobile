// !!!! DON'T MAKE CHANGES HERE, THEY WILL BE OVERWRITTEN !!!!
using Newtonsoft.Json;
using System;
using TheTeamTracker.Mobile.DataLayer.Classes;
namespace TheTeamTracker.Mobile.DataLayer.Models.Base
{				
	public abstract class EntryBase : EntityBase
	{
		
		private string _Comments;
		[JsonProperty(PropertyName = "comments", NullValueHandling = NullValueHandling.Include)]
		public virtual string Comments 
		{ 
			get { return _Comments; }
			set 
			{
				_Comments = value;
				OnPropertyChanged("Comments");
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
		
		private System.Guid? _EnteredById;
		[JsonProperty(PropertyName = "enteredById", NullValueHandling = NullValueHandling.Include)]
		public virtual System.Guid? EnteredById 
		{ 
			get { return _EnteredById; }
			set 
			{
				_EnteredById = value;
				OnPropertyChanged("EnteredById");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid EnteredByIdValue
		{
			get { return EnteredById.GetValueOrDefault(); }
			set
			{ 
				EnteredById = value;
				OnPropertyChanged("EnteredByIdValue");
			}
		}
		
		private System.DateTime? _EntryDate;
		[JsonProperty(PropertyName = "entryDate", NullValueHandling = NullValueHandling.Include)]
		public virtual System.DateTime? EntryDate 
		{ 
			get { return _EntryDate; }
			set 
			{
				_EntryDate = value;
				OnPropertyChanged("EntryDate");
			}
		}	
		[JsonIgnore]
		public virtual System.DateTime EntryDateValue
		{
			get { return EntryDate.GetValueOrDefault(); }
			set
			{ 
				EntryDate = value;
				OnPropertyChanged("EntryDateValue");
			}
		}
		
		private System.Guid? _EntryId;
		[JsonProperty(PropertyName = "entryId", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Guid? EntryId 
		{ 
			get { return _EntryId; }
			set 
			{
				_EntryId = value;
				OnPropertyChanged("EntryId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid EntryIdValue
		{
			get { return EntryId.GetValueOrDefault(); }
			set
			{ 
				EntryId = value;
				OnPropertyChanged("EntryIdValue");
			}
		}
		
		private int? _EntryType;
		[JsonProperty(PropertyName = "entryType", NullValueHandling = NullValueHandling.Include)]
		public virtual int? EntryType 
		{ 
			get { return _EntryType; }
			set 
			{
				_EntryType = value;
				OnPropertyChanged("EntryType");
			}
		}	
		[JsonIgnore]
		public virtual int EntryTypeValue
		{
			get { return EntryType.GetValueOrDefault(); }
			set
			{ 
				EntryType = value;
				OnPropertyChanged("EntryTypeValue");
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
		
		private int? _Rating;
		[JsonProperty(PropertyName = "rating", NullValueHandling = NullValueHandling.Include)]
		public virtual int? Rating 
		{ 
			get { return _Rating; }
			set 
			{
				_Rating = value;
				OnPropertyChanged("Rating");
			}
		}	
		[JsonIgnore]
		public virtual int RatingValue
		{
			get { return Rating.GetValueOrDefault(); }
			set
			{ 
				Rating = value;
				OnPropertyChanged("RatingValue");
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
		[JsonProperty(PropertyName = "entrySubtasks", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<EntrySubtask> EntrySubtasks { get; set; }
		[JsonProperty(PropertyName = "userEntryQueues", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<UserEntryQueue> UserEntryQueues { get; set; }
		
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
		
		private User _EnteredBy;
		[JsonProperty(PropertyName = "enteredBy", NullValueHandling = NullValueHandling.Ignore)]
		public virtual User EnteredBy
		{ 
			get { return _EnteredBy; }
			set
			{
				_EnteredBy = value;
				if (_EnteredBy != null)
				{
					this._EnteredById = _EnteredBy.UserId;
				}
				OnPropertyChanged("EnteredBy");
			}
		}
					
		internal override void resetParentsChildren()
		{
			// TODO: for parents null through
			if (EntrySubtasks != null)
			{
				foreach (var child in EntrySubtasks)	
				{
					child.resetParentsChildren();
				}
			}
		
			if (UserEntryQueues != null)
			{
				foreach (var child in UserEntryQueues)	
				{
					child.resetParentsChildren();
				}
			}
		
			_Company = null;
			_Shift = null;
			_Task = null;
			_User = null;
			_EnteredBy = null;
					
		}
	}
}
