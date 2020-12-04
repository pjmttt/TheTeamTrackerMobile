using System;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.Views;
using TheTeamTracker.Mobile.Views.Scheduling;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace TheTeamTracker.Mobile
{
    public partial class App : Xamarin.Forms.Application
	{

        public App()
        {
            InitializeComponent();
			App.Current.On<Xamarin.Forms.PlatformConfiguration.Android>()
				.UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
			MainPage = new MainPage();
            
        }

        protected override void OnStart()
        {
            if (LoginHelper.CheckExistingLogin())
            {
                ((MainPage)App.Current.MainPage).InitMenuItems();
                var task = ((MainPage)App.Current.MainPage).NavigateTo(new MySchedulePage() { Title = "My Schedule" });
                task.Wait();
            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
