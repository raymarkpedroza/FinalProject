using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookDataAccess
{
    public interface ICommentRepository: IRepository<PASTEBOOK_COMMENT>
    {
        PASTEBOOK_COMMENT GetCommentWithUser(Func<PASTEBOOK_COMMENT, bool> predicate);
    }
}
