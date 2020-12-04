using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTeamTracker.Mobile.ViewModels.Attendance;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheTeamTracker.Mobile.Views.Attendance
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AttendanceEditPage : ContentPage
	{
		public AttendanceEditPage()
		{
			InitializeComponent();
			MessagingCenter.Subscribe<AttendanceEditViewModel>(this, AttendanceEditViewModel.SUCCESS, async (sender) =>
			{
				await ((MainPage)App.Current.MainPage).GoBack();
			});
		}
	}
}