// !!!! DON'T MAKE CHANGES HERE, THEY WILL BE OVERWRITTEN !!!!
using Newtonsoft.Json;
using System;
using TheTeamTracker.Mobile.DataLayer.Classes;
namespace TheTeamTracker.Mobile.DataLayer.Models.Base
{				
	public abstract class UserSubtaskBase : EntityBase
	{
		
		private System.Collections.Generic.List<System.Guid> _EntrySubtaskIds;
		[JsonProperty(PropertyName = "entrySubtaskIds", NullValueHandling = NullValueHandling.Include)]
		public virtual System.Collections.Generic.List<System.Guid> EntrySubtaskIds 
		{ 
			get { return _EntrySubtaskIds; }
			set 
			{
				_EntrySubtaskIds = value;
				OnPropertyChanged("EntrySubtaskIds");
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
		
		private System.Guid? _UserSubtaskId;
		[JsonProperty(PropertyName = "userSubtaskId", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Guid? UserSubtaskId 
		{ 
			get { return _UserSubtaskId; }
			set 
			{
				_UserSubtaskId = value;
				OnPropertyChanged("UserSubtaskId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid UserSubtaskIdValue
		{
			get { return UserSubtaskId.GetValueOrDefault(); }
			set
			{ 
				UserSubtaskId = value;
				OnPropertyChanged("UserSubtaskIdValue");
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
					
		internal override void resetParentsChildren()
		{
			// TODO: for parents null through
			_Status = null;
			_Subtask = null;
					
		}
	}
}
