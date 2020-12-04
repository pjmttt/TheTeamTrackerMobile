using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTeamTracker.Mobile.DataLayer.Models;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.ViewModels.Inventory;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheTeamTracker.Mobile.Views.Inventory
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InventoryListPage : ContentPage
	{
		private InventoryListViewModel _viewModel;
		public InventoryListPage()
		{
			InitializeComponent();
			BindingContext = _viewModel = new InventoryListViewModel();
		}

		protected async void Edit_Clicked(object sender, EventArgs e)
		{
			await editInventoryItem((sender as MenuItem).CommandParameter as DisplayItem);
		}

		protected async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			if (e.Item == null)
				return;

			await editInventoryItem(e.Item as DisplayItem);

			//Deselect Item
			((ListView)sender).SelectedItem = null;
		}

		private async System.Threading.Tasks.Task editInventoryItem(DisplayItem item)
		{
			var editViewModel = new InventoryEditViewModel(_viewModel.OriginalVendors.ToList(), _viewModel.OriginalCategories.ToList(), item.Tag as InventoryItem);
			editViewModel.ItemSaved += (object sender, EventArgs e) =>
				{
					_viewModel.InventoryItemSaved(item.Tag as InventoryItem, item);
				};
			var page = new InventoryEditPage();
			page.BindingContext = editViewModel;
			await ((MainPage)App.Current.MainPage).NavigateTo(page, true);
		}

		protected void Lookup_Changed(object sender, EventArgs e)
		{
			_viewModel.LoadInventoryCommand.Execute(false);
		}

		protected async void Add_Clicked(object sender, EventArgs e)
		{
			var editViewModel = new InventoryEditViewModel(_viewModel.OriginalVendors.ToList(), _viewModel.OriginalCategories.ToList());
			editViewModel.ItemSaved += (object sender2, EventArgs e2) =>
			{
				_viewModel.InventoryItemSaved(sender2 as InventoryItem, null);
			};
			var page = new InventoryEditPage
			{
				BindingContext = editViewModel
			};
			await ((MainPage)App.Current.MainPage).NavigateTo(page, true);
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			if (_viewModel.Inventory.Count < 1)
				_viewModel.LoadInventoryCommand.Execute(true);
		}
	}
}