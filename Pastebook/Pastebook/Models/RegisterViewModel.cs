using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pastebook.Models
{
    public class RegisterViewModel
    {
        public List<CountryModel> ListOfCountryModel = new List<CountryModel>() ;
        public UserModel User = new UserModel();
    }
}