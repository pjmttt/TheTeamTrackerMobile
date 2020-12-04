// !!!! DON'T MAKE CHANGES HERE, THEY WILL BE OVERWRITTEN !!!!
using Newtonsoft.Json;
using System;
using TheTeamTracker.Mobile.DataLayer.Classes;
namespace TheTeamTracker.Mobile.DataLayer.Models.Base
{				
	public abstract class ScheduleTemplateBase : EntityBase
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
		
		private System.Guid? _ScheduleTemplateId;
		[JsonProperty(PropertyName = "scheduleTemplateId", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Guid? ScheduleTemplateId 
		{ 
			get { return _ScheduleTemplateId; }
			set 
			{
				_ScheduleTemplateId = value;
				OnPropertyChanged("ScheduleTemplateId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid ScheduleTemplateIdValue
		{
			get { return ScheduleTemplateId.GetValueOrDefault(); }
			set
			{ 
				ScheduleTemplateId = value;
				OnPropertyChanged("ScheduleTemplateIdValue");
			}
		}
		
		private string _TemplateName;
		[JsonProperty(PropertyName = "templateName", NullValueHandling = NullValueHandling.Include)]
		public virtual string TemplateName 
		{ 
			get { return _TemplateName; }
			set 
			{
				_TemplateName = value;
				OnPropertyChanged("TemplateName");
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
		[JsonProperty(PropertyName = "schedules", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<Schedule> Schedules { get; set; }
		
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
			if (Schedules != null)
			{
				foreach (var child in Schedules)	
				{
					child.resetParentsChildren();
				}
			}
		
			_Company = null;
					
		}
	}
}
