using Pastebook.Managers;
using Pastebook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pastebook.Controllers
{
    public class ReactionController : Controller
    {
        ReactionManager reactionManager = new ReactionManager();
        public JsonResult AddLike(int postId)
        {
            LikeModel like = new LikeModel();
            like.PostId = postId;
            like.LikedBy = (int)Session["UserId"];
            int result = 0;
            result = reactionManager.AddLike(like);

            return Json(new { result = result });
        }

        public JsonResult AddComment(int postId, string content)
        {
            CommentModel comment = new CommentModel();
            comment.PostId = postId;
            comment.Content = content;
            comment.DateCreated = DateTime.Now;
            comment.PosterId = (int)Session["UserId"];
            int result = 0;
            result = reactionManager.AddComment(comment);

            return Json(new { result = result });
        }
    }
}