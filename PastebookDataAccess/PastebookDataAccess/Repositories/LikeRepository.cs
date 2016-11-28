using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PastebookDataAccess
{
    public class LikeRepository : Repository<LIKE>, ILikeRepository
    {
        public List<LIKE> GetLikeWithUser(Func<LIKE, bool> predicate)
        {
            using (var context = new PastebookEntities())
            {
                return context.LIKEs
                    .Include(like => like.USER)
                    .Where(predicate)
                    .ToList();
            }
        }
    }
}
