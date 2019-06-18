using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace mvcElectronix.Models
{
    public class Role
    {
        [Key]
        public int roleId { get; set; }
        [ForeignKey("users")]
        public int userId { get; set; }
        public virtual Users users { get; set; }
    }
}