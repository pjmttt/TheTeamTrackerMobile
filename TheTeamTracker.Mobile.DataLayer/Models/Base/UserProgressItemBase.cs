// !!!! DON'T MAKE CHANGES HERE, THEY WILL BE OVERWRITTEN !!!!
using Newtonsoft.Json;
using System;
using TheTeamTracker.Mobile.DataLayer.Classes;
namespace TheTeamTracker.Mobile.DataLayer.Models.Base
{				
	public abstract class UserProgressItemBase : EntityBase
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
		
		private System.Guid? _CompletedById;
		[JsonProperty(PropertyName = "completedById", NullValueHandling = NullValueHandling.Include)]
		public virtual System.Guid? CompletedById 
		{ 
			get { return _CompletedById; }
			set 
			{
				_CompletedById = value;
				OnPropertyChanged("CompletedById");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid CompletedByIdValue
		{
			get { return CompletedById.GetValueOrDefault(); }
			set
			{ 
				CompletedById = value;
				OnPropertyChanged("CompletedByIdValue");
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
		
		private System.Guid? _ProgressItemId;
		[JsonProperty(PropertyName = "progressItemId", NullValueHandling = NullValueHandling.Include)]
		public virtual System.Guid? ProgressItemId 
		{ 
			get { return _ProgressItemId; }
			set 
			{
				_ProgressItemId = value;
				OnPropertyChanged("ProgressItemId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid ProgressItemIdValue
		{
			get { return ProgressItemId.GetValueOrDefault(); }
			set
			{ 
				ProgressItemId = value;
				OnPropertyChanged("ProgressItemIdValue");
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
		
		private System.Guid? _UserProgressChecklistId;
		[JsonProperty(PropertyName = "userProgressChecklistId", NullValueHandling = NullValueHandling.Include)]
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
		
		private System.Guid? _UserProgressItemId;
		[JsonProperty(PropertyName = "userProgressItemId", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Guid? UserProgressItemId 
		{ 
			get { return _UserProgressItemId; }
			set 
			{
				_UserProgressItemId = value;
				OnPropertyChanged("UserProgressItemId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid UserProgressItemIdValue
		{
			get { return UserProgressItemId.GetValueOrDefault(); }
			set
			{ 
				UserProgressItemId = value;
				OnPropertyChanged("UserProgressItemIdValue");
			}
		}
		
		private ProgressItem _ProgressItem;
		[JsonProperty(PropertyName = "progressItem", NullValueHandling = NullValueHandling.Ignore)]
		public virtual ProgressItem ProgressItem
		{ 
			get { return _ProgressItem; }
			set
			{
				_ProgressItem = value;
				if (_ProgressItem != null)
				{
					this._ProgressItemId = _ProgressItem.ProgressItemId;
				}
				OnPropertyChanged("ProgressItem");
			}
		}
		
		private User _CompletedBy;
		[JsonProperty(PropertyName = "completedBy", NullValueHandling = NullValueHandling.Ignore)]
		public virtual User CompletedBy
		{ 
			get { return _CompletedBy; }
			set
			{
				_CompletedBy = value;
				if (_CompletedBy != null)
				{
					this._CompletedById = _CompletedBy.UserId;
				}
				OnPropertyChanged("CompletedBy");
			}
		}
		
		private UserProgressChecklist _UserProgressChecklist;
		[JsonProperty(PropertyName = "userProgressChecklist", NullValueHandling = NullValueHandling.Ignore)]
		public virtual UserProgressChecklist UserProgressChecklist
		{ 
			get { return _UserProgressChecklist; }
			set
			{
				_UserProgressChecklist = value;
				if (_UserProgressChecklist != null)
				{
					this._UserProgressChecklistId = _UserProgressChecklist.UserProgressChecklistId;
				}
				OnPropertyChanged("UserProgressChecklist");
			}
		}
					
		internal override void resetParentsChildren()
		{
			// TODO: for parents null through
			_ProgressItem = null;
			_CompletedBy = null;
			_UserProgressChecklist = null;
					
		}
	}
}
