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
using UDFModel;

namespace UDFModel	
{
	public partial class UDFValue
	{
		private int _uDFValueID;
		public virtual int UDFValueID
		{
			get
			{
				return this._uDFValueID;
			}
			set
			{
				this._uDFValueID = value;
			}
		}
		
		private string _uDFValue;
		public virtual string Value
		{
			get
			{
				return this._uDFValue;
			}
			set
			{
				this._uDFValue = value;
			}
		}
		
		private int _uDFID;
		public virtual int UDFID
		{
			get
			{
				return this._uDFID;
			}
			set
			{
				this._uDFID = value;
			}
		}
		
		private int _srcID;
		public virtual int SrcID
		{
			get
			{
				return this._srcID;
			}
			set
			{
				this._srcID = value;
			}
		}
		
		private int _contentTypeID;
		public virtual int ContentTypeID
		{
			get
			{
				return this._contentTypeID;
			}
			set
			{
				this._contentTypeID = value;
			}
		}
		
		private UDFInfo _uDF;
		public virtual UDFInfo UDF
		{
			get
			{
				return this._uDF;
			}
			set
			{
				this._uDF = value;
			}
		}
		
	}
}
#pragma warning restore 1591
