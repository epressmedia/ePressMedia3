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
using SiteModel;

namespace SiteModel	
{
	public partial class Widget_detail
	{
		private int _widgetID;
		public virtual int Widget_id
		{
			get
			{
				return this._widgetID;
			}
			set
			{
				this._widgetID = value;
			}
		}
		
		private int _widgetDetailID;
		public virtual int Widget_detail_id
		{
			get
			{
				return this._widgetDetailID;
			}
			set
			{
				this._widgetDetailID = value;
			}
		}
		
		private bool _requiredFg;
		public virtual bool Required_fg
		{
			get
			{
				return this._requiredFg;
			}
			set
			{
				this._requiredFg = value;
			}
		}
		
		private bool _readOnlyFg;
		public virtual bool Read_only_fg
		{
			get
			{
				return this._readOnlyFg;
			}
			set
			{
				this._readOnlyFg = value;
			}
		}
		
		private string _fieldName;
		public virtual string Field_name
		{
			get
			{
				return this._fieldName;
			}
			set
			{
				this._fieldName = value;
			}
		}
		
		private string _fieldDescr;
		public virtual string Field_descr
		{
			get
			{
				return this._fieldDescr;
			}
			set
			{
				this._fieldDescr = value;
			}
		}
		
		private string _fieldDataType;
		public virtual string Field_data_type
		{
			get
			{
				return this._fieldDataType;
			}
			set
			{
				this._fieldDataType = value;
			}
		}
		
		private string _defaultValue;
		public virtual string Default_value
		{
			get
			{
				return this._defaultValue;
			}
			set
			{
				this._defaultValue = value;
			}
		}
		
		private bool _defaultFg;
		public virtual bool Default_fg
		{
			get
			{
				return this._defaultFg;
			}
			set
			{
				this._defaultFg = value;
			}
		}
		
		private string _field_Display_Name;
		public virtual string Field_Display_Name
		{
			get
			{
				return this._field_Display_Name;
			}
			set
			{
				this._field_Display_Name = value;
			}
		}
		
		private string _assemblyName;
		public virtual string AssemblyName
		{
			get
			{
				return this._assemblyName;
			}
			set
			{
				this._assemblyName = value;
			}
		}
		
		private string _className;
		public virtual string ClassName
		{
			get
			{
				return this._className;
			}
			set
			{
				this._className = value;
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
		
		private Widget _widget;
		public virtual Widget Widget
		{
			get
			{
				return this._widget;
			}
			set
			{
				this._widget = value;
			}
		}
		
	}
}
#pragma warning restore 1591
