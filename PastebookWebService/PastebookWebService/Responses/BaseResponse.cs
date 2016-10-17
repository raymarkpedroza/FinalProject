using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PastebookWebService.Responses
{
    [DataContract]
    public class BaseResponse
    {
        public BaseResponse()
        {
            ListOfErrors = new List<string>();
            ListOfExceptions = new List<Exception>();
        }

        [DataMember]
        public List<string> ListOfErrors { get; set; }

        [DataMember]
        public List<Exception> ListOfExceptions { get; set; }
    }
}