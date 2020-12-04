using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TheTeamTracker.Mobile.DataLayer.Models;
using TheTeamTracker.Mobile.ViewModels.Entries;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheTeamTracker.Mobile.Views.Entries
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserProgressChecklistPage : ContentPage
    {
        public UserProgressChecklistPage()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<UserProgressChecklistViewModel>(this, UserProgressChecklistViewModel.SUCCESS, async (sender) =>
            {
                MessagingCenter.Unsubscribe<UserProgressChecklistViewModel>(this, UserProgressChecklistViewModel.SUCCESS);
                await ((MainPage)App.Current.MainPage).GoBack();
            });
        }

        protected void checklist_Changed(object sender, EventArgs e)
		{
			var viewModel = BindingContext as UserProgressChecklistViewModel;
			viewModel.ChecklistChanged();
		}


		protected override void OnAppearing()
        {
            var viewModel = BindingContext as UserProgressChecklistViewModel;
			this.ToolbarItems.Clear();
			if (!viewModel.ForUser)
			{
				var item = new ToolbarItem() { Icon = "ic_save_white.png" };
				item.Command = viewModel.SaveCommand;
				this.ToolbarItems.Add(item);
			}
			viewModel.LoadLookupsCommand.Execute(null);

			base.OnAppearing();
        }

		protected void Task_IndexChanged(object sender, EventArgs e)
		{
			var viewModel = BindingContext as EntryEditViewModel;
			viewModel.Entry.TaskId = viewModel.Entry.Task.TaskId;
			viewModel.RefreshEntrySubtasks();
		}

		private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			(sender as ListView).SelectedItem = null;
		}
	}
}
