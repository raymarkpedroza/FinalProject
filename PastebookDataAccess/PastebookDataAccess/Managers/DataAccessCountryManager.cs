using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PastebookDataAccess.Managers
{
    public class DataAccessCountryManager
    {
        public List<REF_COUNTRY> RetrieveAllCountry()
        {
            List<REF_COUNTRY> listOfCountries = new List<REF_COUNTRY>();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    foreach (var country in context.REF_COUNTRY)
                    {
                        listOfCountries.Add(country);
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return listOfCountries;
        }

        public REF_COUNTRY RetrieveCountry(int id)
        {
            REF_COUNTRY country = new REF_COUNTRY();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    country = context.REF_COUNTRY.Where(x=>x.ID == id).SingleOrDefault();
                }
            }
            catch 
            {
            }

            return country;
        }
    }
}