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
        public string Name_NL { get; set; }

        [Required]
        public string Name_EN { get; set; }

         public string Name {
            get
            {
                return AsisModelHelper.GetFieldValue("Name", this) as string;
            }

            set { }
        }

        

    }
}