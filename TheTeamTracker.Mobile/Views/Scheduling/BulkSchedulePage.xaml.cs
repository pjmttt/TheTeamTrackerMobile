using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.ViewModels.Scheduling;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheTeamTracker.Mobile.Views.Scheduling
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BulkSchedulePage : ContentPage
	{
		public BulkSchedulePage()
		{
			InitializeComponent();
			MessagingCenter.Subscribe<BulkScheduleViewModel>(this, BulkScheduleViewModel.SUCCESS, async (sender) =>
			{
				await ((MainPage)App.Current.MainPage).GoBack();
                var currPage = ((MainPage)App.Current.MainPage).Detail as TTTNavigationPage;
                if (currPage != null && currPage.RootPage is DailySchedulePage)
                    ((currPage.RootPage as DailySchedulePage).BindingContext as DailyScheduleViewModel).LoadSchedulesCommand.Execute(null);

            });
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			var viewModel = BindingContext as BulkScheduleViewModel;
			if (viewModel.Users.Count < 1)
				viewModel.LoadLookupsCommand.Execute(null);
		}
	}
}