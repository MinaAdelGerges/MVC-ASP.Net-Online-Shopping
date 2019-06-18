using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace mvcElectronix.Models
{
    public class Products
    {

        [Key]
        public int productId { get; set; }
        [Required(ErrorMessage = "*")]
        public string name { get; set; }
        [Required(ErrorMessage = "*")]
        public int price { get; set; }
        [Required(ErrorMessage = "*")]
        public string dsecription { get; set; }
        [Required(ErrorMessage = "*")]

        public string image { get; set; }


       [ForeignKey("category")]
        public int categoryId { get; set; }

        public virtual Category category { get; set; }
        public virtual List<Cart> cart { get; set; }
    }
}