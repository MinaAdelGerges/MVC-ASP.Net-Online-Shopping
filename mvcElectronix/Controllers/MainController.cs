using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using mvcElectronix.Models;

namespace mvcElectronix.Controllers
{
    public class MainController : Controller
    {
        ElectronixContext db = new ElectronixContext();
        // GET: Main
        public ActionResult HomePage()
        {
            List<Products> allProducts = db.product.Include(n => n.category).ToList();

            return View(allProducts);
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Users user, HttpPostedFileBase file)
        {

            if (ModelState.IsValid)
            {
                string image = System.IO.Path.GetFileName(file.FileName);
                string Mypath = Server.MapPath("~/images/" + image);
                file.SaveAs(Mypath);
                user.image = image;
                db.user.Add(user);
                db.SaveChanges();


                return RedirectToAction("HomePage");
            }
            else { return View(user); }
        }
        public ActionResult Login()
        {

            return View();
           
           
        }
        [HttpPost]
        public ActionResult Login(Users user)
        {
            ModelState.Remove("age");
            ModelState.Remove("email");
            ModelState.Remove("gender");
            ModelState.Remove("image");
            ModelState.Remove("cPassword");
            if (ModelState.IsValid)
            {
                var check = db.user.FirstOrDefault(n => n.name == user.name && n.password == user.password);
                if (check == null)
                {
                    ViewBag.error = "User Name Or Password is Wrong";
                    return View(user);
                    
                }
                else
                {
                    Session["userid"] = check.userId;
                }
                

                return RedirectToAction("Profile");
            }



            else
            {
                return View(user);
            }
        }

        public  ActionResult Profile()
        {
            if (Session["userid"] != null)
            {
                int id = int.Parse(Session["userid"].ToString());
                var user = db.user.FirstOrDefault(n => n.userId == id);
                ViewBag.name = user.name;
                ViewBag.age = user.age;
                ViewBag.email = user.email;
                ViewBag.gender = user.gender;
                ViewBag.image = "/images/" + user.image;
                ViewBag.id = user.userId;
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult Logout()
        {
            
            Session["userid"] = null;
            return RedirectToAction("HomePage");
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(Users userData)
        {
            ModelState.Remove("age");
            ModelState.Remove("cPassword");
            ModelState.Remove("name");
            ModelState.Remove("email");
            ModelState.Remove("gender");
            ModelState.Remove("image");

            //Users users = new Users();
            if (ModelState.IsValid)
            {
                //db.Entry(userData).State = EntityState.Modified;


                var user = db.user.FirstOrDefault(n => n.userId == userData.userId);
            user.password = userData.password;
                user.cPassword = userData.password;
                db.SaveChanges();
                Session["userid"] = null;
                return RedirectToAction("Login");
        }
            else
            {
                return View("ChangePassword");
    }
}
        public ActionResult EditProfile(int userId)
        {
            ModelState.Remove("password");
            ModelState.Remove("cPassword");
       
            var user = db.user.FirstOrDefault(n => n.userId == userId);
            ViewBag.name = user.name;
            ViewBag.age = user.age;
            ViewBag.email = user.email;
            ViewBag.gender = user.gender;
            ViewBag.image = user.image;
            ViewBag.id = user.userId;
            return View();

        }
        [HttpPost]
        public ActionResult EditProfile(Users userData,HttpPostedFileBase file)
        {
            ModelState.Remove("image");
            if (ModelState.IsValid)
            {
                var user = db.user.FirstOrDefault(n => n.userId == userData.userId);
                string image = System.IO.Path.GetFileName(file.FileName);
                string Mypath = Server.MapPath("~/images/" + image);
                file.SaveAs(Mypath);
                user.image = image;
                
                db.SaveChanges();


                return RedirectToAction("HomePage");
            }
            else { return RedirectToAction("EditProfile",userData); }
            //return RedirectToAction("EditProfile");

        }
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(Category addCategory)
        {
            db.category.Add(addCategory);
            db.SaveChanges();
            return RedirectToAction("Management");
        }
        public ActionResult AddProduct()
        {
            if (Session["userid"] != null)
            {
                //int id = int.Parse(Session["userid"].ToString());
                List<Category> categoryy = db.category.ToList();
                SelectList li = new SelectList(categoryy, "categoryId", "name");
                ViewBag.li = li;
                return View();
            }
            else { return RedirectToAction("Login"); }
        }
        [HttpPost]
        public ActionResult AddProduct(Products product,HttpPostedFileBase file)
        {
            if (Session["userid"] != null)
            {
                string image = System.IO.Path.GetFileName(file.FileName);
                string Mypath = Server.MapPath("~/images/" + image);
                file.SaveAs(Mypath);
                product.image = image;
                db.product.Add(product);
                
                db.SaveChanges();

            
                 return RedirectToAction("HomePage");
        }else
            {
                return View(product);
    }
}
        public ActionResult AddCart(int productId)
        {
            if (Session["userid"] != null)
            {
                var product = db.product.Include(n => n.cart).FirstOrDefault(n => n.productId == productId);
                return View(product);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        [HttpPost]
        public ActionResult AddCart(int userId ,int productId,int amount)

        {
            if (Session["userid"] != null)
            {
                Cart cart = new Cart();
                cart.prodcutId = productId;
                cart.userId = userId;
                cart.amount = amount;


                cart.dataPurchased = DateTime.Now;

                db.cart.Add(cart);
                db.SaveChanges();
                return RedirectToAction("History");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult History()
        {
            if (Session["userid"] != null)
            {
                int id = int.Parse(Session["userid"].ToString());
                var cart = db.cart.Where(n => n.userId == id).ToList();
                return View(cart);
            }
            else { return RedirectToAction("Login"); }
        }
        public ActionResult DeleteProduct(int cartId)
        {
            var cart = db.cart.FirstOrDefault(n => n.cartId == cartId);
            db.cart.Remove(cart);
            db.SaveChanges();
            return RedirectToAction("History");
        }
        public ActionResult Management()
        {
            
            return View();
        }

        }
}
