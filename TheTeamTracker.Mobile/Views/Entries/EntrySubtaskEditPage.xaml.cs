using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.ViewModels.Entries;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheTeamTracker.Mobile.Views.Entries
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EntrySubtaskEditPage : ContentPage
	{
		public EntrySubtaskEditPage ()
		{
			InitializeComponent ();
			MessagingCenter.Subscribe<EntrySubtaskEditViewModel>(this, EntrySubtaskEditViewModel.SUCCESS, async (sender) =>
			{
                MessagingCenter.Unsubscribe<EntrySubtaskEditViewModel>(this, EntrySubtaskEditViewModel.SUCCESS);
                await ((MainPage)App.Current.MainPage).GoBack();
                var page = ((MainPage)App.Current.MainPage).Detail as TTTNavigationPage;
                if (page != null && page.CurrentPage is EntryEditPage)
                    ((page.CurrentPage as EntryEditPage).BindingContext as EntryEditViewModel).RefreshEntrySubtasks();
            });
		}
	}
}