// !!!! DON'T MAKE CHANGES HERE, THEY WILL BE OVERWRITTEN !!!!
using Newtonsoft.Json;
using System;
using TheTeamTracker.Mobile.DataLayer.Classes;
namespace TheTeamTracker.Mobile.DataLayer.Models.Base
{				
	public abstract class InventoryTransactionBase : EntityBase
	{
		
		private string _Comments;
		[JsonProperty(PropertyName = "comments", NullValueHandling = NullValueHandling.Include)]
		public virtual string Comments 
		{ 
			get { return _Comments; }
			set 
			{
				_Comments = value;
				OnPropertyChanged("Comments");
			}
		}
		
		private float? _CostPer;
		[JsonProperty(PropertyName = "costPer", NullValueHandling = NullValueHandling.Include)]
		public virtual float? CostPer 
		{ 
			get { return _CostPer; }
			set 
			{
				_CostPer = value;
				OnPropertyChanged("CostPer");
			}
		}	
		[JsonIgnore]
		public virtual float CostPerValue
		{
			get { return CostPer.GetValueOrDefault(); }
			set
			{ 
				CostPer = value;
				OnPropertyChanged("CostPerValue");
			}
		}
		
		private System.Guid? _EnteredById;
		[JsonProperty(PropertyName = "enteredById", NullValueHandling = NullValueHandling.Include)]
		public virtual System.Guid? EnteredById 
		{ 
			get { return _EnteredById; }
			set 
			{
				_EnteredById = value;
				OnPropertyChanged("EnteredById");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid EnteredByIdValue
		{
			get { return EnteredById.GetValueOrDefault(); }
			set
			{ 
				EnteredById = value;
				OnPropertyChanged("EnteredByIdValue");
			}
		}
		
		private System.Guid? _InventoryItemId;
		[JsonProperty(PropertyName = "inventoryItemId", NullValueHandling = NullValueHandling.Include)]
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
		
		private System.Guid? _InventoryTransactionId;
		[JsonProperty(PropertyName = "inventoryTransactionId", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Guid? InventoryTransactionId 
		{ 
			get { return _InventoryTransactionId; }
			set 
			{
				_InventoryTransactionId = value;
				OnPropertyChanged("InventoryTransactionId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid InventoryTransactionIdValue
		{
			get { return InventoryTransactionId.GetValueOrDefault(); }
			set
			{ 
				InventoryTransactionId = value;
				OnPropertyChanged("InventoryTransactionIdValue");
			}
		}
		
		private int? _Quantity;
		[JsonProperty(PropertyName = "quantity", NullValueHandling = NullValueHandling.Include)]
		public virtual int? Quantity 
		{ 
			get { return _Quantity; }
			set 
			{
				_Quantity = value;
				OnPropertyChanged("Quantity");
			}
		}	
		[JsonIgnore]
		public virtual int QuantityValue
		{
			get { return Quantity.GetValueOrDefault(); }
			set
			{ 
				Quantity = value;
				OnPropertyChanged("QuantityValue");
			}
		}
		
		private float? _QuantityRemaining;
		[JsonProperty(PropertyName = "quantityRemaining", NullValueHandling = NullValueHandling.Include)]
		public virtual float? QuantityRemaining 
		{ 
			get { return _QuantityRemaining; }
			set 
			{
				_QuantityRemaining = value;
				OnPropertyChanged("QuantityRemaining");
			}
		}	
		[JsonIgnore]
		public virtual float QuantityRemainingValue
		{
			get { return QuantityRemaining.GetValueOrDefault(); }
			set
			{ 
				QuantityRemaining = value;
				OnPropertyChanged("QuantityRemainingValue");
			}
		}
		
		private System.DateTime? _TransactionDate;
		[JsonProperty(PropertyName = "transactionDate", NullValueHandling = NullValueHandling.Include)]
		public virtual System.DateTime? TransactionDate 
		{ 
			get { return _TransactionDate; }
			set 
			{
				_TransactionDate = value;
				OnPropertyChanged("TransactionDate");
			}
		}	
		[JsonIgnore]
		public virtual System.DateTime TransactionDateValue
		{
			get { return TransactionDate.GetValueOrDefault(); }
			set
			{ 
				TransactionDate = value;
				OnPropertyChanged("TransactionDateValue");
			}
		}
		[JsonIgnore]
		public virtual System.DateTime? TransactionDateTimezoned
		{
			get { return TransactionDate == null ? (System.DateTime?)null : TimeZoneInfo.ConvertTimeFromUtc(DateTime.SpecifyKind(TransactionDate.Value, DateTimeKind.Unspecified), Globalization.TimeZoneInfo); }
			set
			{ 
				TransactionDate = value == null ? (System.DateTime?)null : TimeZoneInfo.ConvertTimeToUtc(DateTime.SpecifyKind(value.Value, DateTimeKind.Unspecified), Globalization.TimeZoneInfo);
				OnPropertyChanged("TransactionDateTimezoned");
			}
		}
		[JsonIgnore]
		public virtual System.DateTime TransactionDateTimezonedValue
		{
			get { return TransactionDateTimezoned.GetValueOrDefault(); }
			set
			{ 
				TransactionDateTimezoned = value;
				OnPropertyChanged("TransactionDateTimezonedValue");
			}
		}
			
		
		private int? _TransactionType;
		[JsonProperty(PropertyName = "transactionType", NullValueHandling = NullValueHandling.Include)]
		public virtual int? TransactionType 
		{ 
			get { return _TransactionType; }
			set 
			{
				_TransactionType = value;
				OnPropertyChanged("TransactionType");
			}
		}	
		[JsonIgnore]
		public virtual int TransactionTypeValue
		{
			get { return TransactionType.GetValueOrDefault(); }
			set
			{ 
				TransactionType = value;
				OnPropertyChanged("TransactionTypeValue");
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
		
		private InventoryItem _InventoryItem;
		[JsonProperty(PropertyName = "inventoryItem", NullValueHandling = NullValueHandling.Ignore)]
		public virtual InventoryItem InventoryItem
		{ 
			get { return _InventoryItem; }
			set
			{
				_InventoryItem = value;
				if (_InventoryItem != null)
				{
					this._InventoryItemId = _InventoryItem.InventoryItemId;
				}
				OnPropertyChanged("InventoryItem");
			}
		}
		
		private User _EnteredBy;
		[JsonProperty(PropertyName = "enteredBy", NullValueHandling = NullValueHandling.Ignore)]
		public virtual User EnteredBy
		{ 
			get { return _EnteredBy; }
			set
			{
				_EnteredBy = value;
				if (_EnteredBy != null)
				{
					this._EnteredById = _EnteredBy.UserId;
				}
				OnPropertyChanged("EnteredBy");
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
			_InventoryItem = null;
			_EnteredBy = null;
			_Vendor = null;
					
		}
	}
}
