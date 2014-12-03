using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EPM.Core.Common;



public partial class Cp_Site_Areas : System.Web.UI.Page
{

    EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            toolbox1.EnableButtons("save", true);
            lb_areas.Items.Clear();
            List<SiteModel.SiteSetting> siteArea = (from c in context.SiteSettings
                                                   where c.SettingName == "Site Area"
                                                   select c).ToList();

            if (siteArea.Count() > 0)
            {
                string[] areaname = siteArea[0].SettingValue.ToString().Split(',').ToArray();


                foreach (string area in areaname)
                {
                    lb_areas.Items.Add(new ListItem(area, area));
                }

                bindAllProvince(siteArea[0].SettingValue.ToString());
            }
            else
            {
                
                bindAllProvince("");
            }

            
        }
        toolbox1.ToolBarClicked += new Telerik.Web.UI.RadToolBarEventHandler(toolbox1_ToolBarClicked);
    }
    protected void toolbox1_ToolBarClicked(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
    {
        string action = e.Item.Text.ToLower();

        if (action == "save")
            SaveSetting();
        else if (action == "delete")
        {
            lb_areas.Items.RemoveAt(lb_areas.SelectedIndex);

            string siteArea = "";
            foreach (ListItem item in lb_areas.Items)
            {
                siteArea = siteArea + item.Text + ",";
            }


            if (siteArea.Length > 0)
            {
                siteArea = siteArea.Substring(0, siteArea.Length - 1);
            }

            bindAllProvince(siteArea);


            toolbox1.DisableButton("delete");
        }


    }

    void bindAllProvince(string areanames)
    {

        ddl_province.Items.Clear();

        IQueryable<GeoModel.Ref_province> provinceQuery;
        var areas = areanames.Split(',').ToList();


        provinceQuery = from c in context.Ref_provinces
                        select c;

        provinceQuery = provinceQuery.Where(c => !areas.Contains(c.Province_name));

        
       

        ddl_province.DataValueField = "province_cd";
        ddl_province.DataTextField = "province_name";
        ddl_province.DataSource = provinceQuery;
        ddl_province.DataBind();

        ddl_province.Items.Insert(0,(new ListItem("Select Province","0")));
        ddl_province.SelectedIndex = -1;
    }






    void SaveSetting()
    {
        SiteSettingHelper.CreateSettingField("Site Area", "Areas being used in site");

        string siteArea = "";
        foreach (ListItem item in lb_areas.Items)
        {
            siteArea = siteArea + item.Text + ",";
        }


        if (siteArea.Length > 0)
        {
            siteArea = siteArea.Substring(0, siteArea.Length - 1);
        }

        SiteSettingHelper.UpdateSettingValue("Site Area", siteArea);

        

        EPM.Legacy.Common.Utility.RegisterJsAlert(this.Page, "Site Area setting has been saved.");
    }

    protected void btn_add_area_Click(object sender, EventArgs e)
    {
        if (ddl_province.SelectedIndex > 0)
        {
            string selection = ddl_province.SelectedItem.Text;
            lb_areas.Items.Add(new ListItem(selection, selection));
            ddl_province.Items.RemoveAt(ddl_province.SelectedIndex);
        }
    }

    protected void lb_areas_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lb_areas.SelectedIndex >= 0)
        {
            toolbox1.EnableButtons("delete", false);
        }
        else
        {
            toolbox1.DisableButton("delete");
        }
    }
}
