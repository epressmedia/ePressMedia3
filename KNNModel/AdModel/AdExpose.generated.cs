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
using AdModel;

namespace AdModel	
{
	public partial class AdExpose
	{
		private int _adBannerId;
		public virtual int AdBannerId
		{
			get
			{
				return this._adBannerId;
			}
			set
			{
				this._adBannerId = value;
			}
		}
		
		private int _adZoneId;
		public virtual int AdZoneId
		{
			get
			{
				return this._adZoneId;
			}
			set
			{
				this._adZoneId = value;
			}
		}
		
		private DateTime _exposeDate;
		public virtual DateTime ExposeDate
		{
			get
			{
				return this._exposeDate;
			}
			set
			{
				this._exposeDate = value;
			}
		}
		
		private string _pageURL;
		public virtual string PageURL
		{
			get
			{
				return this._pageURL;
			}
			set
			{
				this._pageURL = value;
			}
		}
		
		private int _adExposeId;
		public virtual int AdExposeId
		{
			get
			{
				return this._adExposeId;
			}
			set
			{
				this._adExposeId = value;
			}
		}
		
		private Char _exposeType;
		public virtual Char ExposeType
		{
			get
			{
				return this._exposeType;
			}
			set
			{
				this._exposeType = value;
			}
		}
		
		private AdZone _adZone;
		public virtual AdZone AdZone
		{
			get
			{
				return this._adZone;
			}
			set
			{
				this._adZone = value;
			}
		}
		
		private AdBanner _adBanner;
		public virtual AdBanner AdBanner
		{
			get
			{
				return this._adBanner;
			}
			set
			{
				this._adBanner = value;
			}
		}
		
	}
}
#pragma warning restore 1591
