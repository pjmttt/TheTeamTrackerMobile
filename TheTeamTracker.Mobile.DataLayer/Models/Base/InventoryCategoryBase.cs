// !!!! DON'T MAKE CHANGES HERE, THEY WILL BE OVERWRITTEN !!!!
using Newtonsoft.Json;
using System;
using TheTeamTracker.Mobile.DataLayer.Classes;
namespace TheTeamTracker.Mobile.DataLayer.Models.Base
{				
	public abstract class InventoryCategoryBase : EntityBase
	{
		
		private string _CategoryName;
		[JsonProperty(PropertyName = "categoryName", NullValueHandling = NullValueHandling.Include)]
		public virtual string CategoryName 
		{ 
			get { return _CategoryName; }
			set 
			{
				_CategoryName = value;
				OnPropertyChanged("CategoryName");
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
		
		private System.Guid? _InventoryCategoryId;
		[JsonProperty(PropertyName = "inventoryCategoryId", NullValueHandling = NullValueHandling.Ignore)]
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
		[JsonProperty(PropertyName = "inventoryItems", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<InventoryItem> InventoryItems { get; set; }
		
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
			if (InventoryItems != null)
			{
				foreach (var child in InventoryItems)	
				{
					child.resetParentsChildren();
				}
			}
		
			_Company = null;
					
		}
	}
}
