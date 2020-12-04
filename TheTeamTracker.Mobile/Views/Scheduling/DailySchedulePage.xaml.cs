using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.ViewModels.Scheduling;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheTeamTracker.Mobile.Views.Scheduling
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DailySchedulePage : ContentPage
	{
		private DailyScheduleViewModel _viewModel;
		public DailySchedulePage()
		{
			InitializeComponent();

			BindingContext = _viewModel = new DailyScheduleViewModel();

		}

		protected void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			if (e.Item == null) return;
			((ListView)sender).SelectedItem = null;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (_viewModel.Schedules.Count == 0)
				_viewModel.LoadSchedulesCommand.Execute(null);
		}
	}
}
