using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookDataAccess
{
    public interface ICommentRepository: IRepository<COMMENT>
    {
        COMMENT GetCommentWithUser(Func<COMMENT, bool> predicate);
    }
}
