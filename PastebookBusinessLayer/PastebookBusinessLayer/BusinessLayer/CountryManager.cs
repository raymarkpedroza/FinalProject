using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PastebookEF;
using PastebookDataAccess.Repositories;

namespace PastebookBusinessLayer.BusinessLayer
{
    public class CountryManager : ICountryManager
    {
        private ICountryRepository _countryRepo;

        public CountryManager()
        {
            _countryRepo = new CountryRepository();
        }

        public List<REF_COUNTRY> RetrieveAllCountries()
        {
            List<REF_COUNTRY> listOfCountries = new List<REF_COUNTRY>();
            listOfCountries = _countryRepo.RetrieveAllRecords();

            return listOfCountries;
        }

        public REF_COUNTRY RetrieveCountryById(int countryId)
        {
            REF_COUNTRY country = new REF_COUNTRY();
            country = _countryRepo.RetrieveSpecificRecord(x=>x.ID.Equals(countryId));

            return country;
        }
    }
}
