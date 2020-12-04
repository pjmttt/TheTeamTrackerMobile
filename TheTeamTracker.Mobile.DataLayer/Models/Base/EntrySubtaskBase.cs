// !!!! DON'T MAKE CHANGES HERE, THEY WILL BE OVERWRITTEN !!!!
using Newtonsoft.Json;
using System;
using TheTeamTracker.Mobile.DataLayer.Classes;
namespace TheTeamTracker.Mobile.DataLayer.Models.Base
{				
	public abstract class EntrySubtaskBase : EntityBase
	{
		
		private bool? _Addressed;
		[JsonProperty(PropertyName = "addressed", NullValueHandling = NullValueHandling.Include)]
		public virtual bool? Addressed 
		{ 
			get { return _Addressed; }
			set 
			{
				_Addressed = value;
				OnPropertyChanged("Addressed");
			}
		}	
		[JsonIgnore]
		public virtual bool AddressedValue
		{
			get { return Addressed.GetValueOrDefault(); }
			set
			{ 
				Addressed = value;
				OnPropertyChanged("AddressedValue");
			}
		}
		
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
		
		private System.Guid? _EntryId;
		[JsonProperty(PropertyName = "entryId", NullValueHandling = NullValueHandling.Include)]
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
		
		private System.Guid? _EntrySubtaskId;
		[JsonProperty(PropertyName = "entrySubtaskId", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Guid? EntrySubtaskId 
		{ 
			get { return _EntrySubtaskId; }
			set 
			{
				_EntrySubtaskId = value;
				OnPropertyChanged("EntrySubtaskId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid EntrySubtaskIdValue
		{
			get { return EntrySubtaskId.GetValueOrDefault(); }
			set
			{ 
				EntrySubtaskId = value;
				OnPropertyChanged("EntrySubtaskIdValue");
			}
		}
		
		private System.Guid? _StatusId;
		[JsonProperty(PropertyName = "statusId", NullValueHandling = NullValueHandling.Include)]
		public virtual System.Guid? StatusId 
		{ 
			get { return _StatusId; }
			set 
			{
				_StatusId = value;
				OnPropertyChanged("StatusId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid StatusIdValue
		{
			get { return StatusId.GetValueOrDefault(); }
			set
			{ 
				StatusId = value;
				OnPropertyChanged("StatusIdValue");
			}
		}
		
		private System.Guid? _SubtaskId;
		[JsonProperty(PropertyName = "subtaskId", NullValueHandling = NullValueHandling.Include)]
		public virtual System.Guid? SubtaskId 
		{ 
			get { return _SubtaskId; }
			set 
			{
				_SubtaskId = value;
				OnPropertyChanged("SubtaskId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid SubtaskIdValue
		{
			get { return SubtaskId.GetValueOrDefault(); }
			set
			{ 
				SubtaskId = value;
				OnPropertyChanged("SubtaskIdValue");
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
		
		private Entry _Entry;
		[JsonProperty(PropertyName = "entry", NullValueHandling = NullValueHandling.Ignore)]
		public virtual Entry Entry
		{ 
			get { return _Entry; }
			set
			{
				_Entry = value;
				if (_Entry != null)
				{
					this._EntryId = _Entry.EntryId;
				}
				OnPropertyChanged("Entry");
			}
		}
		
		private Status _Status;
		[JsonProperty(PropertyName = "status", NullValueHandling = NullValueHandling.Ignore)]
		public virtual Status Status
		{ 
			get { return _Status; }
			set
			{
				_Status = value;
				if (_Status != null)
				{
					this._StatusId = _Status.StatusId;
				}
				OnPropertyChanged("Status");
			}
		}
		
		private Subtask _Subtask;
		[JsonProperty(PropertyName = "subtask", NullValueHandling = NullValueHandling.Ignore)]
		public virtual Subtask Subtask
		{ 
			get { return _Subtask; }
			set
			{
				_Subtask = value;
				if (_Subtask != null)
				{
					this._SubtaskId = _Subtask.SubtaskId;
				}
				OnPropertyChanged("Subtask");
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
			_Entry = null;
			_Status = null;
			_Subtask = null;
			_EnteredBy = null;
					
		}
	}
}
