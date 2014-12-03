using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;


namespace EPM.Web.UI
{
    public class BEChangeBox: TextBox
    {

        [DefaultValue(-1)]
        public int RequestId
        {
            get { return (int)(ViewState["RequestId"] ?? -1); }
            set { ViewState["RequestId"] = value; }
        }

        [DefaultValue("")]
        public string Mask
        {
            get { return (string)(ViewState["Mask"] ?? ""); }
            set { ViewState["Mask"] = value; }
        }
        
    
        protected override void Render(HtmlTextWriter writer)
        {

            if (RequestId > 0)
            {
                writer.Write(string.Format("<img src=\"/cp/img/next.png\" alt=\"change\" class=\"BEChange_Change\" />"));

                base.Attributes["requestid"] = RequestId.ToString();
                base.Attributes["status"] = "N";


                base.ReadOnly = true;
                base.CssClass = "BEChange_Text";
                base.Render(writer);
                writer.Write(string.Format("<img src=\"/cp/img/reload.png\" alt=\"Apply\" class=\"BEChange_Apply\" onclick=\"ApproveField('" + RequestId.ToString() + "')\"/> "));
                writer.Write(string.Format("<img src=\"/cp/img/delete.png\" alt=\"Reject\" class=\"BEChange_Reject\" onclick=\"RejectField('" + RequestId.ToString() + "')\" />"));
            }

        }



        
    }
}
