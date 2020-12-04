using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.DataLayer.Classes;
using TheTeamTracker.Mobile.DataLayer.Models;
using TheTeamTracker.Mobile.DataLayer.Services;
using TheTeamTracker.Mobile.Views;
using Xamarin.Forms;

namespace TheTeamTracker.Mobile.ViewModels.Maintenance
{
	public class MaintenanceRequestEditViewModel : BaseViewModel
	{
		public const string SUCCESS = "success";

		public ObservableCollection<User> Users { get; set; }
		public ObservableCollection<User> Assignees { get; set; }

		public Guid? MaintenanceRequestId { get; }
		public MaintenanceRequest Request { get; private set; }
		public List<MaintenanceRequestReply> NewReplies { get; }
		public ICommand SaveCommand { get; }
		public ICommand TakePictureCommand { get; }
		public ICommand LoadImagesCommand { get; }
		public ICommand ReplyCommand { get; }
		public bool HasReplies { get; private set; }
		public event EventHandler ItemSaved;

		public MaintenanceRequestImagesViewViewModel ImagesViewModel { get; }

		public MaintenanceRequestEditViewModel(MaintenanceRequest request, Lookups lookups)
		{
			var usr = LoginHelper.GetLoggedInUser();
			this.Request = request ?? new MaintenanceRequest()
			{
				IsAddressed = false,
				RequestDate = DateTime.Now,
				RequestedById = usr.User.UserIdValue,
				MaintenanceRequestImages = new ObservableCollection<MaintenanceRequestImage>(),
				MaintenanceRequestReplys = new ObservableCollection<MaintenanceRequestReply>()
			};
			this.HasReplies = this.Request.MaintenanceRequestReplys.Any();
			this.Users = new ObservableCollection<User>();
			this.Assignees = new ObservableCollection<User>();
			lookups.Users.ForEach(u => this.Users.Add(u));
			lookups.Users.FindAll(u => u.Roles != null && u.Roles.IndexOf((int)Constants.ROLE.MAINTENANCE_REQUESTS) >= 0).ForEach(u => this.Assignees.Add(u));
			if (this.Request.AssignedToId != null) this.Request.AssignedTo = this.Users.FirstOrDefault(u => u.UserIdValue == this.Request.AssignedToIdValue);
			if (this.Request.RequestedById != null) this.Request.RequestedBy = this.Users.FirstOrDefault(u => u.UserIdValue == this.Request.RequestedByIdValue);
			this.SaveCommand = new Command(async () => await this.saveRequest());
			this.LoadImagesCommand = new Command(async () => await this.loadImages());
			this.ReplyCommand = new Command(async () => await this.addReply());
			this.ImagesViewModel = new MaintenanceRequestImagesViewViewModel(true);
			this.NewReplies = new List<MaintenanceRequestReply>();
		}

		private async System.Threading.Tasks.Task loadImages()
		{
			await ImagesViewModel.SetRequest(this.Request);
		}

		private async System.Threading.Tasks.Task saveRequest()
		{
			if (Request.RequestedBy == null || string.IsNullOrEmpty(Request.RequestDescription))
			{
				MessageHelper.ShowMessage("Requested By and Description required!", "Validation");
				return;
			}

			if (await runTask(async () =>
			{
				var toSave = Common.Clone<MaintenanceRequest>(Request);
				if (toSave.AssignedTo != null) toSave.AssignedToId = toSave.AssignedTo.UserIdValue;
				toSave.AssignedTo = null;
				if (toSave.RequestedBy != null) toSave.RequestedByIdValue = toSave.RequestedBy.UserIdValue;
				toSave.AssignedTo = null;
				MaintenanceRequest saved = null;
				if (toSave.MaintenanceRequestId != null)
					saved = await DataService.PutItemAsync<MaintenanceRequest>("maintenanceRequests", toSave.MaintenanceRequestIdValue, toSave);
				else
					saved = await DataService.PostItemAsync<MaintenanceRequest>("maintenanceRequests", toSave);


				foreach (var ni in ImagesViewModel.NewImages)
				{
					var filesToSend = new List<FileToSend>();
					filesToSend.Add(new FileToSend()
					{
						Bytes = ni.ImageBytes,
						FileName = Guid.NewGuid().ToString() + ".jpg",
						ContentType = ni.MaintenanceRequestImage.ImageType
					});

					var results = await DataService.SendFiles<List<MaintenanceRequestImage>>($"maintenanceRequestImage/{saved.MaintenanceRequestIdValue}",
						filesToSend, true);
					ni.MaintenanceRequestImage.MaintenanceRequestImageIdValue = results[0].MaintenanceRequestImageIdValue;
				}

				if (NewReplies.Any())
				{
					NewReplies.ForEach(nr => nr.MaintenanceRequestIdValue = saved.MaintenanceRequestIdValue);
					var results = await DataService.PostAsync<List<MaintenanceRequestReply>, List<MaintenanceRequestReply>>($"maintenanceRequestReplys", NewReplies);
					foreach (var result in results)
					{
						var nr = NewReplies.First(r => r.ReplyText == result.ReplyText && r.MaintenanceRequestReplyId == null);
						nr.MaintenanceRequestReplyIdValue = result.MaintenanceRequestReplyIdValue;
					}
				}

				foreach (var di in ImagesViewModel.DeletedImages)
				{
					if (di.MaintenanceRequestImageId != null)
						await DataService.DeleteItemAsync("maintenanceRequestImages", di.MaintenanceRequestImageIdValue);
				}

				Request.MaintenanceRequestId = saved.MaintenanceRequestId;
			}))
			{
				ItemSaved?.Invoke(Request, new EventArgs());
				MessagingCenter.Send<MaintenanceRequestEditViewModel>(this, SUCCESS);
			}
		}

		private async System.Threading.Tasks.Task addReply()
		{
			var results = await Acr.UserDialogs.UserDialogs.Instance.PromptAsync("Reply Text");
			var reply = new MaintenanceRequestReply()
			{
				// MaintenanceRequestReplyIdValue = Guid.NewGuid(),
				ReplyDateValue = DateTime.Now,
				ReplyText = results.Text
			};
			this.NewReplies.Add(reply);
			Request.MaintenanceRequestReplys.Add(reply);
			this.HasReplies = true;
			OnPropertyChanged(nameof(HasReplies));
		}
	}
}
