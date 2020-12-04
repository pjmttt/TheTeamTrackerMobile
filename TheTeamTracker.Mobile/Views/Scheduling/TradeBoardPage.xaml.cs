using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.DataLayer.Models;
using TheTeamTracker.Mobile.ViewModels.Scheduling;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheTeamTracker.Mobile.Views.Scheduling
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TradeBoardPage : ContentPage
	{
		private TradeBoardViewModel _viewModel;
		public TradeBoardPage()
		{
			InitializeComponent();

			BindingContext = _viewModel = new TradeBoardViewModel();
		}

		protected async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			if (e.Item == null)
				return;
			await selectSchedule((e.Item as DisplayItem).Tag as ScheduleTrade);
			((ListView)sender).SelectedItem = null;
		}

		private async void TradeForButtonItem_Clicked(object sender, EventArgs e)
		{
			var menuButtonItem = sender as MenuButtonItem;
			var page = new TradeSelectPage();
			page.BindingContext = new TradeSelectViewModel(_viewModel.StartDate, menuButtonItem.CommandParameter as ScheduleTrade);
			await((MainPage)App.Current.MainPage).NavigateTo(page, true);
		}

		private async System.Threading.Tasks.Task selectSchedule(ScheduleTrade trade)
		{
			var page = new TradeSelectPage();
			page.BindingContext = new TradeSelectViewModel(_viewModel.StartDate, trade);
			await ((MainPage)App.Current.MainPage).NavigateTo(page, true);
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			_viewModel.LoadSchedulesCommand.Execute(null);
		}
	}
}
