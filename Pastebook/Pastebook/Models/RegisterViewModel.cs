using PastebookEF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pastebook.Models
{
    public class RegisterViewModel
    {
        public List<REF_COUNTRY> ListOfCountryModel = new List<REF_COUNTRY>();
        public PASTEBOOK_USER User = new PASTEBOOK_USER();

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password required.")]
        [StringLength(50, ErrorMessage = "Maximum characters for password is 50")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Enter Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password doesn't match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}