using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.DataLayer.Models;
using Xamarin.Forms;

namespace TheTeamTracker.Mobile.ViewModels.Entries
{
	public class UserProgressChecklistsViewModel : BaseViewModel
	{
		public ObservableCollection<DisplayItem> UserProgressChecklists { get; set; }
		public bool ForUser { get; set; }
		public bool NotForUser {  get { return !ForUser; } }
		public bool NoChecklists { get; set; }

		public Command LoadUserProgressChecklistsCommand { get; set; }
		public Command DeleteCommand { get; set; }

		public UserProgressChecklistsViewModel(bool forUser)
		{
			UserProgressChecklists = new ObservableCollection<DisplayItem>();
			LoadUserProgressChecklistsCommand = new Command(async () => await ExecuteLoadUserProgressChecklistsCommand());
			DeleteCommand = new Command(async (checklist) => await deleteChecklist(checklist as UserProgressChecklist));
			ForUser = forUser;
		}

		async System.Threading.Tasks.Task ExecuteLoadUserProgressChecklistsCommand()
		{
			UserProgressChecklists.Clear();
			var checklists = await runTask(async () => (await DataService.GetItemsAsync<DataLayer.Models.UserProgressChecklist>("userProgressChecklists" + (ForUser ? "?forUser=true" : ""))).Data);
			NoChecklists = !checklists.Any();
			OnPropertyChanged("NoChecklists");
			checklists = checklists.OrderBy(c => ForUser ? c.ProgressChecklist.ChecklistName : c.User.DisplayName).ToList();
			foreach (var item in checklists)
			{
				UserProgressChecklists.Add(new DisplayItem()
				{
					Line1 = ForUser ? "" : item.User.DisplayName,
					Line2 = item.ProgressChecklist.ChecklistName,
					Tag = item,
					Tag2 = item.CompletedDate == null ? Color.LightGray : Color.Blue,
					Tag3 = NotForUser
				});
			}
		}

		private async System.Threading.Tasks.Task deleteChecklist(UserProgressChecklist checklist)
		{
			IsBusy = true;
			try
			{
				await DataService.DeleteItemAsync("userProgressChecklists", checklist.UserProgressChecklistIdValue);
				IsBusy = false;
				await ExecuteLoadUserProgressChecklistsCommand();
			}
			catch (Exception ex)
			{
				IsBusy = false;
				ExceptionHelper.ShowException(ex);
			}
		}
	}
}
