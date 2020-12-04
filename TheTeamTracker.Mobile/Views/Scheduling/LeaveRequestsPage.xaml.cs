using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTeamTracker.Mobile.DataLayer.Models;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.ViewModels.Scheduling;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheTeamTracker.Mobile.Views.Scheduling
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LeaveRequestsPage : ContentPage
	{
		private LeaveRequestsViewModel viewModel
		{
			get { return BindingContext as LeaveRequestsViewModel; }
		}

		public LeaveRequestsPage()
		{
			InitializeComponent();
		}

		protected async void Edit_Clicked(object sender, EventArgs e)
		{
			await editLeaveRequest((sender as MenuItem).CommandParameter as LeaveRequest);
		}

		protected void Filter_Changed(object sender, EventArgs e)
		{
			viewModel.LoadDataCommand.Execute(null);
		}

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();
			viewModel.DecorateListView(LeaveRequestsList, this.ToolbarItems);
		}

		protected async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			if (e.Item == null)
				return;

			await editLeaveRequest((e.Item as DisplayItem).Tag as LeaveRequest);

			((ListView)sender).SelectedItem = null;
		}

		private async System.Threading.Tasks.Task editLeaveRequest(LeaveRequest request)
		{
			var page = new LeaveRequestEditPage();
			page.BindingContext = new LeaveRequestEditViewModel(viewModel.ForUser, request);
			await ((MainPage)App.Current.MainPage).NavigateTo(page, true);
		}

		protected async void Add_Clicked(object sender, EventArgs e)
		{
			var page = new LeaveRequestEditPage();
			page.BindingContext = new LeaveRequestEditViewModel(viewModel.ForUser, null);
			await ((MainPage)App.Current.MainPage).NavigateTo(page, true);
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (this.ToolbarItems.Count < 2)
			{
				var item = new ToolbarItem() { Icon = "ic_add_white.png" };
				item.Clicked += Add_Clicked;
				this.ToolbarItems.Add(item);
				if (!viewModel.ForUser)
				{
					var srch = new ToolbarItem() { Icon = "ic_search_white.png" };
					srch.Clicked += (object sender, EventArgs e) => viewModel.ToggleFilter();
					this.ToolbarItems.Add(srch);
				}
			}
			viewModel.LoadDataCommand.Execute(null);
		}
	}
}