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

namespace UserModel	
{
	public partial class Subscription
	{
		private string _userName;
		public virtual string UserName
		{
			get
			{
				return this._userName;
			}
			set
			{
				this._userName = value;
			}
		}
		
		private DateTime _subsDate;
		public virtual DateTime SubsDate
		{
			get
			{
				return this._subsDate;
			}
			set
			{
				this._subsDate = value;
			}
		}
		
		private bool _subscribe;
		public virtual bool Subscribe
		{
			get
			{
				return this._subscribe;
			}
			set
			{
				this._subscribe = value;
			}
		}
		
	}
}
#pragma warning restore 1591
