// !!!! DON'T MAKE CHANGES HERE, THEY WILL BE OVERWRITTEN !!!!
using Newtonsoft.Json;
using System;
using TheTeamTracker.Mobile.DataLayer.Classes;
namespace TheTeamTracker.Mobile.DataLayer.Models.Base
{				
	public abstract class DocumentBase : EntityBase
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
		
		private System.Guid? _DocumentId;
		[JsonProperty(PropertyName = "documentId", NullValueHandling = NullValueHandling.Ignore)]
		public virtual System.Guid? DocumentId 
		{ 
			get { return _DocumentId; }
			set 
			{
				_DocumentId = value;
				OnPropertyChanged("DocumentId");
			}
		}	
		[JsonIgnore]
		public virtual System.Guid DocumentIdValue
		{
			get { return DocumentId.GetValueOrDefault(); }
			set
			{ 
				DocumentId = value;
				OnPropertyChanged("DocumentIdValue");
			}
		}
		
		private string _DocumentName;
		[JsonProperty(PropertyName = "documentName", NullValueHandling = NullValueHandling.Include)]
		public virtual string DocumentName 
		{ 
			get { return _DocumentName; }
			set 
			{
				_DocumentName = value;
				OnPropertyChanged("DocumentName");
			}
		}
		
		private string _MimeType;
		[JsonProperty(PropertyName = "mimeType", NullValueHandling = NullValueHandling.Include)]
		public virtual string MimeType 
		{ 
			get { return _MimeType; }
			set 
			{
				_MimeType = value;
				OnPropertyChanged("MimeType");
			}
		}
		
		private System.Collections.Generic.List<System.Guid> _Positions;
		[JsonProperty(PropertyName = "positions", NullValueHandling = NullValueHandling.Include)]
		public virtual System.Collections.Generic.List<System.Guid> Positions 
		{ 
			get { return _Positions; }
			set 
			{
				_Positions = value;
				OnPropertyChanged("Positions");
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
			_Company = null;
					
		}
	}
}
