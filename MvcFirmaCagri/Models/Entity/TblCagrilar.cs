//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcFirmaCagri.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class TblCagrilar
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TblCagrilar()
        {
            this.TblCagriDetay = new HashSet<TblCagriDetay>();
        }
    
        public int ID { get; set; }
        public Nullable<int> CagriFirma { get; set; }
        public string Konu { get; set; }
        public string Aciklama { get; set; }
        public Nullable<System.DateTime> Tarih { get; set; }
        public Nullable<bool> Durum { get; set; }
        public Nullable<int> CagriPersonel { get; set; }
    
        public virtual TblFirmalar TblFirmalar { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblCagriDetay> TblCagriDetay { get; set; }
        public virtual TblPersonel TblPersonel { get; set; }
    }
}
