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
using FormModel;

namespace FormModel	
{
	public partial class Form
	{
		private int _formId;
		public virtual int FormId
		{
			get
			{
				return this._formId;
			}
			set
			{
				this._formId = value;
			}
		}
		
		private string _formName;
		public virtual string FormName
		{
			get
			{
				return this._formName;
			}
			set
			{
				this._formName = value;
			}
		}
		
		private string _formDescription;
		public virtual string FormDescription
		{
			get
			{
				return this._formDescription;
			}
			set
			{
				this._formDescription = value;
			}
		}
		
		private bool _captchaFg;
		public virtual bool CaptchaFg
		{
			get
			{
				return this._captchaFg;
			}
			set
			{
				this._captchaFg = value;
			}
		}
		
		private bool _deletedFg;
		public virtual bool DeletedFg
		{
			get
			{
				return this._deletedFg;
			}
			set
			{
				this._deletedFg = value;
			}
		}
		
		private IList<FormEmail> _formEmails = new List<FormEmail>();
		public virtual IList<FormEmail> FormEmails
		{
			get
			{
				return this._formEmails;
			}
		}
		
	}
}
#pragma warning restore 1591
