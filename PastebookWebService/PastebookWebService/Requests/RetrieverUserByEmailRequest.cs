using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PastebookWebService.Requests
{
    [DataContract]
    public class RetrieverUserByEmailRequest
    {
        [DataMember]
        public string EmailAddress { get; set; }
    }
}