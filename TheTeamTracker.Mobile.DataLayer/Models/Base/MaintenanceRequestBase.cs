// !!!! DON'T MAKE CHANGES HERE, THEY WILL BE OVERWRITTEN !!!!
using Newtonsoft.Json;
using System;
using TheTeamTracker.Mobile.DataLayer.Classes;
namespace TheTeamTracker.Mobile.DataLayer.Models.Base
{				
	public abstract class MaintenanceRequestBase : EntityBase
	{
		
		private System.Guid? _AssignedToId;
		[JsonProperty(PropertyName = "assignedToId", NullValueHandling = NullValueHandling.Include)]
		public virtual System.Guid? AssignedToId 
		{ 
			get { return _AssignedToId; }
			set 
			{
				_AssignedToId = value;
				OnPropertyChanged("AssignedToId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid AssignedToIdValue
		{
			get { return AssignedToId.GetValueOrDefault(); }
			set
			{ 
				AssignedToId = value;
				OnPropertyChanged("AssignedToIdValue");
			}
		}
		
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
		
		private bool? _IsAddressed;
		[JsonProperty(PropertyName = "isAddressed", NullValueHandling = NullValueHandling.Include)]
		public virtual bool? IsAddressed 
		{ 
			get { return _IsAddressed; }
			set 
			{
				_IsAddressed = value;
				OnPropertyChanged("IsAddressed");
			}
		}	
		[JsonIgnore]
		public virtual bool IsAddressedValue
		{
			get { return IsAddressed.GetValueOrDefault(); }
			set
			{ 
				IsAddressed = value;
				OnPropertyChanged("IsAddressedValue");
			}
		}
		
		private System.Guid? _MaintenanceRequestId;
		[JsonProperty(PropertyName = "maintenanceRequestId", NullValueHandling = NullValueHandling.Ignore)]
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
		
		private System.DateTime? _RequestDate;
		[JsonProperty(PropertyName = "requestDate", NullValueHandling = NullValueHandling.Include)]
		public virtual System.DateTime? RequestDate 
		{ 
			get { return _RequestDate; }
			set 
			{
				_RequestDate = value;
				OnPropertyChanged("RequestDate");
			}
		}	
		[JsonIgnore]
		public virtual System.DateTime RequestDateValue
		{
			get { return RequestDate.GetValueOrDefault(); }
			set
			{ 
				RequestDate = value;
				OnPropertyChanged("RequestDateValue");
			}
		}
		
		private string _RequestDescription;
		[JsonProperty(PropertyName = "requestDescription", NullValueHandling = NullValueHandling.Include)]
		public virtual string RequestDescription 
		{ 
			get { return _RequestDescription; }
			set 
			{
				_RequestDescription = value;
				OnPropertyChanged("RequestDescription");
			}
		}
		
		private System.Guid? _RequestedById;
		[JsonProperty(PropertyName = "requestedById", NullValueHandling = NullValueHandling.Include)]
		public virtual System.Guid? RequestedById 
		{ 
			get { return _RequestedById; }
			set 
			{
				_RequestedById = value;
				OnPropertyChanged("RequestedById");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid RequestedByIdValue
		{
			get { return RequestedById.GetValueOrDefault(); }
			set
			{ 
				RequestedById = value;
				OnPropertyChanged("RequestedByIdValue");
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
		[JsonProperty(PropertyName = "maintenanceRequestImages", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<MaintenanceRequestImage> MaintenanceRequestImages { get; set; }
		[JsonProperty(PropertyName = "maintenanceRequestReplys", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<MaintenanceRequestReply> MaintenanceRequestReplys { get; set; }
		
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
		
		private User _RequestedBy;
		[JsonProperty(PropertyName = "requestedBy", NullValueHandling = NullValueHandling.Ignore)]
		public virtual User RequestedBy
		{ 
			get { return _RequestedBy; }
			set
			{
				_RequestedBy = value;
				if (_RequestedBy != null)
				{
					this._RequestedById = _RequestedBy.UserId;
				}
				OnPropertyChanged("RequestedBy");
			}
		}
		
		private User _AssignedTo;
		[JsonProperty(PropertyName = "assignedTo", NullValueHandling = NullValueHandling.Ignore)]
		public virtual User AssignedTo
		{ 
			get { return _AssignedTo; }
			set
			{
				_AssignedTo = value;
				if (_AssignedTo != null)
				{
					this._AssignedToId = _AssignedTo.UserId;
				}
				OnPropertyChanged("AssignedTo");
			}
		}
					
		internal override void resetParentsChildren()
		{
			// TODO: for parents null through
			if (MaintenanceRequestImages != null)
			{
				foreach (var child in MaintenanceRequestImages)	
				{
					child.resetParentsChildren();
				}
			}
		
			if (MaintenanceRequestReplys != null)
			{
				foreach (var child in MaintenanceRequestReplys)	
				{
					child.resetParentsChildren();
				}
			}
		
			_Company = null;
			_RequestedBy = null;
			_AssignedTo = null;
					
		}
	}
}
