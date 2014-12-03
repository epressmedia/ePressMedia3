using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using log4net;

namespace ePressMedia
{
    public partial class Global:System.Web.HttpApplication
    {
        

        void Application_Start(object sender, EventArgs e)
        {
           this.ConfigureWebApi();
           //RegisterRoutes(RouteTable.Routes);
            // Code that runs on application startup
           //log4net.Config.XmlConfigurator.Configure();

   
            
        }
        void Application_BeginRequest(object sender, EventArgs e)
        {

            string fullOrigionalpath = Request.Url.ToString();

            if (fullOrigionalpath.ToLower().Contains("/Page/".ToLower()))
            {
                string filename = fullOrigionalpath.Substring(fullOrigionalpath.LastIndexOf('/') + 1).ToLower();
                int queryindex = filename.IndexOf("?");
                string querystring = "";
                if (queryindex > 0)
                {
                    querystring = filename.Substring(queryindex + 1, filename.Length - queryindex - 1);
                    filename = filename.Replace("?" + querystring, "");
                }




                EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
                if (context.CustomPages.Where(c => c.Name == filename.Replace(".aspx", "") && c.DeletedFg == false).Count() > 0)
                {
                    string aa = querystring;
                    //Context.Server.Transfer(HttpUtility.UrlPathEncode("/page/page.aspx?pg=" + context.CustomPages.Where(c => c.Name == filename.Replace(".aspx", "")).ToList()[0].CustomPageId.ToString() + ((querystring.Length > 0) ? "&" + querystring : "")));
                    string internal_url = HttpUtility.UrlPathEncode("/page/page.aspx?pg=" + context.CustomPages.Where(c =>c.DeletedFg == false && c.Name == filename.Replace(".aspx", "")).ToList()[0].CustomPageId.ToString() + ((querystring.Length > 0) ? "&" + querystring : ""));
                    if (System.Web.HttpRuntime.UsingIntegratedPipeline)
                        Server.TransferRequest(internal_url);
                    else
                        Context.RewritePath(internal_url);
                }
            }

        }

        //public static void RegisterRoutes(RouteCollection routes)
        //{
        //    routes.MapPageRoute("ArticleCategory", "Article/{category}", "~/Article/List.aspx");
        //    routes.MapPageRoute("Article", "Article/{category}/{title}", "~/Article/View.aspx");
        //}

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }
        private static readonly ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Application Error handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Application_Error(object sender, EventArgs e)
        {
            HttpContext context = ((HttpApplication)sender).Context;
            Exception ex = context.Server.GetLastError();

            if (ex == null || !(ex is HttpException) || (ex as HttpException).GetHttpCode() == 404)
            {
                return;
            }
            var sb = new System.Text.StringBuilder();

            try
            {
                sb.AppendLine("Url : " + context.Request.Url);
                sb.AppendLine("Raw Url : " + context.Request.RawUrl);

                while (ex != null)
                {
                    sb.AppendLine("Message : " + ex.Message);
                    sb.AppendLine("Source : " + ex.Source);
                    sb.AppendLine("StackTrace : " + ex.StackTrace);
                    sb.AppendLine("TargetSite : " + ex.TargetSite);
                    ex = ex.InnerException;
                }
            }
            catch (Exception ex2)
            {
                sb.AppendLine("Error logging error : " + ex2.Message);
            }

            //if (BlogSettings.Instance.EnableErrorLogging)
            //{
            //    Utils.Log(sb.ToString());
            //}



            logger.Error(sb.ToString());



            context.Items["LastErrorDetails"] = sb.ToString();
            context.Response.StatusCode = 500;

            // Custom errors section defined in the Web.config, will rewrite (not redirect)
            // this 500 error request to error.aspx.
            //OR
            //HttpContext.Current.Server.ClearError();
            //Response.Redirect("/");
        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }


    
    }
}