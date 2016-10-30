using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PastebookDataAccess
{
    public class LikeRepository : Repository<PASTEBOOK_LIKE>, ILikeRepository
    {
        public List<PASTEBOOK_LIKE> GetLikeWithUser(Func<PASTEBOOK_LIKE, bool> predicate)
        {
            using (var context = new PASTEBOOKEntities())
            {
                return context.PASTEBOOK_LIKE
                    .Include(like => like.PASTEBOOK_USER)
                    .Where(predicate)
                    .ToList();
            }
        }
    }
}
