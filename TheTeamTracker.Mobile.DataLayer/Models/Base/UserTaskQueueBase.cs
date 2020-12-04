// !!!! DON'T MAKE CHANGES HERE, THEY WILL BE OVERWRITTEN !!!!
using Newtonsoft.Json;
using System;
using TheTeamTracker.Mobile.DataLayer.Classes;
namespace TheTeamTracker.Mobile.DataLayer.Models.Base
{				
	public abstract class UserTaskQueueBase : EntityBase
	{
		
		private System.Collections.Generic.List<System.Guid> _EntryIds;
		[JsonProperty(PropertyName = "entryIds", NullValueHandling = NullValueHandling.Include)]
		public virtual System.Collections.Generic.List<System.Guid> EntryIds 
		{ 
			get { return _EntryIds; }
			set 
			{
				_EntryIds = value;
				OnPropertyChanged("EntryIds");
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
		
		private System.Guid? _UserTaskQueueId;
		[JsonProperty(PropertyName = "userTaskQueueId", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Guid? UserTaskQueueId 
		{ 
			get { return _UserTaskQueueId; }
			set 
			{
				_UserTaskQueueId = value;
				OnPropertyChanged("UserTaskQueueId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid UserTaskQueueIdValue
		{
			get { return UserTaskQueueId.GetValueOrDefault(); }
			set
			{ 
				UserTaskQueueId = value;
				OnPropertyChanged("UserTaskQueueIdValue");
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
			_Task = null;
			_User = null;
					
		}
	}
}
