using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PastebookDataAccess
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);
        List<T> GetAll();
        List<T> Find(Func<T, bool> predicate);

        bool Create(T record);
        bool Update(T record);
        bool Delete(T record);
    }
}
