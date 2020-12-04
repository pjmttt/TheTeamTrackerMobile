// !!!! DON'T MAKE CHANGES HERE, THEY WILL BE OVERWRITTEN !!!!
using Newtonsoft.Json;
using System;
using TheTeamTracker.Mobile.DataLayer.Classes;
namespace TheTeamTracker.Mobile.DataLayer.Models.Base
{				
	public abstract class MaintenanceRequestImageBase : EntityBase
	{
		
		private string _ImageType;
		[JsonProperty(PropertyName = "imageType", NullValueHandling = NullValueHandling.Include)]
		public virtual string ImageType 
		{ 
			get { return _ImageType; }
			set 
			{
				_ImageType = value;
				OnPropertyChanged("ImageType");
			}
		}
		
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
		
		private System.Guid? _MaintenanceRequestImageId;
		[JsonProperty(PropertyName = "maintenanceRequestImageId", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Guid? MaintenanceRequestImageId 
		{ 
			get { return _MaintenanceRequestImageId; }
			set 
			{
				_MaintenanceRequestImageId = value;
				OnPropertyChanged("MaintenanceRequestImageId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid MaintenanceRequestImageIdValue
		{
			get { return MaintenanceRequestImageId.GetValueOrDefault(); }
			set
			{ 
				MaintenanceRequestImageId = value;
				OnPropertyChanged("MaintenanceRequestImageIdValue");
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
