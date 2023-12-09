using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fortunes.Models.ViewModels
{
    public class ProductVM
    {
        public Product? product { get; set; }
        public IEnumerable<SelectListItem>? Categorylist { get; set; }
    }
}
