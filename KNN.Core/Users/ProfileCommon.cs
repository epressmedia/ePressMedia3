
using System.Web.Security;
using System.Web.Profile;

namespace EPM.Core.Users
{
    public class UserProfile : ProfileBase
    {


        public virtual string FirstName
        {
            get
            {
                return base["FirstName"] as string;
            }
            set
            {
                base["FirstName"] = value;
            }
        }

        public virtual string LastName
        {
            get
            {
                return ((string)(this.GetPropertyValue("LastName")));
            }
            set
            {
                this.SetPropertyValue("LastName", value);
            }
        }




        public virtual bool PwdExpired
        {
            get
            {
                return ((bool)(this.GetPropertyValue("PwdExpired")));
            }
            set
            {
                this.SetPropertyValue("PwdExpired", value);
            }
        }

        public virtual bool Verified
        {
            get
            {
                return ((bool)(this.GetPropertyValue("Verified")));
            }
            set
            {
                this.SetPropertyValue("Verified", value);
            }
        }


        public virtual string Address1
        {
            get
            {
                return ((string)(this.GetPropertyValue("Address1")));
            }
            set
            {
                this.SetPropertyValue("Address1", value);
            }
        }

        public virtual string Address2
        {
            get
            {
                return ((string)(this.GetPropertyValue("Address2")));
            }
            set
            {
                this.SetPropertyValue("Address2", value);
            }
        }

        public virtual string City
        {
            get
            {
                return ((string)(this.GetPropertyValue("City")));
            }
            set
            {
                this.SetPropertyValue("City", value);
            }
        }

        public virtual string Province
        {
            get
            {
                return ((string)(this.GetPropertyValue("Province")));
            }
            set
            {
                this.SetPropertyValue("Province", value);
            }
        }

        public virtual string Phone
        {
            get
            {
                return ((string)(this.GetPropertyValue("Phone")));
            }
            set
            {
                this.SetPropertyValue("Phone", value);
            }
        }


        public virtual string Zip
        {
            get
            {
                return ((string)(this.GetPropertyValue("Zip")));
            }
            set
            {
                this.SetPropertyValue("Zip", value);
            }
        }

 


        public virtual string UserComment
        {
            get
            {
                return ((string)(this.GetPropertyValue("UserComment")));
            }
            set
            {
                this.SetPropertyValue("UserComment", value);
            }
        }



        public static UserProfile GetUSerProfile(string username)
        {

            return Create(username) as UserProfile;
        }


    }
}
