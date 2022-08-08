using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HomeAccountingWithAspNet.Models.ViewModels
{
    public class ExpenseMV
    {
        public Expense Expense { get; set; }

        public IEnumerable<SelectListItem> TypeDropDown { get; set; }
    }
}
