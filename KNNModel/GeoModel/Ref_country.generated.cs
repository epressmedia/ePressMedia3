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
using GeoModel;

namespace GeoModel	
{
	public partial class Ref_country
	{
		private string _country_Name;
		public virtual string Country_name
		{
			get
			{
				return this._country_Name;
			}
			set
			{
				this._country_Name = value;
			}
		}
		
		private string _country_Cd;
		public virtual string Country_cd
		{
			get
			{
				return this._country_Cd;
			}
			set
			{
				this._country_Cd = value;
			}
		}
		
		private IList<Ref_province> _ref_Provinces = new List<Ref_province>();
		public virtual IList<Ref_province> Ref_provinces
		{
			get
			{
				return this._ref_Provinces;
			}
		}
		
	}
}
#pragma warning restore 1591
