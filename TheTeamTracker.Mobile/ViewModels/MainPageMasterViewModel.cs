using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.ViewModels.Attendance;
using TheTeamTracker.Mobile.ViewModels.Entries;
using TheTeamTracker.Mobile.ViewModels.Scheduling;
using TheTeamTracker.Mobile.Views;
using TheTeamTracker.Mobile.Views.Attendance;
using TheTeamTracker.Mobile.Views.Connect;
using TheTeamTracker.Mobile.Views.Entries;
using TheTeamTracker.Mobile.Views.Inventory;
using TheTeamTracker.Mobile.Views.Maintenance;
using TheTeamTracker.Mobile.Views.Scheduling;
using TheTeamTracker.Mobile.Views.Setup;
using static TheTeamTracker.Mobile.Classes.Constants;

namespace TheTeamTracker.Mobile.ViewModels
{
	public class MainPageMasterViewModel
	{
		public ObservableCollection<MainPageMenuItem> MenuItems { get; set; }

		public MainPageMasterViewModel()
		{
			MenuItems = new ObservableCollection<MainPageMenuItem>();
		}

		public void ClearMenuItems()
		{
			MenuItems.Clear();
		}

		public void InitMenuItems(MainPageMenuItem parent = null)
		{
			ClearMenuItems();
			if (parent != null)
			{
				MenuItems.Add(new MainPageMenuItem() { Id = (int)MenuItem.Back, Title = "Back", Image = "ic_keyboard_arrow_left_black" });
				foreach (var mi in parent.MenuItems)
				{
					MenuItems.Add(mi);
				}
				return;
			}
			var usr = LoginHelper.GetLoggedInUser().User;
			var dutiesMenuItem = new MainPageMenuItem() { Title = "Duties", Image = "ic_assignment_black", HasChildren = true };
			dutiesMenuItem.MenuItems.Add(new MainPageMenuItem { Id = (int)MenuItem.MyDuties, Title = "My Duties", Image = "ic_assignment_ind_black", TargetType = typeof(EntriesPage), GetViewModel = () => new EntriesViewModel(true, false, false) });
			if (usr.Roles.IndexOf((int)ROLE.MANAGER) >= 0)
			{
				dutiesMenuItem.MenuItems.Add(new MainPageMenuItem { Id = (int)MenuItem.AllDuties, Title = "Manage Duties", Image = "ic_assignment_black", TargetType = typeof(EntriesPage), GetViewModel = () => new EntriesViewModel(false, false, false) });
				dutiesMenuItem.MenuItems.Add(new MainPageMenuItem { Id = (int)MenuItem.SearchDuties, Title = "Search Duties", Image = "ic_search_black", TargetType = typeof(EntriesPage), GetViewModel = () => new EntriesViewModel(false, false, true) });
			}

			dutiesMenuItem.MenuItems.Add(new MainPageMenuItem { Id = (int)MenuItem.MyGeneralDuties, Title = "My Extra Duties", Image = "ic_assignment_ind_black", TargetType = typeof(EntriesPage), GetViewModel = () => new EntriesViewModel(true, true, false) });
			if (usr.Roles.IndexOf((int)ROLE.MANAGER) >= 0)
				dutiesMenuItem.MenuItems.Add(new MainPageMenuItem { Id = (int)MenuItem.AllGeneralDuties, Title = "Extra Duties", Image = "ic_assignment_black", TargetType = typeof(EntriesPage), GetViewModel = () => new EntriesViewModel(false, true, false) });

			dutiesMenuItem.MenuItems.Add(new MainPageMenuItem { Id = (int)MenuItem.AvailableExtraDuties, Title = "Available Extra Duties", Image = "ic_assignment_turned_in_black", TargetType = typeof(AvailableEntriesPage) });

			dutiesMenuItem.MenuItems.Add(new MainPageMenuItem { Id = (int)MenuItem.MyProgressChecklists, Title = "My Checklists", Image = "ic_check_black", TargetType = typeof(UserProgressChecklistsPage), GetViewModel = () => new UserProgressChecklistsViewModel(true) });
			if (usr.Roles.IndexOf((int)ROLE.MANAGER) >= 0)
				dutiesMenuItem.MenuItems.Add(new MainPageMenuItem { Id = (int)MenuItem.AllProgressChecklists, Title = "Checklists", Image = "ic_playlist_add_check_black", TargetType = typeof(UserProgressChecklistsPage), GetViewModel = () => new UserProgressChecklistsViewModel(false) });

			MenuItems.Add(dutiesMenuItem);

			var attendanceMenuItem = new MainPageMenuItem() { Title = "Attendance", Image = "ic_people_black", HasChildren = true };
			attendanceMenuItem.MenuItems.Add(new MainPageMenuItem { Id = (int)MenuItem.MyAttendance, Image = "ic_person_black", Title = "My Attendance", TargetType = typeof(AttendanceListPage), GetViewModel = () => new AttendanceListViewModel(true) });
			if (usr.Roles.IndexOf((int)ROLE.MANAGER) >= 0)
				attendanceMenuItem.MenuItems.Add(new MainPageMenuItem { Id = (int)MenuItem.AllAttendance, Image = "ic_people_black", Title = "Attendance", TargetType = typeof(AttendanceListPage), GetViewModel = () => new AttendanceListViewModel(false) });
			MenuItems.Add(attendanceMenuItem);

			var schedulingMenuItem = new MainPageMenuItem() { Title = "Scheduling", Image = "ic_event_note_black", HasChildren = true };
			schedulingMenuItem.MenuItems.Add(new MainPageMenuItem { Id = (int)MenuItem.TradeBoard, Title = "Trade Board", Image = "ic_repeat_black", TargetType = typeof(TradeBoardPage) });

			schedulingMenuItem.MenuItems.Add(new MainPageMenuItem { Id = (int)MenuItem.MySchedule, Title = "My Trades", Image = "ic_swap_horiz_black", TargetType = typeof(TradeListPage), GetViewModel = () => new TradeListViewModel(true) });
			if (usr.Roles.IndexOf((int)ROLE.SCHEDULING) >= 0)
			{
				schedulingMenuItem.MenuItems.Add(new MainPageMenuItem { Id = (int)MenuItem.ApproveDenyTrades, Title = "Approve/Deny Trades", Image = "ic_done_all_black", TargetType = typeof(TradeListPage), GetViewModel = () => new TradeListViewModel(false) });
				schedulingMenuItem.MenuItems.Add(new MainPageMenuItem { Id = (int)MenuItem.ManageSchedule, Title = "Schedules", Image = "ic_event_note_black", TargetType = typeof(ManageSchedulePage) });
			}

			schedulingMenuItem.MenuItems.Add(new MainPageMenuItem { Id = (int)MenuItem.MySchedule, Title = "My Schedule", Image = "ic_event_available_black", TargetType = typeof(MySchedulePage) });
			schedulingMenuItem.MenuItems.Add(new MainPageMenuItem { Id = (int)MenuItem.DailySchedule, Title = "Daily Schedule", Image = "ic_event_black", TargetType = typeof(DailySchedulePage) });

			schedulingMenuItem.MenuItems.Add(new MainPageMenuItem { Id = (int)MenuItem.MyHours, Title = "My Hours", Image = "ic_access_alarm_black", TargetType = typeof(HoursPage), GetViewModel = () => new HoursViewModel(true) });
			schedulingMenuItem.MenuItems.Add(new MainPageMenuItem { Id = (int)MenuItem.StaffHours, Title = "Employee Hours", Image = "ic_access_time_black", TargetType = typeof(HoursPage), GetViewModel = () => new HoursViewModel(false) });

			schedulingMenuItem.MenuItems.Add(new MainPageMenuItem { Id = (int)MenuItem.MyLeaveRequests, Title = "My Time Off", Image = "ic_flight_takeoff_black", TargetType = typeof(LeaveRequestsPage), GetViewModel = () => new LeaveRequestsViewModel(true) });
			if (usr.Roles.IndexOf((int)ROLE.SCHEDULING) >= 0)
				schedulingMenuItem.MenuItems.Add(new MainPageMenuItem { Id = (int)MenuItem.AllLeaveRequests, Title = "Time Off", Image = "ic_airplanemode_active_black", TargetType = typeof(LeaveRequestsPage), GetViewModel = () => new LeaveRequestsViewModel(false) });

			schedulingMenuItem.MenuItems.Add(new MainPageMenuItem { Id = (int)MenuItem.MyAvailability, Title = "My Availability", Image = "ic_thumb_up_black", TargetType = typeof(UserAvailabilityListPage), GetViewModel = () => new UserAvailabilityListViewModel(true) });
			if (usr.Roles.IndexOf((int)ROLE.SCHEDULING) >= 0)
				schedulingMenuItem.MenuItems.Add(new MainPageMenuItem { Id = (int)MenuItem.AllAvailability, Title = "Availability", Image = "ic_thumbs_up_down_black", TargetType = typeof(UserAvailabilityListPage), GetViewModel = () => new UserAvailabilityListViewModel(false) });

			MenuItems.Add(schedulingMenuItem);

			if (usr.Roles.IndexOf((int)ROLE.MAINTENANCE_REQUESTS) >= 0 || usr.Roles.IndexOf((int)ROLE.MANAGER) >= 0 || usr.Roles.IndexOf((int)ROLE.ADMIN) >= 0)
				MenuItems.Add(new MainPageMenuItem { Id = (int)MenuItem.MaintenanceRequests, Title = "Maintenance Requests", Image = "ic_build_black", TargetType = typeof(MaintenanceRequestListPage) });
			if (usr.Roles.IndexOf((int)ROLE.INVENTORY) >= 0)
				MenuItems.Add(new MainPageMenuItem { Id = (int)MenuItem.Inventory, Title = "Inventory", Image = "ic_shopping_basket_black", TargetType = typeof(InventoryListPage) });
			MenuItems.Add(new MainPageMenuItem { Id = (int)MenuItem.EmailStaff, Title = "Send Message", Image = "ic_mail_black", TargetType = typeof(EmailStaffPage) });

			MenuItems.Add(new MainPageMenuItem { Id = (int)MenuItem.ClockIn, Title = "Clock In", Image = "ic_update_black" });
			MenuItems.Add(new MainPageMenuItem { Id = (int)MenuItem.ClockOut, Title = "Clock Out", Image = "ic_restore_black" });
			MenuItems.Add(new MainPageMenuItem { Id = (int)MenuItem.Settings, Title = "My Settings", Image = "ic_settings_black", TargetType = typeof(MySettingsPage) });
			MenuItems.Add(new MainPageMenuItem { Id = (int)MenuItem.ContactUs, Title = "Contact Us", Image = "ic_phone_black", TargetType = typeof(ContactUsPage) });
			MenuItems.Add(new MainPageMenuItem { Id = (int)MenuItem.Logout, Title = "Logout", Image = "ic_input_black" });
		}

