using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PastebookWebService.Responses
{
    [DataContract]
    public class EncryptPasswordResponse
    {
        [DataMember]
        public string Salt { get; set; }

        [DataMember]
        public string HashPassword { get; set; }
    }
}