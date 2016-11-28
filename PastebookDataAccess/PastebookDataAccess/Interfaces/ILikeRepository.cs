using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookDataAccess
{
    public interface ILikeRepository : IRepository<LIKE> 
    {
        List<LIKE> GetLikeWithUser(Func<LIKE, bool> predicate);
    }
}
