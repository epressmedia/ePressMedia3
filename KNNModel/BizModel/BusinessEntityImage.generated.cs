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
using BizModel;

namespace BizModel	
{
	public partial class BusinessEntityImage
	{
		private int _businessEntityImageId;
		public virtual int BusinessEntityImageId
		{
			get
			{
				return this._businessEntityImageId;
			}
			set
			{
				this._businessEntityImageId = value;
			}
		}
		
		private int _businessEntityId;
		public virtual int BusinessEntityId
		{
			get
			{
				return this._businessEntityId;
			}
			set
			{
				this._businessEntityId = value;
			}
		}
		
		private bool _primaryFg;
		public virtual bool PrimaryFg
		{
			get
			{
				return this._primaryFg;
			}
			set
			{
				this._primaryFg = value;
			}
		}
		
		private BusinessEntity _businessEntity;
		public virtual BusinessEntity BusinessEntity
		{
			get
			{
				return this._businessEntity;
			}
			set
			{
				this._businessEntity = value;
			}
		}
		
		private IList<BusinessEntityThumbnail> _businessEntityThumbnails = new List<BusinessEntityThumbnail>();
		public virtual IList<BusinessEntityThumbnail> BusinessEntityThumbnails
		{
			get
			{
				return this._businessEntityThumbnails;
			}
		}
		
	}
}
#pragma warning restore 1591
