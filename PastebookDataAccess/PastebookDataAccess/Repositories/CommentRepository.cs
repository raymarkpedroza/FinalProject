using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PastebookDataAccess
{
    public class CommentRepository : Repository<COMMENT>, ICommentRepository
    {
        public COMMENT GetCommentWithUser(Func<COMMENT, bool> predicate)
        {
            using (var context = new PastebookEntities())
            {
                return context.COMMENTs
                    .Include(comment => comment.USER)
                    .Where(predicate)
                    .FirstOrDefault();
            }
        }
    }
}
