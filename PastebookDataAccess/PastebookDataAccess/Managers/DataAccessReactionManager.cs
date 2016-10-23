using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PastebookDataAccess.Managers
{
    public class DataAccessReactionManager
    {
        public int AddLike(PASTEBOOK_LIKE like)
        {
            int result = 0;

            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    context.PASTEBOOK_LIKE.Add(like);
                    result = context.SaveChanges();
                }
            }
            catch
            {

            }

            return result;
        }

        public int AddComment(PASTEBOOK_COMMENT comment)
        {
            int result = 0;

            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    context.PASTEBOOK_COMMENT.Add(comment);
                    result = context.SaveChanges();
                    result = comment.ID;
                }
            }
            catch
            {

            }

            return result;
        }

        public List<PASTEBOOK_LIKE> RetrieveLike(int postId)
        {
            List<PASTEBOOK_LIKE> listOfLikes = new List<PASTEBOOK_LIKE>();

            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    var result = context.PASTEBOOK_LIKE.Where(x => x.POST_ID == postId);

                    foreach (var like in result)
                    {
                        listOfLikes.Add(like);
                    }
                }
            }
            catch 
            {
                
            }

            return listOfLikes;
        }

        public List<PASTEBOOK_COMMENT> RetrieveComment(int postId)
        {
            List<PASTEBOOK_COMMENT> listOfComments = new List<PASTEBOOK_COMMENT>();

            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    var result = context.PASTEBOOK_COMMENT.Where(x => x.POST_ID == postId);

                    foreach (var comment in result)
                    {
                        listOfComments.Add(comment);
                    }
                }
            }
            catch
            {

            }

            return listOfComments;
        }
    }
}