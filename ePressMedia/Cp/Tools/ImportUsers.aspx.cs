using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Profile;

namespace ePressMedia.Cp.Tools
{
    public partial class ImportUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_import_Click(object sender, EventArgs e)
        {


            int counter = 0;
            string log = "";
            DataTable dt = getUsers();

            foreach (DataRow dr in dt.Rows)
            {
                try{
                MembershipUser user = Membership.CreateUser(dr["id"].ToString(), dr["pass"].ToString(), dr["email"].ToString());

                     Roles.AddUserToRole(user.UserName, EPM.Core.Users.UserRoles.GetDefaultUserGroupName());

                     var profile = ProfileBase.Create(user.UserName);
                    profile.SetPropertyValue("Verified", true);
                    profile.SetPropertyValue("Phone", dr["phone"].ToString());
                    profile.SetPropertyValue("Zip", dr["zip"].ToString());
                    profile.SetPropertyValue("Address1", dr["address1"].ToString());
                    profile.SetPropertyValue("Address2", dr["address2"].ToString());
                    profile.SetPropertyValue("UserComment", dr["comment"].ToString().Length > 300 ? dr["comment"].ToString().Substring(0, 300) : dr["comment"].ToString());
                    profile.SetPropertyValue("FirstName", dr["name"].ToString());
                    profile.SetPropertyValue("LastName", "");
                    profile.SetPropertyValue("PwdExpired", false);

                    profile.Save();
                    counter++;
                }
                catch(MembershipCreateUserException ex)
                {
                    log = log + "User ID(" + dr["id"].ToString() + ") failed - " + ex.StatusCode.ToString()+"</br>";
                }
 
             
            }
            lbl_log.Text = log;
            Label1.Text = counter.ToString() +" users have been successfully imported.";

            
        }

        private DataTable getUsers()
        {
               using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EPMConnection"].ConnectionString))
    {
        SqlCommand dCmd = new SqlCommand("select name, date, id, pass, phone,email,hphone,zip,address1, address2, comment from news_member order by date desc", conn); 
        dCmd.CommandType = CommandType.Text; 
        DataSet dSet = new DataSet(); 

        SqlDataAdapter dAd = new SqlDataAdapter(dCmd); 
 
        dAd.Fill(dSet); 
        return dSet.Tables[0]; 
    } 

        }
    }
}
