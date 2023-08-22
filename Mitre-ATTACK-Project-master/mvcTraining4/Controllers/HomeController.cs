using mvcTraining4.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvcTraining4.Controllers
{

    
    [Authorize]
    public class HomeController : Controller
    {
        DBEntity entity = new DBEntity();
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Services(string submit)
        {
            switch (submit)
            {
                case "Show":
                    return (Show());
                case "Victim Host Information":
                    return (VictimHost());
                case "Save Record":
                    return (SaveRecord());
                case "Victim Network Information":
                    return (VictimNetwork());
            }
            return View();
        }

        public ActionResult Services()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult VictimHost()
        {
            Process proc = null;
            try
            {
                string batDir = string.Format(@"C:\Users\osman\source\repos\mvcTraining4\mvcTraining4\");
                proc = new Process();
                proc.StartInfo.WorkingDirectory = batDir;
                //proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.StartInfo.FileName = "test.bat";
                proc.StartInfo.CreateNoWindow = false;
                proc.Start();

                proc.WaitForExit();


            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine(ex);
            }
            TempData["Message"] = "Gather Victim Host Information attack is done!";
            TempData["AttackName"] = "Gather Victim Host Information";
            return View("Services");
        }
        [HttpPost]
        public ActionResult VictimNetwork()
        {
            Process proc = null;
            try
            {
                string batDir = string.Format(@"C:\Users\osman\source\repos\mvcTraining4\mvcTraining4\");
                proc = new Process();
                proc.StartInfo.WorkingDirectory = batDir;
                //proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.StartInfo.FileName = "test2.bat";
                proc.StartInfo.CreateNoWindow = false;
                proc.Start();

                proc.WaitForExit();
                TempData["Message"] = "Gather Victim Network Information attack is done!";
                TempData["AttackName"] = "Gather Victim Network Information";
            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine(ex);
            }

            return View("Services");
        }
        public ActionResult OldResult(int id)
        {

            var result = entity.AttackInformation.ToList().Where(x => x.CUSTOMER_ID == id);


            return View(result);
        }

        [HttpPost]
        public ActionResult SaveRecord()
        {
            string texts = System.IO.File.ReadAllText(Server.MapPath("~/result.txt"));
           
           
            try
            {
                if (ModelState.IsValid)
                {
                    AttackInformation attack = new AttackInformation();

                    attack.ATTACK_DETAIL = texts;
                    attack.CUSTOMER_ID = Int32.Parse(User.Identity.Name);
                    attack.ATTACK_NAME = TempData["AttackName"].ToString();
                    entity.AttackInformation.Add(attack);

                    entity.SaveChanges();
                    TempData["SaveMessage"] = "Your attack is saved successfully!";
                    return View();
                }
                return View();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }

        [HttpPost]
        public ActionResult Show()
        {
            string[] texts = System.IO.File.ReadAllLines(Server.MapPath(@"~\result.txt"));
            ViewBag.Data = texts;

            return View("Services");
        }
    }

}
