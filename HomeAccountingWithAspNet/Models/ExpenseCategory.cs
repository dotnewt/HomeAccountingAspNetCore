using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAccountingWithAspNet.Models
{
    public class ExpenseCategory
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле Категория является обязательным для заполнения.")]
        [DisplayName("Название")]
        public string Name { get; set; }
    }
}
