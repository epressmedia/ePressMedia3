using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;


namespace EPM.Legacy.Data
{
    #region bases

    /// <summary>
    /// 데이터베이스 테이블 내의 데이터를 필터링 하기 위한 필터 인터페이스
    /// </summary>
    public interface IDataFilter
    {
        /// <summary>
        /// SQL Query 의 WHERE 절에 삽입될 조건 문장을 리턴한다.
        /// </summary>
        /// <returns></returns>
        string ToFilterString();
    }

    /// <summary>
    /// 인덱스 값을 기반으로 한 필터 클래스를 정의하기 위한 추상 기반 클래스
    /// </summary>
    public abstract class DataIndexFilter : IDataFilter
    {
        public int SelectedIndex { get; set; }

        public DataIndexFilter(int selectedIndex)
        {
            SelectedIndex = selectedIndex;
        }

        public abstract string ToFilterString();
    }

    /// <summary>
    /// 문자열 값을 기반으로 한 필터 클래스를 정의하기 위한 추상 기반 클래스
    /// </summary>
    public abstract class DataValueFilter : IDataFilter
    {
        public string SelectedValue { get; set; }

        public DataValueFilter(string selectedValue)
        {
            SelectedValue = selectedValue;
        }

        public abstract string ToFilterString();
    }

    public abstract class DataBooleanFilter : IDataFilter
    {
        public bool IsTrue { get; set; }

        public DataBooleanFilter(bool isTrue)
        {
            IsTrue = isTrue;
        }

        public abstract string ToFilterString();
    }

    #endregion


    /// <summary>
    /// 지역 코드 필터링 클래스
    /// </summary>
    public class AreaFilter : DataValueFilter
    {
        public AreaFilter(string selectedValue) : base(selectedValue) { }

        public static NameValueCollection GetOptions(string province)
        {
            return DataAccess.SelectKeyValuePairs("Areas", "AreaId", "AreaName",
                "Visible=1 AND Province='" + province + "'", "AreaName");
        }

        public static NameValueCollection GetAllOptions(string province)
        {
            return DataAccess.SelectKeyValuePairs("Areas", "AreaId", "AreaName",
                "Province='" + province + "'", "AreaName");
        }

        public override string ToFilterString()
        {
            try
            {
                int i = int.Parse(SelectedValue);
                if (i == 0)
                    return string.Empty;
                else
                    return "Area=" + SelectedValue;
            }
            catch
            { return string.Empty; }
        }
    }

    /// <summary>
    /// Province 필터링 클래스
    /// </summary>
    public class ProvinceFilter : DataValueFilter
    {
        public ProvinceFilter(string selectedValue) : base(selectedValue) { }


        public override string ToFilterString()
        {
            try
            {
                string i = SelectedValue;
                if (i.Length == 0)
                    return string.Empty;
                else
                    return "ref_city.Province_cd='" + SelectedValue + "'";
            }
            catch
            { return string.Empty; }
        }
    }

    /// <summary>
    /// IDataFilter 인터페이스를 구현하는 클래스 인스턴스의 컬렉션을 나타낸다.
    /// </summary>
    public class DataFilterCollection
    {
        List<IDataFilter> filters = new List<IDataFilter>();

        /// <summary>
        /// 쿼리 문의 WHERE 절에 삽입할 수 있는 필터 표현을 얻는다.
        /// </summary>
        public string FilterExpression
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                foreach (IDataFilter filter in filters)
                {
                    string expr = filter.ToFilterString();
                    if (sb.Length > 0 && expr.Length > 0)
                        sb.Append(" AND ");
                    sb.Append(expr);
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// 컬렉션에 필터를 추가한다.
        /// </summary>
        /// <param name="filter">AdFilter 로부터 유도된 클래스의 인스턴스</param>
        public void AddFilter(IDataFilter filter)
        {
            filters.Add(filter);
        }

        public void Clear()
        {
            filters.Clear();
        }
    }
}
