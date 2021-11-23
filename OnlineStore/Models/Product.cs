using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.WebUI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime ProductionDateTime { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        


    }
}
