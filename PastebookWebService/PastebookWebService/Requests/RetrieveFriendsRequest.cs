using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PastebookWebService.Requests
{
    [DataContract]
    public class RetrieveFriendsRequest
    {
        [DataMember]
        public int UserId { get; set; }
    }
}