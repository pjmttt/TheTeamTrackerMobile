using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TheTeamTracker.Mobile.ViewModels.Scheduling;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheTeamTracker.Mobile.Views.Scheduling
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TradeListPage : ContentPage
    {
		private TradeListViewModel viewModel
		{
			get { return BindingContext as TradeListViewModel; }
		}

		public TradeListPage()
        {
            InitializeComponent();
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

			var page = new TradeViewPage();
			page.BindingContext = new TradeViewViewModel(viewModel.ForUser, e.Item as TradeListItem);
			await ((MainPage)App.Current.MainPage).NavigateTo(page, true);

			((ListView)sender).SelectedItem = null;
        }

		protected override void OnAppearing()
		{
			base.OnAppearing();

			viewModel.RefreshTradesCommand.Execute(null);
		}
	}
}
