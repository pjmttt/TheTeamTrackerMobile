// !!!! DON'T MAKE CHANGES HERE, THEY WILL BE OVERWRITTEN !!!!
using Newtonsoft.Json;
using System;
using TheTeamTracker.Mobile.DataLayer.Classes;
namespace TheTeamTracker.Mobile.DataLayer.Models.Base
{				
	public abstract class InventoryItemBase : EntityBase
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
		
		private float? _CostOnHand;
		[JsonProperty(PropertyName = "costOnHand", NullValueHandling = NullValueHandling.Include)]
		public virtual float? CostOnHand 
		{ 
			get { return _CostOnHand; }
			set 
			{
				_CostOnHand = value;
				OnPropertyChanged("CostOnHand");
			}
		}	
		[JsonIgnore]
		public virtual float CostOnHandValue
		{
			get { return CostOnHand.GetValueOrDefault(); }
			set
			{ 
				CostOnHand = value;
				OnPropertyChanged("CostOnHandValue");
			}
		}
		
		private System.DateTime? _ExpirationDate;
		[JsonProperty(PropertyName = "expirationDate", NullValueHandling = NullValueHandling.Include)]
		public virtual System.DateTime? ExpirationDate 
		{ 
			get { return _ExpirationDate; }
			set 
			{
				_ExpirationDate = value;
				OnPropertyChanged("ExpirationDate");
			}
		}	
		[JsonIgnore]
		public virtual System.DateTime ExpirationDateValue
		{
			get { return ExpirationDate.GetValueOrDefault(); }
			set
			{ 
				ExpirationDate = value;
				OnPropertyChanged("ExpirationDateValue");
			}
		}
		
