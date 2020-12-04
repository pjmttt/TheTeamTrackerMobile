// !!!! DON'T MAKE CHANGES HERE, THEY WILL BE OVERWRITTEN !!!!
using Newtonsoft.Json;
using System;
using TheTeamTracker.Mobile.DataLayer.Classes;
namespace TheTeamTracker.Mobile.DataLayer.Models.Base
{				
	public abstract class EmailTemplateBase : EntityBase
	{
		
		private string _Body;
		[JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Include)]
		public virtual string Body 
		{ 
			get { return _Body; }
			set 
			{
				_Body = value;
				OnPropertyChanged("Body");
			}
		}
		
		private string _BodyText;
		[JsonProperty(PropertyName = "bodyText", NullValueHandling = NullValueHandling.Include)]
		public virtual string BodyText 
		{ 
			get { return _BodyText; }
			set 
			{
				_BodyText = value;
				OnPropertyChanged("BodyText");
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
		
		private System.Guid? _EmailTemplateId;
		[JsonProperty(PropertyName = "emailTemplateId", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Guid? EmailTemplateId 
		{ 
			get { return _EmailTemplateId; }
			set 
			{
				_EmailTemplateId = value;
				OnPropertyChanged("EmailTemplateId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid EmailTemplateIdValue
		{
			get { return EmailTemplateId.GetValueOrDefault(); }
			set
			{ 
				EmailTemplateId = value;
				OnPropertyChanged("EmailTemplateIdValue");
			}
		}
		
		private string _Subject;
		[JsonProperty(PropertyName = "subject", NullValueHandling = NullValueHandling.Include)]
		public virtual string Subject 
		{ 
			get { return _Subject; }
			set 
			{
				_Subject = value;
				OnPropertyChanged("Subject");
			}
		}
		
		private int? _TemplateType;
		[JsonProperty(PropertyName = "templateType", NullValueHandling = NullValueHandling.Include)]
		public virtual int? TemplateType 
		{ 
			get { return _TemplateType; }
			set 
			{
				_TemplateType = value;
				OnPropertyChanged("TemplateType");
			}
		}	
		[JsonIgnore]
		public virtual int TemplateTypeValue
		{
			get { return TemplateType.GetValueOrDefault(); }
			set
			{ 
				TemplateType = value;
				OnPropertyChanged("TemplateTypeValue");
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
		[JsonProperty(PropertyName = "managerEmailTemplateStatuss", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<Status> ManagerEmailTemplateStatuss { get; set; }
		
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
			if (ManagerEmailTemplateStatuss != null)
			{
				foreach (var child in ManagerEmailTemplateStatuss)	
				{
					child.resetParentsChildren();
				}
			}
		
			_Company = null;
					
		}
	}
}
