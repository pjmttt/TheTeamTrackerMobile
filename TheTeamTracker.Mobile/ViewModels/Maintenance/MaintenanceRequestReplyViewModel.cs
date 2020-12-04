using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.DataLayer.Models;
using TheTeamTracker.Mobile.DataLayer.Services;
using TheTeamTracker.Mobile.Views;
using Xamarin.Forms;

namespace TheTeamTracker.Mobile.ViewModels.Maintenance
{
    public class MaintenanceRequestReplyViewModel : BaseViewModel
    {
		public const string SUCCESS = "success";

		public MaintenanceRequestReply Reply { get; }
		public MaintenanceRequest Request { get; }
		public bool CanEdit { get; }

		public ICommand SaveCommand { get; }

		public bool HasReplys
		{
			get { return Request.MaintenanceRequestReplys.Any(); }
		}

		public MaintenanceRequestReplyViewModel(MaintenanceRequest request)
		{
			CanEdit = AuthService.UserToken.User.Roles.IndexOf((int)Constants.ROLE.ADMIN) >= 0 ||
				AuthService.UserToken.User.Roles.IndexOf((int)Constants.ROLE.MANAGER) >= 0;
			this.Request = request;
			this.Reply = new MaintenanceRequestReply() { MaintenanceRequestId = request.MaintenanceRequestId };
			this.SaveCommand = new Command(async () => await this.saveReply());
		}

		private async System.Threading.Tasks.Task saveReply()
		{
			if (string.IsNullOrEmpty(Reply.ReplyText))
			{
				MessageHelper.ShowMessage("Reply text is required!", "Validation");
				return;
			}
			IsBusy = true;
			try
			{
				if (!string.IsNullOrEmpty(Reply.ReplyText))
				{
					Reply.ReplyDate = DateTime.Now;
					var reply = await DataService.PostItemAsync<MaintenanceRequestReply>("maintenanceRequestReplys", Reply);
					this.Request.MaintenanceRequestReplys.Add(reply);
				}
				MessagingCenter.Send<MaintenanceRequestReplyViewModel>(this, SUCCESS);
				IsBusy = false;


			}
			catch (Exception ex)
			{
				IsBusy = false;
				if (ex.InnerException != null)
				{
					ex = ex.InnerException;
				}
				ExceptionHelper.ShowException(ex);
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}
