using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPM.Data.Model;


namespace EPM.Business.Model.Admin
{
    public class ControlEditorController
    {

        public static IEnumerable<SiteModel.Widget> GetControlsByWidgetType(string WidgetType)
        {
            EPMEntityModel context = new EPMEntityModel();
            return (from c in context.Widgets
                    where c.WidgetType.Widget_Type_Name.ToLower() == WidgetType
                    select c);
        }

        public static SiteModel.Widget GetContorlByID(int Widget_Id)
        {
            EPMEntityModel context = new EPMEntityModel();
            SiteModel.Widget widget = new SiteModel.Widget();
            if (context.Widgets.Any(c => c.Widget_id == Widget_Id))
            {
                widget = context.Widgets.Single(c => c.Widget_id == Widget_Id);
            }
            return widget;
        }


        private static int GetWidgetTypeIDByName(string WidgetTypeName)
        {
            EPMEntityModel context = new EPMEntityModel();
            return (context.WidgetTypes.Single(c => c.Widget_Type_Name.ToLower() == WidgetTypeName.ToLower()).Widget_Type_Id);
        }

        public static void UpdateHTMLContent(int ID, string content)
        {
            EPMEntityModel context = new EPMEntityModel();
            SiteModel.Widget widget = (context.Widgets.Single(c => c.Widget_id == ID));
            widget.Widget_Data = content;
            context.SaveChanges();
        }

        //public static void AddNewWidget()
        //{
        //    EPMEntityModel contract = new EPMEntityModel();
        //    SiteModel.Widget widget = new SiteModel.Widget();
        //    widget.Widget_name = WidgetName;
        //    widget.WidgetType = GetWidgetTypeIDByName(WidgetTypeName);
        //    widget.created_by = CreatedBy;
        //    widget.created_dt = DateTime.Now;
        //    widget.Widget_descr = Description;
        //    widget.active_fg = Active;
        //    widget.ContentType
            

        //}

        //public static void UpdateWidget(int WidgetID)
        //{
        //}

    }
}
