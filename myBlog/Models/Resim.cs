//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace myBlog.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Resim
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Resim()
        {
            this.Admins = new HashSet<Admin>();
        }
    
        public int id { get; set; }
        public string Adi { get; set; }
        public string Buyuk { get; set; }
        public string Kucuk { get; set; }
        public string Orta { get; set; }
        public int MakaleID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Admin> Admins { get; set; }
        public virtual Makale Makale { get; set; }
    }
}