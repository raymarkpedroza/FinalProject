using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PastebookDataAccess
{
    public class PostRepository : Repository<PASTEBOOK_POST>, IPostRepository
    {
        public List<PASTEBOOK_POST> GetNewsfeedPost(List<int> listOfPosterId)
        {
            using (var context = new PASTEBOOKEntities())
            {
                return context.PASTEBOOK_POST
                    .Include(post => post.PASTEBOOK_USER)
                    .Include(post => post.PASTEBOOK_USER1)
                    .Include(post => post.PASTEBOOK_COMMENT
                        .Select(comment => comment.PASTEBOOK_USER))
                    .Include(post => post.PASTEBOOK_LIKE
                        .Select(like => like.PASTEBOOK_USER))
                    .Where(post => listOfPosterId.Contains(post.POSTER_ID))
                    .OrderByDescending(post => post.CREATED_DATE)
                    .ToList();
            }
        }

        public PASTEBOOK_POST GetPost(int id)
        {
            using (var context = new PASTEBOOKEntities())
            {
                return context.PASTEBOOK_POST
                    .Include(post => post.PASTEBOOK_USER)
                    .Include(post => post.PASTEBOOK_USER1)
                    .Include(post => post.PASTEBOOK_COMMENT.Select(comment => comment.PASTEBOOK_USER))
                    .Include(post => post.PASTEBOOK_LIKE.Select(like => like.PASTEBOOK_USER))
                    .Where(post => post.ID.Equals(id))
                    .SingleOrDefault();
            }
        }

        public List<PASTEBOOK_POST> GetTimelinePost(int id)
        {
            using (var context = new PASTEBOOKEntities())
            {
                return context.PASTEBOOK_POST
                    .Include(post => post.PASTEBOOK_USER)
                    .Include(post => post.PASTEBOOK_USER1)
                    .Include(post => post.PASTEBOOK_COMMENT.Select(comment => comment.PASTEBOOK_USER))
                    .Include(post => post.PASTEBOOK_LIKE.Select(like => like.PASTEBOOK_USER))
                    .Where(post => post.PROFILE_OWNER_ID.Equals(id))
                    .OrderByDescending(post => post.CREATED_DATE)
                    .ToList();
            }
        }

    }
}
