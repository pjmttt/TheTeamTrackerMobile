// !!!! DON'T MAKE CHANGES HERE, THEY WILL BE OVERWRITTEN !!!!
using Newtonsoft.Json;
using System;
using TheTeamTracker.Mobile.DataLayer.Classes;
namespace TheTeamTracker.Mobile.DataLayer.Models.Base
{				
	public abstract class PositionBase : EntityBase
	{
		
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
		
		private System.Guid? _PositionId;
		[JsonProperty(PropertyName = "positionId", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Guid? PositionId 
		{ 
			get { return _PositionId; }
			set 
			{
				_PositionId = value;
				OnPropertyChanged("PositionId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid PositionIdValue
		{
			get { return PositionId.GetValueOrDefault(); }
			set
			{ 
				PositionId = value;
				OnPropertyChanged("PositionIdValue");
			}
		}
		
		private string _PositionName;
		[JsonProperty(PropertyName = "positionName", NullValueHandling = NullValueHandling.Include)]
		public virtual string PositionName 
		{ 
			get { return _PositionName; }
			set 
			{
				_PositionName = value;
				OnPropertyChanged("PositionName");
			}
		}
		
		private string _TextColor;
		[JsonProperty(PropertyName = "textColor", NullValueHandling = NullValueHandling.Include)]
		public virtual string TextColor 
		{ 
			get { return _TextColor; }
			set 
			{
				_TextColor = value;
				OnPropertyChanged("TextColor");
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
		[JsonProperty(PropertyName = "users", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<User> Users { get; set; }
		
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
					
		internal override void resetParentsChildren()
		{
			// TODO: for parents null through
			if (Users != null)
			{
				foreach (var child in Users)	
				{
					child.resetParentsChildren();
				}
			}
		
			_Company = null;
					
		}
	}
}
