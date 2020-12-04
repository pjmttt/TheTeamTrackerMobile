using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.ViewModels.Scheduling;
using Xam.Plugin;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheTeamTracker.Mobile.Views.Scheduling
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ManageSchedulePage : ContentPage
	{
		private ManageScheduleViewModel _viewModel;
		private PopupMenu _popupMenu;
		private List<ToolbarItem> _toolbarItems;
		private ToolbarItem _publishItem;

		public ManageSchedulePage()
		{
			InitializeComponent();

			BindingContext = _viewModel = new ManageScheduleViewModel();
			_viewModel.PropertyChanged += _viewModel_PropertyChanged;
			_toolbarItems = new List<ToolbarItem>();
			addToolbarItem("Email", _viewModel.EmailCommand, false);
			addToolbarItem("Text Message", _viewModel.EmailCommand, true);
			addToolbarItem("Save Template", _viewModel.SaveTemplateCommand);
			addToolbarItem("Load Template", _viewModel.LoadTemplateCommand);
			addToolbarItem("Copy Previous", _viewModel.CopyPreviousCommand);
			addToolbarItem("Delete Schedule(s)", _viewModel.DeleteSchedulesCommand, false);
			addToolbarItem("Delete Unscheduled", _viewModel.DeleteSchedulesCommand, true);

			if (Device.RuntimePlatform == Device.iOS)
			{
				_popupMenu = new PopupMenu();
				var moreItem = new ToolbarItem() { Icon = "ic_more_vert_black" };
				moreItem.Clicked += (object sender, EventArgs e) =>
				{
					_popupMenu.ShowPopup(this.dateNavigator);
				};
				this.ToolbarItems.Add(moreItem);
				_popupMenu.ItemsSource = _toolbarItems.Select(i => i.Text).ToList();
				_popupMenu.OnItemSelected += (string item) =>
				{
					var toolbar = _toolbarItems.First(i => i.Text == item);
					toolbar.Command.Execute(toolbar.CommandParameter);
				};
			}
			else
			{
				_toolbarItems.ForEach(i => this.ToolbarItems.Add(i));
			}

			_publishItem = new ToolbarItem() { Text = "Publish", Order = ToolbarItemOrder.Secondary, Command = _viewModel.PublishCommand };
			_toolbarItems.Add(_publishItem);
		
		}

		private void _viewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "WeekUnpublished")
			{
				if (_popupMenu != null)
				{
					_popupMenu.ItemsSource.Remove(_publishItem.Text);
					if (_viewModel.WeekUnpublished)
					{
						_popupMenu.ItemsSource.Insert(0, _publishItem.Text);
					}
				}
				else
				{
					this.ToolbarItems.Remove(_publishItem);
					if (_viewModel.WeekUnpublished)
					{
						this.ToolbarItems.Insert(0, _publishItem);
					}
				}

				dateNavigator.TextColor = _viewModel.WeekUnpublished ? Color.Red : Color.Black;
			}
		}

		private void addToolbarItem(string text, Command command, object parameter = null)
		{
			_toolbarItems.Add(new ToolbarItem() { Text = text, Order = ToolbarItemOrder.Secondary, Command = command, CommandParameter = parameter });
		}

		protected async void Add_Clicked(object sender, EventArgs e)
		{
			var viewModel = new BulkScheduleViewModel(!_viewModel.WeekUnpublished);
			var page = new BulkSchedulePage();
			page.BindingContext = viewModel;
			await ((MainPage)App.Current.MainPage).NavigateTo(page, true);
		}

		protected void dates_Changed(object sender, EventArgs e)
		{
			_viewModel.LoadSchedulesCommand.Execute(null);
		}

		protected void Lookup_Changed(object sedner, EventArgs e)
		{
			_viewModel.LoadSchedulesCommand.Execute(null);
		}

		protected void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			if (e.Item == null) return;
			((ListView)sender).SelectedItem = null;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			_viewModel.LoadSchedulesCommand.Execute(null);
		}
	}
}
