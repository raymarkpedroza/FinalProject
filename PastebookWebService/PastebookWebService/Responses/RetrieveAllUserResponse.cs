using PastebookWebService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PastebookWebService.Responses
{
    [DataContract]
    public class RetrieveAllUserResponse
    {
        public RetrieveAllUserResponse()
        {
            ListOfUser = new List<UserEntity>();
        }

        [DataMember]
        public List<UserEntity> ListOfUser { get; set; }
    }
}