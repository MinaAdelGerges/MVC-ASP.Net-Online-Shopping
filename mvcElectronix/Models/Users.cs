using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace mvcElectronix.Models
{
    public class Users
    {
        [Key]
        public int userId { get; set; }
        [Required(ErrorMessage = "*")]
        [StringLength(25, MinimumLength = 5, ErrorMessage = "name must between 25 and 5 chars")]
        public string name { get; set; }
        [Range(20, 60, ErrorMessage = "age must between 20 and 60")]
        public int age { get; set; }
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [NotMapped]
        [Compare("password", ErrorMessage = "password not match")]
        [DataType(DataType.Password)]
        public string cPassword { get; set; }
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[a-zA-Z0-9]+@[a-zA-Z0-9]+.[a-zA-Z]{2,4}", ErrorMessage = "invalid email")]
        public string email { get; set; }
        [Required(ErrorMessage = "*")]
        public string gender { get; set; }
        
        public string image { get; set; }
        public virtual List<Cart> cart { get; set; }
        public virtual List<Role> role { get; set; }
    }
}