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
    
    public partial class PASTEBOOK_COMMENT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PASTEBOOK_COMMENT()
        {
            this.PASTEBOOK_NOTIFICATION = new HashSet<PASTEBOOK_NOTIFICATION>();
        }
    
        public int ID { get; set; }
        public int POST_ID { get; set; }
        public int POSTER_ID { get; set; }
        public string CONTENT { get; set; }
        public System.DateTime DATE_CREATED { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PASTEBOOK_NOTIFICATION> PASTEBOOK_NOTIFICATION { get; set; }
        public virtual PASTEBOOK_POST PASTEBOOK_POST { get; set; }
        public virtual PASTEBOOK_USER PASTEBOOK_USER { get; set; }
    }
}