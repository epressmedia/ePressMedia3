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
	public partial class MasterPageConfig
	{
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
		
		private string _name;
		public virtual string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
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
		
		private string _imageUrl;
		public virtual string ImageUrl
		{
			get
			{
				return this._imageUrl;
			}
			set
			{
				this._imageUrl = value;
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
		
		private bool _deletedFg;
		public virtual bool DeletedFg
		{
			get
			{
				return this._deletedFg;
			}
			set
			{
				this._deletedFg = value;
			}
		}
		
		private string _masterPagePath;
		public virtual string MasterPagePath
		{
			get
			{
				return this._masterPagePath;
			}
			set
			{
				this._masterPagePath = value;
			}
		}
		
		private IList<MasterPageLinks> _masterPageLinks = new List<MasterPageLinks>();
		public virtual IList<MasterPageLinks> MasterPageLinks
		{
			get
			{
				return this._masterPageLinks;
			}
		}
		
	}
}
#pragma warning restore 1591
