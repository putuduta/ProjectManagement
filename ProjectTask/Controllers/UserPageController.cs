using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Services;
using System.Web.Services;
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
                List<WorkItem> workItems = new List<WorkItem>();
                UserViewModel userViewModel = new UserViewModel();

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
                                workItems = db.WorkItems.Where(x => x.ProjectID == hp.ProjectID).ToList();

                                userViewModel.workItems = workItems;
                                userViewModel.msProject = hp;

                                return View(userViewModel);
                            }
                        }
                    }
                }

                return null;
            }

            return RedirectToAction("Index", "Login");
         
        }

        [HttpPost]
        public ActionResult Store(WorkItem workItem)
        {
            if (workItem.WorkItemName != null && workItem.WorkItemState != null)
            {
                db.WorkItems.Add(workItem);
                db.SaveChanges();
                return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
            }
            else
            {
                return Json(new { success = false, message = "Error saving data", JsonRequestBehavior.AllowGet });
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public ActionResult GetOneWorkItem(int id)
        {
            WorkItem newWorkItem = new WorkItem();
            WorkItem workItem = new WorkItem();

            newWorkItem = db.WorkItems.Where(x => x.WorkItemID == id).Single();
            workItem.WorkItemID = newWorkItem.WorkItemID;
            workItem.ProjectID = newWorkItem.ProjectID;
            workItem.WorkItemName = newWorkItem.WorkItemName;
            workItem.WorkItemState = newWorkItem.WorkItemState;
            workItem.WorkItemStartDate = newWorkItem.WorkItemStartDate;
            workItem.WorkItemEndDate = newWorkItem.WorkItemEndDate;

            return Json(workItem, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult EditWorkItem(WorkItem workItem)
        {

            if (workItem != null)
            {
                db.Entry(workItem).State = EntityState.Modified;
                db.SaveChanges();
            }



            return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });

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
