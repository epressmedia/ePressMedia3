using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPM.Business.Model.Biz;
using EPM.Core.Biz;




    public partial class Mobile_Common_MobileBizSub1 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString["char"] != null)
            {
                string cha = Request.QueryString["char"].ToString();
                string chr2 = "";

                
                BizResource biz = new BizResource();



                int index = Array.IndexOf(biz.catLetters, cha);
                if (index + 1 < biz.catLetters.Length)
                {
                    chr2 = biz.catLetters[index + 1].ToString();
                }
                bindCategory(cha, chr2);
            }
            else if (Request.QueryString["fav"] != null)
            {
                string cha = Request.QueryString["fav"].ToString();
                bindCategory(cha);
            }


        }


        void bindCategory(string cha, string cha2)
        {

            if (string.IsNullOrEmpty(cha))
            {
                BizSubRepeater.DataSource = null;
                BizSubRepeater.DataBind();
                return;
            }

            string filter = "CatName >= N'" + cha + "' ";

            if (!string.IsNullOrEmpty(cha2))
                filter += "AND CatName < N'" + cha2 + "'";

            //List<BizCategory> bizCats =  BizCategory.SelectBizCategories(filter);
            List<BizModel.BusinessCategory> bizCats = BECategoryController.GetAllBusinessCatgories().ToList();
            BizSubRepeater.DataSource = bizCats;
            BizSubRepeater.DataBind();
        }

        void bindCategory(string cha)
        {

            if (!string.IsNullOrEmpty(cha))
            {
               

                List<BizModel.BusinessCategory> BCs = BECategoryController.GetBusinessCatgoryByNames(cha).ToList();
                if (BCs != null)
                {
                    int[] CatIDs = new int[BCs.Count];
                    int count = 0;
                    foreach (BizModel.BusinessCategory BC in BCs)
                    {
                        CatIDs[count] = BC.CategoryId;
                        count++;
                    }
                    //CatIDs.
                    //BECategoryController.GetBusinessCatgoryByNames(categories).ToArray

                    BizSubRepeater.DataSource = BECategoryController.GetBusinessCategoriesByParentIDs(CatIDs).Where(c=>c.BusinessEntities.Count() > 0);
                    BizSubRepeater.DataBind();
                }
                //return;
            }

            //string filter = "";

            //string[] filters = cha.Split(',');

            //foreach (string f in filters)
            //{
            //    if (filter.Length != 0)
            //        filter = filter + " OR ";
            //    filter = filter + "CatName like N'%" + f.Trim() + "%'";
            //}
            

            ////List<BizCategory> bizCats = BizCategory.SelectBizCategories(filter);
            //List<BizModel.BusinessCategory> bizCats = BECategoryController.GetAllBusinessCatgories().ToList();
            //BizSubRepeater.DataSource = bizCats;
            //BizSubRepeater.DataBind();
        }

        protected void BizSubRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.AlternatingItem) || (e.Item.ItemType == ListItemType.Item))
            {
                BizModel.BusinessCategory cat = e.Item.DataItem as BizModel.BusinessCategory;
                Literal counter = e.Item.FindControl("Count") as Literal;
                counter.Text = BEController.GetActiveBEsByCatID(cat.CategoryId).Count().ToString();
            }
        }

    }
