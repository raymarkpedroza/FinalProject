using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pastebook.Models
{
    public class UserModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        public int CountryId { get; set; }
        public string CountryName { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string MobileNumber { get; set; }

        public string Gender { get; set; }
        public byte[] ProfilePicture { get; set; }
        public DateTime DateCreated { get; set; }
        public string AboutMe { get; set; }
    }
}