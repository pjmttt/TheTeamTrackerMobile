// !!!! DON'T MAKE CHANGES HERE, THEY WILL BE OVERWRITTEN !!!!
using Newtonsoft.Json;
using System;
using TheTeamTracker.Mobile.DataLayer.Classes;
namespace TheTeamTracker.Mobile.DataLayer.Models.Base
{				
	public abstract class UserNoteBase : EntityBase
	{
		
		private string _Note;
		[JsonProperty(PropertyName = "note", NullValueHandling = NullValueHandling.Include)]
		public virtual string Note 
		{ 
			get { return _Note; }
			set 
			{
				_Note = value;
				OnPropertyChanged("Note");
			}
		}
		
		private System.DateTime? _NoteDate;
		[JsonProperty(PropertyName = "noteDate", NullValueHandling = NullValueHandling.Include)]
		public virtual System.DateTime? NoteDate 
		{ 
			get { return _NoteDate; }
			set 
			{
				_NoteDate = value;
				OnPropertyChanged("NoteDate");
			}
		}	
		[JsonIgnore]
		public virtual System.DateTime NoteDateValue
		{
			get { return NoteDate.GetValueOrDefault(); }
			set
			{ 
				NoteDate = value;
				OnPropertyChanged("NoteDateValue");
			}
		}
		[JsonIgnore]
		public virtual System.DateTime? NoteDateTimezoned
		{
			get { return NoteDate == null ? (System.DateTime?)null : TimeZoneInfo.ConvertTimeFromUtc(DateTime.SpecifyKind(NoteDate.Value, DateTimeKind.Unspecified), Globalization.TimeZoneInfo); }
			set
			{ 
				NoteDate = value == null ? (System.DateTime?)null : TimeZoneInfo.ConvertTimeToUtc(DateTime.SpecifyKind(value.Value, DateTimeKind.Unspecified), Globalization.TimeZoneInfo);
				OnPropertyChanged("NoteDateTimezoned");
			}
		}
		[JsonIgnore]
		public virtual System.DateTime NoteDateTimezonedValue
		{
			get { return NoteDateTimezoned.GetValueOrDefault(); }
			set
			{ 
				NoteDateTimezoned = value;
				OnPropertyChanged("NoteDateTimezonedValue");
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
		
		private System.Guid? _UserNoteId;
		[JsonProperty(PropertyName = "userNoteId", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Guid? UserNoteId 
		{ 
			get { return _UserNoteId; }
			set 
			{
				_UserNoteId = value;
				OnPropertyChanged("UserNoteId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid UserNoteIdValue
		{
			get { return UserNoteId.GetValueOrDefault(); }
			set
			{ 
				UserNoteId = value;
				OnPropertyChanged("UserNoteIdValue");
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
			_User = null;
					
		}
	}
}
