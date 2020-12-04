// !!!! DON'T MAKE CHANGES HERE, THEY WILL BE OVERWRITTEN !!!!
using Newtonsoft.Json;
using System;
using TheTeamTracker.Mobile.DataLayer.Classes;
namespace TheTeamTracker.Mobile.DataLayer.Models.Base
{				
	public abstract class ProgressChecklistBase : EntityBase
	{
		
		private string _ChecklistName;
		[JsonProperty(PropertyName = "checklistName", NullValueHandling = NullValueHandling.Include)]
		public virtual string ChecklistName 
		{ 
			get { return _ChecklistName; }
			set 
			{
				_ChecklistName = value;
				OnPropertyChanged("ChecklistName");
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
		
		private System.Guid? _ProgressChecklistId;
		[JsonProperty(PropertyName = "progressChecklistId", NullValueHandling = NullValueHandling.Ignore)]
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
		[JsonProperty(PropertyName = "progressItems", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<ProgressItem> ProgressItems { get; set; }
		[JsonProperty(PropertyName = "userProgressChecklists", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<UserProgressChecklist> UserProgressChecklists { get; set; }
		
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
			if (ProgressItems != null)
			{
				foreach (var child in ProgressItems)	
				{
					child.resetParentsChildren();
				}
			}
		
			if (UserProgressChecklists != null)
			{
				foreach (var child in UserProgressChecklists)	
				{
					child.resetParentsChildren();
				}
			}
		
			_Company = null;
					
		}
	}
}
