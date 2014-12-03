using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Reflection;

namespace ePressMedia
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string fullpath="c:\\aa\\abc";
           Label2.Text =  fullpath.Substring(fullpath.LastIndexOf('\\') + 1, 2);
            invokebyname();
        }

        void invokebyname()
        {
            //Assembly assembly = Assembly.LoadFile("EPM.Business.Model");
            //string class_name = "EPM.Business.Model.Common.ThumbnailTypes";
            //Type ClassType = Type.GetType(class_name);
            //ConstructorInfo classConstructor = ClassType.GetConstructor(Type.EmptyTypes);
            //object ClassObject = classConstructor.Invoke(new object[] { });

            //MethodInfo callingMethod = ClassType.GetMethod("GetThumbnailTypesDictionary");
            //var result = callingMethod.Invoke(ClassObject, null);
            //DropDownList1.DataSource = result;
            //DropDownList1.DataTextField = "Key";
            //DropDownList1.DataValueField = "Key";
            //DropDownList1.DataBind();
            object result = null;
            Assembly assembly = Assembly.Load("EPM.Business.Model");

            Type type = assembly.GetType("EPM.Business.Model.Common.ThumbnailTypes");
            if (type != null)
            {
                MethodInfo methodInfo = type.GetMethod("GetThumbnailTypesDictionary");
                if (methodInfo != null)
                {
                  
                    ParameterInfo[] parameters = methodInfo.GetParameters();
                    result = methodInfo.Invoke(null, null);

                }
            }
            DropDownList1.DataSource = result;
            DropDownList1.DataTextField = "Key";
            DropDownList1.DataValueField = "Key";
            DropDownList1.DataBind();
        }

        public void Invoke(string typeName, string methodName)
        {
            Type type = Type.GetType(typeName);
            object instance = Activator.CreateInstance(type);
            MethodInfo method = type.GetMethod(methodName);
            method.Invoke(instance, null);
            label1.Text = method.ReturnType.ToString();
        }

        void case1()
        {
            UserControl uc = (UserControl)(Page.LoadControl("/Controls/Article/TagCloud.ascx"));
            Type type = uc.GetType();
            System.Reflection.PropertyInfo[] properties = type.GetProperties();

            foreach (System.Reflection.PropertyInfo property in properties)
            {
                CategoryAttribute attribute =
                    Attribute.GetCustomAttribute(property, typeof(System.ComponentModel.CategoryAttribute)) as CategoryAttribute;

                DescriptionAttribute descAttribute = Attribute.GetCustomAttribute(property, typeof(DescriptionAttribute)) as DescriptionAttribute;
                DefaultValueAttribute DefAttribute = Attribute.GetCustomAttribute(property, typeof(DefaultValueAttribute)) as DefaultValueAttribute;
                DisplayNameAttribute DisNameAttribute = Attribute.GetCustomAttribute(property, typeof(DisplayNameAttribute)) as DisplayNameAttribute;
                RequiredAttribute ReqAttribute = Attribute.GetCustomAttribute(property, typeof(RequiredAttribute)) as RequiredAttribute;


                if (attribute != null)
                {
                    if ((attribute.Category.ToString() == "EPMProperty")) // This property has a StoredDataValueAttribute
                    {
                        if (property.Name == "Distribution")
                        {
                            bool isEnum = property.PropertyType.IsEnum;
                            label1.Text = isEnum.ToString();
                            Dictionary<string, string> datasource = EPM.Core.CP.ContentBuilderContoller.GetMembersFromControlEnum(property);
                            DropDownList1.DataSource = datasource;
                            DropDownList1.DataTextField = "Key";
                            DropDownList1.DataValueField = "Key";
                            DropDownList1.DataBind();

                        }
                    }
                }
            }
        }
    }
}