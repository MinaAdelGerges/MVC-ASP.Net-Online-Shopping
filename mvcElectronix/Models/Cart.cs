using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace mvcElectronix.Models
{
    public class Cart
    {
        [Key]
        public int cartId { get; set; }
        [Required(ErrorMessage = "*")]
        public int amount { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "*")]
        public DateTime dataPurchased { get; set; }
        [ForeignKey("users")]
        public int userId { get; set; }
        [ForeignKey("products")]
        public int prodcutId { get; set; }
        public virtual Users users { get; set; }
        public virtual Products products { get; set; }

    }
}