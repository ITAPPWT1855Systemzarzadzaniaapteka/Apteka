//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Apteka.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Operacja
    {
        public Operacja()
        {
            this.Fakturas = new HashSet<Faktura>();
        }
    
        public int ID_operacja { get; set; }
        public int ID_lek { get; set; }
        public string Data { get; set; }
        public int ID_user { get; set; }
        public Nullable<int> Przychod { get; set; }
        public Nullable<int> Rozchod { get; set; }
    
        public virtual Lek Lek { get; set; }
        public virtual ICollection<Faktura> Fakturas { get; set; }
    }
}
