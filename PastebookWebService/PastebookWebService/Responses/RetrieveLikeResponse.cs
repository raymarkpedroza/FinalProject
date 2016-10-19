using PastebookWebService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PastebookWebService.Responses
{
    [DataContract]
    public class RetrieveLikeResponse
    {
        public RetrieveLikeResponse()
        {
            ListOfLikes = new List<LikeEntity>();
        }

        [DataMember]
        public List<LikeEntity> ListOfLikes { get; set; }
    }
}