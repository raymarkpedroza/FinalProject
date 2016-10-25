using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PastebookDataAccess.Repositories
{
    public interface IGenericTransactionDataRepository<T> where T : class
    {
        List<T> RetrieveAllRecords(params Expression<Func<T, object>>[] navigationProperties);
        List<T> RetrieveList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        T RetrieveSpecificRecord(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        bool CreateRecord(T record);
        bool UpdateRecord(T record);
        bool DeleteRecord(T record);
    }
}
