using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DBModels
{
    public class Cart
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int ProductID { get; set; }
        [Required]
        public string UserId { get; set; } = "";
        [Required]
        public int Amount { get; set; }

    }
}
