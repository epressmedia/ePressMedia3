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
	public partial class AdZoneActionType
	{
		private int _adZoneActionTypeId;
		public virtual int AdZoneActionTypeId
		{
			get
			{
				return this._adZoneActionTypeId;
			}
			set
			{
				this._adZoneActionTypeId = value;
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
		
		private bool _allowRotation;
		public virtual bool AllowRotation
		{
			get
			{
				return this._allowRotation;
			}
			set
			{
				this._allowRotation = value;
			}
		}
		
		private bool _allowMultiBanners;
		public virtual bool AllowMultiBanners
		{
			get
			{
				return this._allowMultiBanners;
			}
			set
			{
				this._allowMultiBanners = value;
			}
		}
		
		private bool _listBannerFg;
		public virtual bool ListBannerFg
		{
			get
			{
				return this._listBannerFg;
			}
			set
			{
				this._listBannerFg = value;
			}
		}
		
		private bool _applyWeightFg;
		public virtual bool ApplyWeightFg
		{
			get
			{
				return this._applyWeightFg;
			}
			set
			{
				this._applyWeightFg = value;
			}
		}
		
		private IList<AdZone> _adZones = new List<AdZone>();
		public virtual IList<AdZone> AdZones
		{
			get
			{
				return this._adZones;
			}
		}
		
	}
}
#pragma warning restore 1591