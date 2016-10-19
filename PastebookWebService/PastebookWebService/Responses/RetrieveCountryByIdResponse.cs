using PastebookWebService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PastebookWebService.Responses
{
    [DataContract]
    public class RetrieveCountryByIdResponse
    {
        public RetrieveCountryByIdResponse()
        {
            Country = new CountryEntity();
        }

        [DataMember]
        public CountryEntity Country { get; set; }
    }
}