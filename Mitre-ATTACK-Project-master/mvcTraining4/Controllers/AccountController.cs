using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using mvcTraining4.Models;
using System.Linq;
using System.Data.Entity.Validation;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace mvcTraining4.Controllers
{
    public class AccountController : Controller
    {
        DBEntity entity = new DBEntity();

        //get: /Account/edit/5
        public ActionResult Edit(int id)
        {
            using(DBEntity dbModel = new DBEntity())
            {
                return View(dbModel.Customer.Where(x=> x.CUSTOMER_ID == id).FirstOrDefault());
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, Customer customer)
        {
            
            try
            {
                using (DBEntity dBEntity = new DBEntity())
                {
                    dBEntity.Entry(customer).State = EntityState.Modified;
                    dBEntity.SaveChanges();
                    
                }
                TempData["Message1"] = "Informations updated!";
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{ 0}\" in state \"{ 1}\" has the following validation errors:",
                    eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{ 0}\", Error: \"{ 1}\"",ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
                
            }
        }

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult signUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel credentials)
        {
            bool userExist = entity.Customer.Any(x =>x.CUSTOMER_EMAIL== credentials.email && x.CUSTOMER_PASSWORD==credentials.password);
            Customer customer = entity.Customer.FirstOrDefault(x => x.CUSTOMER_EMAIL == credentials.email && x.CUSTOMER_PASSWORD == credentials.password);

            if (userExist)
            {
                FormsAuthentication.SetAuthCookie(customer.CUSTOMER_ID.ToString(), false);
                TempData["LoginMessage"] = "Login succesfully !";
                return RedirectToAction("Index","Home");
            }
            else
            {
                TempData["LoginMessage2"] = "Login failed !";
            }

            ModelState.AddModelError("", "Username or Password is wrong");

            
            return View();
        }
        [HttpPost]
        public ActionResult signUp(Customer customer)
        {

            try
            {
                entity.Customer.Add(customer);
                entity.SaveChanges();
                TempData["SignUpMessage"] = "Your account created successfully!";

            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                    eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                        ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

            
            return RedirectToAction("Login");

        }
        public ActionResult signOut()
        {


            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }
    }
}