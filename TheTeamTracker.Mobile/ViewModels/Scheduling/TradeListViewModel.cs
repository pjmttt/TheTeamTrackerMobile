using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.DataLayer.Models;
using TheTeamTracker.Mobile.DataLayer.Services;
using Xamarin.Forms;

namespace TheTeamTracker.Mobile.ViewModels.Scheduling
{
	public class TradeListViewModel : BaseViewModel
	{
		public ObservableCollection<TradeListItem> Trades { get; set; }
		public Command RefreshTradesCommand { get; set; }
		public Command AcceptApproveCommand { get; set; }
		public Command DeclineDenyCommand { get; set; }
		public Command DeleteCommand { get; set; }
		public bool ForUser { get; }
		public bool NoTrades { get; set; }

		public static string AcceptText
		{
			get { return "Accept"; }
		}

		public static string ApproveText
		{
			get { return "Approve"; }
		}

		public static string DeclineText
		{
			get { return "Decline"; }
		}

		public static string DenyText
		{
			get { return "Deny"; }
		}

		public TradeListViewModel(bool forUser)
		{
			Trades = new ObservableCollection<TradeListItem>();
			ForUser = forUser;
			RefreshTradesCommand = new Command(async () => await ExecuteLoadTradesCommand());
			DeleteCommand = new Command(async (parameter) => await deleteTrade(parameter as ScheduleTrade));
			AcceptApproveCommand = new Command(async (parameter) => await acceptApprove(parameter as ScheduleTrade));
			DeclineDenyCommand = new Command(async (parameter) => await declineDeny(parameter as ScheduleTrade));
		}

		public static DisplayItem GetItemForSchedule(Schedule schedule)
		{
			var displayItem = new DisplayItem();
			displayItem.Tag = schedule;
			if (schedule == null)
			{
				displayItem.Tag2 = false;
				return displayItem;
			}

			displayItem.Tag3 = " for ";
			displayItem.Line1 = schedule.User == null ? string.Empty : schedule.User.DisplayName + " - " + schedule.ScheduleDateValue.ToShortDateString();
			displayItem.Tag2 = schedule.User != null;
			displayItem.Line2 = $"{schedule.StartTimeTimezonedValue.ToShortTimeString()} - {schedule.EndTimeTimezonedValue.ToShortTimeString()}";
			displayItem.Line3 = $"{schedule.Shift.ShiftName} - {schedule.Task.TaskName}";
			return displayItem;
		}

