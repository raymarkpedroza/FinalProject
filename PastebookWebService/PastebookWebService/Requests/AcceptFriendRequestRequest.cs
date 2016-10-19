using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PastebookWebService.Requests
{
    [DataContract]
    public class AcceptFriendRequestRequest
    {
        [DataMember]
        public int FriendId { get; set; }

        [DataMember]
        public string Request { get; set; }
    }
}