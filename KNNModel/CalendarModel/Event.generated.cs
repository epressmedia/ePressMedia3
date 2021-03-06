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
using CalendarModel;

namespace CalendarModel	
{
	public partial class Event
	{
		private int _viewCount;
		public virtual int ViewCount
		{
			get
			{
				return this._viewCount;
			}
			set
			{
				this._viewCount = value;
			}
		}
		
		private string _url;
		public virtual string Url
		{
			get
			{
				return this._url;
			}
			set
			{
				this._url = value;
			}
		}
		
		private string _title;
		public virtual string Title
		{
			get
			{
				return this._title;
			}
			set
			{
				this._title = value;
			}
		}
		
		private DateTime _startDate;
		public virtual DateTime StartDate
		{
			get
			{
				return this._startDate;
			}
			set
			{
				this._startDate = value;
			}
		}
		
		private DateTime _postDate;
		public virtual DateTime PostDate
		{
			get
			{
				return this._postDate;
			}
			set
			{
				this._postDate = value;
			}
		}
		
		private string _password;
		public virtual string Password
		{
			get
			{
				return this._password;
			}
			set
			{
				this._password = value;
			}
		}
		
		private string _ipAddr;
		public virtual string IpAddr
		{
			get
			{
				return this._ipAddr;
			}
			set
			{
				this._ipAddr = value;
			}
		}
		
		private string _imageUrl;
		public virtual string ImageUrl
		{
			get
			{
				return this._imageUrl;
			}
			set
			{
				this._imageUrl = value;
			}
		}
		
		private string _host;
		public virtual string Host
		{
			get
			{
				return this._host;
			}
			set
			{
				this._host = value;
			}
		}
		
		private string _heldAt;
		public virtual string HeldAt
		{
			get
			{
				return this._heldAt;
			}
			set
			{
				this._heldAt = value;
			}
		}
		
		private byte _eventMinute;
		public virtual byte EventMinute
		{
			get
			{
				return this._eventMinute;
			}
			set
			{
				this._eventMinute = value;
			}
		}
		
		private int _eventId;
		public virtual int EventId
		{
			get
			{
				return this._eventId;
			}
			set
			{
				this._eventId = value;
			}
		}
		
		private byte _eventHour;
		public virtual byte EventHour
		{
			get
			{
				return this._eventHour;
			}
			set
			{
				this._eventHour = value;
			}
		}
		
		private DateTime _endDate;
		public virtual DateTime EndDate
		{
			get
			{
				return this._endDate;
			}
			set
			{
				this._endDate = value;
			}
		}
		
		private string _descr;
		public virtual string Descr
		{
			get
			{
				return this._descr;
			}
			set
			{
				this._descr = value;
			}
		}
		
		private string _contact;
		public virtual string Contact
		{
			get
			{
				return this._contact;
			}
			set
			{
				this._contact = value;
			}
		}
		
		private int _calendar;
		public virtual int Calendar
		{
			get
			{
				return this._calendar;
			}
			set
			{
				this._calendar = value;
			}
		}
		
		private string _address;
		public virtual string Address
		{
			get
			{
				return this._address;
			}
			set
			{
				this._address = value;
			}
		}
		
		private Calendar _calendar1;
		public virtual Calendar Calendar1
		{
			get
			{
				return this._calendar1;
			}
			set
			{
				this._calendar1 = value;
			}
		}
		
	}
}
#pragma warning restore 1591
