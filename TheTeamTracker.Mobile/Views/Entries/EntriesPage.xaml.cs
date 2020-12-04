using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TheTeamTracker.Mobile.DataLayer.Models;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.ViewModels.Entries;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Extended;

namespace TheTeamTracker.Mobile.Views.Entries
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EntriesPage : ContentPage
	{
		private EntriesViewModel viewModel
		{
			get { return this.BindingContext as EntriesViewModel; }
		}
		public EntriesPage()
		{
			InitializeComponent();
		}

		private async void addToolbarItem_Click(object sender, EventArgs e)
		{
			var editViewModel = new EntryEditViewModel(null, viewModel.IsNotGeneral ? 0 : 1, viewModel.ForUser, viewModel.EntryDate);
			editViewModel.EntrySaved += (object sender2, EventArgs e2) =>
			{
				viewModel.Entries.Add(new EntryItem() { Entry = sender2 as DataLayer.Models.Entry });
			};
			var page = new EntryEditPage();
			page.BindingContext = editViewModel;
			await ((MainPage)App.Current.MainPage).NavigateTo(page, true);
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			if (!viewModel.ForUser && this.ToolbarItems.Count < 2)
			{
				if (viewModel.ForSearch)
				{
					var srch = new ToolbarItem() { Icon = "ic_search_white.png" };
					srch.Clicked += (object sender, EventArgs e) => viewModel.ToggleFilter();
					this.ToolbarItems.Add(srch);
				}
				else
				{
					var toolbarItem = new ToolbarItem() { Icon = "ic_add_white.png" };
					toolbarItem.Clicked += addToolbarItem_Click;
					this.ToolbarItems.Add(toolbarItem);
					if (viewModel.IsNotGeneral)
					{
						toolbarItem = new ToolbarItem() { Icon = "ic_input_white.png" };
						toolbarItem.Clicked += Btn_Clicked;
						this.ToolbarItems.Add(toolbarItem);
					}
				}
				var sort = new ToolbarItem() { Icon = "ic_sort_by_alpha_white.png" };
				sort.Clicked += async (object sender, EventArgs e) => await viewModel.ShowSort();
				this.ToolbarItems.Add(sort);

			}

			if (viewModel.ForSearch)
			{
				var behavior = new InfiniteScrollBehavior();
				behavior.SetBinding(InfiniteScrollBehavior.IsLoadingMoreProperty, new Binding(nameof(viewModel.IsScrolling)));
				EntriesList.Behaviors.Add(behavior);
				var activityIndicator = new ActivityIndicator() { VerticalOptions = LayoutOptions.End, HorizontalOptions = LayoutOptions.CenterAndExpand, HeightRequest = 70 };
				activityIndicator.BindingContext = this;
				activityIndicator.SetBinding(ActivityIndicator.IsVisibleProperty, new Binding(nameof(viewModel.IsScrolling)));
				activityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, new Binding(nameof(viewModel.IsScrolling)));
				EntriesList.Footer = activityIndicator;
			}

			if (viewModel.Entries.Count == 0)
				viewModel.LoadEntriesCommand.Execute(null);
		}

		private async void Btn_Clicked(object sender, EventArgs e)
		{
			if (await DisplayAlert("Populate", "Are you sure you want to populate from schedule?", "Yes", "No"))
			{
				(this.BindingContext as EntriesViewModel).PopulateFromScheduleCommand.Execute(null);
			}
		}

		protected void Search_Changed(object sedner, EventArgs e)
		{
			viewModel.LoadEntriesCommand.Execute(null);
		}
	}
}
