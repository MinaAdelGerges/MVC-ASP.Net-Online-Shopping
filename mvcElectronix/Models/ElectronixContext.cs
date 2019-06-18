using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace mvcElectronix.Models
{
    public class ElectronixContext:DbContext
    {
        public ElectronixContext() : base("mvcProject") { }
        public DbSet<Users> user { get; set; }
        public DbSet<Products> product { get; set; }
        public DbSet<Category> category { get; set; }
        public DbSet<Cart> cart { get; set; }
        public DbSet<Role> role { get; set; }
    }
}