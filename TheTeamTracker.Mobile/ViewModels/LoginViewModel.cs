using System;
using System.Windows.Input;
using TheTeamTracker.Mobile.Classes;
using Xamarin.Forms;

namespace TheTeamTracker.Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
		public bool LoggingIn { get; set; }
		public bool ForgettingPassword { get; set; }
		public const string SUCCESS = "success";
		public LoginViewModel()
        {
			Title = "Login";
            LoginCommand = new Command(() => this.login());
			ForgetCommand = new Command(() => this.forget());
			LoggingIn = true;
        }

        public ICommand LoginCommand { get; }
		public ICommand ForgetCommand { get; }

        async private void login()
        {
			if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
			{
				MessageHelper.ShowMessage("Email and password required!", "Validation");
				return;
			}
			try
			{
				this.IsBusy = true;
				await LoginHelper.Login(UserName.Trim(), Password);
				MessagingCenter.Send<LoginViewModel>(this, SUCCESS);
				this.IsBusy = false;
			}
			catch (Exception ex)
			{
				ExceptionHelper.ShowException(ex);
				this.IsBusy = false;
			}
        }

		async private void forget()
		{
			if (string.IsNullOrEmpty(UserName))
			{
				MessageHelper.ShowMessage("Email required!", "Validation");
				return;
			}
			try
			{
				this.IsBusy = true;
				await DataService.PostItemAsync("forgotPassword", new { email = UserName });
				MessageHelper.ShowMessage("An email has been sent to your address!", "Success!");
				this.IsBusy = false;
				ToggleForgot();
			}
			catch (Exception ex)
			{
				ExceptionHelper.ShowException(ex);
				this.IsBusy = false;
			}
		}

		public void ToggleForgot()
		{
			LoggingIn = !LoggingIn;
			ForgettingPassword = !ForgettingPassword;
			OnPropertyChanged("LoggingIn");
			OnPropertyChanged("ForgettingPassword");
		}
    }
}