		private System.Guid? _InventoryCategoryId;
		[JsonProperty(PropertyName = "inventoryCategoryId", NullValueHandling = NullValueHandling.Include)]
		public virtual System.Guid? InventoryCategoryId 
		{ 
			get { return _InventoryCategoryId; }
			set 
			{
				_InventoryCategoryId = value;
				OnPropertyChanged("InventoryCategoryId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid InventoryCategoryIdValue
		{
			get { return InventoryCategoryId.GetValueOrDefault(); }
			set
			{ 
				InventoryCategoryId = value;
				OnPropertyChanged("InventoryCategoryIdValue");
			}
		}
		
		private System.Guid? _InventoryItemId;
		[JsonProperty(PropertyName = "inventoryItemId", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Guid? InventoryItemId 
		{ 
			get { return _InventoryItemId; }
			set 
			{
				_InventoryItemId = value;
				OnPropertyChanged("InventoryItemId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid InventoryItemIdValue
		{
			get { return InventoryItemId.GetValueOrDefault(); }
			set
			{ 
				InventoryItemId = value;
				OnPropertyChanged("InventoryItemIdValue");
			}
		}
		
		private string _InventoryItemName;
		[JsonProperty(PropertyName = "inventoryItemName", NullValueHandling = NullValueHandling.Include)]
		public virtual string InventoryItemName 
		{ 
			get { return _InventoryItemName; }
			set 
			{
				_InventoryItemName = value;
				OnPropertyChanged("InventoryItemName");
			}
		}
		
		private System.DateTime? _LastUpdated;
		[JsonProperty(PropertyName = "lastUpdated", NullValueHandling = NullValueHandling.Include)]
		public virtual System.DateTime? LastUpdated 
		{ 
			get { return _LastUpdated; }
			set 
			{
				_LastUpdated = value;
				OnPropertyChanged("LastUpdated");
			}
		}	
		[JsonIgnore]
		public virtual System.DateTime LastUpdatedValue
		{
			get { return LastUpdated.GetValueOrDefault(); }
			set
			{ 
				LastUpdated = value;
				OnPropertyChanged("LastUpdatedValue");
			}
		}
		[JsonIgnore]
		public virtual System.DateTime? LastUpdatedTimezoned
		{
			get { return LastUpdated == null ? (System.DateTime?)null : TimeZoneInfo.ConvertTimeFromUtc(DateTime.SpecifyKind(LastUpdated.Value, DateTimeKind.Unspecified), Globalization.TimeZoneInfo); }
			set
			{ 
				LastUpdated = value == null ? (System.DateTime?)null : TimeZoneInfo.ConvertTimeToUtc(DateTime.SpecifyKind(value.Value, DateTimeKind.Unspecified), Globalization.TimeZoneInfo);
				OnPropertyChanged("LastUpdatedTimezoned");
			}
		}
		[JsonIgnore]
		public virtual System.DateTime LastUpdatedTimezonedValue
		{
			get { return LastUpdatedTimezoned.GetValueOrDefault(); }
			set
			{ 
				LastUpdatedTimezoned = value;
				OnPropertyChanged("LastUpdatedTimezonedValue");
			}
		}
			
		
		private int? _MinimumQuantity;
		[JsonProperty(PropertyName = "minimumQuantity", NullValueHandling = NullValueHandling.Include)]
		public virtual int? MinimumQuantity 
		{ 
			get { return _MinimumQuantity; }
			set 
			{
				_MinimumQuantity = value;
				OnPropertyChanged("MinimumQuantity");
			}
		}	
		[JsonIgnore]
		public virtual int MinimumQuantityValue
		{
			get { return MinimumQuantity.GetValueOrDefault(); }
			set
			{ 
				MinimumQuantity = value;
				OnPropertyChanged("MinimumQuantityValue");
			}
		}
		
		private int? _QuantityOnHand;
		[JsonProperty(PropertyName = "quantityOnHand", NullValueHandling = NullValueHandling.Include)]
		public virtual int? QuantityOnHand 
		{ 
			get { return _QuantityOnHand; }
			set 
			{
				_QuantityOnHand = value;
				OnPropertyChanged("QuantityOnHand");
			}
		}	
		[JsonIgnore]
		public virtual int QuantityOnHandValue
		{
			get { return QuantityOnHand.GetValueOrDefault(); }
			set
			{ 
				QuantityOnHand = value;
				OnPropertyChanged("QuantityOnHandValue");
			}
		}
		
		private float? _UnitCost;
		[JsonProperty(PropertyName = "unitCost", NullValueHandling = NullValueHandling.Include)]
		public virtual float? UnitCost 
		{ 
			get { return _UnitCost; }
			set 
			{
				_UnitCost = value;
				OnPropertyChanged("UnitCost");
			}
		}	
		[JsonIgnore]
		public virtual float UnitCostValue
		{
			get { return UnitCost.GetValueOrDefault(); }
			set
			{ 
				UnitCost = value;
				OnPropertyChanged("UnitCostValue");
			}
		}
		
		private System.Guid? _VendorId;
		[JsonProperty(PropertyName = "vendorId", NullValueHandling = NullValueHandling.Include)]
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
		[JsonProperty(PropertyName = "inventoryTransactions", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<InventoryTransaction> InventoryTransactions { get; set; }
		
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
		
		private InventoryCategory _InventoryCategory;
		[JsonProperty(PropertyName = "inventoryCategory", NullValueHandling = NullValueHandling.Ignore)]
		public virtual InventoryCategory InventoryCategory
		{ 
			get { return _InventoryCategory; }
			set
			{
				_InventoryCategory = value;
				if (_InventoryCategory != null)
				{
					this._InventoryCategoryId = _InventoryCategory.InventoryCategoryId;
				}
				OnPropertyChanged("InventoryCategory");
			}
		}
		
		private Vendor _Vendor;
		[JsonProperty(PropertyName = "vendor", NullValueHandling = NullValueHandling.Ignore)]
		public virtual Vendor Vendor
		{ 
			get { return _Vendor; }
			set
			{
				_Vendor = value;
				if (_Vendor != null)
				{
					this._VendorId = _Vendor.VendorId;
				}
				OnPropertyChanged("Vendor");
			}
		}
					
		internal override void resetParentsChildren()
		{
			// TODO: for parents null through
			if (InventoryTransactions != null)
			{
				foreach (var child in InventoryTransactions)	
				{
					child.resetParentsChildren();
				}
			}
		
			_Company = null;
			_InventoryCategory = null;
			_Vendor = null;
					
		}
	}
}
