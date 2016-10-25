using Pastebook.Managers;
using Pastebook.Models;
using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PastebookBusinessLayer.BusinessLayer;

namespace Pastebook.Controllers
{
    public class ReactionController : Controller
    {
        InteractionManager interactionManager = new InteractionManager();
        PostManager postManager = new PostManager();
        NotificationManager notificationManager = new NotificationManager();
        AccountManager accountManager = new AccountManager();

        public JsonResult AddLike(int postId)
        {
            PASTEBOOK_LIKE like = new PASTEBOOK_LIKE();
            like.POST_ID = postId;
            like.LIKED_BY = (int)Session["UserId"];

            bool result = false;
            result = interactionManager.LikePost(like);

            PASTEBOOK_NOTIFICATION likePostNotification = new PASTEBOOK_NOTIFICATION();
            likePostNotification.NOTIF_TYPE = "L";
            likePostNotification.SEEN = "N";
            likePostNotification.POST_ID = postId;
            likePostNotification.CREATED_DATE = DateTime.Now;
            likePostNotification.RECEIVER_ID = postManager.RetrievePost(postId).POSTER_ID;
            likePostNotification.SENDER_ID = (int)Session["UserId"];
            likePostNotification.COMMENT_ID = null;

            notificationManager.CreateNotification(likePostNotification);
            return Json(new { result = result });
        }

        public JsonResult Unlike(int likeId)
        {
            PASTEBOOK_LIKE like = new PASTEBOOK_LIKE();

            like = interactionManager.RetrieveLike(likeId);
            bool result = false;

            notificationManager.DeleteNotification(notificationManager.RetrieveLikeNotificationByPostIdAndUserId(interactionManager.RetrieveLike(likeId).POST_ID, (int)Session["UserId"]));
            result = interactionManager.UnlikePost(like);
            return Json(new { result = result });
        }

        public JsonResult AddComment(int postId, string content)
        {
            PASTEBOOK_COMMENT comment = new PASTEBOOK_COMMENT();
            comment.POST_ID = postId;
            comment.CONTENT = content;
            comment.DATE_CREATED = DateTime.Now;
            comment.POSTER_ID = (int)Session["UserId"];
            bool result = false;
            result = interactionManager.CommentOnPost(comment);

            PASTEBOOK_NOTIFICATION commentPostNotification = new PASTEBOOK_NOTIFICATION();
            commentPostNotification.NOTIF_TYPE = "C";
            commentPostNotification.SEEN = "N";
            commentPostNotification.POST_ID = postId;
            commentPostNotification.CREATED_DATE = DateTime.Now;
            commentPostNotification.RECEIVER_ID = postManager.RetrievePost(postId).POSTER_ID;
            commentPostNotification.SENDER_ID = (int)Session["UserId"];
            commentPostNotification.COMMENT_ID = comment.ID;

            notificationManager.CreateNotification(commentPostNotification);

            return Json(new { result = result });
        }

        public PartialViewResult GetFriendRequests()
        {
            List<FriendRequestViewModel> listOfFriendRequestsWithRequester = new List<FriendRequestViewModel>();
            List<PASTEBOOK_FRIEND> listOfFriendRequests = new List<PASTEBOOK_FRIEND>();
            List<int> listOfFriendId = new List<int>();

            listOfFriendRequests = interactionManager.RetrieveFriends((int)Session["UserId"], out listOfFriendId);


            return PartialView("~/Views/Pastebook/_FriendRequestPartialView.cshtml", listOfFriendRequests);
        }

        public PartialViewResult GetLikes(int postId)
        {
            List<PASTEBOOK_LIKE> listOfLikes = new List<PASTEBOOK_LIKE>();
            listOfLikes = interactionManager.RetrieveLikes(postId);

            return PartialView("~/Views/Pastebook/_LikePartialView.cshtml", listOfLikes);
        }

        public PartialViewResult GetNotifications()
        {
            List<NotificationViewModel> listOfNotificationWithSender = new List<NotificationViewModel>();
            List<PASTEBOOK_NOTIFICATION> listOfNotification = new List<PASTEBOOK_NOTIFICATION>();

            listOfNotification = notificationManager.RetrieveNotificationByUserId((int)Session["UserId"]);

            return PartialView("~/Views/Pastebook/_NotificationPartialView.cshtml", listOfNotification.OrderByDescending(x=>x.CREATED_DATE).ToList());
        }

        public ActionResult GetInteractButtons(int id)
        {
            List<int> listOfFriendId = new List<int>();
            ProfileViewModel profileViewModel = new ProfileViewModel();
            profileViewModel.User = accountManager.RetrieveUserById(id);
            profileViewModel.ListOfFriends = interactionManager.RetrieveFriends(id, out listOfFriendId);
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

            bool result = false;

            result = interactionManager.AddFriendRequest(friend);

            return Json(new { result = result });
        }

        public JsonResult AcceptFriend(int friendRequestId)
        {
            bool result = false;
            PASTEBOOK_FRIEND friendRequest = new PASTEBOOK_FRIEND();
            friendRequest = interactionManager.RetrieveFriendRequest(friendRequestId);
            friendRequest.REQUEST = "Y";
            result = interactionManager.UpdateFriendRequest(friendRequest);

            PASTEBOOK_NOTIFICATION acceptFriendNotification = new PASTEBOOK_NOTIFICATION();
            acceptFriendNotification.NOTIF_TYPE = "F";
            acceptFriendNotification.SEEN = "N";
            acceptFriendNotification.POST_ID = null;
            acceptFriendNotification.CREATED_DATE = DateTime.Now;
            acceptFriendNotification.RECEIVER_ID = interactionManager.RetrieveFriendRequest(friendRequestId).USER_ID;
            acceptFriendNotification.SENDER_ID = (int)Session["UserId"];
            acceptFriendNotification.COMMENT_ID = null;

            notificationManager.CreateNotification(acceptFriendNotification);

            return Json(new { result = result });
        }

    }
}