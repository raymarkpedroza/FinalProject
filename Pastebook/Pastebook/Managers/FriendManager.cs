using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Pastebook.PastebookServiceReference;
using Pastebook.Models;
using Pastebook.Mappers;

namespace Pastebook.Managers
{
    public class FriendManager
    {
        PastebookServiceClient pastebookServiceClient = new PastebookServiceClient();

        public int AddFriend(FriendModel friend)
        {
            AddFriendRequest request = new AddFriendRequest();
            request.Friend = Mapper.MapMVCFriendModelToWCFFriendEntity(friend);

            AddFriendResponse response = new AddFriendResponse();
            response = pastebookServiceClient.AddFriend(request);

            return response.Result;
        }

        public List<FriendModel> RetrieveFriends(int id)
        {
            List<FriendModel> listOfFriends = new List<FriendModel>();

            RetrieveFriendsRequest request = new RetrieveFriendsRequest();
            request.UserId = id;

            RetrieveFriendsResponse response = new RetrieveFriendsResponse();
            response = pastebookServiceClient.RetrieveFriends(request);

            foreach (var friend in response.listOfFriends)
            {
                listOfFriends.Add(Mapper.MapWCFFriendEntityToMVCFriendModel(friend));
            }

            return listOfFriends;
        }

    }
}