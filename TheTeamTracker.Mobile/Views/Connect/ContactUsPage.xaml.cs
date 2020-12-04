using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTeamTracker.Mobile.ViewModels.Connect;
using TheTeamTracker.Mobile.Views.Scheduling;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheTeamTracker.Mobile.Views.Connect
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ContactUsPage : ContentPage
	{
		private ContactUsViewModel viewModel
		{
			get { return this.BindingContext as ContactUsViewModel; }
		}

		public ContactUsPage()
		{
			InitializeComponent();
			MessagingCenter.Subscribe<ContactUsViewModel>(this, ContactUsViewModel.SUCCESS, async (sender) =>
			{
				await ((MainPage)App.Current.MainPage).NavigateTo(new MySchedulePage() { Title = "My Schedule" });
			});
			this.BindingContext = new ContactUsViewModel();
		}
	}
}