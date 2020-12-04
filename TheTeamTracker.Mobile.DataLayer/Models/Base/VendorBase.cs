// !!!! DON'T MAKE CHANGES HERE, THEY WILL BE OVERWRITTEN !!!!
using Newtonsoft.Json;
using System;
using TheTeamTracker.Mobile.DataLayer.Classes;
namespace TheTeamTracker.Mobile.DataLayer.Models.Base
{				
	public abstract class VendorBase : EntityBase
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
		
		private System.Guid? _VendorId;
		[JsonProperty(PropertyName = "vendorId", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Guid? VendorId 
		{ 
			get { return _VendorId; }
			set 
			{
				_VendorId = value;
				OnPropertyChanged("VendorId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid VendorIdValue
		{
			get { return VendorId.GetValueOrDefault(); }
			set
			{ 
				VendorId = value;
				OnPropertyChanged("VendorIdValue");
			}
		}
		
		private string _VendorName;
		[JsonProperty(PropertyName = "vendorName", NullValueHandling = NullValueHandling.Include)]
		public virtual string VendorName 
		{ 
			get { return _VendorName; }
			set 
			{
				_VendorName = value;
				OnPropertyChanged("VendorName");
			}
		}
		[JsonProperty(PropertyName = "inventoryItems", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<InventoryItem> InventoryItems { get; set; }
		[JsonProperty(PropertyName = "inventoryTransactions", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<InventoryTransaction> InventoryTransactions { get; set; }
					
		internal override void resetParentsChildren()
		{
			// TODO: for parents null through
			if (InventoryItems != null)
			{
				foreach (var child in InventoryItems)	
				{
					child.resetParentsChildren();
				}
			}
		
			if (InventoryTransactions != null)
			{
				foreach (var child in InventoryTransactions)	
				{
					child.resetParentsChildren();
				}
			}
		
					
		}
	}
}
