// !!!! DON'T MAKE CHANGES HERE, THEY WILL BE OVERWRITTEN !!!!
using Newtonsoft.Json;
using System;
using TheTeamTracker.Mobile.DataLayer.Classes;
namespace TheTeamTracker.Mobile.DataLayer.Models.Base
{				
	public abstract class UserEntryQueueBase : EntityBase
	{
		
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
		
		private System.Guid? _UserEntryQueueId;
		[JsonProperty(PropertyName = "userEntryQueueId", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Guid? UserEntryQueueId 
		{ 
			get { return _UserEntryQueueId; }
			set 
			{
				_UserEntryQueueId = value;
				OnPropertyChanged("UserEntryQueueId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid UserEntryQueueIdValue
		{
			get { return UserEntryQueueId.GetValueOrDefault(); }
			set
			{ 
				UserEntryQueueId = value;
				OnPropertyChanged("UserEntryQueueIdValue");
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
					
		internal override void resetParentsChildren()
		{
			// TODO: for parents null through
			_Entry = null;
					
		}
	}
}
