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
    
    public partial class dsup_coordinator
    {
        public int IDDSupCoordinator { get; set; }
        public Nullable<int> IDRelPerson { get; set; }
        public Nullable<System.DateTime> DateRegister { get; set; }
        public Nullable<System.DateTime> DateStart { get; set; }
        public Nullable<System.DateTime> DateEnd { get; set; }
        public string Region { get; set; }
        public Nullable<int> IDUserCreated { get; set; }
        public Nullable<int> IDUserModified { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateModified { get; set; }
    }
}
