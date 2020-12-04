using Newtonsoft.Json.Linq;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.DataLayer.Models;
using TheTeamTracker.Mobile.Views;
using Xamarin.Forms;

namespace TheTeamTracker.Mobile.ViewModels.Maintenance
{
	public class MaintenanceRequestImagesViewModel : BaseViewModel
	{
		public MaintenanceRequestImagesViewViewModel ImagesViewModel { get; }
		public MaintenanceRequest Request { get; private set; }
		public ICommand LoadImagesCommand { get; }

		public MaintenanceRequestImagesViewModel(MaintenanceRequest request)
		{
			this.Request = request;
			ImagesViewModel = new MaintenanceRequestImagesViewViewModel(false);
			this.LoadImagesCommand = new Command(async () => await this.loadImages());
		}

		private async System.Threading.Tasks.Task loadImages()
		{
			await ImagesViewModel.SetRequest(this.Request);
		}
	}
}
