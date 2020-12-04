// !!!! DON'T MAKE CHANGES HERE, THEY WILL BE OVERWRITTEN !!!!
using Newtonsoft.Json;
using System;
using TheTeamTracker.Mobile.DataLayer.Classes;
namespace TheTeamTracker.Mobile.DataLayer.Models.Base
{				
	public abstract class AttendanceReasonBase : EntityBase
	{
		
		private System.Guid? _AttendanceReasonId;
		[JsonProperty(PropertyName = "attendanceReasonId", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Guid? AttendanceReasonId 
		{ 
			get { return _AttendanceReasonId; }
			set 
			{
				_AttendanceReasonId = value;
				OnPropertyChanged("AttendanceReasonId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid AttendanceReasonIdValue
		{
			get { return AttendanceReasonId.GetValueOrDefault(); }
			set
			{ 
				AttendanceReasonId = value;
				OnPropertyChanged("AttendanceReasonIdValue");
			}
		}
		
		private string _BackgroundColor;
		[JsonProperty(PropertyName = "backgroundColor", NullValueHandling = NullValueHandling.Include)]
		public virtual string BackgroundColor 
		{ 
			get { return _BackgroundColor; }
			set 
			{
				_BackgroundColor = value;
				OnPropertyChanged("BackgroundColor");
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
		
		private string _ReasonName;
		[JsonProperty(PropertyName = "reasonName", NullValueHandling = NullValueHandling.Include)]
		public virtual string ReasonName 
		{ 
			get { return _ReasonName; }
			set 
			{
				_ReasonName = value;
				OnPropertyChanged("ReasonName");
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
		[JsonProperty(PropertyName = "attendances", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Collections.ObjectModel.ObservableCollection<Attendance> Attendances { get; set; }
		
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
			if (Attendances != null)
			{
				foreach (var child in Attendances)	
				{
					child.resetParentsChildren();
				}
			}
		
			_Company = null;
					
		}
	}
}
