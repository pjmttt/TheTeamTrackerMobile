using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTeamTracker.Mobile.ViewModels.Connect;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheTeamTracker.Mobile.Views.Connect
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EmailStaffPage : ContentPage
	{
		private EmailStaffViewModel viewModel
		{
			get { return this.BindingContext as EmailStaffViewModel; }
		}

		public EmailStaffPage()
		{
			InitializeComponent();
			this.BindingContext = new EmailStaffViewModel();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			if (viewModel.Users.Count < 1)
				viewModel.LoadLookupsCommand.Execute(null);
		}



		protected void User_Changed(object sender, EventArgs e)
		{
			viewModel.UserChangedCommand.Execute(null);
		}

		protected void Position_Changed(object sender, EventArgs e)
		{
			viewModel.PositionChangedCommand.Execute(null);
		}
	}
}