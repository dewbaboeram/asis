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
    
    public partial class asis_tablelogchange
    {
        public int IDAsisTableLogChange { get; set; }
        public System.DateTime DateTimeOperation { get; set; }
        public string FieldName { get; set; }
        public string BeforeChange { get; set; }
        public string AfterChange { get; set; }
        public int IDAsisTableLog { get; set; }
    }
}
