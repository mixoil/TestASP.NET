using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestASP.NET
{
    public enum Country
    {
        [Display(Name = "США")]
        USA,
        [Display(Name = "Италия")]
        Italy,
        [Display(Name = "Россия")]
        Russia
    }
}
