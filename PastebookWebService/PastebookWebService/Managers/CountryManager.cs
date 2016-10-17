using PastebookEF;
using PastebookWebService.Entities;
using PastebookWebService.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PastebookWebService.Managers
{
    public class CountryManager
    {
        public List<CountryEntity> RetrieveAllCountry()
        {
            List<CountryEntity> listOfCountries = new List<CountryEntity>();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    foreach (var country in context.REF_COUNTRY)
                    {
                        listOfCountries.Add(Mapper.MapDBCountryTableToWCFCountryEntity(country));
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return listOfCountries;
        }
    }
}