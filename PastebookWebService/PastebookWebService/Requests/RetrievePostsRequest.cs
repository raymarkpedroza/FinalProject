using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PastebookWebService.Requests
{
    [DataContract]
    public class RetrievePostsRequest
    {
        public RetrievePostsRequest()
        {
            ListOfFriendsId = new List<int>();
        }
        
        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public List<int> ListOfFriendsId { get; set; }
    }
}