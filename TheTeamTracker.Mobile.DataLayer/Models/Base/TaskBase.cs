// !!!! DON'T MAKE CHANGES HERE, THEY WILL BE OVERWRITTEN !!!!
using Newtonsoft.Json;
using System;
using TheTeamTracker.Mobile.DataLayer.Classes;
namespace TheTeamTracker.Mobile.DataLayer.Models.Base
{				
	public abstract class TaskBase : EntityBase
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
		
		private int? _Difficulty;
		[JsonProperty(PropertyName = "difficulty", NullValueHandling = NullValueHandling.Include)]
		public virtual int? Difficulty 
		{ 
			get { return _Difficulty; }
			set 
			{
				_Difficulty = value;
				OnPropertyChanged("Difficulty");
			}
		}	
		[JsonIgnore]
		public virtual int DifficultyValue
		{
			get { return Difficulty.GetValueOrDefault(); }
			set
			{ 
				Difficulty = value;
				OnPropertyChanged("DifficultyValue");
			}
		}
		
		private int? _NotifyAfter;
		[JsonProperty(PropertyName = "notifyAfter", NullValueHandling = NullValueHandling.Include)]
		public virtual int? NotifyAfter 
		{ 
			get { return _NotifyAfter; }
			set 
			{
				_NotifyAfter = value;
				OnPropertyChanged("NotifyAfter");
			}
		}	
		[JsonIgnore]
		public virtual int NotifyAfterValue
		{
			get { return NotifyAfter.GetValueOrDefault(); }
			set
			{ 
				NotifyAfter = value;
				OnPropertyChanged("NotifyAfterValue");
			}
		}
		
		private System.Guid? _TaskId;
		[JsonProperty(PropertyName = "taskId", NullValueHandling = NullValueHandling.Ignore)]
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
		
		private string _TaskName;
		[JsonProperty(PropertyName = "taskName", NullValueHandling = NullValueHandling.Include)]
		public virtual string TaskName 
		{ 
			get { return _TaskName; }
			set 
			{
				_TaskName = value;
				OnPropertyChanged("TaskName");
			}
		}
		
		private int? _TaskType;
		[JsonProperty(PropertyName = "taskType", NullValueHandling = NullValueHandling.Include)]
		public virtual int? TaskType 
		{ 
			get { return _TaskType; }
			set 
			{
				_TaskType = value;
				OnPropertyChanged("TaskType");
			}
		}	
		[JsonIgnore]
		public virtual int TaskTypeValue
		{
			get { return TaskType.GetValueOrDefault(); }
			set
			{ 
				TaskType = value;
				OnPropertyChanged("TaskTypeValue");
			}
		}
		
		private string _TextColor;
		[JsonProperty(PropertyName = "textColor", NullValueHandling = NullValueHandling.Include)]
		public virtual string TextColor 
		{ 
			get { return _TextColor; }
			set 
			{
				_TextColor = value;
				OnPropertyChanged("TextColor");
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
		[JsonProperty(PropertyName = "subtasks", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<Subtask> Subtasks { get; set; }
		[JsonProperty(PropertyName = "userTaskQueues", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<UserTaskQueue> UserTaskQueues { get; set; }
		
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
		
			if (Subtasks != null)
			{
				foreach (var child in Subtasks)	
				{
					child.resetParentsChildren();
				}
			}
		
			if (UserTaskQueues != null)
			{
				foreach (var child in UserTaskQueues)	
				{
					child.resetParentsChildren();
				}
			}
		
			_Company = null;
					
		}
	}
}
