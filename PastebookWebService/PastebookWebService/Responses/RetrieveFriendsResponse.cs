using PastebookWebService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PastebookWebService.Responses
{
    [DataContract]
    public class RetrieveFriendsResponse
    {
        public RetrieveFriendsResponse()
        {
            listOfFriends = new List<FriendEntity>();
        }

        [DataMember]
        public List<FriendEntity> listOfFriends { get; set; }
    }
}