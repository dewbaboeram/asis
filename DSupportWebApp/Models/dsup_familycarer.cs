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
    
    public partial class dsup_familycarer
    {
        public int IDDSupFamilySupCarer { get; set; }
        public Nullable<int> IDDSupCarer { get; set; }
        public Nullable<int> IDDSupFamily { get; set; }
        public Nullable<int> IDDSupCoordinator { get; set; }
        public Nullable<System.DateTime> DateStart { get; set; }
        public Nullable<System.DateTime> DateEnd { get; set; }
        public string Info { get; set; }
        public Nullable<int> IDAttStatus { get; set; }
        public Nullable<bool> Contract { get; set; }
        public Nullable<double> FeeCarer { get; set; }
        public Nullable<double> FeeD_Support { get; set; }
        public Nullable<int> IDUserCreated { get; set; }
        public Nullable<int> IDUserModified { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateModified { get; set; }
    }
}
