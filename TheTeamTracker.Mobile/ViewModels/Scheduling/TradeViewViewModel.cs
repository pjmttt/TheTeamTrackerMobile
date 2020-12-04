using System;
using System.Collections.Generic;
using System.Text;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.DataLayer.Models;
using Xamarin.Forms;

namespace TheTeamTracker.Mobile.ViewModels.Scheduling
{
    public class TradeViewViewModel : BaseViewModel
    {
		public const string SUCCESS = "success";
		public bool ForUser { get; }
		public TradeListItem TradeListItem { get; }

		public Command AcceptCommand { get; }
		public Command ApproveCommand { get; }
		public Command DeclineCommand { get; }
		public Command DenyCommand { get; }

		public bool ShowAcceptDecline
		{
			get { return TradeListItem.AcceptApproveText == TradeListViewModel.AcceptText; }
		}

		public bool ShowApproveDeny
		{
			get { return TradeListItem.AcceptApproveText == TradeListViewModel.ApproveText; }
		}

		public bool AllowCommentEdit
		{
			get { return ShowAcceptDecline || ShowApproveDeny;  }
		}

		public bool DisallowCommentEdit { get { return !AllowCommentEdit; } }

		public TradeViewViewModel(bool forUser, TradeListItem tradeListItem)
		{
			ForUser = forUser;
			TradeListItem = tradeListItem;
			AcceptCommand = new Command(async () => await acceptAsync());
			ApproveCommand = new Command(async () => await approveAsync());
			DeclineCommand = new Command(async () => await declineAsync());
			DenyCommand = new Command(async () => await denyAsync());
		}

		private async System.Threading.Tasks.Task tradeAcceptDeclineAsync(bool accept)
		{
			IsBusy = true;

			try
			{
				await DataService.PutItemAsync("acceptDeclineTrade", TradeListItem.ScheduleTrade.ScheduleTradeIdValue, new
				{
					accept,
					comments = TradeListItem.ScheduleTrade.Comments,
				});
				MessagingCenter.Send<TradeViewViewModel>(this, SUCCESS);
				IsBusy = false;
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

		private async System.Threading.Tasks.Task tradeApproveDenyAsync(bool approve)
		{
			IsBusy = true;

			try
			{
				await DataService.PutItemAsync("approveDenyTrade", TradeListItem.ScheduleTrade.ScheduleTradeIdValue, new
				{
					approve,
					comments = TradeListItem.ScheduleTrade.Comments,
				});
				MessagingCenter.Send<TradeViewViewModel>(this, SUCCESS);
				IsBusy = false;
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

		private async System.Threading.Tasks.Task acceptAsync()
		{
			await this.tradeAcceptDeclineAsync(true);
		}

		private async System.Threading.Tasks.Task approveAsync()
		{
			await tradeApproveDenyAsync(true);
		}

		private async System.Threading.Tasks.Task declineAsync()
		{
			await this.tradeAcceptDeclineAsync(false);
		}

		private async System.Threading.Tasks.Task denyAsync()
		{
			await tradeApproveDenyAsync(false);
		}
	}
}
