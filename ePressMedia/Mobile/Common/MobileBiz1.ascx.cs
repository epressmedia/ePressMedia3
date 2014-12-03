using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EPM.Core.Biz;


    public partial class Mobile_Common_MobileBiz1 : System.Web.UI.UserControl
    {

        
        protected void Page_Load(object sender, EventArgs e)
        {

            BizResource biz = new BizResource();
            //BizRepeater.DataSource = biz.catLetters;
            //BizRepeater.DataBind();

            FavBizRepeater.DataSource = Load_FavCat_Class();
            FavBizRepeater.DataBind();

            
        }


         List<Favorite_Catagory>  Load_FavCat_Class()
        {
            List<Favorite_Catagory> fav_cat = new List<Favorite_Catagory>();
            
            
            fav_cat.Insert(0, new Favorite_Catagory("변호사","변호사","icon_attorney.png"));
            fav_cat.Insert(0, new Favorite_Catagory("자동차", "자동차", "icon_auto.png"));
            fav_cat.Insert(0, new Favorite_Catagory("미용", "미용", "icon_beauty.png"));
            fav_cat.Insert(0, new Favorite_Catagory("병원", "병원,약국", "icon_clinic.png"));
            fav_cat.Insert(0, new Favorite_Catagory("건축", "건축", "icon_construction.png"));
            fav_cat.Insert(0, new Favorite_Catagory("금융", "금융", "icon_cpa.png"));
            fav_cat.Insert(0, new Favorite_Catagory("학교/학원", "학교,학원", "icon_edu.png"));
            fav_cat.Insert(0, new Favorite_Catagory("회계사", "회계사", "icon_cpa3.png"));
            fav_cat.Insert(0, new Favorite_Catagory("숙박", "호텔/모텔,홈스테이/민박", "icon_hotel.png"));
            fav_cat.Insert(0, new Favorite_Catagory("부동산", "부동산", "icon_realestate.png"));
            fav_cat.Insert(0, new Favorite_Catagory("종교", "종교", "icon_religion.png"));
            fav_cat.Insert(0, new Favorite_Catagory("식당", "식당", "icon_restaurant.png"));
            fav_cat.Insert(0, new Favorite_Catagory("여행사", "여행사", "icon_travel.png"));




            return fav_cat.OrderBy(x => x.CatName).ToList();

        }



        class Favorite_Catagory
        {

            public Favorite_Catagory(string _catName, string _tagName, string _icon)
            {
                CatName = _catName;
                TagName = _tagName;
                Icon = _icon;
            }

            private string catName;
            public string CatName
            {
                set{catName = value;}
                get { return catName; }
            }

            private string tagName;
            public string TagName
            {
                set { tagName = value; }
                get { return tagName; }
            }
            
            public string URL
            {
                
                get { return "Biz.aspx?fav="+tagName; }
            }

            private string icon;
            public string Icon
            {
                set { icon = "images/"+value; }
                get { return icon; }
            }
        }

    }
