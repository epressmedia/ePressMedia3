#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the ClassGenerator.ttinclude code generation file.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Common;
using System.Collections.Generic;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Data.Common;
using Telerik.OpenAccess.Metadata.Fluent;
using Telerik.OpenAccess.Metadata.Fluent.Advanced;

namespace SiteModel	
{
	public partial class PageTemplate
	{
		private int _templateId;
		public virtual int TemplateId
		{
			get
			{
				return this._templateId;
			}
			set
			{
				this._templateId = value;
			}
		}
		
		private string _name;
		public virtual string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}
		
		private string _description;
		public virtual string Description
		{
			get
			{
				return this._description;
			}
			set
			{
				this._description = value;
			}
		}
		
		private string _metadataStr;
		public virtual string MetadataStr
		{
			get
			{
				return this._metadataStr;
			}
			set
			{
				this._metadataStr = value;
			}
		}
		
		private int _templateVersion;
		public virtual int TemplateVersion
		{
			get
			{
				return this._templateVersion;
			}
			set
			{
				this._templateVersion = value;
			}
		}
		
		private DateTime? _created_Dt;
		public virtual DateTime? Created_dt
		{
			get
			{
				return this._created_Dt;
			}
			set
			{
				this._created_Dt = value;
			}
		}
		
		private string _created_By;
		public virtual string Created_by
		{
			get
			{
				return this._created_By;
			}
			set
			{
				this._created_By = value;
			}
		}
		
		private DateTime _modified_Dt;
		public virtual DateTime Modified_dt
		{
			get
			{
				return this._modified_Dt;
			}
			set
			{
				this._modified_Dt = value;
			}
		}
		
		private string _modified_By;
		public virtual string Modified_by
		{
			get
			{
				return this._modified_By;
			}
			set
			{
				this._modified_By = value;
			}
		}
		
		private bool _deleted_Fg;
		public virtual bool Deleted_fg
		{
			get
			{
				return this._deleted_Fg;
			}
			set
			{
				this._deleted_Fg = value;
			}
		}
		
	}
}
#pragma warning restore 1591