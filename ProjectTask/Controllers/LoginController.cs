using ProjectTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProjectTask.Controllers
{
    public class LoginController : Controller
    {
        ProjectEntities db = new ProjectEntities();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(MsUser msUser)
        {
            string username = msUser.Username;
            string password = msUser.UserPassword;


            string status = "0";
            var usr = db.MsUsers.Where(x => x.Username == username && x.UserPassword == password).SingleOrDefault();


            if (usr != null)
            {
                Session["Username"] = usr.Username.ToString();
                status = "1";
                return RedirectToAction("Index", "Home");
            } else 
            {
                status = "3";
            }

            return base.Content("<div>Username atau Password salah</div>", "text/html");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "login");
        }

    }
}