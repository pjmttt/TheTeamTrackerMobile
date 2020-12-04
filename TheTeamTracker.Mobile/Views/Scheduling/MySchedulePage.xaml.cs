using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TheTeamTracker.Mobile.DataLayer.Models;
using TheTeamTracker.Mobile.ViewModels.Scheduling;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheTeamTracker.Mobile.Views.Scheduling
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MySchedulePage : ContentPage
	{
		private MyScheduleViewModel _viewModel;
		public MySchedulePage()
		{
			InitializeComponent();
			BindingContext = _viewModel = new MyScheduleViewModel();
		}

		protected void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			if (e.Item == null) return;
			((ListView)sender).SelectedItem = null;
		}

		protected async void Trade_Clicked(object sender, EventArgs e)
		{
			var mnuItem = (MenuItem)sender;
			var sched = mnuItem.CommandParameter as Schedule;
			if (sched.ScheduleTrades.Any())
			{
				await DisplayAlert("Trade", "Schedule has already been marked for trade!", "OK");
				return;
			}
			if (await DisplayAlert("Trade", "Are you sure you want to trade this schedule?", "Yes", "No"))
			{
				await (this.BindingContext as MyScheduleViewModel).TradeSchedule(sched);
			}
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (_viewModel.Schedules.Count == 0)
				_viewModel.LoadSchedulesCommand.Execute(null);
		}
	}
}
