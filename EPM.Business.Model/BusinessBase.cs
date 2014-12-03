using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Telerik.OpenAccess;




namespace EPM.Business.Model
{
    public class BusinessBase
    {
        protected static readonly string SCOPE_KEY = "ScopePerHttpRequest";
        protected IObjectScope scope;

        protected BusinessBase()
        {
            if (HttpContext.Current != null)
            {
                //if (HttpContext.Current.Items[SCOPE_KEY] == null)
                //    HttpContext.Current.Items.Add(SCOPE_KEY, scope = DataAccessScopeProvider.GetNewObjectScope());
                //else
                //    scope = HttpContext.Current.Items[SCOPE_KEY] as IObjectScope;

                
            }
        }

        public void StartTransaction()
        {
            if (!scope.Transaction.IsActive)
                scope.Transaction.Begin();
        }

        public void CommitChanges()
        {
            try
            {
                if (scope.Transaction.IsActive)
                    scope.Transaction.Commit();
                else
                    throw new Exception(GetType().FullName + ".CommitChanges()");
            }
            catch (Exception ex)
            {
                RollBackChanges();
                throw new Exception(ex.Message);
            }
        }

        public void RollBackChanges()
        {
            if (scope.Transaction.IsActive)
                scope.Transaction.Rollback();
        }

        public static void Dispose()
        {
            if (HttpContext.Current.Items[SCOPE_KEY] == null)
                return;
            IObjectScope scope = HttpContext.Current.Session[SCOPE_KEY] as IObjectScope;
            if (scope != null)
                scope.Dispose();
            HttpContext.Current.Items.Remove(SCOPE_KEY);
        }

        protected void ValidateTransaction(string method)
        {
            if (!scope.Transaction.IsActive)
                throw new Exception(GetType().FullName + "." + method);
        } 
    }
}
