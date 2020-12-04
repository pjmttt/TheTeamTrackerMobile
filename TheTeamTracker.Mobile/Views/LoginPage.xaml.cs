using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TheTeamTracker.Mobile.ViewModels;
using TheTeamTracker.Mobile.Views.Scheduling;

namespace TheTeamTracker.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
			MessagingCenter.Subscribe<LoginViewModel>(this, LoginViewModel.SUCCESS, async (sender) =>
			{
				((MainPage)App.Current.MainPage).InitMenuItems();
				await ((MainPage)App.Current.MainPage).NavigateTo(new MySchedulePage() { Title = "My Schedule" });
			});
		}

		public void Forgot_Tapped(object sender, EventArgs e)
		{
			(BindingContext as LoginViewModel).ToggleForgot();
		}

	}
}