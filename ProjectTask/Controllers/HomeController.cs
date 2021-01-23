using ProjectTask.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Services;
using System.Web.Services;

namespace ProjectTask.Controllers
{
    public class HomeController : Controller
    {
        private ProjectTaskEntities db = new ProjectTaskEntities();

        public ActionResult Index()
        {
            ViewModel viewModel = new ViewModel();
            if (Session["Username"] != null)
            {
                List<MsProject> msProjects = new List<MsProject>();
                List<MsUser> msUsers = new List<MsUser>();


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
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult Store(MsProject msProject)
        {
            if (msProject.ProjectName != null && msProject.ProjectDescription != null)
            {
                db.MsProjects.Add(msProject);
                db.SaveChanges();
                return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
            }
            else
            {
                return Json(new { success = false, message = "Error saving data", JsonRequestBehavior.AllowGet });
            }
        }

        [HttpPost]
        public ActionResult StoreAuthUser(HeaderProject headerProject)
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

        public ActionResult UserManagement()
        {
            ViewModel viewModel = new ViewModel();
            if (Session["Username"] != null)
            {
                List<MsProject> msProjects = new List<MsProject>();
                List<MsUser> msUsers = new List<MsUser>();

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
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public JsonResult AddUser(MsUser msUser)
        {

            if (msUser != null && msUser.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(msUser.ImageUpload.FileName);
                string extension = Path.GetExtension(msUser.ImageUpload.FileName);
                fileName = msUser.Username + "_" + fileName + extension;
                msUser.UserPhoto = fileName;
                msUser.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/AppFile/Images"), fileName));

                if (msUser.UserStatus == null)
                {
                    msUser.UserStatus = "Inactive";
                    db.MsUsers.Add(msUser);
                    db.SaveChanges();
                }
                else
                {

                    db.MsUsers.Add(msUser);
                    db.SaveChanges();
                }


            }
            else
            {
                return Json(new { success = false, message = "Cannot Successfully", JsonRequestBehavior.AllowGet });
            }
            return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });

        }

        [HttpPost]
        public JsonResult EditUser(MsUser msUser)
        {

            if (msUser.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(msUser.ImageUpload.FileName);
                string extension = Path.GetExtension(msUser.ImageUpload.FileName);
                fileName = msUser.Username + "_" + fileName + extension;
                msUser.UserPhoto = fileName;
                msUser.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/AppFile/Images"), fileName));

                if (msUser.UserStatus == null)
                {
                    msUser.UserStatus = "Inactive";
                }

                db.Entry(msUser).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                if (msUser.UserStatus == null)
                {
                    msUser.UserStatus = "Inactive";
                }

                db.Entry(msUser).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public ActionResult GetOneUser(int id)
        {
            MsUser newUser = new MsUser();
            MsUser msUser = new MsUser();

            newUser = db.MsUsers.Where(x => x.UserID == id).Single();
            msUser.UserID = newUser.UserID;
            msUser.Username = newUser.Username;
            msUser.UserPassword = newUser.UserPassword;
            msUser.UserPhoto = newUser.UserPhoto;
            msUser.UserRoles = newUser.UserRoles;
            msUser.UserStatus = newUser.UserStatus;

            return Json(msUser, JsonRequestBehavior.AllowGet);

        }
    }
}