using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.ViewModels;
using TheTeamTracker.Mobile.Views.Scheduling;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheTeamTracker.Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : MasterDetailPage
	{
		public MainPage()
		{
			InitializeComponent();
			MasterPage.ListView.ItemSelected += ListView_ItemSelected;
		}

		public async Task NavigateTo(ContentPage TargetPage, bool push = false)
		{
			if (push)
			{
				await this.Detail.Navigation.PushAsync(TargetPage);
			}
			else
			{
				this.Detail = new TTTNavigationPage(TargetPage, true);
			}
			this.Title = TargetPage.Title;
			this.IsPresented = false;
		}

		public async Task GoBack()
		{
			await this.Detail.Navigation.PopAsync();
		}

		public void InitMenuItems(MainPageMenuItem parent = null)
		{
			((MainPageMasterViewModel)this.Master.BindingContext).InitMenuItems(parent);
		}

		private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var item = e.SelectedItem as MainPageMenuItem;
			if (item == null)
				return;

			if (item.Id == (int)ViewModels.MenuItem.ClockIn)
			{
				if (await ClockInOut.ClockIn())
					await DisplayAlert("Success", "You have been clocked in.", "OK");
			}
			else if (item.Id == (int)ViewModels.MenuItem.ClockOut)
			{
				if (await ClockInOut.ClockOut())
					await DisplayAlert("Success", "You have been clocked out.", "OK");
			}
			else if (item.Id == (int)ViewModels.MenuItem.Logout)
			{
				LoginHelper.Logout();
				((MainPageMasterViewModel)this.Master.BindingContext).ClearMenuItems();
				var page = new LoginPage();
				page.Title = "Login";
				Detail = new TTTNavigationPage(page);
			}
			else if (item.Id == (int)ViewModels.MenuItem.Back)
			{
				InitMenuItems();
				return;
			}
			else if (item.MenuItems.Any())
			{
				InitMenuItems(item);
				return;
			}
			else
			{
				var page = (Page)Activator.CreateInstance(item.TargetType);
				if (item.GetViewModel != null)
					page.BindingContext = item.GetViewModel();
				page.Title = item.Title;
				page.Icon = item.Image;

				Detail = new TTTNavigationPage(page, true);
			}
			IsPresented = false;

			MasterPage.ListView.SelectedItem = null;
		}
	}
}