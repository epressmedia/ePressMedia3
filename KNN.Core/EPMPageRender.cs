using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using log4net;
using System.Reflection;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Telerik.Web.UI;
using System.Linq.Expressions;

namespace EPM.Core
{
    public static class EPMPageRender
    {
        
        private static readonly ILog Logger = log4net.LogManager.GetLogger(typeof(EPMPageRender));
        
        public static void PageRender()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml_setting"></param>
        /// <param name="page"></param>
        /// <param name="preview"></param>
        /// <param name="adminMode"></param>
        /// <param name="master"></param>
        public static void PageRender(string xml_setting, bool preview, bool adminMode = false, bool master = false)
        {
            Page page = System.Web.HttpContext.Current.Handler as Page;

            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            // check if the xml setting is set
            if (!string.IsNullOrEmpty(xml_setting))
            {

                // Load settings in xml format
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(xml_setting);

                XDocument xdoc = XDocument.Parse(xml_setting);

                //Get list of contents
                XmlNodeList contents = xml.SelectNodes("/PageRoot/Contents/Content");

                // Loop Through each content in contents xmlnodelist
                if (master)
                {
                    if (System.Web.HttpContext.Current.Session["MasterName"] != null)
                    {
                        if (page.Master.FindControl("contentWrap") != null)
                        {
                            HtmlControl contentWrap = ((HtmlControl)page.Master.FindControl("contentWrap"));
                            string current_class = contentWrap.Attributes["class"];
                            string new_class = System.Web.HttpContext.Current.Session["MasterName"].ToString().Replace(" ", "_");
                            
                            contentWrap.Attributes["class"] = current_class + " " + new_class;

                            //Label aa = new Label();
                            //aa.ID = "myID";
                            //aa.Text = "class=" + new_class + " / master file" + (xml.SelectSingleNode("/PageRoot/Configs/MasterPage")).Attributes["Value"].Value;
                            //contentWrap.Controls.Add(aa);
                            
                             
                        }
                    }
                }


                foreach (XmlNode content in contents)
                {
                    string cont_name = content.Attributes["ID"].Value.ToString();

                    // find content in Master


                    Control placeholderControl = page.Master.FindControl(cont_name);
                    

                    if (placeholderControl != null)
                    {
                        var selectedContent = from c in xdoc.Descendants("Content")
                                              where (string)c.Attribute("ID") == cont_name
                                              select c;

                        IEnumerable<XElement> controls = from o in selectedContent.Elements()
                                                         orderby (int)o.Attribute("Seq")
                                                         select o;


                        // get controls in each content by seq
                        foreach (XElement control in controls)
                        {
                            XmlDocument control_node = XmlHelper.GetXmlNode(control);
                            //control_node.LoadXml(control_node.InnerXml.ToString());
                            //control_node.DocumentElement.Attributes

                            string control_id = control_node.DocumentElement.Attributes["ID"].Value.ToString();
                            string control_name = control_node.DocumentElement.Attributes["Name"].Value.ToString();
                            string control_src = control_node.DocumentElement.Attributes["src"].Value.ToString();
                            string control_seq = control_node.DocumentElement.Attributes["Seq"].Value.ToString();
                            int control_type = int.Parse(control_node.DocumentElement.Attributes["Type"].Value.ToString());

                            SiteModel.WidgetType widget_Type = (from c in context.WidgetTypes
                                                                where c.Widget_Type_Id == control_type
                                                                select c).Single();


                            if (widget_Type.Widget_Type_Name.ToLower() == "ascx")
                            {

                                LoadASCX(control, placeholderControl, page, control_node, control_id, control_src, cont_name, control_seq, control_name, preview, adminMode, master);

                            }

                            else if (widget_Type.Widget_Type_Name.ToLower() == "image")
                            {
                                LoadImage(placeholderControl, control_node, control_id, control_src, cont_name, control_seq, control_name, preview, adminMode,master);

                            }
                            else if (widget_Type.Widget_Type_Name.ToLower() == "html")
                            {
                               LoadHTMLContent(page, placeholderControl,control_node, cont_name,control_id, control_name, control_seq, control_src, preview, adminMode,master);
                            }
                            else if (widget_Type.Widget_Type_Name.ToLower() == "banner ad")
                            {
                                LoadAdZone(placeholderControl, control_node, control_id, control_src, cont_name, control_seq, control_name, preview, adminMode, master);
                            }



                            
                            //Logger.Debug("Loaded: [" + control_id.ToString() + "] " + control_name);

                        }

                    }
               } 
                

            }
         
        }


