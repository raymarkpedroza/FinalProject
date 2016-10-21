using Pastebook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PastebookEF;
using PastebookDataAccess.Managers;

namespace Pastebook.Managers
{
    public class CountryManagerMVC
    {
        CountryManager dataAccessCountryManager = new CountryManager();

        public List<REF_COUNTRY> RetrieveAllCountries()
        {
            List<REF_COUNTRY> listOfCountries = new List<REF_COUNTRY>();

            listOfCountries = dataAccessCountryManager.RetrieveAllCountry();

            return listOfCountries;
        }

        public REF_COUNTRY RetrieveCountry(int countryId)
        {
            REF_COUNTRY country = new REF_COUNTRY();

            country = dataAccessCountryManager.RetrieveCountry(countryId);
            return country;
        }
    }
}