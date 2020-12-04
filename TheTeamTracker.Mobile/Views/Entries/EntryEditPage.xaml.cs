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
    public partial class EntryEditPage : ContentPage
    {
        public EntryEditPage()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<EntryEditViewModel>(this, EntryEditViewModel.SUCCESS, async (sender) =>
            {
                MessagingCenter.Unsubscribe<EntryEditViewModel>(this, EntryEditViewModel.SUCCESS);
                await ((MainPage)App.Current.MainPage).GoBack();
            });
        }

        protected async void Handle_ItemTapped(object sender, EventArgs e)
        {
            var args = e as TappedEventArgs;
            if (args.Parameter == null)
                return;

            var viewModel = BindingContext as EntryEditViewModel;
            if (viewModel.ForUser) return;
            var item = args.Parameter as EntrySubtaskItem;
            var esViewModel = new EntrySubtaskEditViewModel(viewModel.Entry, item.EntrySubtask, viewModel.Statuses);
            var page = new EntrySubtaskEditPage();
            page.BindingContext = esViewModel;
            await ((MainPage)App.Current.MainPage).NavigateTo(page, true);

            //Deselect Item
            // ((ListView)sender).SelectedItem = null;
        }

        private void ViewModel_EntrySubtasksLoaded(object sender, EventArgs e)
        {
            stackSubtasks.Children.Clear();
            foreach (var st in (this.BindingContext as EntryEditViewModel).EntrySubtasks)
            {
				var horiz = new StackLayout() { Orientation = StackOrientation.Horizontal };
				horiz.Children.Add(new BoxView() {
					BackgroundColor = st.BackgroundColor,
					VerticalOptions = LayoutOptions.FillAndExpand,
					WidthRequest = 10
				});
                var stackLayout = new StackLayout()
                {
                    Padding = new Thickness(9),
                    Margin = new Thickness(3, 0, 0, 0),
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };
                var recognizer = new TapGestureRecognizer();
                recognizer.CommandParameter = st;
                recognizer.Tapped += Handle_ItemTapped;
                stackLayout.GestureRecognizers.Add(recognizer);

                stackLayout.Children.Add(new Label() { Text = st.SubtaskNameStatus });
                if (!string.IsNullOrEmpty(st.EntrySubtask.Comments))
                    stackLayout.Children.Add(new Label() { Text = st.EntrySubtask.Comments });
                if (st.EntrySubtask.AddressedValue)
                {
                    stackLayout.Children.Add(new Label()
                    {
                        FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                        Text = "Addressed",
                    });
                }
				horiz.Children.Add(stackLayout);
                stackSubtasks.Children.Add(horiz);
                stackSubtasks.Children.Add(new BoxView() { HeightRequest = 1, Color = Color.Gray, HorizontalOptions = LayoutOptions.FillAndExpand });
            }
        }

        protected async void ChangeAll_Clicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as EntryEditViewModel;
            if (viewModel.ForUser) return;
            var esViewModel = new EntrySubtaskEditViewModel(viewModel.Entry, null, viewModel.Statuses);
            var page = new EntrySubtaskEditPage();
            page.BindingContext = esViewModel;
            await ((MainPage)App.Current.MainPage).NavigateTo(page, true);
        }

        protected override void OnAppearing()
        {
            var viewModel = BindingContext as EntryEditViewModel;
            viewModel.EntrySubtasksLoaded -= ViewModel_EntrySubtasksLoaded;
            viewModel.EntrySubtasksLoaded += ViewModel_EntrySubtasksLoaded;

            shiftPicker.IsVisible = !viewModel.ForUser && viewModel.EntryType == 0;
            shiftValueLabel.IsVisible = viewModel.ForUser && viewModel.EntryType == 0;
            shiftHeaderLabel.IsVisible = viewModel.EntryType == 0;
			this.ToolbarItems.Clear();
			if (!viewModel.ForUser)
			{
				var item = new ToolbarItem() { Icon = "ic_save_white.png" };
				item.Command = viewModel.SaveCommand;
				this.ToolbarItems.Add(item);
			}
			base.OnAppearing();
			
			viewModel.LoadScreenCommand.Execute(null);
        }

		protected void Task_IndexChanged(object sender, EventArgs e)
		{
			var viewModel = BindingContext as EntryEditViewModel;
			viewModel.Entry.Task = viewModel.Task;
			viewModel.RefreshEntrySubtasks();
		}

		protected void Rating_TextChanged(object sender, EventArgs e)
		{
			var viewModel = BindingContext as EntryEditViewModel;
			viewModel.RaiseScoreChanged();
		}

		//protected async void Back_Click(object sender, EventArgs e)
		//{
		//	await ((MainPage)App.Current.MainPage).GoBack();
		//}
	}
}
