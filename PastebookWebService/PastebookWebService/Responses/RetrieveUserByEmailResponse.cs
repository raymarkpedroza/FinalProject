using PastebookWebService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PastebookWebService.Responses
{
    [DataContract]
    public class RetrieveUserByEmailResponse
    {
        public RetrieveUserByEmailResponse()
        {
            User = new UserEntity();
        }

        [DataMember]
        public UserEntity User { get; set; }
    }
}