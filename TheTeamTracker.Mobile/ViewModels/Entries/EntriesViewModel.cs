using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.DataLayer.Models;
using Xam.Plugin;
using Xamarin.Forms;
using Xamarin.Forms.Extended;

namespace TheTeamTracker.Mobile.ViewModels.Entries
{
	public class EntriesViewModel : BaseViewModel
	{
		private const int PAGE_SIZE = 20;
		private const string ASCENDING = " (ascending)";
		private const string DESCENDING = " (descending)";
		public ObservableCollection<EntryItem> Entries { get; set; }
		public Command LoadEntriesCommand { get; set; }
		public Command PopulateFromScheduleCommand { get; set; }
		public string SubURL { get; set; }
		public bool IsNotGeneral { get; set; }
		public bool ForUser { get; set; }
		public bool NotForUser { get { return !ForUser; } }
		public bool CanPopulate { get { return NotForUser && IsNotGeneral; } }
		public DateTime EntryDate { get; set; }

		public bool ForSearch { get; set; }
		public bool NotForSearch { get { return !ForSearch; } }
		public ObservableCollection<User> Users { get; set; }
		public ObservableCollection<Task> Tasks { get; set; }
		public User SelectedUser { get; set; }
		public Task SelectedTask { get; set; }
		public bool ShowFilter { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		public string EntryDateFormatted { get; set; }
		public bool NoEntries
		{
			get { return !Entries.Any(); }
		}
		bool isScrolling = false;
		public bool IsScrolling
		{
			get { return isScrolling; }
			set { SetProperty(ref isScrolling, value); }
		}
		private Guid _allGID;
		private int _userSort = -1;
		private int _dateSort = 1;
		private bool _dateFirst = true;
		private bool _hasMore;

		public EntriesViewModel(bool forUser, bool general, bool forSearch)
		{
			_allGID = Guid.NewGuid();
			SubURL = general ? "generalentries" : "entries";
			ForUser = forUser;
			IsNotGeneral = !general;
			if (forSearch)
			{
				Entries = new InfiniteScrollCollection<EntryItem>()
				{
					OnLoadMore = async () =>
					{
						if (IsBusy) return null;
						var page = (Entries.Count / PAGE_SIZE) + 1;
						return await getData(page);
					},
					OnCanLoadMore = () => _hasMore
				};
			}
			else
			{
				Entries = new ObservableCollection<EntryItem>();
			}
			Users = new ObservableCollection<User>();
			Tasks = new ObservableCollection<Task>();
			LoadEntriesCommand = new Command(async () => await ExecuteLoadEntriesCommand());
			PopulateFromScheduleCommand = new Command(async () => await ExecutePopulateFromScheduleCommand());
			EntryDate = DateTime.Now;
			StartDate = DateTime.Now;
			EndDate = DateTime.Now;
			ForSearch = forSearch;
			ShowFilter = ForSearch;
		}
		public void ToggleFilter()
		{
			this.ShowFilter = !this.ShowFilter;
			OnPropertyChanged("ShowFilter");
		}
		private async System.Threading.Tasks.Task ExecuteLoadEntriesCommand()
		{
			await runTask(async () =>
			{
				if (this.ForSearch && !this.Users.Any())
				{
					var lookups = await DataService.GetLookups(0);
					Users.Add(new User() { UserIdValue = _allGID, FirstName = "(All)" });
					lookups.Users.ForEach(u => this.Users.Add(u));
					Tasks.Add(new Task() { TaskIdValue = _allGID, TaskName = "(All)" });
					lookups.Tasks.ForEach(t => this.Tasks.Add(t));
				}
				Entries.Clear();
				(await getData(1)).ForEach(d => Entries.Add(d));
			});
			OnPropertyChanged("NoEntries");
		}
		protected async System.Threading.Tasks.Task<List<EntryItem>> getData(int page)
		{
			var entries = new List<EntryItem>();
			Items<DataLayer.Models.Entry> items = null;

			if (this.ForSearch)
			{
				var searchParams = new EntrySearchParameters();
				if (SelectedUser != null && SelectedUser.UserIdValue != _allGID)
				{
					searchParams.Users.Add(SelectedUser.UserIdValue);
				}
				if (SelectedTask != null && SelectedTask.TaskIdValue != _allGID)
				{
					searchParams.Tasks.Add(SelectedTask.TaskIdValue);
				}
				searchParams.StartDate = StartDate;
				searchParams.EndDate = EndDate;
				var sort = "&orderBy=";
				if (_dateFirst)
				{
					sort += "entryDate" + (_dateSort == 1 ? " desc" : "");
					sort += ",user.firstName" + (_userSort == 1 ? " desc" : "");
					sort += ",user.lastName" + (_userSort == 1 ? " desc" : "");
				}
				else
				{
					sort += "user.firstName" + (_userSort == 1 ? " desc" : "");
					sort += ",user.lastName" + (_userSort == 1 ? " desc" : "");
					sort += ",entryDate" + (_dateSort == 1 ? " desc" : "");
				}
				items = await DataService.PostAsync<EntrySearchParameters, Items<DataLayer.Models.Entry>>($"searchEntries?limit={PAGE_SIZE}&offset={(page - 1) * PAGE_SIZE}{sort}", searchParams);
			}
			else
			{
				EntryDateFormatted = EntryDate.ToShortDateString();
				OnPropertyChanged("EntryDateFormatted");
				items = await DataService.GetItemsAsync<DataLayer.Models.Entry>($"{SubURL}?start={EntryDate.ToString("MM-dd-yyyy")}&end={EntryDate.ToString("MM-dd-yyyy")}" + (ForUser ? "&forUser=true" : ""));
			}

			var data = items.Data;
			_hasMore = Entries.Count + data.Count < items.Count;
			if (!ForSearch)
			{
				data.Sort((a, b) =>
				{
					var dateVal = _dateSort * (int)(b.EntryDateValue - a.EntryDateValue).TotalSeconds;
					var userVal = 0;
					if (a.User != null && b.User != null)
					{
						userVal = _userSort * string.Compare(b.User.DisplayName, a.User.DisplayName, true);
					}

					if ((_dateFirst && dateVal != 0) || (!_dateFirst && userVal == 0)) return dateVal;
					if ((_dateFirst && dateVal == 0) || (!_dateFirst && userVal != 0)) return userVal;
					return 0;
				});
			}

			foreach (var item in data)
			{
				if (item.UserId == null) continue;
				entries.Add(new EntryItem() { Entry = item });
			}
			return entries;
		}
		async System.Threading.Tasks.Task ExecutePopulateFromScheduleCommand()
		{
			IsBusy = true;
			try
			{
				await DataService.PostItemAsync("entriesFromSchedule", new
				{
					forDate = this.EntryDate.ToString("MM/dd/yyyy")
				});
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
			await ExecuteLoadEntriesCommand();
		}
		public async System.Threading.Tasks.Task ShowSort()
		{
			List<string> items = new List<string>();
			items.Add($"Date {ASCENDING}");
			items.Add($"Date {DESCENDING}");
			items.Add($"Employee {ASCENDING}");
			items.Add($"Employee {DESCENDING}");

			var selected = await Acr.UserDialogs.UserDialogs.Instance.ActionSheetAsync("Sort", "Cancel", null, null, items.ToArray());
			if (!string.IsNullOrEmpty(selected) && selected != "Cancel")
				sortSelected(items.IndexOf(selected));
		}
		private void sortSelected(int index)
		{
			if (index < 2)
			{
				_dateSort = index == 0 ? -1 : 1;
				_dateFirst = true;
			}
			else
			{
				_userSort = index == 2 ? -1 : 1;
				_dateFirst = false;
			}
			LoadEntriesCommand.Execute(null);
		}

	}
	public class EntryItem : INotifyPropertyChanged
	{
		public DataLayer.Models.Entry Entry { get; set; }
		private List<StatusWithColor> _statuses;
		public event PropertyChangedEventHandler PropertyChanged;
		public List<StatusWithColor> Statuses
		{
			get
			{
				if (_statuses == null && Entry != null)
					_statuses = Entry.DistinctStatuses.Select(s => new StatusWithColor(s)).ToList();
				return _statuses;
			}
		}
		public void ResetStatuses()
		{
			_statuses = null;
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Statuses"));
			Entry.OnPropertyChanged("HeaderText");
		}
	}
	public class StatusWithColor
	{
		public string StatusName { get; set; }
		public Color BackgroundColor { get; set; }
		public Color TextColor { get; set; }
		public StatusWithColor(Status status)
		{
			StatusName = status.Abbreviation;
			BackgroundColor = !string.IsNullOrEmpty(status.BackgroundColor) ? Color.FromHex(status.BackgroundColor) : Color.White;
			TextColor = !string.IsNullOrEmpty(status.TextColor) ? Color.FromHex(status.TextColor) : Color.Black;
		}
	}

	public class EntrySearchParameters
	{
		[JsonProperty("users")]
		public List<Guid> Users { get; set; }
		[JsonProperty("tasks")]
		public List<Guid> Tasks { get; set; }
		[JsonProperty("startDate")]
		public DateTime StartDate { get; set; }
		[JsonProperty("endDate")]
		public DateTime EndDate { get; set; }

		public EntrySearchParameters()
		{
			Users = new List<Guid>();
			Tasks = new List<Guid>();
		}
	}
}
