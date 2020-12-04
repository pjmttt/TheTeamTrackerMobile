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
	public partial class MaintenanceRequestEditPage : ContentPage
	{
		public MaintenanceRequestEditPage()
		{
			InitializeComponent();
			MessagingCenter.Subscribe<MaintenanceRequestEditViewModel>(this, MaintenanceRequestEditViewModel.SUCCESS, async (sender) =>
			{
				MessagingCenter.Unsubscribe<MaintenanceRequestEditViewModel>(this, MaintenanceRequestEditViewModel.SUCCESS);
				await ((MainPage)App.Current.MainPage).GoBack();
			});
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			var viewModel = BindingContext as MaintenanceRequestEditViewModel;
			if (!viewModel.ImagesViewModel.Images.Any())
				viewModel.LoadImagesCommand.Execute(null);
		}
	}
}