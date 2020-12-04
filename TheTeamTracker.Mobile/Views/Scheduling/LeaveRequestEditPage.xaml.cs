using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTeamTracker.Mobile.ViewModels.Scheduling;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheTeamTracker.Mobile.Views.Scheduling
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LeaveRequestEditPage : ContentPage
	{
		private LeaveRequestEditViewModel viewModel
		{
			get { return BindingContext as LeaveRequestEditViewModel; }
		}
		public LeaveRequestEditPage()
		{
			InitializeComponent();
			MessagingCenter.Subscribe<LeaveRequestEditViewModel>(this, LeaveRequestEditViewModel.SUCCESS, async (sender) =>
			{
				MessagingCenter.Unsubscribe<LeaveRequestEditViewModel>(this, LeaveRequestEditViewModel.SUCCESS);
				await ((MainPage)App.Current.MainPage).GoBack();
			});
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (viewModel.Users.Count == 0)
				viewModel.LoadLookupsCommand.Execute(null);
		}
	}
}