		#region INotifyPropertyChanged Implementation
		public event PropertyChangedEventHandler PropertyChanged;
		void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			if (PropertyChanged == null)
				return;

			PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion
	}

	public class MainPageMenuItem
	{
		public MainPageMenuItem()
		{
			TargetType = typeof(LoginPage);
			MenuItems = new ObservableCollection<MainPageMenuItem>();
		}
		public int Id { get; set; }
		public string Title { get; set; }
		public string Image { get; set; }
		public bool HasChildren { get; set; }

		public Type TargetType { get; set; }
		public Func<BaseViewModel> GetViewModel { get; set; }

		public ObservableCollection<MainPageMenuItem> MenuItems { get; }
	}

	public enum MenuItem : int
	{
		MyDuties,
		AllDuties,
		SearchDuties,
		MyGeneralDuties,
		AllGeneralDuties,
		AvailableExtraDuties,
		MyProgressChecklists,
		AllProgressChecklists,
		MyAttendance,
		AllAttendance,
		TradeBoard,
		ApproveDenyTrades,
		MySchedule,
		ManageSchedule,
		DailySchedule,
		MyHours,
		StaffHours,
		MyLeaveRequests,
		AllLeaveRequests,
		MyAvailability,
		AllAvailability,
		MaintenanceRequests,
		Inventory,
		EmailStaff,
		ContactUs,
		Logout,
		Settings,
		ClockIn,
		ClockOut,

		Back,
	}
}
