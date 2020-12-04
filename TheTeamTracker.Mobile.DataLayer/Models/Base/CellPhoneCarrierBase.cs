// !!!! DON'T MAKE CHANGES HERE, THEY WILL BE OVERWRITTEN !!!!
using Newtonsoft.Json;
using System;
using TheTeamTracker.Mobile.DataLayer.Classes;
namespace TheTeamTracker.Mobile.DataLayer.Models.Base
{				
	public abstract class CellPhoneCarrierBase : EntityBase
	{
		
		private string _CarrierName;
		[JsonProperty(PropertyName = "carrierName", NullValueHandling = NullValueHandling.Include)]
		public virtual string CarrierName 
		{ 
			get { return _CarrierName; }
			set 
			{
				_CarrierName = value;
				OnPropertyChanged("CarrierName");
			}
		}
		
		private System.Guid? _CellPhoneCarrierId;
		[JsonProperty(PropertyName = "cellPhoneCarrierId", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Guid? CellPhoneCarrierId 
		{ 
			get { return _CellPhoneCarrierId; }
			set 
			{
				_CellPhoneCarrierId = value;
				OnPropertyChanged("CellPhoneCarrierId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid CellPhoneCarrierIdValue
		{
			get { return CellPhoneCarrierId.GetValueOrDefault(); }
			set
			{ 
				CellPhoneCarrierId = value;
				OnPropertyChanged("CellPhoneCarrierIdValue");
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
		
		private string _Domain;
		[JsonProperty(PropertyName = "domain", NullValueHandling = NullValueHandling.Include)]
		public virtual string Domain 
		{ 
			get { return _Domain; }
			set 
			{
				_Domain = value;
				OnPropertyChanged("Domain");
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
