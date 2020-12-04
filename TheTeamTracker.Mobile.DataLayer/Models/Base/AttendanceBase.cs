// !!!! DON'T MAKE CHANGES HERE, THEY WILL BE OVERWRITTEN !!!!
using Newtonsoft.Json;
using System;
using TheTeamTracker.Mobile.DataLayer.Classes;
namespace TheTeamTracker.Mobile.DataLayer.Models.Base
{				
	public abstract class AttendanceBase : EntityBase
	{
		
		private System.DateTime? _AttendanceDate;
		[JsonProperty(PropertyName = "attendanceDate", NullValueHandling = NullValueHandling.Include)]
		public virtual System.DateTime? AttendanceDate 
		{ 
			get { return _AttendanceDate; }
			set 
			{
				_AttendanceDate = value;
				OnPropertyChanged("AttendanceDate");
			}
		}	
		[JsonIgnore]
		public virtual System.DateTime AttendanceDateValue
		{
			get { return AttendanceDate.GetValueOrDefault(); }
			set
			{ 
				AttendanceDate = value;
				OnPropertyChanged("AttendanceDateValue");
			}
		}
		
		private System.Guid? _AttendanceId;
		[JsonProperty(PropertyName = "attendanceId", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Guid? AttendanceId 
		{ 
			get { return _AttendanceId; }
			set 
			{
				_AttendanceId = value;
				OnPropertyChanged("AttendanceId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid AttendanceIdValue
		{
			get { return AttendanceId.GetValueOrDefault(); }
			set
			{ 
				AttendanceId = value;
				OnPropertyChanged("AttendanceIdValue");
			}
		}
		
		private System.Guid? _AttendanceReasonId;
		[JsonProperty(PropertyName = "attendanceReasonId", NullValueHandling = NullValueHandling.Include)]
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
		
		private AttendanceReason _AttendanceReason;
		[JsonProperty(PropertyName = "attendanceReason", NullValueHandling = NullValueHandling.Ignore)]
		public virtual AttendanceReason AttendanceReason
		{ 
			get { return _AttendanceReason; }
			set
			{
				_AttendanceReason = value;
				if (_AttendanceReason != null)
				{
					this._AttendanceReasonId = _AttendanceReason.AttendanceReasonId;
				}
				OnPropertyChanged("AttendanceReason");
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
			_AttendanceReason = null;
			_Company = null;
			_User = null;
					
		}
	}
}
