// !!!! DON'T MAKE CHANGES HERE, THEY WILL BE OVERWRITTEN !!!!
using Newtonsoft.Json;
using System;
using TheTeamTracker.Mobile.DataLayer.Classes;
namespace TheTeamTracker.Mobile.DataLayer.Models.Base
{				
	public abstract class ProgressItemBase : EntityBase
	{
		
		private string _ItemDescription;
		[JsonProperty(PropertyName = "itemDescription", NullValueHandling = NullValueHandling.Include)]
		public virtual string ItemDescription 
		{ 
			get { return _ItemDescription; }
			set 
			{
				_ItemDescription = value;
				OnPropertyChanged("ItemDescription");
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
		
		private System.Guid? _ProgressItemId;
		[JsonProperty(PropertyName = "progressItemId", NullValueHandling = NullValueHandling.Ignore)]
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
		[JsonProperty(PropertyName = "userProgressItems", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<UserProgressItem> UserProgressItems { get; set; }
		
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
		
			_ProgressChecklist = null;
					
		}
	}
}
