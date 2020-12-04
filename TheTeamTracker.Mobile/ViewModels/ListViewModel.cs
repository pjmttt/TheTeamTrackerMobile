using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheTeamTracker.Mobile.Classes;
using Xamarin.Forms;
using Xamarin.Forms.Extended;

namespace TheTeamTracker.Mobile.ViewModels
{
	public abstract class ListViewModel<TEntity> : BaseViewModel
	{
		private const int PAGE_SIZE = 20;
		private const string ASCENDING = " (ascending)";
		private const string DESCENDING = " (descending)";
		private bool _hasMore;
		private bool _listInited;
		private bool _viewModelInited;

		protected List<Tuple<string, bool>> sortQueue { get; }
		protected abstract void populateDisplayItem(DisplayItem item, TEntity entity);
		protected abstract string endpoint { get; }
		protected virtual string getPostParams() { return string.Empty; }

		protected Dictionary<string, string> sortFields { get; }

		public bool NoResults { get; set; }

		public InfiniteScrollCollection<DisplayItem> DataSource { get; set; }
		public Command LoadDataCommand { get; set; }
		bool isScrolling = false;
		public bool IsScrolling
		{
			get { return isScrolling; }
			set { SetProperty(ref isScrolling, value); }
		}


		public ListViewModel() : base()
		{
			DataSource = new InfiniteScrollCollection<DisplayItem>()
			{
				OnLoadMore = async () =>
				{
					if (IsBusy) return null;
					var page = (DataSource.Count / PAGE_SIZE) + 1;
					return await getData(page);
				},
				OnCanLoadMore = () => _hasMore
			};
			LoadDataCommand = new Command(async () => await executeLoadDataCommand());
			sortFields = new Dictionary<string, string>();
			sortQueue = new List<Tuple<string, bool>>();
		}

		protected async System.Threading.Tasks.Task<List<DisplayItem>> getData(int page)
		{
			var displayItems = new List<DisplayItem>();
			var sort = string.Empty;
			bool firstIn = true;
			for (int i = sortQueue.Count - 1; i >= 0; i--)
			{
				var q = sortQueue[i];
				sort += $"{(firstIn ? "&orderBy=" : ",")}{q.Item1}{(q.Item2 ? " desc" : "")}";
				firstIn = false;
			}
			var items = await DataService.GetItemsAsync<TEntity>($"{endpoint}?limit={PAGE_SIZE}&offset={(page - 1) * PAGE_SIZE}{sort}{getPostParams()}");
			var entities = items.Data;
			_hasMore = DataSource.Count + entities.Count < items.Count;
			foreach (var entity in entities)
			{
				var displayItem = new DisplayItem();
				populateDisplayItem(displayItem, entity);
				displayItems.Add(displayItem);
			}
			return displayItems;
		}

		private async System.Threading.Tasks.Task executeLoadDataCommand()
		{
			await runTask(async () =>
			{
				if (!_viewModelInited)
				{
					_viewModelInited = true;
					await onInit();
				}
				DataSource.Clear();
				(await getData(1)).ForEach(i => DataSource.Add(i));

				NoResults = !DataSource.Any();
				OnPropertyChanged(nameof(NoResults));
			});
		}

		protected virtual System.Threading.Tasks.Task onInit() { return null; }

		public void DecorateListView(ListView listView, IList<ToolbarItem> toolbarItems = null)
		{
			if (_listInited) return;
			_listInited = true;
			listView.SetBinding(ListView.ItemsSourceProperty, new Binding(nameof(DataSource)));
			listView.HasUnevenRows = true;
			listView.IsPullToRefreshEnabled = true;
			listView.SetBinding(ListView.IsRefreshingProperty, new Binding(nameof(IsRefreshing)));
			listView.SetBinding(ListView.RefreshCommandProperty, new Binding(nameof(LoadDataCommand)));
			var behavior = new InfiniteScrollBehavior();
			behavior.SetBinding(InfiniteScrollBehavior.IsLoadingMoreProperty, new Binding(nameof(IsScrolling)));
			listView.Behaviors.Add(behavior);
			var activityIndicator = new ActivityIndicator() { VerticalOptions = LayoutOptions.End, HorizontalOptions = LayoutOptions.CenterAndExpand, HeightRequest = 70 };
			activityIndicator.BindingContext = this;
			activityIndicator.SetBinding(ActivityIndicator.IsVisibleProperty, new Binding(nameof(IsScrolling)));
			activityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, new Binding(nameof(IsScrolling)));
			listView.Footer = activityIndicator;

			if (toolbarItems != null)
			{
				List<string> items = new List<string>();
				foreach (var kvp in sortFields)
				{
					items.Add($"{kvp.Key}{ASCENDING}");
					items.Add($"{kvp.Key}{DESCENDING}");
				}
				var item = new ToolbarItem() { Icon = "ic_sort_by_alpha_white.png" };
				item.Clicked += async (object sender, EventArgs e) =>
				{
					var selected = await Acr.UserDialogs.UserDialogs.Instance.ActionSheetAsync("Sort", "Cancel", null, null, items.ToArray());
					if (!string.IsNullOrEmpty(selected) && selected != "Cancel")
						sortSelected(selected);
				};
				toolbarItems.Add(item);
			}
		}

		private void sortSelected(string item)
		{
			bool sortDescending = false;
			string currentSortField = string.Empty;
			if (item.EndsWith(DESCENDING))
			{
				sortDescending = true;
				currentSortField = sortFields[item.Replace(DESCENDING, "")];
			}
			else
			{
				sortDescending = false;
				currentSortField = sortFields[item.Replace(ASCENDING, "")];
			}
			if (currentSortField == "user")
			{
				currentSortField = $"user.firstName{(sortDescending ? " desc" : "")},user.lastName{(sortDescending ? " desc" : "")}";
				sortDescending = false;
				var oldusersort = sortQueue.FirstOrDefault(k => k.Item1.Contains("user.firstName"));
				if (oldusersort != null)
					sortQueue.Remove(oldusersort);
			}
			var oldsort = sortQueue.FirstOrDefault(k => k.Item1 == currentSortField);
			if (oldsort != null)
				sortQueue.Remove(oldsort);
			sortQueue.Add(new Tuple<string, bool>(currentSortField, sortDescending));
			LoadDataCommand.Execute(null);
		}

		protected string getDateRangeWhere(string fld, DateTime start, DateTime end)
		{
			var where = string.Empty;
			where += $"{fld} gte {start.ToString("MM-dd-yyyy")}";
			where += $",{fld} lt {end.AddDays(1).ToString("MM-dd-yyyy")}";
			return where;
		}
	}
}
