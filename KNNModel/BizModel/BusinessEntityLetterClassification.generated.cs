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
using BizModel;

namespace BizModel	
{
	public partial class BusinessEntityLetterClassification
	{
		private int _classificationId;
		public virtual int ClassificationId
		{
			get
			{
				return this._classificationId;
			}
			set
			{
				this._classificationId = value;
			}
		}
		
		private string _classificationLetter;
		public virtual string ClassificationLetter
		{
			get
			{
				return this._classificationLetter;
			}
			set
			{
				this._classificationLetter = value;
			}
		}
		
		private bool _activeFg;
		public virtual bool ActiveFg
		{
			get
			{
				return this._activeFg;
			}
			set
			{
				this._activeFg = value;
			}
		}
		
		private IList<BusinessCategory> _businessCategories = new List<BusinessCategory>();
		public virtual IList<BusinessCategory> BusinessCategories
		{
			get
			{
				return this._businessCategories;
			}
		}
		
	}
}
#pragma warning restore 1591
