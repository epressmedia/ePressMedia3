using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;


using Knn;


namespace Knn.Common
{
    public class Geo1
    {

        
        public static NameValueCollection GetCitiesByProvince(string province)
        {
            return DataAccess.SelectKeyValuePairs("ref_city", "city_id", "city_name","province_cd = '" + province.ToUpper() + "'", "city_name");


        }

        public static NameValueCollection GetProvinceByCountry(string country)
        {
            return DataAccess.SelectKeyValuePairs("Ref_Province", "province_cd", "province_name", "country_cd = '" + country + "'", "province_name");
        }

        public static NameValueCollection GetAllProvinces()
        {
            return DataAccess.SelectKeyValuePairs("Ref_Province", "province_cd", "province_name", "1=1", "country_cd, province_name");
        }

        public static NameValueCollection GetContries()
        {
            return DataAccess.SelectKeyValuePairs("Ref_country", "country_cd", "country_name", "1=1", "country_name");
        }

        public static NameValueCollection ValidateProvinceCity(string province, string city)
        {
            string query = @"select c.province_cd, city_id from ref_province p
                            inner join ref_city c on p.province_cd = c.province_cd
                               where c.province_cd = '" + province + "' and city_name='" + city+"'";
            SqlCommand command = new SqlCommand(query);
            return DataAccess.SelectKeyValuePairs(command);
        }

        public static NameValueCollection GetAreaInfoByAreaID(string areaID)
        {
            return DataAccess.SelectKeyValuePairs("ref_city", "province_cd", "city_name", "city_id = '" + areaID + "'");
        }

        public static NameValueCollection GetAreaInfoByPortalCode(string postalCode)
        {
            string query = @"select province_cd, city_name  from ref_postalcode p
                        inner join ref_city c on p.city_id = c.city_id
                    where replace(postal_code,' ','') ='" + postalCode.Replace(" ","") + "'";
            SqlCommand command = new SqlCommand(query);
            return DataAccess.SelectKeyValuePairs(command);
        }
    }
}
