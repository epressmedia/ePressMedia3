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
	public partial class UDFReference
	{
		private int _referenceID;
		public virtual int ReferenceID
		{
			get
			{
				return this._referenceID;
			}
			set
			{
				this._referenceID = value;
			}
		}
		
		private string _displayValue;
		public virtual string DisplayValue
		{
			get
			{
				return this._displayValue;
			}
			set
			{
				this._displayValue = value;
			}
		}
		
		private string _internalValue;
		public virtual string InternalValue
		{
			get
			{
				return this._internalValue;
			}
			set
			{
				this._internalValue = value;
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
		
		private DateTime _effrectiveDate;
		public virtual DateTime EffrectiveDate
		{
			get
			{
				return this._effrectiveDate;
			}
			set
			{
				this._effrectiveDate = value;
			}
		}
		
		private DateTime? _terminateDate;
		public virtual DateTime? TerminateDate
		{
			get
			{
				return this._terminateDate;
			}
			set
			{
				this._terminateDate = value;
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