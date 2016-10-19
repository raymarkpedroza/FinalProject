using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PastebookWebService.Requests
{
    [DataContract]
    public class RetrieveUserByIdRequest
    {
        [DataMember]
        public int Id { get; set; }
    }
}