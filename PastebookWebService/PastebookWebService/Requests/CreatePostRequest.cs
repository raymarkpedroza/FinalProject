using PastebookWebService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PastebookWebService.Requests
{
    [DataContract]
    public class CreatePostRequest
    {
        public CreatePostRequest()
        {
            Post = new PostEntity();
        }

        [DataMember]
        public PostEntity Post { get; set; }
    }
}