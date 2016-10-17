using PastebookWebService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PastebookWebService.Responses
{
    [DataContract]
    public class RetrieveAllCountriesResponse
    {
        public RetrieveAllCountriesResponse()
        {
            ListOfCountries = new List<CountryEntity>();
        }

        [DataMember]
        public List<CountryEntity> ListOfCountries { get; set; }
    }
}