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

namespace BizModel	
{
	public partial class BusinessEntityConfig
	{
		private int _id;
		public virtual int Id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
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
		
		private string _detailMetadataStr;
		public virtual string DetailMetadataStr
		{
			get
			{
				return this._detailMetadataStr;
			}
			set
			{
				this._detailMetadataStr = value;
			}
		}
		
	}
}
#pragma warning restore 1591
