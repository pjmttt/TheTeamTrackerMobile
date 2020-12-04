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
	public partial class HoursPage : ContentPage
	{
		private HoursViewModel viewModel
		{
			get { return BindingContext as HoursViewModel; }
		}

		public HoursPage()
		{
			InitializeComponent();
		}

		protected void dates_Changed(object sender, EventArgs e)
		{
			viewModel.LoadDataCommand.Execute(null);
		}

		protected async void Edit_Clicked(object sender, EventArgs e)
		{
			await editHours((sender as MenuItem).CommandParameter as UserClock);
		}

		protected void Lookup_Changed(object sedner, EventArgs e)
		{
			viewModel.LoadDataCommand.Execute(null);
		}

		protected async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			if (e.Item == null)
				return;

			await editHours((e.Item as DisplayItem).Tag as UserClock);

			((ListView)sender).SelectedItem = null;
		}

		private async System.Threading.Tasks.Task editHours(UserClock hours)
		{
			var page = new HoursEditPage();
			page.BindingContext = new HoursEditViewModel(hours, viewModel.ForUser, viewModel.GetUsers().ToList());
			await ((MainPage)App.Current.MainPage).NavigateTo(page, true);
		}

		protected async void Add_Clicked(object sender, EventArgs e)
		{
			var page = new HoursEditPage();
			page.BindingContext = new HoursEditViewModel(null, viewModel.ForUser, viewModel.GetUsers().ToList());
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
				var srch = new ToolbarItem() { Icon = "ic_search_white.png" };
				srch.Clicked += (object sender, EventArgs e) => viewModel.ToggleFilter();
				this.ToolbarItems.Add(srch);
			}
			viewModel.LoadDataCommand.Execute(null);
		}

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();
			viewModel.DecorateListView(HoursList, this.ToolbarItems);
		}
	}
}
