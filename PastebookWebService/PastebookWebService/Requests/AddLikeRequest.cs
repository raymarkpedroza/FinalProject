using PastebookWebService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PastebookWebService.Requests
{
    [DataContract]
    public class AddLikeRequest
    {
        public AddLikeRequest()
        {
            Like = new LikeEntity();
        }

        [DataMember]
        public LikeEntity Like { get; set; }
    }
}