using System;
using System.Collections.Generic;
using System.Text;
using TheTeamTracker.Mobile.Classes;
using Xamarin.Forms;

namespace TheTeamTracker.Mobile.ViewModels.Connect
{
	public class ContactUsViewModel : BaseViewModel
	{
		public const string SUCCESS = "success";

		public string Message { get; set; }
		public Command SendCommand { get; }

		public ContactUsViewModel()
		{
			this.SendCommand = new Command(async () => await this.sendMessage());
		}

		private async System.Threading.Tasks.Task sendMessage()
		{
			if (await runTask(async () =>
			{
				await DataService.PostItemAsync<object>("contactus", new { message = this.Message });

			}))
			{
				MessageHelper.ShowMessage("Message has been sent.", "Success");
				MessagingCenter.Send<ContactUsViewModel>(this, SUCCESS);
			}
		}
	}
}
