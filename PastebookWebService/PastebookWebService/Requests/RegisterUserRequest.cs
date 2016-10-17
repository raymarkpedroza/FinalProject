using PastebookWebService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PastebookWebService.Requests
{
    [DataContract]
    public class RegisterUserRequest
    {
        public RegisterUserRequest()
        {
            wcfUserEntity = new UserEntity();
        }

        [DataMember]
        public UserEntity wcfUserEntity { get; set; }
    }
}