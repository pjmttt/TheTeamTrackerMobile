using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TheTeamTracker.Mobile.DataLayer.Models;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.ViewModels.Entries;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TheTeamTracker.Mobile.DataLayer.Services;
using static TheTeamTracker.Mobile.Classes.Constants;

namespace TheTeamTracker.Mobile.Views.Entries
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AvailableEntriesPage : ContentPage
	{
		private AvailableEntriesViewModel _viewModel;
		public AvailableEntriesPage()
		{
			InitializeComponent();

			this.BindingContext = _viewModel = new AvailableEntriesViewModel();
		}

		protected async void Handle_ItemTapped(object sender, EventArgs e)
		{
			var args = e as ItemTappedEventArgs;
			if (args.Item == null)
				return;

			var entry = (args.Item as DisplayItem).Tag as DataLayer.Models.Entry;
			var editViewModel = new AvailableEntryEditViewModel(entry);
			var page = new AvailableEntryEditPage();
			page.BindingContext = editViewModel;
			await ((MainPage)App.Current.MainPage).NavigateTo(page, true);
		}

		private async void addToolbarItem_Click(object sender, EventArgs e)
		{
			var editViewModel = new AvailableEntryEditViewModel(null);
			var page = new AvailableEntryEditPage();
			page.BindingContext = editViewModel;
			await ((MainPage)App.Current.MainPage).NavigateTo(page, true);
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			if (this.ToolbarItems.Count < 1)
			{
				var toolbarItem = new ToolbarItem() { Icon = "ic_add_white.png" };
				toolbarItem.Clicked += addToolbarItem_Click;
				this.ToolbarItems.Add(toolbarItem);
			}
			_viewModel.LoadEntriesCommand.Execute(null);
		}

		protected async void Edit_Clicked(object sender, EventArgs e)
		{
			var entry = (sender as MenuItem).CommandParameter as DataLayer.Models.Entry;
			var editViewModel = new AvailableEntryEditViewModel(entry);
			var page = new AvailableEntryEditPage();
			page.BindingContext = editViewModel;
			await ((MainPage)App.Current.MainPage).NavigateTo(page, true);
		}
	}
}
