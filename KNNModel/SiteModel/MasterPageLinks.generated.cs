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
	public partial class MasterPageLinks
	{
		private int _masterPageLinkId;
		public virtual int MasterPageLinkId
		{
			get
			{
				return this._masterPageLinkId;
			}
			set
			{
				this._masterPageLinkId = value;
			}
		}
		
		private int _masterPageId;
		public virtual int MasterPageId
		{
			get
			{
				return this._masterPageId;
			}
			set
			{
				this._masterPageId = value;
			}
		}
		
		private string _pagePath;
		public virtual string PagePath
		{
			get
			{
				return this._pagePath;
			}
			set
			{
				this._pagePath = value;
			}
		}
		
		private MasterPageConfig _masterPageConfig;
		public virtual MasterPageConfig MasterPageConfig
		{
			get
			{
				return this._masterPageConfig;
			}
			set
			{
				this._masterPageConfig = value;
			}
		}
		
	}
}
#pragma warning restore 1591
