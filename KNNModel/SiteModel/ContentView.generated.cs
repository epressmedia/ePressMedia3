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
	public partial class ContentView
	{
		private string _page;
		public virtual string Page
		{
			get
			{
				return this._page;
			}
			set
			{
				this._page = value;
			}
		}
		
		private bool? _enabled;
		public virtual bool? Enabled
		{
			get
			{
				return this._enabled;
			}
			set
			{
				this._enabled = value;
			}
		}
		
		private string _contentViewName;
		public virtual string ContentViewName
		{
			get
			{
				return this._contentViewName;
			}
			set
			{
				this._contentViewName = value;
			}
		}
		
		private int _contentViewId;
		public virtual int ContentViewId
		{
			get
			{
				return this._contentViewId;
			}
			set
			{
				this._contentViewId = value;
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
		
		private IList<SiteMenu> _siteMenus = new List<SiteMenu>();
		public virtual IList<SiteMenu> SiteMenus
		{
			get
			{
				return this._siteMenus;
			}
		}
		
	}
}
#pragma warning restore 1591
