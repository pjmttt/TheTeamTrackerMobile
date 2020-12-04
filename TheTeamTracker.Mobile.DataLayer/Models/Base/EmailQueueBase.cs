// !!!! DON'T MAKE CHANGES HERE, THEY WILL BE OVERWRITTEN !!!!
using Newtonsoft.Json;
using System;
using TheTeamTracker.Mobile.DataLayer.Classes;
namespace TheTeamTracker.Mobile.DataLayer.Models.Base
{				
	public abstract class EmailQueueBase : EntityBase
	{
		
		private string _Body;
		[JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Include)]
		public virtual string Body 
		{ 
			get { return _Body; }
			set 
			{
				_Body = value;
				OnPropertyChanged("Body");
			}
		}
		
		private System.DateTime? _EmailDate;
		[JsonProperty(PropertyName = "emailDate", NullValueHandling = NullValueHandling.Include)]
		public virtual System.DateTime? EmailDate 
		{ 
			get { return _EmailDate; }
			set 
			{
				_EmailDate = value;
				OnPropertyChanged("EmailDate");
			}
		}	
		[JsonIgnore]
		public virtual System.DateTime EmailDateValue
		{
			get { return EmailDate.GetValueOrDefault(); }
			set
			{ 
				EmailDate = value;
				OnPropertyChanged("EmailDateValue");
			}
		}
		[JsonIgnore]
		public virtual System.DateTime? EmailDateTimezoned
		{
			get { return EmailDate == null ? (System.DateTime?)null : TimeZoneInfo.ConvertTimeFromUtc(DateTime.SpecifyKind(EmailDate.Value, DateTimeKind.Unspecified), Globalization.TimeZoneInfo); }
			set
			{ 
				EmailDate = value == null ? (System.DateTime?)null : TimeZoneInfo.ConvertTimeToUtc(DateTime.SpecifyKind(value.Value, DateTimeKind.Unspecified), Globalization.TimeZoneInfo);
				OnPropertyChanged("EmailDateTimezoned");
			}
		}
		[JsonIgnore]
		public virtual System.DateTime EmailDateTimezonedValue
		{
			get { return EmailDateTimezoned.GetValueOrDefault(); }
			set
			{ 
				EmailDateTimezoned = value;
				OnPropertyChanged("EmailDateTimezonedValue");
			}
		}
			
		
		private System.Guid? _EmailQueueId;
		[JsonProperty(PropertyName = "emailQueueId", NullValueHandling = NullValueHandling.Ignore)]
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
		
		private bool? _IsText;
		[JsonProperty(PropertyName = "isText", NullValueHandling = NullValueHandling.Include)]
		public virtual bool? IsText 
		{ 
			get { return _IsText; }
			set 
			{
				_IsText = value;
				OnPropertyChanged("IsText");
			}
		}	
		[JsonIgnore]
		public virtual bool IsTextValue
		{
			get { return IsText.GetValueOrDefault(); }
			set
			{ 
				IsText = value;
				OnPropertyChanged("IsTextValue");
			}
		}
		
		private System.Guid? _ParentId;
		[JsonProperty(PropertyName = "parentId", NullValueHandling = NullValueHandling.Include)]
		public virtual System.Guid? ParentId 
		{ 
			get { return _ParentId; }
			set 
			{
				_ParentId = value;
				OnPropertyChanged("ParentId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid ParentIdValue
		{
			get { return ParentId.GetValueOrDefault(); }
			set
			{ 
				ParentId = value;
				OnPropertyChanged("ParentIdValue");
			}
		}
		
		private string _ReplyTo;
		[JsonProperty(PropertyName = "replyTo", NullValueHandling = NullValueHandling.Include)]
		public virtual string ReplyTo 
		{ 
			get { return _ReplyTo; }
			set 
			{
				_ReplyTo = value;
				OnPropertyChanged("ReplyTo");
			}
		}
		
		private string _Subject;
		[JsonProperty(PropertyName = "subject", NullValueHandling = NullValueHandling.Include)]
		public virtual string Subject 
		{ 
			get { return _Subject; }
			set 
			{
				_Subject = value;
				OnPropertyChanged("Subject");
			}
		}
		
		private System.Collections.Generic.List<string> _Tos;
		[JsonProperty(PropertyName = "tos", NullValueHandling = NullValueHandling.Include)]
		public virtual System.Collections.Generic.List<string> Tos 
		{ 
			get { return _Tos; }
			set 
			{
				_Tos = value;
				OnPropertyChanged("Tos");
			}
		}
		[JsonProperty(PropertyName = "emailQueueAttachments", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<EmailQueueAttachment> EmailQueueAttachments { get; set; }
					
		internal override void resetParentsChildren()
		{
			// TODO: for parents null through
			if (EmailQueueAttachments != null)
			{
				foreach (var child in EmailQueueAttachments)	
				{
					child.resetParentsChildren();
				}
			}
		
					
		}
	}
}
