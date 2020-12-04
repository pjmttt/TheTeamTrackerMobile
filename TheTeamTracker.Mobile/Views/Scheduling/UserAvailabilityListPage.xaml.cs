using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.DataLayer.Models;
using TheTeamTracker.Mobile.ViewModels.Scheduling;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheTeamTracker.Mobile.Views.Scheduling
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserAvailabilityListPage : ContentPage
	{
		private UserAvailabilityListViewModel viewModel
		{
			get { return BindingContext as UserAvailabilityListViewModel; }
		}

		public UserAvailabilityListPage()
		{
			InitializeComponent();
		}

		protected void Lookup_Changed(object sender, EventArgs e)
		{
			viewModel.RefreshAvailabilityCommand.Execute(null);
		}

		protected async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			if (e.Item == null)
				return;

			await editAvailability((e.Item as DisplayItem).Tag as UserAvailability);

			((ListView)sender).SelectedItem = null;
		}

		private async void Edit_Clicked(object sender, EventArgs e)
		{
			await editAvailability((sender as MenuItem).CommandParameter as UserAvailability);
		}

		private async System.Threading.Tasks.Task editAvailability(UserAvailability availability)
		{
			var page = new UserAvailabilityEditPage();
			var editViewModel = new UserAvailabilityEditViewModel(viewModel.ForUser, availability, viewModel.Users.ToList(), viewModel.SelectedUser);
			page.BindingContext = editViewModel;
			await ((MainPage)App.Current.MainPage).NavigateTo(page, true);
		}

		protected async void Add_Clicked(object sender, EventArgs e)
		{
			await editAvailability(null);
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			viewModel.RefreshAvailabilityCommand.Execute(null);
		}
	}
}