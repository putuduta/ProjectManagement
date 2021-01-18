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
            ViewModel viewModel = new ViewModel();
            if(Session["Username"] != null)
            {
                
                List<MsProject> msProjects = new List<MsProject>();
                List<MsUser> msUsers = new List<MsUser>();
                using (ProjectEntities db = new ProjectEntities())
                {
                    msProjects = db.MsProjects.OrderBy(mP => mP.ProjectID).ToList();
                    msUsers = db.MsUsers.OrderBy(mP => mP.UserID).ToList();
                    viewModel.msProjects = msProjects;
                    viewModel.msUsers = msUsers;
                    var total = db.MsProjects.Count();

                    if (total > 0)
                    {
                        return View(viewModel);
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

        public ActionResult StoreAuthUser(HeaderProject headerProject)
        {
            using (ProjectEntities db = new ProjectEntities())
            {
                if (headerProject.UserID != null && headerProject.ProjectID != null)
                {
                    db.HeaderProjects.Add(headerProject);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
                }
                else
                {
                    return Json(new { success = false, message = "Error saving data", JsonRequestBehavior.AllowGet });
                }

            }
        }
    }
}