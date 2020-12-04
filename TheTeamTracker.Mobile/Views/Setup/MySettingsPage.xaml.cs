using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.ViewModels.Setup;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static TheTeamTracker.Mobile.Classes.Constants;

namespace TheTeamTracker.Mobile.Views.Setup
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MySettingsPage : ContentPage
	{
		private MySettingsViewModel _viewModel;
		public MySettingsPage()
		{
			InitializeComponent();
			this.BindingContext = _viewModel = new MySettingsViewModel();
			_viewModel.SettingsLoaded += _viewModel_SettingsLoaded;
		}

		private void _viewModel_SettingsLoaded(object sender, EventArgs e)
		{
			var usr = _viewModel.User;
			List<int> notifications = new List<int>();
			notifications.Add((int)NOTIFICATION.DAILY_REPORT);
			notifications.Add((int)NOTIFICATION.TRADE_REQUESTS);

			foreach (var n in notifications)
			{
				foreach (var stack in new List<StackLayout>() { stackEmailNotifications, stackTextNotifications })
				{
					var stackRow = new StackLayout();
					stackRow.Orientation = StackOrientation.Horizontal;
					stackRow.Children.Add(new Label() { Text = notificationDisplay(n) });
					var sw = new Switch()
					{
						IsToggled =
							(stack == stackEmailNotifications ? usr.EmailNotifications.IndexOf(n) >= 0
										: usr.TextNotifications.IndexOf(n) >= 0)
					};
					sw.Toggled += (object sender2, ToggledEventArgs e2) =>
					{
						(this.BindingContext as MySettingsViewModel).NotificationChangedCommand.Execute(
							new Tuple<int, bool, bool>(n, stack == stackEmailNotifications, sw.IsToggled));
					};
					stackRow.Children.Add(sw);
					stack.Children.Add(stackRow);
				}
			}
		}

		private string notificationDisplay(int notification)
		{
			var display = ((NOTIFICATION)notification).ToString().ToLower();
			return string.Join(" ", display.Split('_').Select(s => s.Substring(0, 1).ToUpper() + s.Substring(1)));
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			(this.BindingContext as MySettingsViewModel).ExecuteLoadCommand.Execute(null);
		}
	}
}