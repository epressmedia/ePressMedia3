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
using EmailModel;

namespace EmailModel	
{
	public partial class EmailAction
	{
		private int _emailActionId;
		public virtual int EmailActionId
		{
			get
			{
				return this._emailActionId;
			}
			set
			{
				this._emailActionId = value;
			}
		}
		
		private string _emailActionName;
		public virtual string EmailActionName
		{
			get
			{
				return this._emailActionName;
			}
			set
			{
				this._emailActionName = value;
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
		
		private int _emailEventId;
		public virtual int EmailEventId
		{
			get
			{
				return this._emailEventId;
			}
			set
			{
				this._emailEventId = value;
			}
		}
		
		private EmailEvent _emailEvent;
		public virtual EmailEvent EmailEvent
		{
			get
			{
				return this._emailEvent;
			}
			set
			{
				this._emailEvent = value;
			}
		}
		
	}
}
#pragma warning restore 1591