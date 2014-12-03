using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace EPM.Business.Model
{
    public interface IRepository<T> : IDisposable
    {
        IList<T> GetAll();

        int CountAll();

        List<T> FinaAll(Expression<Func<T, bool>> predicate);

        void Add(T entity);

        void Delete(T entity);
    }
}
