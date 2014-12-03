using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using Knn.Data;

namespace Knn.Article.Filters
{
    public class CategoryFilter : DataIndexFilter
    {
        public const int CategoryAll = -1;

        public CategoryFilter(int cat) : base(cat) { }

        public override string ToFilterString()
        {
            if (SelectedIndex >= 0)
                return "CategoryId=" + SelectedIndex;
            else
                return "CategoryId > 0";
        }
    }

    public class CategoryRangeFilter : IDataFilter
    {
        public int CategoryFrom { get; set; }
        public int CategoryTo { get; set; }

        public CategoryRangeFilter(int categoryFrom, int categoryTo)
        {
            CategoryFrom = categoryFrom;
            CategoryTo = categoryTo;
        }

        public string ToFilterString()
        {
            return string.Format("(CategoryId >= {0} AND CategoryId <= {1})",
                CategoryFrom, CategoryTo);
        }
    }

    public class CategorySetFilter : IDataFilter
    {
        List<int> categories = new List<int>();
        public CategorySetFilter() {}

        public void AddCategory(int category)
        {
            categories.Add(category);
        }

        public string ToFilterString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");

            for (int i = 0; i < categories.Count; i++)
            {
                sb.Append("CategoryId=" + categories[i]);
                if (i != categories.Count - 1)
                    sb.Append(" OR ");
            }

            sb.Append(")");

            return sb.ToString();
        }
    }
    
    public class KeywordFilter : DataValueFilter
    {
        public KeywordFilter(string value) : base(value) { }

        public override string ToFilterString()
        {
            if (SelectedValue.Trim().Equals(string.Empty))
                return string.Empty;

            StringBuilder filter = new StringBuilder();

            char[] del = { ' ' };
            filter.Append("(");
            string[] words = SelectedValue.Split(del);

            for (int i = 0; i < words.Length; i++)
            {
                filter.Append(string.Format("Title LIKE N'%{0}%' OR Body LIKE N'%{0}%' OR Reporter LIKE N'%{0}%'", words[i]));
                if (i != words.Length - 1)
                    filter.Append(" OR ");
            }

            filter.Append(")");
            return filter.ToString();
        }
    }

    public class IssueDateFilter : DataValueFilter
    {
        public IssueDateFilter() : base(null) {}
        public IssueDateFilter(DateTime dateTime) : base(dateTime.ToShortDateString()) {}

        public override string ToFilterString()
        {
            if (SelectedValue == null)
                return "IssueDate < GETDATE()";
            else
                return "IssueData < '" + SelectedValue + "'";
        }
    }

    public class SuspendedFilter : DataBooleanFilter
    {
        public SuspendedFilter(bool isTrue) : base(isTrue) {}

        public override string ToFilterString()
        {
            return "Suspended=" + (IsTrue ? "1" : "0");
        }
    }
}
