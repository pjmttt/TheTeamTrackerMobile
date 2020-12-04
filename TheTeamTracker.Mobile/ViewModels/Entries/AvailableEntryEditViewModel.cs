using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.DataLayer.Classes;
using TheTeamTracker.Mobile.DataLayer.Models;
using TheTeamTracker.Mobile.DataLayer.Services;
using TheTeamTracker.Mobile.Views.Entries;
using Xamarin.Forms;

namespace TheTeamTracker.Mobile.ViewModels.Entries
{
	public class AvailableEntryEditViewModel : BaseViewModel
	{
		public const string SUCCESS = "success";
		public DataLayer.Models.Entry Entry { get; set; }
		public ObservableCollection<Task> Tasks { get; set; }
		public Command LoadScreenCommand;
		public Command SaveCommand { get; }

		public AvailableEntryEditViewModel(DataLayer.Models.Entry entry)
		{
			Entry = entry ?? new DataLayer.Models.Entry() { EntryType = 1 };
			LoadScreenCommand = new Command(async () => await ExecuteLoadScreen());
			Tasks = new ObservableCollection<Task>();
			this.SaveCommand = new Command(async () => await this.saveEntry());
		}

		async System.Threading.Tasks.Task ExecuteLoadScreen()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
                if (Tasks.Count < 1)
                {
                    var lookups = await DataService.GetLookups(1);
                    lookups.Tasks.ForEach(t => Tasks.Add(t));
                    if (Entry.TaskId != null) Entry.Task = lookups.Tasks.FirstOrDefault(t => t.TaskId == Entry.TaskId);
					if (Entry.EnteredById == null)
					{
						var usr = AuthService.UserToken.User;
						Entry.EnteredBy = usr;
						Entry.EnteredById = usr.UserId;
					}
                }

				OnPropertyChanged("Entry");
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

		private async System.Threading.Tasks.Task saveEntry()
		{
			IsBusy = true;
			try
			{
				var forSave = Entry.GetForSave<DataLayer.Models.Entry>(); // EntitySaveHelper.GetEntryForSave(Entry);
				if (Entry.EntryId != null)
				{
					var saved = await DataService.PutItemAsync<DataLayer.Models.Entry>("generalentries", forSave.EntryId.Value, forSave);
					Entry.EntryId = saved.EntryId;
				}
				else
				{
					var saved = await DataService.PostItemAsync<DataLayer.Models.Entry>("generalentries", forSave);
					Entry.EntryId = saved.EntryId;
				}
				MessagingCenter.Send<AvailableEntryEditViewModel>(this, SUCCESS);
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
