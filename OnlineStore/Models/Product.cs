using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OnlineStore.WebUI.Models
{
    [Display(Name = "Товар")]
    public class Product : IEnumerable
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Название товара")]
        public string Title { get; set; }
        [Display(Name = "Дата производства")]
        [DataType(DataType.Date)]
        public DateTime ProductionDateTime { get; set; }
        [Display(Name = "Цена")]
        [Range(0, 999.99, ErrorMessage = "Введено неверное число")]
        public double Price { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Display(Name = "Код категории")] 
        public Category Category { get; set; }
        
        public string File { get; set; }
        public int? CategoryId { get; set; }
        
        public int? CartId { get; set; }
        

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    public class Category
    {
       
        public int Id { get; set; }
        [Required(ErrorMessage = "Введите название категории")]
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
        public Category()
        {
            Products = new List<Product>();
        }

    }

}
