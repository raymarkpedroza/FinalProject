using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Pastebook.Models;
using PastebookEF;

namespace Pastebook.Managers
{
    public class FriendManager
    {
        public int AddFriend(PASTEBOOK_FRIEND friend)
        {
            int result = 0;

            return result;
        }

        //public int AcceptFriend(int friendRequestId, int userId, int receiverId)
        //{
        //    AcceptFriendRequestRequest request = new AcceptFriendRequestRequest();
        //    request.FriendId = friendRequestId;
        //    request.Request = "Y";

        //    AcceptFriendRequestResponse response = new AcceptFriendRequestResponse();
        //    response = pastebookServiceClient.AcceptFriendRequest(request);

        //    AddNotificationRequest addNotificationRequest = new AddNotificationRequest();
        //    addNotificationRequest.Notification.NotificationType = "F";
        //    addNotificationRequest.Notification.Seen = 'N';
        //    addNotificationRequest.Notification.CommentId = null;
        //    addNotificationRequest.Notification.PostId = null;
        //    addNotificationRequest.Notification.CreatedDate = DateTime.Now;
        //    addNotificationRequest.Notification.SenderId = userId;
        //    addNotificationRequest.Notification.ReceiverId = receiverId;

        //    AddNotificationResponse addNotificationResponse = new AddNotificationResponse();
        //    addNotificationResponse = pastebookServiceClient.AddNotification(addNotificationRequest);

        //    return response.Result;
        //}

        //public List<FriendModel> RetrieveFriends(int id)
        //{
        //    List<FriendModel> listOfFriends = new List<FriendModel>();

        //    RetrieveFriendsRequest request = new RetrieveFriendsRequest();
        //    request.UserId = id;

        //    RetrieveFriendsResponse response = new RetrieveFriendsResponse();
        //    response = pastebookServiceClient.RetrieveFriends(request);

        //    foreach (var friend in response.listOfFriends)
        //    {
        //        listOfFriends.Add(Mapper.MapWCFFriendEntityToMVCFriendModel(friend));
        //    }

        //    return listOfFriends;
        //}

    }
}