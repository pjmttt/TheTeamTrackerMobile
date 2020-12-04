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
    public partial class AvailableEntryEditPage : ContentPage
    {
        public AvailableEntryEditPage()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<AvailableEntryEditViewModel>(this, AvailableEntryEditViewModel.SUCCESS, async (sender) =>
            {
                MessagingCenter.Unsubscribe<AvailableEntryEditViewModel>(this, AvailableEntryEditViewModel.SUCCESS);
                await ((MainPage)App.Current.MainPage).GoBack();
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

			var viewModel = BindingContext as AvailableEntryEditViewModel;
			viewModel.LoadScreenCommand.Execute(null);
        }
    }
}
