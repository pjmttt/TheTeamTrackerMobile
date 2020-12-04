using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.DataLayer.Models;
using Xamarin.Forms;

namespace TheTeamTracker.Mobile.ViewModels.Inventory
{
	public class InventoryEditViewModel : BaseViewModel
	{
		public const string SUCCESS = "success";

		public ObservableCollection<Vendor> Vendors { get; }
		public ObservableCollection<InventoryCategory> InventoryCategories { get; }
		public InventoryItem InventoryItem { get; }
		public InventoryCategory SelectedCategory { get; set; }
		public Vendor SelectedVendor { get; set; }
		public event EventHandler ItemSaved;

		public ICommand SaveCommand { get; }

		public string LastUpdatedFormatted
		{
			get { return InventoryItem == null || InventoryItem.LastUpdated == null ? string.Empty : InventoryItem.LastUpdatedTimezonedValue.ToShortDateString(); }
		}

		public InventoryEditViewModel(List<Vendor> vendors, List<InventoryCategory> inventoryCategories, InventoryItem inventoryItem = null)
		{
			this.Vendors = new ObservableCollection<Vendor>(vendors.OrderBy(v => v.VendorName));
			this.InventoryCategories = new ObservableCollection<InventoryCategory>(inventoryCategories.OrderBy(c => c.CategoryName));
			this.InventoryItem = inventoryItem ?? new InventoryItem() { CompanyId = LoginHelper.GetLoggedInUser().User.CompanyId };

			if (this.InventoryItem.InventoryCategoryId != null) this.SelectedCategory = this.InventoryCategories.FirstOrDefault(ic => ic.InventoryCategoryId == this.InventoryItem.InventoryCategoryId);
			if (this.InventoryItem.VendorId != null) this.SelectedVendor = this.Vendors.FirstOrDefault(v => v.VendorId == this.InventoryItem.VendorId);

			this.SaveCommand = new Command(async () => await this.saveInventoryItem());
		}

		private async System.Threading.Tasks.Task saveInventoryItem()
		{
			if (SelectedCategory == null || string.IsNullOrEmpty(InventoryItem.InventoryItemName))
			{
				MessageHelper.ShowMessage("Name and category required!", "Validation");
				return;
			}


			IsBusy = true;
			try
			{
				if (SelectedVendor != null) InventoryItem.VendorId = SelectedVendor.VendorId;
				if (SelectedCategory != null) InventoryItem.InventoryCategoryId = SelectedCategory.InventoryCategoryId;
				var toSave = InventoryItem.GetForSave<InventoryItem>();
				InventoryItem saved;
				if (toSave.InventoryItemId != null)
					saved = await DataService.PutItemAsync<InventoryItem>("inventoryItems", toSave.InventoryItemId.Value, toSave);
				else
					saved = await DataService.PostItemAsync<InventoryItem>("inventoryItems", toSave);
				InventoryItem.InventoryItemId = saved.InventoryItemId;
				InventoryItem.LastUpdated = saved.LastUpdated;
				ItemSaved?.Invoke(InventoryItem, new EventArgs());
				MessagingCenter.Send<InventoryEditViewModel>(this, SUCCESS);
				IsBusy = false;


			}
			catch (Exception ex)
			{
				IsBusy = false;
				if (ex.InnerException != null)
				{
					ex = ex.InnerException;
				}
				ExceptionHelper.ShowException(ex);
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}
