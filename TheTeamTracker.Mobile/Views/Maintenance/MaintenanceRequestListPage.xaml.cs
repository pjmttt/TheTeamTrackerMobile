using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTeamTracker.Mobile.DataLayer.Models;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.ViewModels.Maintenance;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheTeamTracker.Mobile.Views.Maintenance
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MaintenanceRequestListPage : ContentPage
	{
		private MaintenanceRequestListViewModel _viewModel;
		public MaintenanceRequestListPage()
		{
			InitializeComponent();

			BindingContext = _viewModel = new MaintenanceRequestListViewModel();

		}

		protected async void Edit_Clicked(object sender, EventArgs e)
		{
			await editMaintenanceItem((sender as MenuItem).CommandParameter as DisplayItem);
		}

		protected async void Reply_Clicked(object sender, EventArgs e)
		{
			await addReply((sender as MenuItem).CommandParameter as MaintenanceRequest);
		}

		protected async void Images_Clicked(object sender, EventArgs e)
		{
			var page = new MaintenanceRequestImagesPage();
			page.BindingContext = new MaintenanceRequestImagesViewModel((sender as MenuItem).CommandParameter as MaintenanceRequest);
			await ((MainPage)App.Current.MainPage).NavigateTo(page, true);
		}

		protected async void Images_Tapped(object sender, EventArgs e)
		{
			var img = sender as Image;
			var page = new MaintenanceRequestImagesPage();
			page.BindingContext = new MaintenanceRequestImagesViewModel((img.GestureRecognizers[0] as TapGestureRecognizer).CommandParameter as MaintenanceRequest);
			await((MainPage)App.Current.MainPage).NavigateTo(page, true);
		}

		protected async void Reply_Tapped(object sender, EventArgs e)
		{
			var img = sender as Image;
			await addReply((img.GestureRecognizers[0] as TapGestureRecognizer).CommandParameter as MaintenanceRequest);
		}

		protected async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			if (e.Item == null)
				return;

			await editMaintenanceItem(e.Item as DisplayItem);

			((ListView)sender).SelectedItem = null;
		}

		private async System.Threading.Tasks.Task addReply(MaintenanceRequest request)
		{
			var page = new MaintenanceRequestReplyPage();
			page.BindingContext = new MaintenanceRequestReplyViewModel(request);
			await ((MainPage)App.Current.MainPage).NavigateTo(page, true);
		}

		private async System.Threading.Tasks.Task editMaintenanceItem(DisplayItem item)
		{
			var editViewModel = new MaintenanceRequestEditViewModel(item.Tag as MaintenanceRequest, _viewModel.Lookups);
			editViewModel.ItemSaved += (object sender, EventArgs e) =>
			{
				_viewModel.MaintenanceRequestSaved(item.Tag as MaintenanceRequest, item);
			};
			var page = new MaintenanceRequestEditPage();
			page.BindingContext = editViewModel;
			await ((MainPage)App.Current.MainPage).NavigateTo(page, true);
		}

		protected async void Add_Clicked(object sender, EventArgs e)
		{
			var editViewModel = new MaintenanceRequestEditViewModel(null, _viewModel.Lookups);
			editViewModel.ItemSaved += (object sender2, EventArgs e2) =>
			{
				_viewModel.MaintenanceRequestSaved(sender2 as MaintenanceRequest, null);
			};
			var page = new MaintenanceRequestEditPage();
			page.BindingContext = editViewModel;
			await ((MainPage)App.Current.MainPage).NavigateTo(page, true);
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			if (!_viewModel.DataSource.Any())
				_viewModel.LoadDataCommand.Execute(null);
		}

		protected void Lookup_Changed(object sender, EventArgs e)
		{
			_viewModel.LoadDataCommand.Execute(null);
		}

		protected override void OnBindingContextChanged()
		{
			_viewModel.DecorateListView(RequestsList, this.ToolbarItems);
			base.OnBindingContextChanged();
		}
	}
}