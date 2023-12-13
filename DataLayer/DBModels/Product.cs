using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DBModels
{
    public class Product
    {
        [Key]
        public int ID { get; set; }
        public string ProductName { get; set; } = "";
        public double  Price { get; set; }
        public double Size { get; set; }
        public string Description { get; set; } = "";
        public string Color { get; set; } = "";
        public bool isActive { get; set; } = true;

        public virtual ICollection<Stock> Stocks { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }

    }
}
