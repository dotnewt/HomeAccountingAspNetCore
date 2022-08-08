using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAccountingWithAspNet.Models
{
    public class Income
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Категория")]
        public int IncomeTypeId { get; set; }

        [DisplayName("Категория")]
        [ForeignKey("IncomeTypeId")]
        public virtual IncomeCategory Category { get; set; }

        [DataType(DataType.Date)]
       // [DisplayFormat(ApplyFormatInEditMode = true)]
        [DisplayName("Дата")]
        public DateTime dateTime { get; set; }

        
        [Range(1, int.MaxValue, ErrorMessage = "Сумма должна быть больше 0!!")]
        [DisplayName("Сумма")]
        public int Amount { get; set; }

        [DisplayName("Комментарий")]
        public string Comment { get; set; }

    }
}
