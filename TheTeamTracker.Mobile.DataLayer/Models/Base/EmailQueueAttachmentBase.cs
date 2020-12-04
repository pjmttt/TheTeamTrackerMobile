// !!!! DON'T MAKE CHANGES HERE, THEY WILL BE OVERWRITTEN !!!!
using Newtonsoft.Json;
using System;
using TheTeamTracker.Mobile.DataLayer.Classes;
namespace TheTeamTracker.Mobile.DataLayer.Models.Base
{				
	public abstract class EmailQueueAttachmentBase : EntityBase
	{
		
		private byte[] _Attachment;
		[JsonProperty(PropertyName = "attachment", NullValueHandling = NullValueHandling.Include)]
		public virtual byte[] Attachment 
		{ 
			get { return _Attachment; }
			set 
			{
				_Attachment = value;
				OnPropertyChanged("Attachment");
			}
		}
		
		private string _AttachmentName;
		[JsonProperty(PropertyName = "attachmentName", NullValueHandling = NullValueHandling.Include)]
		public virtual string AttachmentName 
		{ 
			get { return _AttachmentName; }
			set 
			{
				_AttachmentName = value;
				OnPropertyChanged("AttachmentName");
			}
		}
		
		private string _AttachmentType;
		[JsonProperty(PropertyName = "attachmentType", NullValueHandling = NullValueHandling.Include)]
		public virtual string AttachmentType 
		{ 
			get { return _AttachmentType; }
			set 
			{
				_AttachmentType = value;
				OnPropertyChanged("AttachmentType");
			}
		}
		
		private System.Guid? _EmailQueueAttachmentId;
		[JsonProperty(PropertyName = "emailQueueAttachmentId", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Guid? EmailQueueAttachmentId 
		{ 
			get { return _EmailQueueAttachmentId; }
			set 
			{
				_EmailQueueAttachmentId = value;
				OnPropertyChanged("EmailQueueAttachmentId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid EmailQueueAttachmentIdValue
		{
			get { return EmailQueueAttachmentId.GetValueOrDefault(); }
			set
			{ 
				EmailQueueAttachmentId = value;
				OnPropertyChanged("EmailQueueAttachmentIdValue");
			}
		}
		
		private System.Guid? _EmailQueueId;
		[JsonProperty(PropertyName = "emailQueueId", NullValueHandling = NullValueHandling.Include)]
		public virtual System.Guid? EmailQueueId 
		{ 
			get { return _EmailQueueId; }
			set 
			{
				_EmailQueueId = value;
				OnPropertyChanged("EmailQueueId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid EmailQueueIdValue
		{
			get { return EmailQueueId.GetValueOrDefault(); }
			set
			{ 
				EmailQueueId = value;
				OnPropertyChanged("EmailQueueIdValue");
			}
		}
		
		private EmailQueue _EmailQueue;
		[JsonProperty(PropertyName = "emailQueue", NullValueHandling = NullValueHandling.Ignore)]
		public virtual EmailQueue EmailQueue
		{ 
			get { return _EmailQueue; }
			set
			{
				_EmailQueue = value;
				if (_EmailQueue != null)
				{
					this._EmailQueueId = _EmailQueue.EmailQueueId;
				}
				OnPropertyChanged("EmailQueue");
			}
		}
					
		internal override void resetParentsChildren()
		{
			// TODO: for parents null through
			_EmailQueue = null;
					
		}
	}
}
