// !!!! DON'T MAKE CHANGES HERE, THEY WILL BE OVERWRITTEN !!!!
using Newtonsoft.Json;
using System;
using TheTeamTracker.Mobile.DataLayer.Classes;
namespace TheTeamTracker.Mobile.DataLayer.Models.Base
{				
	public abstract class MaintenanceRequestReplyBase : EntityBase
	{
		
		private System.Guid? _MaintenanceRequestId;
		[JsonProperty(PropertyName = "maintenanceRequestId", NullValueHandling = NullValueHandling.Include)]
		public virtual System.Guid? MaintenanceRequestId 
		{ 
			get { return _MaintenanceRequestId; }
			set 
			{
				_MaintenanceRequestId = value;
				OnPropertyChanged("MaintenanceRequestId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid MaintenanceRequestIdValue
		{
			get { return MaintenanceRequestId.GetValueOrDefault(); }
			set
			{ 
				MaintenanceRequestId = value;
				OnPropertyChanged("MaintenanceRequestIdValue");
			}
		}
		
		private System.Guid? _MaintenanceRequestReplyId;
		[JsonProperty(PropertyName = "maintenanceRequestReplyId", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Guid? MaintenanceRequestReplyId 
		{ 
			get { return _MaintenanceRequestReplyId; }
			set 
			{
				_MaintenanceRequestReplyId = value;
				OnPropertyChanged("MaintenanceRequestReplyId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid MaintenanceRequestReplyIdValue
		{
			get { return MaintenanceRequestReplyId.GetValueOrDefault(); }
			set
			{ 
				MaintenanceRequestReplyId = value;
				OnPropertyChanged("MaintenanceRequestReplyIdValue");
			}
		}
		
		private System.DateTime? _ReplyDate;
		[JsonProperty(PropertyName = "replyDate", NullValueHandling = NullValueHandling.Include)]
		public virtual System.DateTime? ReplyDate 
		{ 
			get { return _ReplyDate; }
			set 
			{
				_ReplyDate = value;
				OnPropertyChanged("ReplyDate");
			}
		}	
		[JsonIgnore]
		public virtual System.DateTime ReplyDateValue
		{
			get { return ReplyDate.GetValueOrDefault(); }
			set
			{ 
				ReplyDate = value;
				OnPropertyChanged("ReplyDateValue");
			}
		}
		
		private string _ReplyText;
		[JsonProperty(PropertyName = "replyText", NullValueHandling = NullValueHandling.Include)]
		public virtual string ReplyText 
		{ 
			get { return _ReplyText; }
			set 
			{
				_ReplyText = value;
				OnPropertyChanged("ReplyText");
			}
		}
		
		private string _UpdatedBy;
		[JsonProperty(PropertyName = "updatedBy", NullValueHandling = NullValueHandling.Include)]
		public virtual string UpdatedBy 
		{ 
			get { return _UpdatedBy; }
			set 
			{
				_UpdatedBy = value;
				OnPropertyChanged("UpdatedBy");
			}
		}
		
		private MaintenanceRequest _MaintenanceRequest;
		[JsonProperty(PropertyName = "maintenanceRequest", NullValueHandling = NullValueHandling.Ignore)]
		public virtual MaintenanceRequest MaintenanceRequest
		{ 
			get { return _MaintenanceRequest; }
			set
			{
				_MaintenanceRequest = value;
				if (_MaintenanceRequest != null)
				{
					this._MaintenanceRequestId = _MaintenanceRequest.MaintenanceRequestId;
				}
				OnPropertyChanged("MaintenanceRequest");
			}
		}
					
		internal override void resetParentsChildren()
		{
			// TODO: for parents null through
			_MaintenanceRequest = null;
					
		}
	}
}
