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
	public partial class MaintenanceRequestImagesPage : ContentPage
	{
		public MaintenanceRequestImagesPage()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			var viewModel = BindingContext as MaintenanceRequestImagesViewModel;
			if (viewModel.ImagesViewModel.Images.Count < 1)
				viewModel.LoadImagesCommand.Execute(null);
		}
	}
}