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
using ClassifiedModel;

namespace ClassifiedModel	
{
	public partial class ClassifiedTag
	{
		private int _tagId;
		public virtual int TagId
		{
			get
			{
				return this._tagId;
			}
			set
			{
				this._tagId = value;
			}
		}
		
		private int _classifiedCatId;
		public virtual int ClassifiedCatId
		{
			get
			{
				return this._classifiedCatId;
			}
			set
			{
				this._classifiedCatId = value;
			}
		}
		
		private string _tagName;
		public virtual string TagName
		{
			get
			{
				return this._tagName;
			}
			set
			{
				this._tagName = value;
			}
		}
		
		private ClassifiedCategory _classifiedCategory;
		public virtual ClassifiedCategory ClassifiedCategory
		{
			get
			{
				return this._classifiedCategory;
			}
			set
			{
				this._classifiedCategory = value;
			}
		}
		
	}
}
#pragma warning restore 1591