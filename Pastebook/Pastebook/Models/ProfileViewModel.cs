﻿using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pastebook.Models
{
    public class ProfileViewModel
    {
        public PASTEBOOK_USER User = new PASTEBOOK_USER();
        public List<PASTEBOOK_FRIEND> ListOfFriends = new List<PASTEBOOK_FRIEND>();
        public List<REF_COUNTRY> ListOfCountryModel = new List<REF_COUNTRY>();
        public string CountryName { get; set; }
    }
}