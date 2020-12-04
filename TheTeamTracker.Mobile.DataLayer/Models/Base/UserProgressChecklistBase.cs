// !!!! DON'T MAKE CHANGES HERE, THEY WILL BE OVERWRITTEN !!!!
using Newtonsoft.Json;
using System;
using TheTeamTracker.Mobile.DataLayer.Classes;
namespace TheTeamTracker.Mobile.DataLayer.Models.Base
{				
	public abstract class UserProgressChecklistBase : EntityBase
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
		
		private System.DateTime? _CompletedDate;
		[JsonProperty(PropertyName = "completedDate", NullValueHandling = NullValueHandling.Include)]
		public virtual System.DateTime? CompletedDate 
		{ 
			get { return _CompletedDate; }
			set 
			{
				_CompletedDate = value;
				OnPropertyChanged("CompletedDate");
			}
		}	
		[JsonIgnore]
		public virtual System.DateTime CompletedDateValue
		{
			get { return CompletedDate.GetValueOrDefault(); }
			set
			{ 
				CompletedDate = value;
				OnPropertyChanged("CompletedDateValue");
			}
		}
		
		private System.Guid? _ManagerId;
		[JsonProperty(PropertyName = "managerId", NullValueHandling = NullValueHandling.Include)]
		public virtual System.Guid? ManagerId 
		{ 
			get { return _ManagerId; }
			set 
			{
				_ManagerId = value;
				OnPropertyChanged("ManagerId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid ManagerIdValue
		{
			get { return ManagerId.GetValueOrDefault(); }
			set
			{ 
				ManagerId = value;
				OnPropertyChanged("ManagerIdValue");
			}
		}
		
		private System.Guid? _ProgressChecklistId;
		[JsonProperty(PropertyName = "progressChecklistId", NullValueHandling = NullValueHandling.Include)]
		public virtual System.Guid? ProgressChecklistId 
		{ 
			get { return _ProgressChecklistId; }
			set 
			{
				_ProgressChecklistId = value;
				OnPropertyChanged("ProgressChecklistId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid ProgressChecklistIdValue
		{
			get { return ProgressChecklistId.GetValueOrDefault(); }
			set
			{ 
				ProgressChecklistId = value;
				OnPropertyChanged("ProgressChecklistIdValue");
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
		
		private System.Guid? _UserProgressChecklistId;
		[JsonProperty(PropertyName = "userProgressChecklistId", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Guid? UserProgressChecklistId 
		{ 
			get { return _UserProgressChecklistId; }
			set 
			{
				_UserProgressChecklistId = value;
				OnPropertyChanged("UserProgressChecklistId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid UserProgressChecklistIdValue
		{
			get { return UserProgressChecklistId.GetValueOrDefault(); }
			set
			{ 
				UserProgressChecklistId = value;
				OnPropertyChanged("UserProgressChecklistIdValue");
			}
		}
		[JsonProperty(PropertyName = "userProgressItems", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<UserProgressItem> UserProgressItems { get; set; }
		
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
		
		private ProgressChecklist _ProgressChecklist;
		[JsonProperty(PropertyName = "progressChecklist", NullValueHandling = NullValueHandling.Ignore)]
		public virtual ProgressChecklist ProgressChecklist
		{ 
			get { return _ProgressChecklist; }
			set
			{
				_ProgressChecklist = value;
				if (_ProgressChecklist != null)
				{
					this._ProgressChecklistId = _ProgressChecklist.ProgressChecklistId;
				}
				OnPropertyChanged("ProgressChecklist");
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
		
		private User _Manager;
		[JsonProperty(PropertyName = "manager", NullValueHandling = NullValueHandling.Ignore)]
		public virtual User Manager
		{ 
			get { return _Manager; }
			set
			{
				_Manager = value;
				if (_Manager != null)
				{
					this._ManagerId = _Manager.UserId;
				}
				OnPropertyChanged("Manager");
			}
		}
					
		internal override void resetParentsChildren()
		{
			// TODO: for parents null through
			if (UserProgressItems != null)
			{
				foreach (var child in UserProgressItems)	
				{
					child.resetParentsChildren();
				}
			}
		
			_Company = null;
			_ProgressChecklist = null;
			_User = null;
			_Manager = null;
					
		}
	}
}
