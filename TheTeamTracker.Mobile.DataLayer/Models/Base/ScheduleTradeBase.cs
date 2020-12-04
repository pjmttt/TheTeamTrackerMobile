// !!!! DON'T MAKE CHANGES HERE, THEY WILL BE OVERWRITTEN !!!!
using Newtonsoft.Json;
using System;
using TheTeamTracker.Mobile.DataLayer.Classes;
namespace TheTeamTracker.Mobile.DataLayer.Models.Base
{				
	public abstract class ScheduleTradeBase : EntityBase
	{
		
		private string _Comments;
		[JsonProperty(PropertyName = "comments", NullValueHandling = NullValueHandling.Include)]
		public virtual string Comments 
		{ 
			get { return _Comments; }
			set 
			{
				_Comments = value;
				OnPropertyChanged("Comments");
			}
		}
		
		private System.Guid? _ScheduleId;
		[JsonProperty(PropertyName = "scheduleId", NullValueHandling = NullValueHandling.Include)]
		public virtual System.Guid? ScheduleId 
		{ 
			get { return _ScheduleId; }
			set 
			{
				_ScheduleId = value;
				OnPropertyChanged("ScheduleId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid ScheduleIdValue
		{
			get { return ScheduleId.GetValueOrDefault(); }
			set
			{ 
				ScheduleId = value;
				OnPropertyChanged("ScheduleIdValue");
			}
		}
		
		private System.Guid? _ScheduleTradeId;
		[JsonProperty(PropertyName = "scheduleTradeId", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Guid? ScheduleTradeId 
		{ 
			get { return _ScheduleTradeId; }
			set 
			{
				_ScheduleTradeId = value;
				OnPropertyChanged("ScheduleTradeId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid ScheduleTradeIdValue
		{
			get { return ScheduleTradeId.GetValueOrDefault(); }
			set
			{ 
				ScheduleTradeId = value;
				OnPropertyChanged("ScheduleTradeIdValue");
			}
		}
		
		private System.Guid? _TradeForScheduleId;
		[JsonProperty(PropertyName = "tradeForScheduleId", NullValueHandling = NullValueHandling.Include)]
		public virtual System.Guid? TradeForScheduleId 
		{ 
			get { return _TradeForScheduleId; }
			set 
			{
				_TradeForScheduleId = value;
				OnPropertyChanged("TradeForScheduleId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid TradeForScheduleIdValue
		{
			get { return TradeForScheduleId.GetValueOrDefault(); }
			set
			{ 
				TradeForScheduleId = value;
				OnPropertyChanged("TradeForScheduleIdValue");
			}
		}
		
		private int? _TradeStatus;
		[JsonProperty(PropertyName = "tradeStatus", NullValueHandling = NullValueHandling.Include)]
		public virtual int? TradeStatus 
		{ 
			get { return _TradeStatus; }
			set 
			{
				_TradeStatus = value;
				OnPropertyChanged("TradeStatus");
			}
		}	
		[JsonIgnore]
		public virtual int TradeStatusValue
		{
			get { return TradeStatus.GetValueOrDefault(); }
			set
			{ 
				TradeStatus = value;
				OnPropertyChanged("TradeStatusValue");
			}
		}
		
		private System.Guid? _TradeUserId;
		[JsonProperty(PropertyName = "tradeUserId", NullValueHandling = NullValueHandling.Include)]
		public virtual System.Guid? TradeUserId 
		{ 
			get { return _TradeUserId; }
			set 
			{
				_TradeUserId = value;
				OnPropertyChanged("TradeUserId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid TradeUserIdValue
		{
			get { return TradeUserId.GetValueOrDefault(); }
			set
			{ 
				TradeUserId = value;
				OnPropertyChanged("TradeUserIdValue");
			}
		}
		
		private Schedule _Schedule;
		[JsonProperty(PropertyName = "schedule", NullValueHandling = NullValueHandling.Ignore)]
		public virtual Schedule Schedule
		{ 
			get { return _Schedule; }
			set
			{
				_Schedule = value;
				if (_Schedule != null)
				{
					this._ScheduleId = _Schedule.ScheduleId;
				}
				OnPropertyChanged("Schedule");
			}
		}
		
		private Schedule _TradeForSchedule;
		[JsonProperty(PropertyName = "tradeForSchedule", NullValueHandling = NullValueHandling.Ignore)]
		public virtual Schedule TradeForSchedule
		{ 
			get { return _TradeForSchedule; }
			set
			{
				_TradeForSchedule = value;
				if (_TradeForSchedule != null)
				{
					this._TradeForScheduleId = _TradeForSchedule.ScheduleId;
				}
				OnPropertyChanged("TradeForSchedule");
			}
		}
		
		private User _TradeUser;
		[JsonProperty(PropertyName = "tradeUser", NullValueHandling = NullValueHandling.Ignore)]
		public virtual User TradeUser
		{ 
			get { return _TradeUser; }
			set
			{
				_TradeUser = value;
				if (_TradeUser != null)
				{
					this._TradeUserId = _TradeUser.UserId;
				}
				OnPropertyChanged("TradeUser");
			}
		}
					
		internal override void resetParentsChildren()
		{
			// TODO: for parents null through
			_Schedule = null;
			_TradeForSchedule = null;
			_TradeUser = null;
					
		}
	}
}
