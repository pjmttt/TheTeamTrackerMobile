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
	public partial class TradeViewPage : ContentPage
	{
		public TradeViewPage()
		{
			InitializeComponent();
			MessagingCenter.Subscribe<TradeViewViewModel>(this, TradeViewViewModel.SUCCESS, async (sender) =>
			{
				MessagingCenter.Unsubscribe<TradeViewViewModel>(this, TradeViewViewModel.SUCCESS);
				await ((MainPage)App.Current.MainPage).GoBack();
			});
		}
	}
}