using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pastebook.Models
{
    public class ProfileViewModel
    {
        public USER User = new USER();
        public List<FRIEND> ListOfFriends = new List<FRIEND>();
        public List<REF_COUNTRY> ListOfCountryModel = new List<REF_COUNTRY>();
        public string CountryName { get; set; }
    }
}