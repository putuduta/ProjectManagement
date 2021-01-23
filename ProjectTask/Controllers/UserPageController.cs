using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectTask.Models;

namespace ProjectTask.Controllers
{
    public class UserPageController : Controller
    {
        private ProjectTaskEntities db = new ProjectTaskEntities();

        // GET: UserPage
        public ActionResult Index()
        {
            if (Session["Username"] != null)
            {
                List<MsProject> msProjects = new List<MsProject>();
                List<HeaderProject> headerProjects = new List<HeaderProject>();

                msProjects = db.MsProjects.OrderBy(mP => mP.ProjectID).ToList();
                headerProjects = db.HeaderProjects.OrderBy(mP => mP.HeaderProjectID).ToList();

                MsProject hp = new MsProject();

                foreach (var item in headerProjects)
                {
                    if (Session["UserID"].ToString() == item.UserID.ToString())
                    {
                        foreach (var projects in msProjects)
                        {
                            if (item.ProjectID == projects.ProjectID)
                            {
                                hp.ProjectID = projects.ProjectID;
                                hp.ProjectName = projects.ProjectName;
                                hp.ProjectDescription = projects.ProjectDescription;
                                return View(hp);
                            }
                        }
                    }
                }

                return null;
            }

            return RedirectToAction("Index", "Login");
         
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
