using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DSupportWebApp.Models
{
    public partial class asis_attmain
    {
        [Required]
        public string NameNL { get; set; }

        [Required]
        public string NameEN { get; set; }

         public string Name {
            get
            {
                return AsisModelHelper.GetFieldValue("Name", this) as string;
            }

            set { }
        }

        

    }
}