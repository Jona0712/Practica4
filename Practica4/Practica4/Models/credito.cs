//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Practica4.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class credito
    {
        public int codigo { get; set; }
        public Nullable<float> Monto { get; set; }
        public string Descripcion { get; set; }
        public string estado { get; set; }
        public Nullable<int> cuenta { get; set; }
    
        public virtual cuenta cuenta1 { get; set; }
    }
}
