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
	public partial class Widget
	{
		private string _widgetName;
		public virtual string Widget_name
		{
			get
			{
				return this._widgetName;
			}
			set
			{
				this._widgetName = value;
			}
		}
		
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
		
		private string _widgetDescr;
		public virtual string Widget_descr
		{
			get
			{
				return this._widgetDescr;
			}
			set
			{
				this._widgetDescr = value;
			}
		}
		
		private string _filePath;
		public virtual string File_path
		{
			get
			{
				return this._filePath;
			}
			set
			{
				this._filePath = value;
			}
		}
		
		private int _contentTypeId;
		public virtual int ContentTypeId
		{
			get
			{
				return this._contentTypeId;
			}
			set
			{
				this._contentTypeId = value;
			}
		}
		
		private DateTime _createdDt;
		public virtual DateTime created_dt
		{
			get
			{
				return this._createdDt;
			}
			set
			{
				this._createdDt = value;
			}
		}
		
		private int _createdBy;
		public virtual int created_by
		{
			get
			{
				return this._createdBy;
			}
			set
			{
				this._createdBy = value;
			}
		}
		
		private DateTime _modifiedDt;
		public virtual DateTime modified_dt
		{
			get
			{
				return this._modifiedDt;
			}
			set
			{
				this._modifiedDt = value;
			}
		}
		
		private int _modifiedBy;
		public virtual int modified_by
		{
			get
			{
				return this._modifiedBy;
			}
			set
			{
				this._modifiedBy = value;
			}
		}
		
		private bool _activeFg;
		public virtual bool active_fg
		{
			get
			{
				return this._activeFg;
			}
			set
			{
				this._activeFg = value;
			}
		}
		
		private int _widgetTypeId;
		public virtual int Widget_type_id
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
		
		private string _widgetData;
		public virtual string Widget_Data
		{
			get
			{
				return this._widgetData;
			}
			set
			{
				this._widgetData = value;
			}
		}
		
		private bool _frontEditable;
		public virtual bool FrontEditable
		{
			get
			{
				return this._frontEditable;
			}
			set
			{
				this._frontEditable = value;
			}
		}
		
		private ContentType _contentType;
		public virtual ContentType ContentType
		{
			get
			{
				return this._contentType;
			}
			set
			{
				this._contentType = value;
			}
		}
		
		private WidgetType _widgetType;
		public virtual WidgetType WidgetType
		{
			get
			{
				return this._widgetType;
			}
			set
			{
				this._widgetType = value;
			}
		}
		
		private IList<Widget_detail> _widgetDetails = new List<Widget_detail>();
		public virtual IList<Widget_detail> Widget_details
		{
			get
			{
				return this._widgetDetails;
			}
		}
		
	}
}
#pragma warning restore 1591