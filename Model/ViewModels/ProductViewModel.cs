using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models.ViewModels
{
    public class ProductViewModel
    {
        public Product product { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> categories { get; set; }
    }
}
