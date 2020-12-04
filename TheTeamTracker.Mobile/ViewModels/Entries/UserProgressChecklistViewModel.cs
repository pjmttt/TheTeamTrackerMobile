using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.DataLayer.Classes;
using TheTeamTracker.Mobile.DataLayer.Models;
using Xamarin.Forms;
namespace TheTeamTracker.Mobile.ViewModels.Entries
{
	public class UserProgressChecklistViewModel : BaseViewModel
	{
		private Guid _progressChecklistId = Guid.Empty;
		public UserProgressChecklist UserProgressChecklist { get; }
		public ObservableCollection<DisplayItem> UserProgressItems { get; }
		public ObservableCollection<ProgressChecklist> ProgressChecklists { get; }
		public ObservableCollection<User> Users { get; }
		public const string SUCCESS = "success";
		public bool ForUser { get; }
		public bool NotForUser { get { return !ForUser; } }
		public bool IsNew { get; set; }
		public bool IsNotNew
		{
			get { return !IsNew; }
		}
		public bool ShowEmployeeLabel
		{
			get { return !ForUser; }
		}
		public bool ShowEmployeePicker
		{
			get { return !ForUser && IsNew; }
		}
		public bool ShowEmployeeText
		{
			get { return !ForUser && !IsNew; }
		}
		public Command SaveCommand { get; }
		public Command LoadLookupsCommand { get; }
		public Command SetCompleteCommand { get; }
		public UserProgressChecklistViewModel(bool forUser, UserProgressChecklist checklist)
		{
			ForUser = forUser;
			var usr = LoginHelper.GetLoggedInUser();
			UserProgressChecklist = checklist ?? new DataLayer.Models.UserProgressChecklist()
			{
				StartDateValue = DateTime.Now,
				UserProgressItems = new ObservableCollection<UserProgressItem>(),
				Manager = usr.User,
				ManagerIdValue = usr.User.UserIdValue,
			};
			_progressChecklistId = UserProgressChecklist.ProgressChecklistIdValue;
			IsNew = checklist == null;
			UserProgressItems = new ObservableCollection<DisplayItem>();
			populateProgressItems();
			Users = new ObservableCollection<User>();
			ProgressChecklists = new ObservableCollection<ProgressChecklist>();
			this.SaveCommand = new Command(async () => await this.saveChecklist(true));
			this.LoadLookupsCommand = new Command(async () => await this.loadLookups());
			this.SetCompleteCommand = new Command((item) => this.setComplete(item as DisplayItem));
		}
		private void itemToDisplayItem(UserProgressItem item, DisplayItem displayItem)
		{
			displayItem.Line1 = item.ProgressItem.ItemDescription;
			displayItem.Line2 = item.CompletedDate == null ? "Pending" : (item.CompletedDateValue.ToShortDateString() + " - " + (item.CompletedBy == null ? string.Empty : item.CompletedBy.DisplayName));
			displayItem.Line3 = item.Comments;
			displayItem.Tag = item;
			displayItem.Tag2 = item.CompletedDate != null;
			displayItem.Tag3 = string.IsNullOrEmpty(item.Comments);
			displayItem.Tag4 = item.CompletedDate == null ? Color.LightGray : Color.Blue;
			displayItem.Tag5 = !ForUser && item.CompletedDate == null;
		}
		private void populateProgressItems()
		{
			UserProgressItems.Clear();
			foreach (var i in UserProgressChecklist.UserProgressItems.OrderBy(i => i.ProgressItem.Sequence))
			{
				var disp = new DisplayItem();
				itemToDisplayItem(i, disp);
				UserProgressItems.Add(disp);
			}
		}
		private async System.Threading.Tasks.Task loadLookups()
		{
			if (IsBusy)
				return;
			IsBusy = true;
			try
			{
				var lookups = await DataService.GetLookups(5);
				foreach (var u in lookups.Users.OrderBy(u => u.FirstName))
				{
					Users.Add(u);
				}
				if (UserProgressChecklist.UserId != null) UserProgressChecklist.User = Users.FirstOrDefault(u => u.UserIdValue == UserProgressChecklist.UserIdValue);
				foreach (var p in lookups.ProgressChecklists.OrderBy(p => p.ChecklistName))
				{
					ProgressChecklists.Add(p);
				}
				if (UserProgressChecklist.ProgressChecklistId != null) UserProgressChecklist.ProgressChecklist = ProgressChecklists.FirstOrDefault(pc => pc.ProgressChecklistIdValue == UserProgressChecklist.ProgressChecklistIdValue);
				OnPropertyChanged("UserProgressChecklist");
			}
			catch (Exception ex)
			{
				IsBusy = false;
				ExceptionHelper.ShowException(ex);
			}
			finally
			{
				IsBusy = false;
			}
		}
		private async System.Threading.Tasks.Task saveChecklist(bool navigateBack)
		{
			if (UserProgressChecklist.User == null || UserProgressChecklist.ProgressChecklist == null || UserProgressChecklist.StartDate == null)
			{
				MessageHelper.ShowMessage("Employee, checklist and start date required!", "Validation");
				return;
			}
			IsBusy = true;
			try
			{
				var toSave = Common.Clone<UserProgressChecklist>(UserProgressChecklist);
				toSave.UserId = toSave.User.UserId;
				toSave.User = null;
				toSave.ManagerId = toSave.Manager.UserId;
				toSave.Manager = null;
				toSave.ProgressChecklistId = toSave.ProgressChecklist.ProgressChecklistId;
				toSave.ProgressChecklist = null;
				foreach (var i in toSave.UserProgressItems)
				{
					i.CompletedById = i.CompletedBy?.UserId;
					i.CompletedBy = null;
					i.ProgressItemId = i.ProgressItem.ProgressItemId;
					i.ProgressItem = null;
				}
				if (toSave.UserProgressChecklistId != null)
					await DataService.PutItemAsync<DataLayer.Models.UserProgressChecklist>("userProgressChecklists", toSave.UserProgressChecklistId.Value, toSave);
				else
					await DataService.PostItemAsync<DataLayer.Models.UserProgressChecklist>("userProgressChecklists", toSave);
				if (navigateBack)
					MessagingCenter.Send<UserProgressChecklistViewModel>(this, SUCCESS);
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
		private void setComplete(DisplayItem userProgressDisplayItem)
		{
			var usr = LoginHelper.GetLoggedInUser();
			var userProgressItem = userProgressDisplayItem.Tag as UserProgressItem;
			userProgressItem.CompletedDate = DateTime.Now;
			userProgressItem.CompletedBy = usr.User;
			userProgressItem.CompletedByIdValue = usr.User.UserIdValue;
			//if (IsNotNew)
			//	await saveChecklist(false);
			itemToDisplayItem(userProgressItem, userProgressDisplayItem);
			userProgressDisplayItem.Refresh();
		}
		public void ChecklistChanged()
		{
			if (UserProgressChecklist.ProgressChecklistIdValue == _progressChecklistId)
				return;
			_progressChecklistId = UserProgressChecklist.ProgressChecklistIdValue;
			UserProgressChecklist.UserProgressItems.Clear();
			foreach (var i in UserProgressChecklist.ProgressChecklist.ProgressItems)
			{
				var item = new UserProgressItem() { ProgressItem = i };
				UserProgressChecklist.UserProgressItems.Add(item);
			}
			populateProgressItems();
		}
	}
}