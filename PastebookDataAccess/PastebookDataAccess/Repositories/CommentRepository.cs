using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PastebookDataAccess
{
    public class CommentRepository : Repository<PASTEBOOK_COMMENT>, ICommentRepository
    {
        public PASTEBOOK_COMMENT GetCommentWithUser(Func<PASTEBOOK_COMMENT, bool> predicate)
        {
            using (var context = new PASTEBOOKEntities())
            {
                return context.PASTEBOOK_COMMENT
                    .Include(comment => comment.PASTEBOOK_USER)
                    .Where(predicate)
                    .FirstOrDefault();
            }
        }
    }
}
