using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.WebUI.Models
{
    public class Product
    {
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "Название товара")]
        public string Title { get; set; }
        [Display(Name = "Дата производства")]
        [DataType(DataType.Date)]
        public DateTime ProductionDateTime { get; set; }
        [Display(Name = "Цена")]
        [Column(TypeName = "decimal(18, 2)")]
        public double Price { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        


    }
}