		private async System.Threading.Tasks.Task ExecuteLoadTradesCommand()
		{
			if (IsBusy)
				return;

			IsRefreshing = false;
			IsBusy = true;

			var usr = AuthService.UserToken.User;

			try
			{
				Trades.Clear();
				var items = await DataService.GetItemsAsync<ScheduleTrade>("scheduleTrades" + (ForUser ? "?forUser=true" : "?status=" + ((int)Constants.TRADE_STATUS.PENDING_APPROVAL).ToString()));
				NoTrades = !items.Data.Any();
				OnPropertyChanged("NoTrades");
				foreach (var item in items.Data.OrderByDescending(d => d.Schedule.ScheduleDateValue))
				{
					var tradeListItem = new TradeListItem();
					tradeListItem.ScheduleTrade = item;
					tradeListItem.Schedule = GetItemForSchedule(item.Schedule);
					if (item.TradeForScheduleId != null)
						tradeListItem.ToSchedule = GetItemForSchedule(item.TradeForSchedule);

					tradeListItem.InfoColumn = new DisplayItem();
					tradeListItem.InfoColumn.Tag = false;
					tradeListItem.AcceptApproveText = "";
					tradeListItem.DeclineDenyText = "";
					if (ForUser && item.TradeStatus == (int)Constants.TRADE_STATUS.REQUESTED && item.Schedule.UserIdValue == usr.UserIdValue)
					{
						tradeListItem.InfoColumn.Tag = true;
						tradeListItem.AcceptApproveText = AcceptText;
						tradeListItem.DeclineDenyText = DeclineText;
					}
					else if (!ForUser && item.TradeStatus == (int)Constants.TRADE_STATUS.PENDING_APPROVAL)
					{
						tradeListItem.InfoColumn.Tag = true;
						tradeListItem.AcceptApproveText = ApproveText;
						tradeListItem.DeclineDenyText = DenyText;
					}

					tradeListItem.InfoColumn.Line1 = item.TradeUser == null ? "" : item.TradeUser.DisplayName + " - ";

					switch (item.TradeStatusValue)
					{
						case (int)Constants.TRADE_STATUS.SUBMITTED:
							tradeListItem.Color = Color.LightGray;
							tradeListItem.InfoColumn.Line1 += "Posted";
							break;
						case (int)Constants.TRADE_STATUS.REQUESTED:
							tradeListItem.Color = Color.Yellow;
							tradeListItem.InfoColumn.Line1 += "Requested";
							break;
						case (int)Constants.TRADE_STATUS.PENDING_APPROVAL:
							tradeListItem.Color = Color.Green;
							tradeListItem.InfoColumn.Line1 += "Pending Approval";
							break;
						case (int)Constants.TRADE_STATUS.APPROVED:
							tradeListItem.Color = Color.Blue;
							tradeListItem.InfoColumn.Line1 += "Approved";
							break;
						case (int)Constants.TRADE_STATUS.DENIED:
							tradeListItem.Color = Color.Red;
							tradeListItem.InfoColumn.Line1 += "Denied";
							break;
						default:
							tradeListItem.Color = Color.Black;
							break;
					}

					Trades.Add(tradeListItem);
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

		private async System.Threading.Tasks.Task deleteTrade(ScheduleTrade trade)
		{
			await runTask(async () => await DataService.DeleteItemAsync("scheduleTrades", trade.ScheduleTradeIdValue),
				"Are you sure you want to delete this trade?");
			await ExecuteLoadTradesCommand();
		}

		private async System.Threading.Tasks.Task acceptApprove(ScheduleTrade scheduleTrade)
		{
			if (scheduleTrade.TradeStatus == (int)Constants.TRADE_STATUS.REQUESTED)
			{
				await tradeAcceptDeclineAsync(scheduleTrade, true);
			}
			else if (scheduleTrade.TradeStatus == (int)Constants.TRADE_STATUS.PENDING_APPROVAL)
			{
				await tradeApproveDenyAsync(scheduleTrade, true);
			}
		}

		private async System.Threading.Tasks.Task declineDeny(ScheduleTrade scheduleTrade)
		{
			if (scheduleTrade.TradeStatus == (int)Constants.TRADE_STATUS.REQUESTED)
			{
				await tradeAcceptDeclineAsync(scheduleTrade, false);
			}
			else if (scheduleTrade.TradeStatus == (int)Constants.TRADE_STATUS.PENDING_APPROVAL)
			{
				await tradeApproveDenyAsync(scheduleTrade, false);
			}
		}

		private async System.Threading.Tasks.Task tradeAcceptDeclineAsync(ScheduleTrade scheduleTrade, bool accept)
		{
			if (await runTask(async () =>
			{
				await DataService.PutItemAsync("acceptDeclineTrade", scheduleTrade.ScheduleTradeIdValue, new
				{
					accept
				});
			}, $"Are you sure you want to {(accept ? "Accept" : "Decline")} this trade?"))
				await ExecuteLoadTradesCommand();
		}

		private async System.Threading.Tasks.Task tradeApproveDenyAsync(ScheduleTrade scheduleTrade, bool approve)
		{
			if (await runTask(async () =>
			{
				await DataService.PutItemAsync("approveDenyTrade", scheduleTrade.ScheduleTradeIdValue, new
				{
					approve
				});
			}, $"Are you sure you want to {(approve ? "Approve" : "Deny")} this trade?"))
				await ExecuteLoadTradesCommand();
		}
	}

	public class TradeListItem
	{
		public ScheduleTrade ScheduleTrade { get; set; }
		public DisplayItem Schedule { get; set; }
		public DisplayItem ToSchedule { get; set; }
		public DisplayItem InfoColumn { get; set; }
		public Color Color { get; set; }
		public string AcceptApproveText { get; set; }
		public string DeclineDenyText { get; set; }
	}
}
