using System;
using System.Collections.Generic;
using System.Text;

namespace TheTeamTracker.Mobile.ViewModels.Maintenance
{
	public class MaintenanceRequestImageViewModel : BaseViewModel
	{
		public MaintenanceImage Image { get; }
		public MaintenanceRequestImageViewModel(MaintenanceImage image)
		{
			this.Image = image;
		}

	}
}
