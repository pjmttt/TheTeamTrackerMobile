using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.ViewModels.Attendance;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheTeamTracker.Mobile.Views.Attendance
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AttendanceListPage : ContentPage
	{
		public ObservableCollection<string> Items { get; set; }

		private AttendanceListViewModel viewModel
		{
			get { return this.BindingContext as AttendanceListViewModel; }
		}

		public AttendanceListPage()
		{
			InitializeComponent();
		}

		protected async void Edit_Clicked(object sender, EventArgs e)
		{
			var mnuItem = sender as MenuItem;
			await editAttendance(mnuItem.CommandParameter as DataLayer.Models.Attendance);
		}

		protected async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			if (e.Item == null || viewModel.ForUser)
			{
				((ListView)sender).SelectedItem = null;
				return;
			}

			((ListView)sender).SelectedItem = null;

			await editAttendance(((DisplayItem)e.Item).Tag as DataLayer.Models.Attendance);
		}

		private async Task editAttendance(DataLayer.Models.Attendance attendance)
		{ 
			var editViewModel = new AttendanceEditViewModel(viewModel.Users.ToList(), viewModel.Reasons.ToList(), attendance);
			var page = new AttendanceEditPage();
			page.BindingContext = editViewModel;
			await ((MainPage)App.Current.MainPage).NavigateTo(page, true);
		}

		protected async void Add_Clicked(object sender, EventArgs e)
		{
			var editViewModel = new AttendanceEditViewModel(viewModel.Users.ToList(), viewModel.Reasons.ToList(), null);
			var page = new AttendanceEditPage();
			page.BindingContext = editViewModel;
			await ((MainPage)App.Current.MainPage).NavigateTo(page, true);
		}

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();
			viewModel.DecorateListView(AttendanceList, this.ToolbarItems);
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			if (!viewModel.ForUser && this.ToolbarItems.Count < 2)
			{
				var item = new ToolbarItem() { Icon = "ic_add_white.png" };
				item.Clicked += Add_Clicked;
				this.ToolbarItems.Add(item);
			}
			viewModel.LoadDataCommand.Execute(null);
		}
	}
}
