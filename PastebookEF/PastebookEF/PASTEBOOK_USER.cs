//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PastebookEF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class PASTEBOOK_USER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PASTEBOOK_USER()
        {
            this.PASTEBOOK_COMMENT = new HashSet<PASTEBOOK_COMMENT>();
            this.PASTEBOOK_FRIEND = new HashSet<PASTEBOOK_FRIEND>();
            this.PASTEBOOK_FRIEND1 = new HashSet<PASTEBOOK_FRIEND>();
            this.PASTEBOOK_LIKE = new HashSet<PASTEBOOK_LIKE>();
            this.PASTEBOOK_NOTIFICATION = new HashSet<PASTEBOOK_NOTIFICATION>();
            this.PASTEBOOK_NOTIFICATION1 = new HashSet<PASTEBOOK_NOTIFICATION>();
            this.PASTEBOOK_POST = new HashSet<PASTEBOOK_POST>();
            this.PASTEBOOK_POST1 = new HashSet<PASTEBOOK_POST>();
        }
    
        public int ID { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Maximum characters for username is 50")]
        [RegularExpression("^[a-zA-Z0-9._-]{1,50}",ErrorMessage ="Invalid Username Format")]
        [DisplayName("Username")]
        public string USER_NAME { get; set; }

        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "Maximum characters for password is 50")]
        public string PASSWORD { get; set; }
        public string SALT { get; set; }

        [Required(ErrorMessage = "Firstname is required.")]
        [DisplayName("First Name")]
        [StringLength(50, ErrorMessage = "Maximum characters for first name is 50")]
        [RegularExpression("^[a-zA-Z0-9. -]{1,50}", ErrorMessage = "Invalid First Name")]
        public string FIRST_NAME { get; set; }

        [Required(ErrorMessage = "Lastname is required.")]
        [DisplayName("Last Name")]
        [StringLength(50, ErrorMessage = "Maximum characters for last name is 50")]
        [RegularExpression("^[a-zA-Z0-9. -]{1,50}", ErrorMessage = "Invalid Last Name")]
        public string LAST_NAME { get; set; }

        [Required(ErrorMessage = "Birthday is required")]
        [DataType(DataType.DateTime)]
        [DisplayName("Birthday")]
        public System.DateTime BIRTHDAY { get; set; }

        [DisplayName("Country")]
        public Nullable<int> COUNTRY_ID { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Phone]
        [DisplayName("Mobile Number")]
        public string MOBILE_NO { get; set; }

        [DisplayName("Gender")]
        public string GENDER { get; set; }
        public byte[] PROFILE_PIC { get; set; }

        [DisplayName("Joined Pastebook")]
        public System.DateTime DATE_CREATED { get; set; }

        [DisplayName("About Me")]
        public string ABOUT_ME { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [DisplayName("Email Address")]
        [DataType(DataType.EmailAddress)]
        public string EMAIL_ADDRESS { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PASTEBOOK_COMMENT> PASTEBOOK_COMMENT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PASTEBOOK_FRIEND> PASTEBOOK_FRIEND { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PASTEBOOK_FRIEND> PASTEBOOK_FRIEND1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PASTEBOOK_LIKE> PASTEBOOK_LIKE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PASTEBOOK_NOTIFICATION> PASTEBOOK_NOTIFICATION { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PASTEBOOK_NOTIFICATION> PASTEBOOK_NOTIFICATION1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PASTEBOOK_POST> PASTEBOOK_POST { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PASTEBOOK_POST> PASTEBOOK_POST1 { get; set; }
        public virtual REF_COUNTRY REF_COUNTRY { get; set; }
    }
}
