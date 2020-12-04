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
	public partial class UserAvailabilityEditPage : ContentPage
	{
		private UserAvailabilityEditViewModel viewModel
		{
			get { return BindingContext as UserAvailabilityEditViewModel; }
		}
		public UserAvailabilityEditPage()
		{
			InitializeComponent();
			MessagingCenter.Subscribe<UserAvailabilityEditViewModel>(this, UserAvailabilityEditViewModel.SUCCESS, async (sender) =>
			{
				MessagingCenter.Unsubscribe<UserAvailabilityEditViewModel>(this, UserAvailabilityEditViewModel.SUCCESS);
				await ((MainPage)App.Current.MainPage).GoBack();
			});
		}
	}
}