        public static void LoadASCX(XElement control, Control placeholderControl, Page page, XmlDocument control_node, string control_id, string control_src, string cont_name, string control_seq, string control_name, bool preview, bool adminMode, bool master)
        {
            // select param nodes
            var parameters = from p in control.Elements("Param")
                             select p;

            // Loop through the param nodes if exits and enter them to IDictionary<>
            IDictionary<string, string> param_list_dic = new Dictionary<string, string>();
            if (parameters.Count() > 0)
            {
                foreach (XElement parameter in parameters)
                {
                    XmlDocument param_node = XmlHelper.GetXmlNode(parameter);

                    param_list_dic.Add(param_node.DocumentElement.Attributes["Name"].Value.ToString(), param_node.DocumentElement.Attributes["Value"].Value.ToString());
                }
            }

            // Now load the control and its properties
            UserControl uc = (UserControl)(page.LoadControl(control_src));
            Logger.Debug("control_src:" + control_src);
            Type type = uc.GetType();
            System.Reflection.PropertyInfo[] properties = type.GetProperties();


            foreach (PropertyInfo property in properties)
            {
                CategoryAttribute attribute = Attribute.GetCustomAttribute(property, typeof(CategoryAttribute)) as CategoryAttribute;
                 DataTypeAttribute dataTypeAttribute = Attribute.GetCustomAttribute(property, typeof(DataTypeAttribute)) as DataTypeAttribute;

                if (attribute != null)
                {
                    if (attribute.Category.ToString() == "EPMProperty") // This property has a StoredDataValueAttribute
                    {

                        // Do only when the property is writable
                        if (property.CanWrite && (param_list_dic.Count() > 0))
                        {
                            foreach (KeyValuePair<string, string> param in param_list_dic)
                            {
                                string param_name = param.Key.ToString().Trim();
                                if (property.Name == param_name)
                                {
                                    string param_value = param.Value.ToString().Trim();
                                    if (param_value.Length > 0)
                                    {
                                        if ((dataTypeAttribute != null) && (dataTypeAttribute.CustomDataType == "ThumbnailType"))
                                        {
                                            Dictionary<string, bool> mydic = new Dictionary<string, bool>();
                                            mydic.Add(param_value, true);
                                            property.SetValue(uc, Convert.ChangeType(mydic, property.PropertyType), null);

                                        }
                                        else
                                        {
                                            if (property.PropertyType.IsEnum)
                                            {
                                                property.SetValue(uc, System.Enum.Parse(property.PropertyType, param_value), null);
                                            }
                                            else
                                            {

                                                property.SetValue(uc, Convert.ChangeType(param_value, property.PropertyType), null);
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }

                }
            }
            //Make ID unique

            uc.ID = (master) ? "Master_" + control_id : "" + control_id;// control_node.DocumentElement.Attributes["ID"].Value.ToString().Replace(".ascx","");
            if (control_node.DocumentElement.Attributes["C_Style"] == null)
            {
                placeholderControl.Controls.Add(uc);
            }
            else
            {
                string beginning_div = "";
                string ending_div = "";
                string containerStyle = control_node.DocumentElement.Attributes["C_Style"].Value.ToString();
                string cssclass = "";
                if (control_node.DocumentElement.Attributes["Class"] != null)
                    cssclass = control_node.DocumentElement.Attributes["Class"].Value.ToString().Trim();

                if (preview)
                {
                    beginning_div = "<div class='Preview_Op' ref='blue'" + ((preview) ? "style=\"" + containerStyle + "\"" : "") + " >" +
                        "<div class='Preview_Label' >" + cont_name + "(" + control_seq + ") - " + control_name + "</div>";
                }
                else if (adminMode)
                    beginning_div = "<div class='Admin_Op'>";

                beginning_div = beginning_div + "<div id=\"" + control_id + "_Container\"" + ((preview) ? " class=\""+cssclass+" PreviewContainer\" " : "class=\"" + cssclass+"\"") + " " + ((preview) ? "" : "style=\"" + containerStyle + "\"") + ">";
                placeholderControl.Controls.Add(new LiteralControl(beginning_div));

                if ((adminMode) && (!preview)&&(!master))
                {
                    //    placeholder.Controls.Add(new LiteralControl("<div class='Admin_Op'>"));

                    Telerik.Web.UI.RadAjaxPanel ajax_control = new Telerik.Web.UI.RadAjaxPanel();
                    string ajax_unique_name = placeholderControl.ID.ToString() +"_"+ control_id.ToString();
                    ajax_control.ID = "editButton" + "_" + ajax_unique_name;// control_name.ToString() + "_" + control_seq.ToString();


                    Button editButton = new Button();
                    editButton.ID = "btn_editButton_" + ajax_unique_name;// control_name.ToString() + "_" + control_seq.ToString();
                    editButton.Text = "";
                    editButton.CssClass = "ASCXFrontEditButton FrontEditButton";
                    editButton.Attributes["ControlTypeFor"] = "ascx";

                    editButton.OnClientClick = "editControl('" + control_id.ToString() + "','" + "ascx" + "','" + placeholderControl.ID + "');";
                    ajax_control.Controls.Add(editButton);
                    placeholderControl.Controls.Add(ajax_control);

                }

                placeholderControl.Controls.Add(uc);



                if (preview)
                {
                    ending_div = "</div></div>";

                }
                else if (adminMode)
                    ending_div = "</div>";

                ending_div = ending_div + "</div>";

                placeholderControl.Controls.Add(new LiteralControl(ending_div));

            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="placeholderControl"></param>
        /// <param name="control_node"></param>
        /// <param name="control_id"></param>
        /// <param name="control_src"></param>
        /// <param name="cont_name"></param>
        /// <param name="control_seq"></param>
        /// <param name="control_name"></param>
        /// <param name="preview"></param>
        /// <param name="adminMode"></param>
        /// <param name="master"></param>
        public static void LoadImage(Control placeholderControl, XmlDocument control_node, string control_id, string control_src, string cont_name, string control_seq, string control_name, bool preview, bool adminMode, bool master)
        {

            string imageStyle = control_node.DocumentElement.Attributes["Style"].Value.ToString();
            string containerStyle = control_node.DocumentElement.Attributes["C_Style"].Value.ToString();
            string cssclass = "";
            string href = "";
            string target = "";
            if (control_node.DocumentElement.Attributes["class"] != null)
                cssclass = control_node.DocumentElement.Attributes["class"].Value.ToString();
            if (control_node.DocumentElement.Attributes["href"] != null)
                href = control_node.DocumentElement.Attributes["href"].Value.ToString().Trim();
            if (control_node.DocumentElement.Attributes["target"] != null)
                target = control_node.DocumentElement.Attributes["target"].Value.ToString().Trim();

            Image image = new Image();
            image.ID = control_id;
            image.ImageUrl = control_src;
            image.Attributes["Style"] = imageStyle;
            

            string beginning_div = "";
            string ending_div = "";

            if (preview)
            {
                beginning_div = "<div class='Preview_Op' ref='blue'" + ((preview) ? "style=\"" + containerStyle + "\"" : "") + " ><div class='Preview_Label'>" + cont_name + "(" + control_seq + ") - " + control_name + "</div>";
            }
            else if (adminMode)
                beginning_div = "<div class='Admin_Op' >";

            beginning_div = beginning_div+"<div id=\"" + ((master) ? "Master_" : "") + control_id + "_Container\"" + ((preview) ? " class=\"" + cssclass + " PreviewContainer\" " : "class=\"" + cssclass + "\"") + " " + ((preview) ? "" : "style=\"" + containerStyle + "\"") + " >";
            if (!string.IsNullOrEmpty(href))
                beginning_div = beginning_div + "<a href=\"" + href + "\" target=\"" + (string.IsNullOrEmpty(target) ? "_blank" : target) + "\">";
            placeholderControl.Controls.Add(new LiteralControl(beginning_div));


            if ((adminMode) && (!preview) &&(!master))
            {
                //    placeholder.Controls.Add(new LiteralControl("<div class='Admin_Op'>"));

                Telerik.Web.UI.RadAjaxPanel ajax_control = new Telerik.Web.UI.RadAjaxPanel();
                string ajax_unique_name = placeholderControl.ID.ToString() + "_" + control_id.ToString();
                ajax_control.ID = "editButton_" + ajax_unique_name;


                Button editButton = new Button();
                editButton.ID = "btn_editButton_" + ajax_unique_name;
                editButton.Text = "";
                editButton.CssClass = "ImageFrontEditButton FrontEditButton";
                    editButton.Attributes["ControlTypeFor"] = "image";

                    editButton.OnClientClick = "editControl('" + control_id.ToString() + "','" + "image" + "','" + placeholderControl.ID + "');";
                ajax_control.Controls.Add(editButton);
                placeholderControl.Controls.Add(ajax_control);

            }

            placeholderControl.Controls.Add(image);

            if (!string.IsNullOrEmpty(href))
                ending_div = ending_div + "</a>";
            if (preview)
            {
                ending_div = ending_div + "</div></div>";

            }
            else if (adminMode)
                ending_div = ending_div + "</div>";

            ending_div = ending_div + "</div>";

            placeholderControl.Controls.Add(new LiteralControl(ending_div));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="placeholder"></param>
        /// <param name="cont_name"></param>
        /// <param name="control_id"></param>
        /// <param name="control_name"></param>
        /// <param name="control_seq"></param>
        /// <param name="control_src"></param>
        /// <param name="preview"></param>
        /// <param name="adminMode"></param>
        public static void LoadHTMLContent(Page page, Control placeholder, XmlDocument control_node, string cont_name,string control_id,string control_name, string control_seq, string control_src, bool preview, bool adminMode,bool master)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();

            string cssclass = "";
            string style = "";
            if (control_node.DocumentElement.Attributes["C_Style"] != null)
                style = control_node.DocumentElement.Attributes["C_Style"].Value.ToString();
            if (control_node.DocumentElement.Attributes["Class"] != null)
                cssclass = control_node.DocumentElement.Attributes["Class"].Value.ToString().Trim();


            SiteModel.Widget widget = context.Widgets.Single(c => c.Widget_id == int.Parse(control_src));
            string html_text = widget.Widget_Data.ToString();
            bool showEdit = widget.FrontEditable;

                 string beginning_div = "";
                 string ending_div = "";




                                if (preview)
                                     beginning_div = @"<div class='Preview_Op' ref='blue'>
                            <div class='Preview_Label' >" + cont_name + "(" + control_seq + ") - " + control_name + "</div><div id=\"" + control_id + "_Container\" class=\"" + cssclass + "" + ((preview) ? " class=\" PreviewContainer\" " : "\"") + " " +((style.Length > 0)?"style=\"" + style + "\"":"") + " >";
                                else if (adminMode  )
                                    beginning_div = "<div class='Admin_Op' >";

                                beginning_div = beginning_div + "<div id=\"" + ((master) ? "Master_" + control_id : control_id) + "\"class=\"" + cssclass + "\""+ ((style.Length > 0)?"style=\"" + style + "\"":"")+" >";
            

                                 placeholder.Controls.Add(new LiteralControl(beginning_div));



                                 if ((adminMode) && (!preview) && (!master))
                                 {
                                     //    placeholder.Controls.Add(new LiteralControl("<div class='Admin_Op'>"));

                                     Telerik.Web.UI.RadAjaxPanel ajax_control = new Telerik.Web.UI.RadAjaxPanel();
                                     string ajax_unique_name = placeholder.ID.ToString() + "_" + control_id.ToString();
                                     ajax_control.ID = "editButton_" + ajax_unique_name;


                                     Button editButton = new Button();
                                     editButton.ID = "btn_editButton_" + ajax_unique_name;
                                     editButton.Text = "";
                                     editButton.CssClass = "HtmlFrontEditButton FrontEditButton";

                                     if (!showEdit)
                                         editButton.Visible = false;
                                     else // to be used to determine
                                         editButton.Attributes["ControlTypeFor"] = "html";

                                     //editButton.Attributes.Add("ControlID",control_src.ToString());
                                     //editButton.Attributes.Add("Type", widget_Type.Widget_Type_Name.ToLower());
                                     editButton.OnClientClick = "editControl(" + control_src.ToString() + ",'" + "html" + "','"+placeholder.ID+"');";
                                     ajax_control.Controls.Add(editButton);
                                     placeholder.Controls.Add(ajax_control);

                                 }

                                     // Adding html control                    
                                     //Literal html_control =  new Literal();//
                                     //html_control.ID=placeholder.ID.ToString()+"_"+ //  LiteralControl(html_text)
                                     placeholder.Controls.Add(new LiteralControl(html_text));

                                     if (preview)
                                         ending_div = "</div></div>";
                                     else if (adminMode)
                                         ending_div = "</div>";
                                     ending_div = ending_div + "</div>";

                                     placeholder.Controls.Add(new LiteralControl(ending_div));

                                 
    
        }

        public static void LoadAdZone(Control placeholderControl, XmlDocument control_node, string control_id, string control_src, string cont_name, string control_seq, string control_name, bool preview, bool adminMode, bool master)
        {
            AdModel.AdZone zone = EPM.Business.Model.Ad.ZoneController.GetZoneById(int.Parse(control_src));

            if (zone.ActiveFg)
            {
                EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();

                string cssclass = "";
                string style = "";
                if (control_node.DocumentElement.Attributes["C_Style"] != null)
                    style = control_node.DocumentElement.Attributes["C_Style"].Value.ToString();
                if (control_node.DocumentElement.Attributes["Class"] != null)
                    cssclass = control_node.DocumentElement.Attributes["Class"].Value.ToString().Trim();



                string beginning_div = "";
                string ending_div = "";

                if (preview)
                {
                    beginning_div = "<div class='Preview_Op' ref='blue'" + ((preview) ? "style=\"" + style + "\"" : "") + " ><div class='Preview_Label'>" + cont_name + "(" + control_seq + ") - " + control_name + "</div>";
                }
                else if (adminMode)
                    beginning_div = "<div class='Admin_Op' >";

                beginning_div = beginning_div + "<div id=\"" + ((master) ? "Master_" : "") + control_id + "_Container\"" + ((preview) ? " class=\"" + cssclass + " PreviewContainer\" " : "class=\"" + cssclass + "\"") + " " + ((preview) ? "" : "style=\"" + style + "\"") + " >";

                beginning_div = beginning_div + "<div id = \"" + control_id + "\">";
                placeholderControl.Controls.Add(new LiteralControl(beginning_div));
                if ((adminMode) && (!preview) && (!master))
                {
                    //    placeholder.Controls.Add(new LiteralControl("<div class='Admin_Op'>"));

                    Telerik.Web.UI.RadAjaxPanel ajax_control = new Telerik.Web.UI.RadAjaxPanel();
                    string ajax_unique_name = placeholderControl.ID.ToString() + "_" + control_id.ToString();
                    ajax_control.ID = "editButton_" + ajax_unique_name;


                    Button editButton = new Button();
                    editButton.ID = "btn_editButton_" + ajax_unique_name;
                    editButton.Text = "";
                    editButton.CssClass = "AdZoneFrontEditButton FrontEditButton";
                    editButton.Attributes["ControlTypeFor"] = "adzone";

                    editButton.OnClientClick = "editControl('" + control_id.ToString() + "','" + "adzone" + "','" + placeholderControl.ID + "');";
                    ajax_control.Controls.Add(editButton);
                    placeholderControl.Controls.Add(ajax_control);

                }


                IQueryable<AdModel.AdBanner> banners = null;


                if (!zone.AdZoneActionType.AllowMultiBanners)
                {
                    banners = EPM.Business.Model.Ad.BannerContoller.GetBannersByZoneId(int.Parse(control_src)).Where(c => c.ActiveFg == true).Take(1);
                }
                else
                {
                    // Order by Sequence
                    if (!zone.AdZoneActionType.AllowRotation)
                        banners = EPM.Business.Model.Ad.MembershipContoller.GetMembersByZoneId(zone.AdZoneId);
                    else
                    {
                        if (zone.AdZoneActionType.ListBannerFg)
                            banners = EPM.Business.Model.Ad.MembershipContoller.GetRandomOrderMembersByZoneId(zone.AdZoneId);
                        else
                        {
                            if (zone.ApplyWeightFg)
                                banners = EPM.Business.Model.Ad.MembershipContoller.GetMemberByZoneIdWithWeightApply(zone.AdZoneId);
                            else
                                banners = EPM.Business.Model.Ad.MembershipContoller.GetRandomOrderMembersByZoneId(zone.AdZoneId);
                        }
                    }

                    banners = banners.OrderBy(c => c.BannerSequence);

                }

                int i = 0;
                foreach (AdModel.AdBanner banner in banners)
                {
                    //if (!string.IsNullOrEmpty(banner.LinkURL))
                    //    placeholderControl.Controls.Add(new LiteralControl("<a href=\"" + banner.LinkURL + "\" target=\"" + (string.IsNullOrEmpty(banner.LinkTarkget) ? "_blank" : banner.LinkTarkget) + "\">"));

                    HyperLink link = new HyperLink();
                    link.NavigateUrl = banner.LinkURL;
                    link.ID = control_id + "_Z" + zone.AdZoneId.ToString() + "_S" + banner.BannerSequence + "_B" + banner.AdBannerId.ToString() + "_" + i.ToString();
                    link.Target = banner.LinkTarkget.ToLower();
                    
                    i++;



                    HtmlImage image = new HtmlImage();
                    image.Alt = banner.LinkAltString;
                    image.Src = banner.SourcePath;
                    image.Attributes.Add("Style", "margin: 2px 0 2px 0;");

                    if (banner.Width != null)
                        image.Width = int.Parse(banner.Width.ToString());
                    if (banner.Height != null)
                        image.Height = int.Parse(banner.Height.ToString());

                    if (banner.Width == null && banner.Height == null)
                    {
                        image.Attributes.Add("Style", "margin: 2px 0 2px 0; max-width:100%;");
                    }

                    link.Controls.Add(image);

                    placeholderControl.Controls.Add(link);

                    EPM.Business.Model.Ad.StatisticController.CountExpose(banner.AdBannerId, zone.AdZoneId, System.Web.HttpContext.Current.Request.RawUrl, 'D');
                }




                ending_div = ending_div + "</div>";

                if (preview)
                {
                    ending_div = ending_div + "</div></div>";

                }
                else if (adminMode)
                    ending_div = ending_div + "</div>";

                ending_div = ending_div + "</div>";

                placeholderControl.Controls.Add(new LiteralControl(ending_div));
            }
        }
     

    }

}
