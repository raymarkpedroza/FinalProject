using PastebookWebService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PastebookWebService.Responses
{
    [DataContract]
    public class RetrievePostsResponse
    {
        public RetrievePostsResponse()
        {
            ListOfPosts = new List<PostEntity>();
        }

        [DataMember]
        public List<PostEntity> ListOfPosts { get; set; }
    }
}