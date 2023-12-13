using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class ProductViewModel
    {
        public int ID { get; set; }
        public string ProductName { get; set; } = "";
        public double Price { get; set; }
        public double Size { get; set; }
        public string Description { get; set; } = "";
        public string Color { get; set; } = "";
    }
}
