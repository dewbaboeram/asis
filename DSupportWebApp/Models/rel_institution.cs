//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DSupportWebApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class rel_institution
    {
        public int IDRelInstitution { get; set; }
        public string Name { get; set; }
        public string V_address { get; set; }
        public string V_houseNr { get; set; }
        public string V_houseAdd { get; set; }
        public string V_postcode { get; set; }
        public string V_place { get; set; }
        public string V_State { get; set; }
        public Nullable<int> IDGeoPlace { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Memo { get; set; }
        public Nullable<int> IDUserCreated { get; set; }
        public Nullable<int> IDUserModified { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateModified { get; set; }
    }
}
