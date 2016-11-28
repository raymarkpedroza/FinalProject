using PastebookBusinessLayer.BusinessLayer;
using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pastebook.Controllers
{
    public class LikeController : Controller
    {
        LikeManager likeManager = new LikeManager();
        PostManager postManager = new PostManager();
        NotificationManager notificationManager = new NotificationManager();

        public JsonResult AddLike(int postId)
        {
            LIKE like = new LIKE();
            like.POST_ID = postId;
            like.LIKED_BY = (int)Session["UserId"];

            bool result = false;
            result = likeManager.Like(like);

            int posterId = postManager.GetPost(postId).POSTER_ID;

            if (posterId != (int)Session["UserId"])
            {
                NOTIFICATION likePostNotification = new NOTIFICATION();
                likePostNotification.NOTIF_TYPE = "L";
                likePostNotification.SEEN = "N";
                likePostNotification.POST_ID = postId;
                likePostNotification.CREATED_DATE = DateTime.Now;
                likePostNotification.RECEIVER_ID = posterId;
                likePostNotification.SENDER_ID = (int)Session["UserId"];
                likePostNotification.COMMENT_ID = null;

                notificationManager.CreateNotification(likePostNotification);
            }

            return Json(new { result = result });
        }


        public JsonResult Unlike(int likeId)
        {
            LIKE like = new LIKE();

            like = likeManager.GetLike(likeId);
            bool result = false;

            if (like.LIKED_BY != (int)Session["UserId"])
            {
                notificationManager.DeleteNotification(notificationManager.FindNotification(x => x.POST_ID == likeManager.GetLike(likeId).POST_ID && x.RECEIVER_ID == (int)Session["UserId"]));
            }

            result = likeManager.Unlike(like);

            return Json(new { result = result });
        }


        public PartialViewResult GetLikes(int postId)
        {
            List<LIKE> listOfLikes = new List<LIKE>();
            listOfLikes = likeManager.GetLikeWithUser(x => x.POST_ID == postId);

            return PartialView("~/Views/Pastebook/_LikePartialView.cshtml", listOfLikes);
        }
    }
}