using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pastebook.Models
{
    public class EditProfileViewModel
    {
        public List<REF_COUNTRY> ListOfCountryModel = new List<REF_COUNTRY>();
        public PASTEBOOK_USER User = new PASTEBOOK_USER();
        public string ConfirmPassword { get; set; }
    }
}