using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookDataAccess
{
    public interface IUserRepository : IRepository<USER>
    {
        List<USER> GetUserWithCountry(Func<USER, bool> predicate);
    }
}
