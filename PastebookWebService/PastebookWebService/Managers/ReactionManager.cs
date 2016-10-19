using PastebookEF;
using PastebookWebService.Entities;
using PastebookWebService.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PastebookWebService.Managers
{
    public class ReactionManager
    {
        public int AddLike(LikeEntity like)
        {
            int result = 0;

            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    context.PASTEBOOK_LIKE.Add(Mapper.MapWCFLikeEntityToDBLikeTable(like));
                    result = context.SaveChanges();
                }
            }
            catch
            {

            }

            return result;
        }

        public int AddComment(CommentEntity comment)
        {
            int result = 0;

            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    context.PASTEBOOK_COMMENT.Add(Mapper.MapWCFCommentEntityToDBCommentTable(comment));
                    result = context.SaveChanges();
                }
            }
            catch
            {

            }

            return result;
        }

        public List<LikeEntity> RetrieveLike(int postId)
        {
            List<LikeEntity> listOfLikes = new List<LikeEntity>();

            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    var result = context.PASTEBOOK_LIKE.Where(x => x.POST_ID == postId);

                    foreach (var like in result)
                    {
                        listOfLikes.Add(Mapper.MapDBLikeTableToWCFLikeEntity(like));
                    }
                }
            }
            catch 
            {
                
            }

            return listOfLikes;
        }

        public List<CommentEntity> RetrieveComment(int postId)
        {
            List<CommentEntity> listOfComments = new List<CommentEntity>();

            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    var result = context.PASTEBOOK_COMMENT.Where(x => x.POST_ID == postId);

                    foreach (var comment in result)
                    {
                        listOfComments.Add(Mapper.MapDBCommentTableToWCFCommentEntity(comment));
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