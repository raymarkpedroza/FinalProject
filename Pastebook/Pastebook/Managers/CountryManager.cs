using Pastebook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Pastebook.PastebookServiceReference;
using Pastebook.Mappers;

namespace Pastebook.Managers
{
    public class CountryManager
    {
        PastebookServiceClient pastebookServiceClient = new PastebookServiceClient();
        public List<CountryModel> RetrieveAllCountries()
        {
            List<CountryModel> listOfCountries = new List<CountryModel>();
            RetrieveAllCountriesResponse response = new RetrieveAllCountriesResponse();

            response = pastebookServiceClient.RetrieveAllCountries();

            foreach (var country in response.ListOfCountries)
            {
                listOfCountries.Add(Mapper.MapWCFCountryEntityToMVCCountryModel(country));
            }

            return listOfCountries;
        }

        public CountryModel RetrieveCountry(int countryId)
        {
            CountryModel country = new CountryModel();
            RetrieveCountryByIdRequest request = new RetrieveCountryByIdRequest();
            request.CountryId = countryId;

            RetrieveCountryByIdResponse response = new RetrieveCountryByIdResponse();
            response = pastebookServiceClient.RetrieveCountryById(request);
            country = Mapper.MapWCFCountryEntityToMVCCountryModel(response.Country);

            return country;
        }
    }
}