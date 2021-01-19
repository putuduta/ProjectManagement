using ProjectTask.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
                using (TaskProjectEntities db = new TaskProjectEntities())
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
            
            using (TaskProjectEntities db = new TaskProjectEntities())
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

        [HttpPost]
        public ActionResult StoreAuthUser(HeaderProject headerProject)
        {
            using (TaskProjectEntities db = new TaskProjectEntities())
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

        public ActionResult UserManagement()
        {
            ViewModel viewModel = new ViewModel();
            if (Session["Username"] != null)
            {
                List<MsProject> msProjects = new List<MsProject>();
                List<MsUser> msUsers = new List<MsUser>();
                using (TaskProjectEntities db = new TaskProjectEntities())
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

            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public JsonResult AddUser(MsUser msUser) 
        {
            using (TaskProjectEntities db = new TaskProjectEntities())
            {
                if(msUser != null && msUser.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(msUser.ImageUpload.FileName);
                    string extension = Path.GetExtension(msUser.ImageUpload.FileName);
                    fileName = msUser.Username + "_" + fileName + extension;
                    msUser.UserPhoto = fileName;
                    msUser.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/AppFile/Images"), fileName));
                    db.MsUsers.Add(msUser);
                    db.SaveChanges();

                } else 
                {
                    return Json(new { success = false, message = "Cannot Successfully", JsonRequestBehavior.AllowGet });
                }
                return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
            }
        }
    }
}