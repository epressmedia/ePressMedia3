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
	public partial class Application
	{
		private string _loweredApplicationName;
		public virtual string LoweredApplicationName
		{
			get
			{
				return this._loweredApplicationName;
			}
			set
			{
				this._loweredApplicationName = value;
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
		
		private string _applicationName;
		public virtual string ApplicationName
		{
			get
			{
				return this._applicationName;
			}
			set
			{
				this._applicationName = value;
			}
		}
		
		private Guid _applicationId;
		public virtual Guid ApplicationId
		{
			get
			{
				return this._applicationId;
			}
			set
			{
				this._applicationId = value;
			}
		}
		
		private IList<Users> _aspnet_Users = new List<Users>();
		public virtual IList<Users> Aspnet_Users
		{
			get
			{
				return this._aspnet_Users;
			}
		}
		
		private IList<UserRoles> _aspnet_Roles = new List<UserRoles>();
		public virtual IList<UserRoles> Aspnet_Roles
		{
			get
			{
				return this._aspnet_Roles;
			}
		}
		
		private IList<UserMembership> _aspnet_Memberships = new List<UserMembership>();
		public virtual IList<UserMembership> Aspnet_Memberships
		{
			get
			{
				return this._aspnet_Memberships;
			}
		}
		
	}
}
#pragma warning restore 1591
