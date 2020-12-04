using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.DataLayer.Models;
using TheTeamTracker.Mobile.ViewModels.Entries;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheTeamTracker.Mobile.Views.Entries
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserProgressChecklistsPage : ContentPage
	{
		private UserProgressChecklistsViewModel viewModel
		{
			get { return BindingContext as UserProgressChecklistsViewModel; }
		}

		public UserProgressChecklistsPage()
		{
			InitializeComponent();
		}

		protected async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			if (e.Item == null)
				return;

			await editUserProgressChecklist((e.Item as DisplayItem).Tag as UserProgressChecklist);

			((ListView)sender).SelectedItem = null;
		}

		private async void Edit_Clicked(object sender, EventArgs e)
		{
			await editUserProgressChecklist((sender as MenuItem).CommandParameter as UserProgressChecklist);
		}

		private async System.Threading.Tasks.Task editUserProgressChecklist(UserProgressChecklist checklist)
		{
			var page = new UserProgressChecklistPage();
			var editViewModel = new UserProgressChecklistViewModel(viewModel.ForUser, checklist);
			page.BindingContext = editViewModel;
			await ((MainPage)App.Current.MainPage).NavigateTo(page, true);
		}

		protected async void Add_Clicked(object sender, EventArgs e)
		{
			await editUserProgressChecklist(null);
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			this.ToolbarItems.Clear();
			if (!viewModel.ForUser)
			{
				var item = new ToolbarItem() { Icon = "ic_add_white.png" };
				item.Clicked += Add_Clicked;
				this.ToolbarItems.Add(item);
			}
			viewModel.LoadUserProgressChecklistsCommand.Execute(null);
		}
	}
}