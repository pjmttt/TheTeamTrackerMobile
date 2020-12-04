using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.DataLayer.Models;
using TheTeamTracker.Mobile.DataLayer.Services;
using Xamarin.Forms;

namespace TheTeamTracker.Mobile.ViewModels.Connect
{
	public class EmailStaffViewModel : BaseViewModel
	{
		public const string SUCCESS = "success";

		public ObservableCollection<User> Users { get; }
		public ObservableCollection<Position> Positions { get; }
		public User SelectedUser { get; set; }
		public Position SelectedPosition { get; set; }
		public Command LoadLookupsCommand { get; }
		public Command SendCommand { get; set; }
		public Command UserChangedCommand { get; }
		public Command PositionChangedCommand { get; }
		public Command RemoveRecipientCommand { get; }
		public UserComment UserComment { get; set; }

		private bool _allstaff;
		public bool AllStaff
		{
			get { return _allstaff; }
			set
			{
				_allstaff = value;
				OnPropertyChanged("AllStaff");
				OnPropertyChanged("NotAllStaff");
			}
		}

		public bool NotAllStaff
		{
			get { return !AllStaff; }
		}
		

		public ObservableCollection<User> Recipients { get; set; }

		public EmailStaffViewModel()
		{
			UserComment = new UserComment() { SendEmail = true, SendText = true };
			Users = new ObservableCollection<User>();
			Positions = new ObservableCollection<Position>();
			Recipients = new ObservableCollection<User>();
			LoadLookupsCommand = new Command(async () => await loadLookups());
			SendCommand = new Command(async () => await send());
			UserChangedCommand = new Command(() => userChanged());
			PositionChangedCommand = new Command(() => positionChanged());
			RemoveRecipientCommand = new Command((recipient) => removeRecipient((User)recipient));

		}

		private async System.Threading.Tasks.Task loadLookups()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				var lookups = await DataService.GetLookups(1000);
				List<Position> positions = new List<Position>();
				foreach (var u in lookups.Users.OrderBy(u => u.FirstName))
				{
					Users.Add(u);
					if (u.PositionId != null && !positions.Any(p => p.PositionId == u.PositionId))
					{
						positions.Add(u.Position);
					}
				}
				foreach (var p in positions.OrderBy(p => p.PositionName))
				{
					Positions.Add(p);
				}
			}
			catch (Exception ex)
			{
				IsBusy = false;
				ExceptionHelper.ShowException(ex);
			}
			finally
			{
				IsBusy = false;
			}
		}

		private async System.Threading.Tasks.Task send()
		{
			if (string.IsNullOrEmpty(UserComment.Subject) || string.IsNullOrEmpty(UserComment.Comment) || (!UserComment.SendTextValue && !UserComment.SendEmailValue) || (!AllStaff && !Recipients.Any()))
			{
				MessageHelper.ShowMessage("Subject, body, email or text, and recipient(s) required!", "Validation");
				return;
			}

			IsBusy = true;
			try
			{
				UserComment.CommentDateTimezoned = DateTime.Now;
				UserComment.UserId = AuthService.UserToken.User.UserId;
				UserComment.UserIds = AllStaff ? Users.Select(u => u.UserIdValue).ToList() : Recipients.Select(r => r.UserIdValue).ToList();
				await DataService.PostItemAsync<UserComment>("userComments", UserComment);
				MessageHelper.ShowMessage("Message has been sent.", "Success");
				UserComment = new UserComment() { SendEmail = true, SendText = true };
				Recipients.Clear();
				AllStaff = false;
				OnPropertyChanged("UserComment");
				IsBusy = false;
			}
			catch (Exception ex)
			{
				IsBusy = false;
				if (ex.InnerException != null)
				{
					ex = ex.InnerException;
				}
				ExceptionHelper.ShowException(ex);
			}
			finally
			{
				IsBusy = false;
			}
		}

		private void userChanged()
		{
			if (SelectedUser != null && !Recipients.Any(r => r.UserId == SelectedUser.UserId))
				Recipients.Add(SelectedUser);
			SelectedUser = null;
			OnPropertyChanged("SelectedUser");
		}

		private void removeRecipient(User recipient)
		{
			Recipients.Remove(recipient);
		}

		private void positionChanged()
		{
			foreach (var u in Users)
			{
				if (SelectedPosition != null && u.PositionIdValue == SelectedPosition.PositionIdValue)
					Recipients.Add(u);
			}
			SelectedPosition = null;
			OnPropertyChanged("SelectedPosition");
		}
	}
}
