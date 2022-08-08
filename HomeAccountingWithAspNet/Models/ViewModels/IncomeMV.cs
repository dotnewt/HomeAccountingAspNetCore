using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HomeAccountingWithAspNet.Models.ViewModels
{
    public class IncomeMV
    {
        public Income Income{ get; set; }

        public IEnumerable<SelectListItem> TypeDropDown { get; set; }
    }
}
