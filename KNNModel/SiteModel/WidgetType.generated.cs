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
	public partial class WidgetType
	{
		private int _widgetTypeId;
		public virtual int Widget_Type_Id
		{
			get
			{
				return this._widgetTypeId;
			}
			set
			{
				this._widgetTypeId = value;
			}
		}
		
		private string _widgetTypeDescr;
		public virtual string Widget_Type_Descr
		{
			get
			{
				return this._widgetTypeDescr;
			}
			set
			{
				this._widgetTypeDescr = value;
			}
		}
		
		private string _widgetTypeName;
		public virtual string Widget_Type_Name
		{
			get
			{
				return this._widgetTypeName;
			}
			set
			{
				this._widgetTypeName = value;
			}
		}
		
		private IList<Widget> _widgets = new List<Widget>();
		public virtual IList<Widget> Widgets
		{
			get
			{
				return this._widgets;
			}
		}
		
	}
}
#pragma warning restore 1591
