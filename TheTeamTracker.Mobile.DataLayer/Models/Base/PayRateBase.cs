// !!!! DON'T MAKE CHANGES HERE, THEY WILL BE OVERWRITTEN !!!!
using Newtonsoft.Json;
using System;
using TheTeamTracker.Mobile.DataLayer.Classes;
namespace TheTeamTracker.Mobile.DataLayer.Models.Base
{				
	public abstract class PayRateBase : EntityBase
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
		
		private string _Description;
		[JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Include)]
		public virtual string Description 
		{ 
			get { return _Description; }
			set 
			{
				_Description = value;
				OnPropertyChanged("Description");
			}
		}
		
		private System.Guid? _PayRateId;
		[JsonProperty(PropertyName = "payRateId", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Guid? PayRateId 
		{ 
			get { return _PayRateId; }
			set 
			{
				_PayRateId = value;
				OnPropertyChanged("PayRateId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid PayRateIdValue
		{
			get { return PayRateId.GetValueOrDefault(); }
			set
			{ 
				PayRateId = value;
				OnPropertyChanged("PayRateIdValue");
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
		[JsonProperty(PropertyName = "users", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<User> Users { get; set; }
		
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
			if (Users != null)
			{
				foreach (var child in Users)	
				{
					child.resetParentsChildren();
				}
			}
		
			_Company = null;
					
		}
	}
}
