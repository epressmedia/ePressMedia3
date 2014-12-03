using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.OpenAccess;

namespace EPM.Business.Model
{
    public abstract class Repository<T> : IRepository<T>
    {

        protected Repository(IUnitOfWork context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context", "The given parametr cannot be null.");
            }

            this.Context = context;
        }

        protected IUnitOfWork Context
        {
            get;
            private set;
        }
        public virtual IList<T> GetAll()
        {
            return this.Context.GetAll<T>().ToList();
        }

        public virtual int CountAll()
        {
            return this.Context.GetAll<T>().Count();
        }

        public virtual List<T> FinaAll(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return this.Context.GetAll<T>().Where(predicate).ToList();
        }

        public virtual void Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "The given parameter cannot be null.");
            }
            this.Context.Add(entity);
        }

        public virtual void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "The given parameter cannot be null.");
            }

            this.Context.Delete(entity);
        }


        public virtual void Dispose()
        {
            this.Context = null;
        }
    }
}
