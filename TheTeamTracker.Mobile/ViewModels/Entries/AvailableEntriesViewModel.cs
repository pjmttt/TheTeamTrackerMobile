using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.DataLayer.Models;
using TheTeamTracker.Mobile.DataLayer.Services;
using Xamarin.Forms;

namespace TheTeamTracker.Mobile.ViewModels.Entries
{
    public class AvailableEntriesViewModel : BaseViewModel
    {
        public ObservableCollection<DisplayItem> Entries { get; set; }
        public Command LoadEntriesCommand { get; set; }
		public Command DeleteCommand { get; }
		public Command SelectCommand { get; }

		public bool NoEntries
		{
			get { return !Entries.Any(); }
		}

		public bool CanEdit { get; set; }
		public bool CannotEdit {  get { return !CanEdit; } }

        public AvailableEntriesViewModel()
        {
            Entries = new ObservableCollection<DisplayItem>();
            LoadEntriesCommand = new Command(async () => await ExecuteLoadEntriesCommand());
			CanEdit = AuthService.UserToken.User.Roles.IndexOf((int)Constants.ROLE.MANAGER) >= 0;
			DeleteCommand = new Command(async (parameter) => await ExecuteDeleteCommand(parameter as DataLayer.Models.Entry));
			SelectCommand = new Command(async (parameter) => await ExecuteSelectCommand(parameter as DataLayer.Models.Entry));
		}

		async System.Threading.Tasks.Task ExecuteLoadEntriesCommand()
        {
            if (IsBusy)
                return;

			if (!IsRefreshing)
				IsBusy = true;

			try
            {
                Entries.Clear();
                var items = await DataService.GetItemsAsync<DataLayer.Models.Entry>("generalentries?extras=true");
                foreach (var item in items.Data)
                {
					Entries.Add(new DisplayItem()
					{
						Line1 = item.Task.TaskName,
						Line2 = item.Comments,
						Tag = item,
						Tag2 = !string.IsNullOrEmpty(item.Comments)
					});
                }
				OnPropertyChanged("NoEntries");
				IsRefreshing = false;
				IsBusy = false;
			}
			catch (Exception ex)
            {
				IsRefreshing = false;
				IsBusy = false;
				ExceptionHelper.ShowException(ex);
            }
            finally
            {
				IsRefreshing = false;
				IsBusy = false;
			}
        }

		public async System.Threading.Tasks.Task ExecuteDeleteCommand(DataLayer.Models.Entry entry)
		{
			try
			{
				IsBusy = true;
				await DataService.DeleteItemAsync("entries", entry.EntryIdValue);
				IsBusy = false;
				await ExecuteLoadEntriesCommand();
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

		public async System.Threading.Tasks.Task ExecuteSelectCommand(DataLayer.Models.Entry entry)
		{
			try
			{
				IsBusy = true;
				await DataService.PostItemAsync<object>("pickupEntry", new { entryId = entry.EntryIdValue });
				IsBusy = false;
				await ExecuteLoadEntriesCommand();
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
	}
}
