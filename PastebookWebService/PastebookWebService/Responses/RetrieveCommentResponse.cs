using PastebookWebService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PastebookWebService.Responses
{
    [DataContract]
    public class RetrieveCommentResponse
    {
        public RetrieveCommentResponse()
        {
            ListOfComments = new List<CommentEntity>();
        }

        [DataMember]
        public List<CommentEntity> ListOfComments { get; set; }
    }
}