using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PastebookEF;
using PastebookDataAccess;

namespace PastebookBusinessLayer.BusinessLayer
{
    public class CountryManager
    {
        ICountryRepository _countryRepository;

        public CountryManager()
        {
            _countryRepository = new CountryRepository();
        }

        public List<REF_COUNTRY> GetAllCountries()
        {
            return _countryRepository.GetAll();
        }

        public REF_COUNTRY GetCountry(int id)
        {
            return _countryRepository.Get(id);
        }
    }
}
