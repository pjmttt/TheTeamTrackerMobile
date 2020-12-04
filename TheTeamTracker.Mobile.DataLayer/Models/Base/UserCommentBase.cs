// !!!! DON'T MAKE CHANGES HERE, THEY WILL BE OVERWRITTEN !!!!
using Newtonsoft.Json;
using System;
using TheTeamTracker.Mobile.DataLayer.Classes;
namespace TheTeamTracker.Mobile.DataLayer.Models.Base
{				
	public abstract class UserCommentBase : EntityBase
	{
		
		private string _Comment;
		[JsonProperty(PropertyName = "comment", NullValueHandling = NullValueHandling.Include)]
		public virtual string Comment 
		{ 
			get { return _Comment; }
			set 
			{
				_Comment = value;
				OnPropertyChanged("Comment");
			}
		}
		
		private System.DateTime? _CommentDate;
		[JsonProperty(PropertyName = "commentDate", NullValueHandling = NullValueHandling.Include)]
		public virtual System.DateTime? CommentDate 
		{ 
			get { return _CommentDate; }
			set 
			{
				_CommentDate = value;
				OnPropertyChanged("CommentDate");
			}
		}	
		[JsonIgnore]
		public virtual System.DateTime CommentDateValue
		{
			get { return CommentDate.GetValueOrDefault(); }
			set
			{ 
				CommentDate = value;
				OnPropertyChanged("CommentDateValue");
			}
		}
		[JsonIgnore]
		public virtual System.DateTime? CommentDateTimezoned
		{
			get { return CommentDate == null ? (System.DateTime?)null : TimeZoneInfo.ConvertTimeFromUtc(DateTime.SpecifyKind(CommentDate.Value, DateTimeKind.Unspecified), Globalization.TimeZoneInfo); }
			set
			{ 
				CommentDate = value == null ? (System.DateTime?)null : TimeZoneInfo.ConvertTimeToUtc(DateTime.SpecifyKind(value.Value, DateTimeKind.Unspecified), Globalization.TimeZoneInfo);
				OnPropertyChanged("CommentDateTimezoned");
			}
		}
		[JsonIgnore]
		public virtual System.DateTime CommentDateTimezonedValue
		{
			get { return CommentDateTimezoned.GetValueOrDefault(); }
			set
			{ 
				CommentDateTimezoned = value;
				OnPropertyChanged("CommentDateTimezonedValue");
			}
		}
			
		
		private System.Guid? _CompanyId;
		[JsonProperty(PropertyName = "companyId", NullValueHandling = NullValueHandling.Include)]
		public virtual System.Guid? CompanyId 
		{ 
			get { return _CompanyId; }
			set 
			{
				_CompanyId = value;
				OnPropertyChanged("CompanyId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid CompanyIdValue
		{
			get { return CompanyId.GetValueOrDefault(); }
			set
			{ 
				CompanyId = value;
				OnPropertyChanged("CompanyIdValue");
			}
		}
		
		private bool? _SendEmail;
		[JsonProperty(PropertyName = "sendEmail", NullValueHandling = NullValueHandling.Include)]
		public virtual bool? SendEmail 
		{ 
			get { return _SendEmail; }
			set 
			{
				_SendEmail = value;
				OnPropertyChanged("SendEmail");
			}
		}	
		[JsonIgnore]
		public virtual bool SendEmailValue
		{
			get { return SendEmail.GetValueOrDefault(); }
			set
			{ 
				SendEmail = value;
				OnPropertyChanged("SendEmailValue");
			}
		}
		
		private bool? _SendText;
		[JsonProperty(PropertyName = "sendText", NullValueHandling = NullValueHandling.Include)]
		public virtual bool? SendText 
		{ 
			get { return _SendText; }
			set 
			{
				_SendText = value;
				OnPropertyChanged("SendText");
			}
		}	
		[JsonIgnore]
		public virtual bool SendTextValue
		{
			get { return SendText.GetValueOrDefault(); }
			set
			{ 
				SendText = value;
				OnPropertyChanged("SendTextValue");
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
		
		private System.Guid? _UserCommentId;
		[JsonProperty(PropertyName = "userCommentId", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Guid? UserCommentId 
		{ 
			get { return _UserCommentId; }
			set 
			{
				_UserCommentId = value;
				OnPropertyChanged("UserCommentId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid UserCommentIdValue
		{
			get { return UserCommentId.GetValueOrDefault(); }
			set
			{ 
				UserCommentId = value;
				OnPropertyChanged("UserCommentIdValue");
			}
		}
		
		private System.Guid? _UserId;
		[JsonProperty(PropertyName = "userId", NullValueHandling = NullValueHandling.Include)]
		public virtual System.Guid? UserId 
		{ 
			get { return _UserId; }
			set 
			{
				_UserId = value;
				OnPropertyChanged("UserId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid UserIdValue
		{
			get { return UserId.GetValueOrDefault(); }
			set
			{ 
				UserId = value;
				OnPropertyChanged("UserIdValue");
			}
		}
		
		private System.Collections.Generic.List<System.Guid> _UserIds;
		[JsonProperty(PropertyName = "userIds", NullValueHandling = NullValueHandling.Include)]
		public virtual System.Collections.Generic.List<System.Guid> UserIds 
		{ 
			get { return _UserIds; }
			set 
			{
				_UserIds = value;
				OnPropertyChanged("UserIds");
			}
		}
		
		private Company _Company;
		[JsonProperty(PropertyName = "company", NullValueHandling = NullValueHandling.Ignore)]
		public virtual Company Company
		{ 
			get { return _Company; }
			set
			{
				_Company = value;
				if (_Company != null)
				{
					this._CompanyId = _Company.CompanyId;
				}
				OnPropertyChanged("Company");
			}
		}
		
		private User _User;
		[JsonProperty(PropertyName = "user", NullValueHandling = NullValueHandling.Ignore)]
		public virtual User User
		{ 
			get { return _User; }
			set
			{
				_User = value;
				if (_User != null)
				{
					this._UserId = _User.UserId;
				}
				OnPropertyChanged("User");
			}
		}
					
		internal override void resetParentsChildren()
		{
			// TODO: for parents null through
			_Company = null;
			_User = null;
					
		}
	}
}
