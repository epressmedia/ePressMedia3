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

namespace EPM.Data.Model	
{
	public partial class EPM_vw_user_info
	{
		private bool _status;
		public virtual bool Status
		{
			get
			{
				return this._status;
			}
			set
			{
				this._status = value;
			}
		}
		
		private string _loginName;
		public virtual string LoginName
		{
			get
			{
				return this._loginName;
			}
			set
			{
				this._loginName = value;
			}
		}
		
		private DateTime _lastLoginDate;
		public virtual DateTime LastLoginDate
		{
			get
			{
				return this._lastLoginDate;
			}
			set
			{
				this._lastLoginDate = value;
			}
		}
		
		private bool _isValidated;
		public virtual bool IsValidated
		{
			get
			{
				return this._isValidated;
			}
			set
			{
				this._isValidated = value;
			}
		}
		
		private bool _isLockedOut;
		public virtual bool IsLockedOut
		{
			get
			{
				return this._isLockedOut;
			}
			set
			{
				this._isLockedOut = value;
			}
		}
		
		private string _fullName;
		public virtual string FullName
		{
			get
			{
				return this._fullName;
			}
			set
			{
				this._fullName = value;
			}
		}
		
		private string _email;
		public virtual string Email
		{
			get
			{
				return this._email;
			}
			set
			{
				this._email = value;
			}
		}
		
		private DateTime _createDate;
		public virtual DateTime CreateDate
		{
			get
			{
				return this._createDate;
			}
			set
			{
				this._createDate = value;
			}
		}
		
		private Guid _userId;
		public virtual Guid UserId
		{
			get
			{
				return this._userId;
			}
			set
			{
				this._userId = value;
			}
		}
		
	}
}
#pragma warning restore 1591
