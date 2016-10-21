using Pastebook.Managers;
using Pastebook.Models;
using PastebookDataAccess.Managers;
using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pastebook.Controllers
{
    public class ReactionController : Controller
    {
        DataAccessReactionManager daReactionManager = new DataAccessReactionManager();
        DataAccessUserManager daUserManager = new DataAccessUserManager();
        DataAccessCountryManager daCountryManager = new DataAccessCountryManager();
        DataAccessFriendManager daFriendManager = new DataAccessFriendManager();
        DataAccessPostManager daPostManager  = new DataAccessPostManager();
        DataAccessNotificationManager daNotifactionManager = new DataAccessNotificationManager();

        public JsonResult AddLike(int postId)
        {
            PASTEBOOK_LIKE like = new PASTEBOOK_LIKE();
            like.POST_ID = postId;
            like.LIKED_BY = (int)Session["UserId"];
            int result = 0;
            result = daReactionManager.AddLike(like);

            PASTEBOOK_NOTIFICATION posterLikeNotification = new PASTEBOOK_NOTIFICATION();
            posterLikeNotification.NOTIF_TYPE = "L";
            posterLikeNotification.SEEN = "N";
            posterLikeNotification.POST_ID = postId;
            posterLikeNotification.CREATED_DATE = DateTime.Now;
            posterLikeNotification.RECEIVER_ID = daPostManager.RetrievePost(postId).POSTER_ID;
            posterLikeNotification.SENDER_ID = (int)Session["UserId"];
            posterLikeNotification.COMMENT_ID = null;

            PASTEBOOK_NOTIFICATION profileOwnerLikeNotification = new PASTEBOOK_NOTIFICATION();
            profileOwnerLikeNotification.NOTIF_TYPE = "L";
            profileOwnerLikeNotification.SEEN = "N";
            profileOwnerLikeNotification.POST_ID = postId;
            profileOwnerLikeNotification.CREATED_DATE = DateTime.Now;
            profileOwnerLikeNotification.RECEIVER_ID = daPostManager.RetrievePost(postId).PROFILE_OWNER_ID;
            profileOwnerLikeNotification.SENDER_ID = (int)Session["UserId"];
            profileOwnerLikeNotification.COMMENT_ID = null;

            daNotifactionManager.AddNotification(posterLikeNotification);
            daNotifactionManager.AddNotification(profileOwnerLikeNotification);

            return Json(new { result = result });
        }

        public JsonResult AddComment(int postId, string content)
        {
            PASTEBOOK_COMMENT comment = new PASTEBOOK_COMMENT();
            comment.POST_ID = postId;
            comment.CONTENT = content;
            comment.DATE_CREATED = DateTime.Now;
            comment.POSTER_ID = (int)Session["UserId"];
            int result = 0;
            result = daReactionManager.AddComment(comment);

            PASTEBOOK_NOTIFICATION posterCommentNotification = new PASTEBOOK_NOTIFICATION();
            posterCommentNotification.NOTIF_TYPE = "C";
            posterCommentNotification.SEEN = "N";
            posterCommentNotification.POST_ID = postId;
            posterCommentNotification.CREATED_DATE = DateTime.Now;
            posterCommentNotification.RECEIVER_ID = daPostManager.RetrievePost(postId).POSTER_ID;
            posterCommentNotification.SENDER_ID = (int)Session["UserId"];
            //posterCommentNotification.COMMENT_ID = daReactionManager.RetrieveComment(postId).Select(x => x.ID).SingleOrDefault(); ;

            PASTEBOOK_NOTIFICATION profileOwnerCommentNotification = new PASTEBOOK_NOTIFICATION();
            profileOwnerCommentNotification.NOTIF_TYPE = "C";
            profileOwnerCommentNotification.SEEN = "N";
            profileOwnerCommentNotification.POST_ID = postId;
            profileOwnerCommentNotification.CREATED_DATE = DateTime.Now;
            profileOwnerCommentNotification.RECEIVER_ID = daPostManager.RetrievePost(postId).PROFILE_OWNER_ID;
            profileOwnerCommentNotification.SENDER_ID = (int)Session["UserId"];
            //profileOwnerCommentNotification.COMMENT_ID = null;

            daNotifactionManager.AddNotification(posterCommentNotification);
            daNotifactionManager.AddNotification(profileOwnerCommentNotification);

            return Json(new { result = result });
        }

        public PartialViewResult GetFriendRequests()
        {
            List<FriendRequestViewModel> listOfFriendRequestsWithRequester = new List<FriendRequestViewModel>();
            List<PASTEBOOK_FRIEND> listOfFriendRequests = new List<PASTEBOOK_FRIEND>();

            listOfFriendRequests = daFriendManager.RetrieveFriends((int)Session["UserId"]);

            foreach (var friendRequest in listOfFriendRequests)
            {
                listOfFriendRequestsWithRequester.Add(new FriendRequestViewModel() { FriendRequest = friendRequest, Requester = daUserManager.RetrieveUserById(friendRequest.USER_ID) });
            }

            return PartialView("~/Views/Pastebook/_FriendRequestPartialView.cshtml", listOfFriendRequestsWithRequester);
        }

        public PartialViewResult GetNotifications()
        {
            List<NotificationViewModel> listOfNotificationWithSender = new List<NotificationViewModel>();
            List<PASTEBOOK_NOTIFICATION> listOfNotification = new List<PASTEBOOK_NOTIFICATION>();

            listOfNotification = daNotifactionManager.RetrieveNotifications((int)Session["UserId"]);

            foreach (var notification in listOfNotification)
            {
                listOfNotificationWithSender.Add(new NotificationViewModel() { Notification = notification, Sender = daUserManager.RetrieveUserById(notification.SENDER_ID), ListOfPosts=daPostManager.RetrievePosts(notification.RECEIVER_ID, new List<int>()) });
            }

            return PartialView("~/Views/Pastebook/_NotificationPartialView.cshtml",listOfNotificationWithSender);
        }

        public ActionResult GetInteractButtons(int id)
        {
            ProfileViewModel profileViewModel = new ProfileViewModel();
            profileViewModel.User = daUserManager.RetrieveUserById(id);
            profileViewModel.ListOfFriends = daFriendManager.RetrieveFriends(id);
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

            int result = 0;

            result = daFriendManager.AddFriend(friend);

            return Json(new { result = result });
        }

        public JsonResult AcceptFriend(int friendRequestId)
        {
            int result = 0;
            result = daFriendManager.AcceptFriendRequest(friendRequestId, "Y");

            PASTEBOOK_NOTIFICATION acceptFriendNotification = new PASTEBOOK_NOTIFICATION();
            acceptFriendNotification.NOTIF_TYPE = "F";
            acceptFriendNotification.SEEN = "N";
            acceptFriendNotification.POST_ID = null;
            acceptFriendNotification.CREATED_DATE = DateTime.Now;
            acceptFriendNotification.RECEIVER_ID = daFriendManager.RetrieveFriend(friendRequestId).USER_ID;
            acceptFriendNotification.SENDER_ID = (int)Session["UserId"];
            acceptFriendNotification.COMMENT_ID = null;

            daNotifactionManager.AddNotification(acceptFriendNotification);

            return Json(new { result = result });
        }

    }
}