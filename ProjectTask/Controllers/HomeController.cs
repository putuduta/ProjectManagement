using ProjectTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectTask.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(Session["Username"] != null)
            {
                List<MsProject> msProjects = new List<MsProject>();
                using (ProjectEntities db = new ProjectEntities())
                {
                    msProjects = db.MsProjects.OrderBy(mP => mP.ProjectID).ToList();
                    var total = db.MsProjects.Count();

                    if (total > 0)
                    {
                        return View(msProjects);
                    }
                    else
                    {
                        return null;
                    }
                }
            } else
            {
                return RedirectToAction("Index", "Login");
            }

            
        }

        [HttpPost]
        public ActionResult Store(MsProject msProject)
        {
            
            using (ProjectEntities db = new ProjectEntities())
            {
                if(msProject.ProjectName != null && msProject.ProjectDescription != null)
                {
                    db.MsProjects.Add(msProject);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
                } else
                {
                    return Json(new { success = false, message = "Error saving data", JsonRequestBehavior.AllowGet });
                }

            }
        }
    }
}