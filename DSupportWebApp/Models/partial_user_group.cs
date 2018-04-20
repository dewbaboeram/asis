using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSupportWebApp.Models
{
    public partial class user_group
    {
        [AllowHtml]
        public string Description_NL { get; set; }
        [AllowHtml]
        public string Description_EN { get; set; }
    }
}