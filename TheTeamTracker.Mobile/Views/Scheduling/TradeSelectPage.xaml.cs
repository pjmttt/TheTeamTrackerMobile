using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TheTeamTracker.Mobile.DataLayer.Models;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.ViewModels.Scheduling;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheTeamTracker.Mobile.Views.Scheduling
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TradeSelectPage : ContentPage
	{
		private TradeSelectViewModel viewModel
		{
			get { return BindingContext as TradeSelectViewModel; }
		}

		public TradeSelectPage()
		{
			InitializeComponent();
			MessagingCenter.Subscribe<TradeSelectViewModel>(this, TradeSelectViewModel.SUCCESS, async (sender) =>
			{
				await ((MainPage)App.Current.MainPage).GoBack();
			});
		}

		protected async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			if (e.Item == null)
				return;

			var sched = (e.Item as DisplayItem).Tag as Schedule;

			((ListView)sender).SelectedItem = null;

			bool cont = false;
			if (sched != null && viewModel.ScheduleConflicts(sched))
			{
				if (await DisplayAlert("Conflict", "The selected schedule conflicts with one of yours, continue?", "Yes", "No"))
				{
					cont = true;
				}
			}
			else if (await DisplayAlert("Trade", "Are you sure you want to trade this schedule?", "Yes", "No"))
			{
				cont = true;
			}

			if (!cont) return;
			await viewModel.TradeSchedule(sched);

		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			viewModel.LoadSchedulesCommand.Execute(null);
		}
	}
}
