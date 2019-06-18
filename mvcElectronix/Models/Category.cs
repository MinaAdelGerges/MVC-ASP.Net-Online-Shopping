using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace mvcElectronix.Models
{
    public class Category
    {
        [Key]
        public int categoryId { get; set; }
        public string name { get; set; }
        public virtual List<Products> prodcuts { get; set; }
        

    }
}