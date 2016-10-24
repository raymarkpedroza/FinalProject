using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookBusinessLayer.BusinessLayer
{
    public interface ICountryManager
    {
        List<REF_COUNTRY> RetrieveAllCountries();
        REF_COUNTRY RetrieveCountryById(int countryId);
    }
}
