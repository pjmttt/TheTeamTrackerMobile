using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.DataLayer.Models;
using Xamarin.Forms;

namespace TheTeamTracker.Mobile.ViewModels.Inventory
{
	public class InventoryListViewModel : BaseViewModel
	{
		private List<InventoryItem> _inventoryItems;
		public ObservableCollection<DisplayItem> Inventory { get; }
		public ObservableCollection<Vendor> Vendors { get; }
		public ObservableCollection<InventoryCategory> InventoryCategories { get; }
		public List<Vendor> OriginalVendors { get; private set; }
		public List<InventoryCategory> OriginalCategories { get; private set; }
		public Vendor SelectedVendor { get; set; }
		public InventoryCategory SelectedCategory { get; set; }
		public bool ShowFilter { get; set; }
		public bool NeededOnly { get; set; }

		public Command LoadInventoryCommand { get; }
		public Command DeleteCommand { get; }
		public Command ToggleFilterCommand { get; }

		public InventoryListViewModel()
		{
			Inventory = new ObservableCollection<DisplayItem>();
			InventoryCategories = new ObservableCollection<InventoryCategory>();
			Vendors = new ObservableCollection<Vendor>();
			LoadInventoryCommand = new Command(async (reload) => await ExecuteLoadInventoryCommand(reload == null ? true : (bool)reload));
			DeleteCommand = new Command(async (parameter) => await ExecuteDeleteCommand(parameter as DisplayItem));
			ToggleFilterCommand = new Command(() =>
			{
				this.ShowFilter = !this.ShowFilter;
				this.OnPropertyChanged("ShowFilter");
			});
		}

		private void inventoryItemToDisplayItem(InventoryItem invItem, DisplayItem displayItem)
		{
			var invCat = invItem.InventoryCategoryId != null ? InventoryCategories.FirstOrDefault(ic => ic.InventoryCategoryId == invItem.InventoryCategoryId) : null;
			var vendor = invItem.VendorId != null ? Vendors.FirstOrDefault(v => v.VendorId == invItem.VendorId) : null;
			bool isNeeded = invItem.QuantityOnHand.GetValueOrDefault() < invItem.MinimumQuantity.GetValueOrDefault();
			displayItem.Line1 = invItem.InventoryItemName;
			displayItem.Line2 = (invCat == null ? "" : invCat.CategoryName) + (vendor == null ? "" : $" - {vendor.VendorName}");
			displayItem.Line3 = $"In Stock: {invItem.QuantityOnHand.GetValueOrDefault()} | Min Qty: {invItem.MinimumQuantity.GetValueOrDefault()} | Needed: {(isNeeded ? (invItem.MinimumQuantity.GetValueOrDefault() - invItem.QuantityOnHand.GetValueOrDefault()) : 0)}";
			displayItem.Tag = invItem;
			displayItem.Tag2 = isNeeded ? Color.Red : Color.LightGray;
		}

		public void InventoryItemSaved(InventoryItem inventoryItem, DisplayItem displayItem)
		{
			if (displayItem == null)
			{
				displayItem = new DisplayItem();
				_inventoryItems.Add(inventoryItem);
				inventoryItemToDisplayItem(inventoryItem, displayItem);
				Inventory.Add(displayItem);
			}
			else
			{
				inventoryItemToDisplayItem(inventoryItem, displayItem);
				displayItem.Refresh();
			}
		}

		async System.Threading.Tasks.Task ExecuteLoadInventoryCommand(bool reload)
		{
			if (IsBusy)
				return;

			if (!IsRefreshing)
				IsBusy = true;

			try
			{
				if (Vendors.Count < 1)
				{
					Vendors.Add(new Vendor() { VendorName = "(All)" });
					OriginalVendors = (await DataService.GetItemsAsync<Vendor>("vendors")).Data;
					OriginalVendors.OrderBy(v => v.VendorName).ToList().ForEach(d => Vendors.Add(d));
				}

				if (InventoryCategories.Count < 1)
				{
					InventoryCategories.Add(new InventoryCategory() { CategoryName = "(All)" });
					OriginalCategories = (await DataService.GetItemsAsync<InventoryCategory>("inventoryCategories")).Data;
					OriginalCategories.OrderBy(c => c.CategoryName).ToList().ForEach(d => InventoryCategories.Add(d));
				}

				Inventory.Clear();
				if (reload || _inventoryItems == null)
					_inventoryItems = (await DataService.GetItemsAsync<InventoryItem>("inventoryItems")).Data;
				foreach (var item in _inventoryItems
					.Where(i => SelectedVendor == null || SelectedVendor.VendorId == null || i.VendorId == SelectedVendor.VendorId)
					.Where(i => SelectedCategory == null || SelectedCategory.InventoryCategoryId == null || i.InventoryCategoryId == SelectedCategory.InventoryCategoryId)
					.OrderBy(i => i.InventoryItemName))
				{
					bool isNeeded = item.QuantityOnHand.GetValueOrDefault() < item.MinimumQuantity.GetValueOrDefault();
					if (!isNeeded && NeededOnly) continue;
					var dispItem = new DisplayItem();
					inventoryItemToDisplayItem(item, dispItem);
					Inventory.Add(dispItem);
				}
			}
			catch (Exception ex)
			{
				IsBusy = false;
				IsRefreshing = false;
				ExceptionHelper.ShowException(ex);
			}
			finally
			{
				IsRefreshing = false;
				IsBusy = false;
			}
		}

		public async System.Threading.Tasks.Task ExecuteDeleteCommand(DisplayItem displayItem)
		{
			var inventoryItem = displayItem.Tag as InventoryItem;
			if (!await runTask(async () => await DataService.DeleteItemAsync("inventoryItems", inventoryItem.InventoryItemIdValue),
				"Are you sure you want to delete " + inventoryItem.InventoryItemName)) return;
			Inventory.Remove(displayItem);
		}
	}
}
