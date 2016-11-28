using Pastebook.Models;
using PastebookBusinessLayer.BusinessLayer;
using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pastebook.Controllers
{
    public class FriendController : Controller
    {
        AccountManager accountManager = new AccountManager();
        FriendManager friendManager = new FriendManager();
        NotificationManager notificationManager = new NotificationManager();

        [PastebookAuthorize]
        [HttpGet, Route("friends/all")]
        public ActionResult Friends()
        {
            List<FRIEND> listOfFriend = new List<FRIEND>();
            FriendListViewModel friendListViewModel = new FriendListViewModel();
            List<int> listOfFriendId = new List<int>();

            listOfFriend = friendManager.GetFriendList((int)Session["UserId"]);

            foreach (var friend in listOfFriend)
            {
                if (friend.USER_ID == (int)Session["UserId"])
                {
                    friendListViewModel.ListOfFriendsWithDetails.Add(new FriendUserModel() { Friend = friend, FriendDetails = friend.USER });
                }
            }

            return View("~/Views/Pastebook/Friends.cshtml",friendListViewModel);
        }

        public PartialViewResult GetFriendList(int id)
        {
            List<FRIEND> listOfFriend = new List<FRIEND>();
            FriendListViewModel friendListViewModel = new FriendListViewModel();
            List<int> listOfFriendId = new List<int>();

            listOfFriend = friendManager.GetFriendList(id);

            foreach (var friend in listOfFriend)
            {
                if (friend.USER_ID == id)
                {
                    friendListViewModel.ListOfFriendsWithDetails.Add(new FriendUserModel() { Friend = friend, FriendDetails = friend.USER });
                }
            }

            friendListViewModel.ListOfFriendsWithDetails.OrderBy(x => x.FriendDetails.FIRST_NAME);

            return PartialView("~/Views/Pastebook/_FriendListPartialView.cshtml", friendListViewModel);
        }

        public ActionResult GetInteractButtons(int id)
        {
            List<int> listOfFriendId = new List<int>();
            ProfileViewModel profileViewModel = new ProfileViewModel();
            profileViewModel.User = accountManager.GetUser(x => x.ID == id);
            profileViewModel.ListOfFriends = friendManager.GetListOfFriendRequest(id);
            return PartialView("~/Views/Pastebook/_InteractButtonsPartialView.cshtml", profileViewModel);
        }

        public JsonResult AddFriend(int friendId)
        {
            FRIEND friend = new FRIEND();
            friend.USER_ID = (int)Session["UserId"];
            friend.FRIEND_ID = friendId;
            friend.CREATED_DATE = DateTime.Now;
            friend.BLOCKED = "N";
            friend.REQUEST = "N";

            NOTIFICATION addFriendNotification = new NOTIFICATION();
            addFriendNotification.NOTIF_TYPE = "F";
            addFriendNotification.SEEN = "N";
            addFriendNotification.POST_ID = null;
            addFriendNotification.CREATED_DATE = DateTime.Now;
            addFriendNotification.RECEIVER_ID = friendId;
            addFriendNotification.SENDER_ID = (int)Session["UserId"];
            addFriendNotification.COMMENT_ID = null;

            notificationManager.CreateNotification(addFriendNotification);

            return Json(new { result = friendManager.AddFriendRequest(friend) });
        }

        public JsonResult RejectFriendRequest(int friendId)
        {
            FRIEND friendRequest = new FRIEND();
            friendRequest = friendManager.GetFriendRequest(friendId);

            bool result = false;
            result = friendManager.RejectFriendRequest(friendRequest);

            return Json(new { result = result });
        }

        public JsonResult AcceptFriend(int friendRequestId)
        {
            bool result = false;
            FRIEND friendRequest = new FRIEND();
            friendRequest = friendManager.GetFriendRequest(friendRequestId);
            friendRequest.REQUEST = "Y";
            result = friendManager.AcceptFriendRequest(friendRequest);


            FRIEND newFriend = new FRIEND();

            newFriend.FRIEND_ID = friendRequest.USER_ID;
            newFriend.USER_ID = friendRequest.FRIEND_ID;
            newFriend.REQUEST = "Y";
            newFriend.CREATED_DATE = DateTime.Now;
            newFriend.BLOCKED = "N";

            result = friendManager.AddFriendRequest(newFriend);


            return Json(new { result = result });
        }

    }
}