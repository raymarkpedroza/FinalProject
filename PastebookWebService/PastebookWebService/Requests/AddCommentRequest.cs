using PastebookWebService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PastebookWebService.Requests
{
    [DataContract]
    public class AddCommentRequest
    {
        public AddCommentRequest()
        {
            Comment = new CommentEntity();
        }
        [DataMember]
        public CommentEntity Comment { get; set; }
    }
}