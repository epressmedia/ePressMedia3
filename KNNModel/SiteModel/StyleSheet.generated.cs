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

namespace SiteModel	
{
	public partial class StyleSheet
	{
		private int _styleSheetId;
		public virtual int StyleSheetId
		{
			get
			{
				return this._styleSheetId;
			}
			set
			{
				this._styleSheetId = value;
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
		
		private bool _enabled;
		public virtual bool Enabled
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
		
		private bool _systemFg;
		public virtual bool SystemFg
		{
			get
			{
				return this._systemFg;
			}
			set
			{
				this._systemFg = value;
			}
		}
		
		private int _sequenceNo;
		public virtual int SequenceNo
		{
			get
			{
				return this._sequenceNo;
			}
			set
			{
				this._sequenceNo = value;
			}
		}
		
	}
}
#pragma warning restore 1591
