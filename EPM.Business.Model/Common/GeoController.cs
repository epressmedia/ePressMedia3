using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPM.Data.Model;
using GeoModel;

namespace EPM.Business.Model.Common
{
    public static class GeoController
    {
        
        public static List<Ref_city> GetCitiesByProvince(string province_cd)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            List<Ref_city> cities = (from c in context.Ref_cities
                                     where c.Province_cd.ToLower() == province_cd.ToLower()
                                     select c).ToList();

            return cities;


        }

        public static List<Ref_province> GetProvincesByCountry(string country_cd)
        {

            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            List<Ref_province> provinces = (from c in context.Ref_provinces
                                            where c.Country_cd.ToLower() == country_cd.ToLower()
                                            orderby c.Province_name
                                            select c).ToList();

            return provinces;
        }

        public static List<Ref_province> GetAllProvinces()
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();


            List<SiteModel.SiteSetting> siteArea = (from c in context.SiteSettings
                                                    where c.SettingName == "Site Area"
                                                    select c).ToList();


            List<Ref_province> provinces;


            if (siteArea[0].SettingValue.ToString().Length > 0)
            {
                var filter = siteArea[0].SettingValue.ToString().Split(',').ToList();
                provinces = (from c in context.Ref_provinces
                             where filter.Contains(c.Province_name)
                             orderby c.Province_name
                             select c).ToList();
            }
            else
            {
                provinces = (from c in context.Ref_provinces
                             orderby c.Province_name
                             select c).ToList();
            }

            return provinces;
        }

        public static List<Ref_country> GetContries()
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            List<Ref_country> countries = (from c in context.Ref_countries
                                           orderby c.Country_name
                                           select c).ToList();

            return countries;
        }

        public static List<Ref_city> ValidateProvinceCity(string province_cd, string city_name)
        {


            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            List<Ref_city> cities = (from c in context.Ref_cities
                                     where c.Province_cd.ToLower() == province_cd.ToLower() && c.City_name == city_name
                                     orderby c.City_name
                                     select c).ToList();

            return cities;

        }


        public static List<Ref_city> GetGeoInfoByPortalCode(string postalCode)
        {

            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            List<Ref_city> cities = (from c in context.Ref_postalcodes
                                     where c.Postal_code.Replace(" ", "") == postalCode.Replace(" ", "")
                                     orderby c.Ref_city.City_name
                                     select c.Ref_city).ToList();

            return cities;

        }

        public static string GetStateFullNameByAbbreviation(string abbreviation)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            if (context.Ref_provinces.Any(c => c.Province_cd == abbreviation))
            {
                return context.Ref_provinces.SingleOrDefault(c => c.Province_cd == abbreviation).Province_name;
            }
            else
            {
                return "";
            }

        }
    }
}
