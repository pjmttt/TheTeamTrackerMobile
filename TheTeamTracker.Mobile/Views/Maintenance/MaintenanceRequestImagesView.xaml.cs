using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTeamTracker.Mobile.DataLayer.Models;
using TheTeamTracker.Mobile.ViewModels.Maintenance;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheTeamTracker.Mobile.Views.Maintenance
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MaintenanceRequestImagesView : ContentView
	{
		public MaintenanceRequestImagesView()
		{
			InitializeComponent();
		}

		private async System.Threading.Tasks.Task viewImage(MaintenanceImage image)
		{
			var page = new MaintenanceRequestImagePage();
			page.BindingContext = new MaintenanceRequestImageViewModel(image);
			await((MainPage)App.Current.MainPage).NavigateTo(page, true);
		}

		//private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
		//{
		//	if (e.Item == null) return;
		//	var item = e.Item as MaintenanceImage;
		//	await viewImage(item);
		//}

		private async void View_Clicked(object sender, EventArgs e)
		{
			var btn = sender as MenuItem;
			await viewImage(btn.CommandParameter as MaintenanceImage);
		}

		private void Delete_Clicked(object sender, EventArgs e)
		{
			var viewModel = BindingContext as MaintenanceRequestImagesViewViewModel;
			viewModel.DeleteCommand.Execute((sender as MenuItem).CommandParameter);
		}

		private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
		{
			var img = (sender as Xamarin.Forms.Image).BindingContext as MaintenanceImage;
			await viewImage(img);
		}
	}
}