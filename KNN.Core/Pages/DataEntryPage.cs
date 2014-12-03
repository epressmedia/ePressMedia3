using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using EPM.Legacy.Security;

namespace EPM.Core.Pages
{
    public  class DataEntryPage : System.Web.UI.UserControl
    {
        private static EntryViewMode viewModes;
        public static EntryViewMode ViewMode
        {
            get { return viewModes; }
            set { viewModes = value; }
        }

        private static int contentID = -1;
        public static int ContentID
        {
            get { return contentID; }
            set { contentID = value; }
        }

        private static int categoryID = -1;
        public static int CategoryID
        {
            get { return categoryID; }
            set { categoryID = value; }
        }

        public static string ReturnURL
        {
            get;
            set;
        }

        public static string Module
        { get; set; }

        public static string Message
        { get; set; }


        public enum EntryViewMode
        {
            Add,
            Edit,
            View,
            Delete
        }

        bool passwordinput = false;
        public bool PasswordInput
        {
            get { return passwordinput; }
            set { passwordinput = value; }
        }


        public AccessControl GetAccessControl(ResourceType resourceType)
        {
            return  AccessControl.SelectAccessControlByUserName(
                Page.User.Identity.Name, resourceType, CategoryID);
        }

        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            EPM.Core.Common.StyleSheetLoader.LoadStyleSheets();

            if (Request.QueryString["msg"] != null)
                Message = Request.QueryString["msg"].ToString();
            if (Request.QueryString["passwordinput"] != null)
                PasswordInput = bool.Parse(Request.QueryString["passwordinput"].ToString());

            if (Request.QueryString["area"] != null)
                Module = Request.QueryString["area"].ToString().ToLower();

            if (Request.QueryString["Mode"] == null)
                ViewMode = EntryViewMode.View;
            else
            {
                switch (Request.QueryString["Mode"].ToLower())
                {
                    case "add":
                        ViewMode = EntryViewMode.Add;
                        break;
                    case "edit":
                        ViewMode = EntryViewMode.Edit;
                        break;
                    case "delete":
                        ViewMode = EntryViewMode.Delete;
                        break;
                    default:
                        ViewMode = EntryViewMode.View;
                        break;
                }
            }

            if (Request.QueryString["returnURL"] != null)
                ReturnURL = Request.QueryString["returnURL"].ToString();

            if ((ViewMode == EntryViewMode.Edit || ViewMode == EntryViewMode.Delete) && (Request.QueryString["cid"] != null))
            {
                ContentID = int.Parse(Request.QueryString["cid"]);
            }


            if (Request.QueryString["p"] != null)
                CategoryID = int.Parse(Request.QueryString["p"]);



            
        }
    }
}
