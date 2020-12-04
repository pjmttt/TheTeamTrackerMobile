// !!!! DON'T MAKE CHANGES HERE, THEY WILL BE OVERWRITTEN !!!!
using Newtonsoft.Json;
using System;
using TheTeamTracker.Mobile.DataLayer.Classes;
namespace TheTeamTracker.Mobile.DataLayer.Models.Base
{				
	public abstract class StatusBase : EntityBase
	{
		
		private string _Abbreviation;
		[JsonProperty(PropertyName = "abbreviation", NullValueHandling = NullValueHandling.Include)]
		public virtual string Abbreviation 
		{ 
			get { return _Abbreviation; }
			set 
			{
				_Abbreviation = value;
				OnPropertyChanged("Abbreviation");
			}
		}
		
		private string _BackgroundColor;
		[JsonProperty(PropertyName = "backgroundColor", NullValueHandling = NullValueHandling.Include)]
		public virtual string BackgroundColor 
		{ 
			get { return _BackgroundColor; }
			set 
			{
				_BackgroundColor = value;
				OnPropertyChanged("BackgroundColor");
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
		
		private System.Guid? _ManagerEmailTemplateId;
		[JsonProperty(PropertyName = "managerEmailTemplateId", NullValueHandling = NullValueHandling.Include)]
		public virtual System.Guid? ManagerEmailTemplateId 
		{ 
			get { return _ManagerEmailTemplateId; }
			set 
			{
				_ManagerEmailTemplateId = value;
				OnPropertyChanged("ManagerEmailTemplateId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid ManagerEmailTemplateIdValue
		{
			get { return ManagerEmailTemplateId.GetValueOrDefault(); }
			set
			{ 
				ManagerEmailTemplateId = value;
				OnPropertyChanged("ManagerEmailTemplateIdValue");
			}
		}
		
		private int? _NotifyManagerAfter;
		[JsonProperty(PropertyName = "notifyManagerAfter", NullValueHandling = NullValueHandling.Include)]
		public virtual int? NotifyManagerAfter 
		{ 
			get { return _NotifyManagerAfter; }
			set 
			{
				_NotifyManagerAfter = value;
				OnPropertyChanged("NotifyManagerAfter");
			}
		}	
		[JsonIgnore]
		public virtual int NotifyManagerAfterValue
		{
			get { return NotifyManagerAfter.GetValueOrDefault(); }
			set
			{ 
				NotifyManagerAfter = value;
				OnPropertyChanged("NotifyManagerAfterValue");
			}
		}
		
		private System.Guid? _StatusId;
		[JsonProperty(PropertyName = "statusId", NullValueHandling = NullValueHandling.Ignore)]
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
		
		private string _StatusName;
		[JsonProperty(PropertyName = "statusName", NullValueHandling = NullValueHandling.Include)]
		public virtual string StatusName 
		{ 
			get { return _StatusName; }
			set 
			{
				_StatusName = value;
				OnPropertyChanged("StatusName");
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
		[JsonProperty(PropertyName = "entrySubtasks", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<EntrySubtask> EntrySubtasks { get; set; }
		[JsonProperty(PropertyName = "userSubtasks", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<UserSubtask> UserSubtasks { get; set; }
		
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
		
		private EmailTemplate _ManagerEmailTemplate;
		[JsonProperty(PropertyName = "managerEmailTemplate", NullValueHandling = NullValueHandling.Ignore)]
		public virtual EmailTemplate ManagerEmailTemplate
		{ 
			get { return _ManagerEmailTemplate; }
			set
			{
				_ManagerEmailTemplate = value;
				if (_ManagerEmailTemplate != null)
				{
					this._ManagerEmailTemplateId = _ManagerEmailTemplate.EmailTemplateId;
				}
				OnPropertyChanged("ManagerEmailTemplate");
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
		
			_Company = null;
			_ManagerEmailTemplate = null;
					
		}
	}
}
