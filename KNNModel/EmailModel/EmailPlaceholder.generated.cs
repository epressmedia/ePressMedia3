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

namespace EmailModel	
{
	public partial class EmailPlaceholder
	{
		private int _placeholderId;
		public virtual int PlaceholderId
		{
			get
			{
				return this._placeholderId;
			}
			set
			{
				this._placeholderId = value;
			}
		}
		
		private string _placeholderName;
		public virtual string PlaceholderName
		{
			get
			{
				return this._placeholderName;
			}
			set
			{
				this._placeholderName = value;
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
		
		private Char _methodType;
		public virtual Char MethodType
		{
			get
			{
				return this._methodType;
			}
			set
			{
				this._methodType = value;
			}
		}
		
		private string _methodName;
		public virtual string MethodName
		{
			get
			{
				return this._methodName;
			}
			set
			{
				this._methodName = value;
			}
		}
		
		private int _param1;
		public virtual int Param1
		{
			get
			{
				return this._param1;
			}
			set
			{
				this._param1 = value;
			}
		}
		
		private int _param2;
		public virtual int Param2
		{
			get
			{
				return this._param2;
			}
			set
			{
				this._param2 = value;
			}
		}
		
		private string _columnName;
		public virtual string ColumnName
		{
			get
			{
				return this._columnName;
			}
			set
			{
				this._columnName = value;
			}
		}
		
	}
}
#pragma warning restore 1591