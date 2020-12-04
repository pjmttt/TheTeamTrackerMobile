using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.DataLayer.Classes;
using TheTeamTracker.Mobile.DataLayer.Models;
using TheTeamTracker.Mobile.DataLayer.Services;
using Xamarin.Forms;
namespace TheTeamTracker.Mobile.ViewModels.Maintenance
{
	public class MaintenanceRequestListViewModel : ListViewModel<MaintenanceRequest>
	{
		public Lookups Lookups { get; set; }
		public Command DeleteCommand { get; }
		public Command AddressCommand { get; }
		public bool UnaddressedOnly { get; set; }

		protected override string endpoint => "maintenanceRequests";

		public MaintenanceRequestListViewModel()
		{
			UnaddressedOnly = true;
			DeleteCommand = new Command(async (parameter) => await ExecuteDeleteCommand(parameter as DisplayItem));
			AddressCommand = new Command(async (parameter) => await ExecuteAddressCommand(parameter as DisplayItem));
			sortFields.Add("Request Date", "requestDate");
			sortQueue.Add(new Tuple<string, bool>("requestDate", true));
		}

		protected override string getPostParams()
		{
			if (UnaddressedOnly)
				return "&where=isAddressed eq false";
			return base.getPostParams();
		}

		protected override void populateDisplayItem(DisplayItem item, MaintenanceRequest entity)
		{
			var usr = Lookups.Users.FirstOrDefault(u => u.UserId == entity.AssignedToId);
			string displayName = usr == null ? "Unassigned" : usr.DisplayName;
			var by = Lookups.Users.FirstOrDefault(u => u.UserId == entity.RequestedById);
			var byDisplayName = string.Empty;
			if (by == null) byDisplayName = "Unknown";
			else byDisplayName = by.DisplayName;
			item.Line1 = $"{entity.RequestDate.GetValueOrDefault().ToShortDateString()} - {displayName}";
			item.Line2 = $"Requested by {byDisplayName}";
			item.Line3 = entity.RequestDescription;
			item.Tag = entity;
			item.Tag2 = entity.IsAddressedValue ? Color.Blue : Color.LightGray;
			item.Tag3 = entity.MaintenanceRequestImages.Any();
			item.Tag4 = entity.MaintenanceRequestReplys.Any();
		}

		public void MaintenanceRequestSaved(MaintenanceRequest request, DisplayItem displayItem)
		{
			if (request.IsAddressedValue && !UnaddressedOnly)
			{
				if (displayItem != null)
					LoadDataCommand.Execute(null);
			}
			else if (displayItem == null)
			{
				LoadDataCommand.Execute(null);
			}
			else
			{
				populateDisplayItem(displayItem, request);
				displayItem.Refresh();
			}
		}

		protected override async System.Threading.Tasks.Task onInit()
		{
			if (Lookups == null)
				Lookups = await DataService.GetLookups(2);
		}

		public async System.Threading.Tasks.Task ExecuteDeleteCommand(DisplayItem displayItem)
		{
			var request = displayItem.Tag as MaintenanceRequest;
			if (!await runTask(async () => await DataService.DeleteItemAsync("maintenanceRequests", request.MaintenanceRequestIdValue),
				"Are you sure you want to delete this request")) return;
			LoadDataCommand.Execute(null);
		}

		public async System.Threading.Tasks.Task ExecuteAddressCommand(DisplayItem displayItem)
		{
			var request = displayItem.Tag as MaintenanceRequest;
			if (!await runTask(async () =>
			{
				request.IsAddressed = true;
				var toSave = Common.Clone<MaintenanceRequest>(request);
				if (toSave.AssignedTo != null) toSave.AssignedToId = toSave.AssignedTo.UserIdValue;
				toSave.AssignedTo = null;
				if (toSave.RequestedBy != null) toSave.RequestedByIdValue = toSave.RequestedBy.UserIdValue;
				toSave.AssignedTo = null;
				await DataService.PutItemAsync<MaintenanceRequest>("maintenanceRequests", toSave.MaintenanceRequestIdValue, toSave);
			},
				"Are you sure you want to mark this request as addressed?")) return;

			if (UnaddressedOnly)
			{
				LoadDataCommand.Execute(null);
			}
			else
			{
				populateDisplayItem(displayItem, request);
				displayItem.Refresh();
			}
		}
	}
}
