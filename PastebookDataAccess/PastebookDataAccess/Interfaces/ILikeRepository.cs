using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookDataAccess
{
    public interface ILikeRepository : IRepository<PASTEBOOK_LIKE> 
    {
        List<PASTEBOOK_LIKE> GetLikeWithUser(Func<PASTEBOOK_LIKE, bool> predicate);
    }
}
