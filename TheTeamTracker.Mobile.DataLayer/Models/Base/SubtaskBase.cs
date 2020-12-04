// !!!! DON'T MAKE CHANGES HERE, THEY WILL BE OVERWRITTEN !!!!
using Newtonsoft.Json;
using System;
using TheTeamTracker.Mobile.DataLayer.Classes;
namespace TheTeamTracker.Mobile.DataLayer.Models.Base
{				
	public abstract class SubtaskBase : EntityBase
	{
		
		private int? _Sequence;
		[JsonProperty(PropertyName = "sequence", NullValueHandling = NullValueHandling.Include)]
		public virtual int? Sequence 
		{ 
			get { return _Sequence; }
			set 
			{
				_Sequence = value;
				OnPropertyChanged("Sequence");
			}
		}	
		[JsonIgnore]
		public virtual int SequenceValue
		{
			get { return Sequence.GetValueOrDefault(); }
			set
			{ 
				Sequence = value;
				OnPropertyChanged("SequenceValue");
			}
		}
		
		private System.Guid? _SubtaskId;
		[JsonProperty(PropertyName = "subtaskId", NullValueHandling = NullValueHandling.Ignore)]
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
		
		private string _SubtaskName;
		[JsonProperty(PropertyName = "subtaskName", NullValueHandling = NullValueHandling.Include)]
		public virtual string SubtaskName 
		{ 
			get { return _SubtaskName; }
			set 
			{
				_SubtaskName = value;
				OnPropertyChanged("SubtaskName");
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
		[JsonProperty(PropertyName = "entrySubtasks", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<EntrySubtask> EntrySubtasks { get; set; }
		[JsonProperty(PropertyName = "userSubtasks", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<UserSubtask> UserSubtasks { get; set; }
		
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
		
			if (UserSubtasks != null)
			{
				foreach (var child in UserSubtasks)	
				{
					child.resetParentsChildren();
				}
			}
		
			_Task = null;
					
		}
	}
}
