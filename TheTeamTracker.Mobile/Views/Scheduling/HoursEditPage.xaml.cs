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
	public partial class HoursEditPage : ContentPage
	{
		private HoursEditViewModel viewModel
		{
			get { return BindingContext as HoursEditViewModel; }
		}
		public HoursEditPage()
		{
			InitializeComponent();
			MessagingCenter.Subscribe<HoursEditViewModel>(this, HoursEditViewModel.SUCCESS, async (sender) =>
			{
				MessagingCenter.Unsubscribe<HoursEditViewModel>(this, HoursEditViewModel.SUCCESS);
				await ((MainPage)App.Current.MainPage).GoBack();
			});
		}
	}
}