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
using UDFModel;

namespace UDFModel	
{
	public partial class UDFInfo
	{
		private int _uDFID;
		public virtual int UDFID
		{
			get
			{
				return this._uDFID;
			}
			set
			{
				this._uDFID = value;
			}
		}
		
		private int _dataTypeID;
		public virtual int DataTypeID
		{
			get
			{
				return this._dataTypeID;
			}
			set
			{
				this._dataTypeID = value;
			}
		}
		
		private string _uDFName;
		public virtual string UDFName
		{
			get
			{
				return this._uDFName;
			}
			set
			{
				this._uDFName = value;
			}
		}
		
		private string _uDFDescription;
		public virtual string UDFDescription
		{
			get
			{
				return this._uDFDescription;
			}
			set
			{
				this._uDFDescription = value;
			}
		}
		
		private bool _referenceFg;
		public virtual bool ReferenceFg
		{
			get
			{
				return this._referenceFg;
			}
			set
			{
				this._referenceFg = value;
			}
		}
		
		private Guid _createdBy;
		public virtual Guid CreatedBy
		{
			get
			{
				return this._createdBy;
			}
			set
			{
				this._createdBy = value;
			}
		}
		
		private DateTime _createdDate;
		public virtual DateTime CreatedDate
		{
			get
			{
				return this._createdDate;
			}
			set
			{
				this._createdDate = value;
			}
		}
		
		private DateTime? _modifiedDate;
		public virtual DateTime? ModifiedDate
		{
			get
			{
				return this._modifiedDate;
			}
			set
			{
				this._modifiedDate = value;
			}
		}
		
		private string _prefixLabel;
		public virtual string PrefixLabel
		{
			get
			{
				return this._prefixLabel;
			}
			set
			{
				this._prefixLabel = value;
			}
		}
		
		private string _postfixLabel;
		public virtual string PostfixLabel
		{
			get
			{
				return this._postfixLabel;
			}
			set
			{
				this._postfixLabel = value;
			}
		}
		
		private Guid? _modifiedBy;
		public virtual Guid? ModifiedBy
		{
			get
			{
				return this._modifiedBy;
			}
			set
			{
				this._modifiedBy = value;
			}
		}
		
		private string _label;
		public virtual string Label
		{
			get
			{
				return this._label;
			}
			set
			{
				this._label = value;
			}
		}
		
		private UDFDataType _uDFDataType;
		public virtual UDFDataType UDFDataType
		{
			get
			{
				return this._uDFDataType;
			}
			set
			{
				this._uDFDataType = value;
			}
		}
		
		private IList<UDFReference> _uDFReferences = new List<UDFReference>();
		public virtual IList<UDFReference> UDFReferences
		{
			get
			{
				return this._uDFReferences;
			}
		}
		
		private IList<UDFValue> _uDFValues = new List<UDFValue>();
		public virtual IList<UDFValue> UDFValues
		{
			get
			{
				return this._uDFValues;
			}
		}
		
		private IList<UDFAssignment> _uDFAssignments = new List<UDFAssignment>();
		public virtual IList<UDFAssignment> UDFAssignments
		{
			get
			{
				return this._uDFAssignments;
			}
		}
		
	}
}
#pragma warning restore 1591
