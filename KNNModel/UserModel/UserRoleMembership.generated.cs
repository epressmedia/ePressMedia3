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
using UserModel;

namespace UserModel	
{
	public partial class UserRoleMembership
	{
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
		
		private Guid _roleId;
		public virtual Guid RoleId
		{
			get
			{
				return this._roleId;
			}
			set
			{
				this._roleId = value;
			}
		}
		
		private Users _users;
		public virtual Users Users
		{
			get
			{
				return this._users;
			}
			set
			{
				this._users = value;
			}
		}
		
		private UserRoles _userRoles;
		public virtual UserRoles UserRoles
		{
			get
			{
				return this._userRoles;
			}
			set
			{
				this._userRoles = value;
			}
		}
		
	}
}
#pragma warning restore 1591
