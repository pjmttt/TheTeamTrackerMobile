using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTeamTracker.Mobile.ViewModels.Maintenance;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheTeamTracker.Mobile.Views.Maintenance
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MaintenanceRequestReplyPage : ContentPage
	{
		private MaintenanceRequestReplyViewModel viewModel
		{
			get { return this.BindingContext as MaintenanceRequestReplyViewModel; }
		}
		public MaintenanceRequestReplyPage()
		{
			InitializeComponent();
			MessagingCenter.Subscribe<MaintenanceRequestReplyViewModel>(this, MaintenanceRequestReplyViewModel.SUCCESS, async (sender) =>
			{
				MessagingCenter.Unsubscribe<MaintenanceRequestReplyViewModel>(this, MaintenanceRequestReplyViewModel.SUCCESS);
				await ((MainPage)App.Current.MainPage).GoBack();
			});
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			if (this.ToolbarItems.Count < 1 && viewModel.CanEdit)
			{
				this.ToolbarItems.Add(new ToolbarItem()
				{
					Icon = "ic_save_white.png",
					Command = viewModel.SaveCommand
				});
			}
		}
	}
}