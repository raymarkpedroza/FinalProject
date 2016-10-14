using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pastebook.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string ConfirmPassword { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string EmailAddress { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthday { get; set; }
        public int CountryId { get; set; }
        public string MobileNumber { get; set; }
        public char Gender { get; set; }
        public string ProfilePicture { get; set; }
        public DateTime DATE_CREATED { get; set; }
        public string AboutMe { get; set; }
    }
}