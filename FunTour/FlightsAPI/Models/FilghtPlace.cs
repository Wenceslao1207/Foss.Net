//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FlightsAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class FilghtPlace
    {
        public int numPlace { get; set; }
        public string Place_Owner_Name { get; set; }
        public int Place_Owner_DNI { get; set; }
        public System.DateTime FP_Date { get; set; }
        public string idFlight { get; set; }
    
        public virtual CommercialFlight CommercialFlight { get; set; }
    }
}
