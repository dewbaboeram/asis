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
    
    public partial class trans_message
    {
        public int IDMessage { get; set; }
        public Nullable<int> IDAttMessageType { get; set; }
        public string Name_NL { get; set; }
        public string Name_EN { get; set; }
        public Nullable<int> IDUserCreated { get; set; }
        public Nullable<int> IDUserModified { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
    }
}
