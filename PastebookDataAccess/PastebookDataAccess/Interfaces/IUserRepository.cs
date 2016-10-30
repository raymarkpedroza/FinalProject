using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookDataAccess
{
    public interface IUserRepository : IRepository<PASTEBOOK_USER>
    {
        List<PASTEBOOK_USER> GetUserWithCountry(Func<PASTEBOOK_USER, bool> predicate);
    }
}
