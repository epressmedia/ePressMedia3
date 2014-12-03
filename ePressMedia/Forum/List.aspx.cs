using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using log4net;
using EPM.Legacy.Security;


    public partial class Forum_Forums : EPM.Core.EPMBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //for logging to file
            try
            {
                int cat = int.Parse(Request.QueryString["p"]);

                if (!AccessControl.AuthorizeUser(Page.User.Identity.Name, ResourceType.Forum, cat, Permission.List))
                    System.Web.Security.FormsAuthentication.RedirectToLoginPage();


                LoadPageControls(cat, ContentTypes.Forum, UseForTypes.ListView);

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
        
    }
