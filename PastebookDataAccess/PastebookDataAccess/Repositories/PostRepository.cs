using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PastebookDataAccess
{
    public class PostRepository : Repository<POST>, IPostRepository
    {
        public List<POST> GetNewsfeedPost(List<int> listOfPosterId)
        {
            using (var context = new PastebookEntities())
            {
                return context.POSTs
                    .Include(post => post.USER)
                    .Include(post => post.USER1)
                    .Include(post => post.COMMENTs
                        .Select(comment => comment.USER))
                    .Include(post => post.LIKEs
                        .Select(like => like.USER))
                    .Where(post => listOfPosterId.Contains(post.POSTER_ID))
                    .OrderByDescending(post => post.CREATED_DATE)
                    .ToList();
            }
        }

        public POST GetPost(int id)
        {
            using (var context = new PastebookEntities())
            {
                return context.POSTs
                    .Include(post => post.USER)
                    .Include(post => post.USER1)
                    .Include(post => post.COMMENTs.Select(comment => comment.USER))
                    .Include(post => post.LIKEs.Select(like => like.USER))
                    .Where(post => post.ID.Equals(id))
                    .SingleOrDefault();
            }
        }

        public List<POST> GetTimelinePost(int id)
        {
            using (var context = new PastebookEntities())
            {
                return context.POSTs
                    .Include(post => post.USER)
                    .Include(post => post.USER1)
                    .Include(post => post.COMMENTs.Select(comment => comment.USER))
                    .Include(post => post.LIKEs.Select(like => like.USER))
                    .Where(post => post.PROFILE_OWNER_ID.Equals(id))
                    .OrderByDescending(post => post.CREATED_DATE)
                    .ToList();
            }
        }

    }
}
