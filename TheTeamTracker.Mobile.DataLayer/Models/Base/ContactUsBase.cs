// !!!! DON'T MAKE CHANGES HERE, THEY WILL BE OVERWRITTEN !!!!
using Newtonsoft.Json;
using System;
using TheTeamTracker.Mobile.DataLayer.Classes;
namespace TheTeamTracker.Mobile.DataLayer.Models.Base
{				
	public abstract class ContactUsBase : EntityBase
	{
		
		private System.Guid? _ContactUsId;
		[JsonProperty(PropertyName = "contactUsId", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Guid? ContactUsId 
		{ 
			get { return _ContactUsId; }
			set 
			{
				_ContactUsId = value;
				OnPropertyChanged("ContactUsId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid ContactUsIdValue
		{
			get { return ContactUsId.GetValueOrDefault(); }
			set
			{ 
				ContactUsId = value;
				OnPropertyChanged("ContactUsIdValue");
			}
		}
		
		private string _Message;
		[JsonProperty(PropertyName = "message", NullValueHandling = NullValueHandling.Include)]
		public virtual string Message 
		{ 
			get { return _Message; }
			set 
			{
				_Message = value;
				OnPropertyChanged("Message");
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
					
		internal override void resetParentsChildren()
		{
			// TODO: for parents null through
					
		}
	}
}
