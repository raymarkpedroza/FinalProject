using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PastebookWebService.Requests
{
    [DataContract]
    public class PasswordMatchRequest
    {
        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string Salt { get; set; }

        [DataMember]
        public string HashPassword { get; set; }
    }
}