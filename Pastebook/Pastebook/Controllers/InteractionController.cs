using Pastebook.Models;
using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PastebookBusinessLayer.BusinessLayer;
using System.Text.RegularExpressions;

namespace Pastebook.Controllers
{
    public class InteractionController : Controller
    {
        InteractionManager interactionManager = new InteractionManager();
        PostManager postManager = new PostManager();
        NotificationManager notificationManager = new NotificationManager();
        AccountManager accountManager = new AccountManager();
        ValidationManager validator = new ValidationManager();

        public JsonResult AddLike(int postId)
        {
            PASTEBOOK_LIKE like = new PASTEBOOK_LIKE();
            like.POST_ID = postId;
            like.LIKED_BY = (int)Session["UserId"];

            bool result = false;
            result = interactionManager.Like(like);

            int posterId = postManager.GetPost(postId).POSTER_ID;

            if (posterId != (int)Session["UserId"])
            {
                PASTEBOOK_NOTIFICATION likePostNotification = new PASTEBOOK_NOTIFICATION();
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
            PASTEBOOK_LIKE like = new PASTEBOOK_LIKE();

            like = interactionManager.GetLike(likeId);
            bool result = false;

            if (like.LIKED_BY != (int)Session["UserId"])
            {
                notificationManager.DeleteNotification(notificationManager.FindNotification(x => x.POST_ID == interactionManager.GetLike(likeId).POST_ID && x.RECEIVER_ID == (int)Session["UserId"]));
            }

            result = interactionManager.Unlike(like);

            return Json(new { result = result });
        }

        public JsonResult AddComment(int postId, string content)
        {
            PASTEBOOK_COMMENT comment = new PASTEBOOK_COMMENT();
            comment.POST_ID = postId;
            comment.CONTENT = content.Trim();
            comment.DATE_CREATED = DateTime.Now;
            comment.POSTER_ID = (int)Session["UserId"];
            bool result = false;
            result = interactionManager.AddComment(comment);

            int posterId = postManager.GetPost(postId).POSTER_ID;

            if (posterId != (int)Session["UserId"])
            {
                PASTEBOOK_NOTIFICATION commentPostNotification = new PASTEBOOK_NOTIFICATION();
                commentPostNotification.NOTIF_TYPE = "C";
                commentPostNotification.SEEN = "N";
                commentPostNotification.POST_ID = postId;
                commentPostNotification.CREATED_DATE = DateTime.Now;
                commentPostNotification.RECEIVER_ID = posterId;
                commentPostNotification.SENDER_ID = (int)Session["UserId"];
                commentPostNotification.COMMENT_ID = comment.ID;

                notificationManager.CreateNotification(commentPostNotification);
            }

            return Json(new { result = result });
        }

        public JsonResult CheckIfCommentIsValid(string content)
        {
            string errorText = string.Empty;

            if (validator.CheckIfWhiteSpace(content))
            {
                errorText = "Commenting white spaces is not allowed";
            }

            if (validator.CheckIfIsNullOrEmpty(content))
            {
                errorText = "Commenting empty content is not allowed";
            }

            if (validator.CheckIfOutOfStringLimit(content, 1000))
            {
                errorText = "Maximum characters for commenting is 1000 characters";
            }

            return Json(new {result = errorText}, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetLikes(int postId)
        {
            List<PASTEBOOK_LIKE> listOfLikes = new List<PASTEBOOK_LIKE>();
            listOfLikes = interactionManager.GetLikeWithUser(x=>x.POST_ID == postId);

            return PartialView("~/Views/Pastebook/_LikePartialView.cshtml", listOfLikes);
        }

        public PartialViewResult GetNotifications()
        {
            List<NotificationViewModel> listOfNotificationWithSender = new List<NotificationViewModel>();
            List<PASTEBOOK_NOTIFICATION> listOfNotification = new List<PASTEBOOK_NOTIFICATION>();

            listOfNotification = notificationManager.GetNotificationWithUser(notification => notification.RECEIVER_ID == (int)Session["UserId"] && notification.SENDER_ID != (int)Session["UserId"]);

            return PartialView("~/Views/Pastebook/_NotificationPartialView.cshtml", listOfNotification);
        }

        public PartialViewResult GetNotificationItems()
        {
            List<NotificationViewModel> listOfNotificationWithSender = new List<NotificationViewModel>();
            List<PASTEBOOK_NOTIFICATION> listOfNotification = new List<PASTEBOOK_NOTIFICATION>();

            listOfNotification = notificationManager.GetNotificationWithUser(notification => notification.RECEIVER_ID == (int)Session["UserId"]);

            return PartialView("~/Views/Pastebook/_NotificationItemPartialView.cshtml", listOfNotification.ToList());

        }

        public JsonResult SawNotification()
        {
            List<NotificationViewModel> listOfNotificationWithSender = new List<NotificationViewModel>();
            List<PASTEBOOK_NOTIFICATION> listOfNotification = new List<PASTEBOOK_NOTIFICATION>();

            listOfNotification = notificationManager.GetListOfUnseenNotification((int)Session["UserId"]);

            bool result = false;

            foreach (var notification in listOfNotification)
            {
                notification.SEEN = "Y";
                result = notificationManager.UpdateNotification(notification);
            }

            return Json(new { result = result });
        }

        public ActionResult GetInteractButtons(int id)
        {
            List<int> listOfFriendId = new List<int>();
            ProfileViewModel profileViewModel = new ProfileViewModel();
            profileViewModel.User = accountManager.GetUser(x=>x.ID == id);
            profileViewModel.ListOfFriends = interactionManager.GetListOfFriendRequest(id);
            return PartialView("~/Views/Pastebook/_InteractButtonsPartialView.cshtml", profileViewModel);
        }

        public JsonResult AddFriend(int friendId)
        {
            PASTEBOOK_FRIEND friend = new PASTEBOOK_FRIEND();
            friend.USER_ID = (int)Session["UserId"];
            friend.FRIEND_ID = friendId;
            friend.CREATED_DATE = DateTime.Now;
            friend.IsBLOCKED = "N";
            friend.REQUEST = "N";

            PASTEBOOK_NOTIFICATION addFriendNotification = new PASTEBOOK_NOTIFICATION();
            addFriendNotification.NOTIF_TYPE = "F";
            addFriendNotification.SEEN = "N";
            addFriendNotification.POST_ID = null;
            addFriendNotification.CREATED_DATE = DateTime.Now;
            addFriendNotification.RECEIVER_ID = friendId;
            addFriendNotification.SENDER_ID = (int)Session["UserId"];
            addFriendNotification.COMMENT_ID = null;

            notificationManager.CreateNotification(addFriendNotification);

            return Json(new { result = interactionManager.AddFriendRequest(friend)});
        }

        public JsonResult RejectFriendRequest(int friendId)
        {
            PASTEBOOK_FRIEND friendRequest = new PASTEBOOK_FRIEND();
            friendRequest = interactionManager.GetFriendRequest(friendId);

            bool result = false;
            result = interactionManager.RejectFriendRequest(friendRequest);

            return Json(new { result = result });
        }

        [HttpGet, Route("notification/viewallnotification")]
        public ActionResult Notification()
        {
            List<NotificationViewModel> listOfNotificationWithSender = new List<NotificationViewModel>();
            List<PASTEBOOK_NOTIFICATION> listOfNotification = new List<PASTEBOOK_NOTIFICATION>();

            listOfNotification = notificationManager.GetNotificationWithUser(notification => notification.RECEIVER_ID == (int)Session["UserId"]);

            return View("~/Views/Pastebook/Notifications.cshtml", listOfNotification.ToList());
        }

        public JsonResult GetNotificationsCount()
        {
            int notifCount = 0;

            if (Session != null && Session["UserId"] != null)
            {
                notifCount = notificationManager.GetListOfUnseenNotification((int)Session["UserId"]).Count();
            }

            return Json(new { result = notifCount }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AcceptFriend(int friendRequestId)
        {
            bool result = false;
            PASTEBOOK_FRIEND friendRequest = new PASTEBOOK_FRIEND();
            friendRequest = interactionManager.GetFriendRequest(friendRequestId);
            friendRequest.REQUEST = "Y";
            result = interactionManager.AcceptFriendRequest(friendRequest);


            PASTEBOOK_FRIEND newFriend = new PASTEBOOK_FRIEND();

            newFriend.FRIEND_ID = friendRequest.USER_ID;
            newFriend.USER_ID = friendRequest.FRIEND_ID;
            newFriend.REQUEST = "Y";
            newFriend.CREATED_DATE = DateTime.Now;
            newFriend.IsBLOCKED = "N";

            result = interactionManager.AddFriendRequest(newFriend);


            return Json(new { result = result });
        }

    }
}