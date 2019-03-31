using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Milty.Models
{
    [Authorize]
    public class UserTasksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: UserTasks
        public ActionResult Index()
        {
            var userTasks = db.UserTasks.ToList();
            foreach (UserTask task in userTasks) {
                var userForTask = UserManager.FindById(task.User);
                if (userForTask != null) 
                    task.User = userForTask.Lastname + " " + userForTask.Firstname;

                if (task.Repository != null && task.Repository != "")
                {
                    try
                    {
                        Repository repository = db.Repositories.Find(int.Parse(task.Repository));
                        if (repository != null)
                        {
                            task.Repository = repository.Name;
                        }
                    } catch (Exception) {
                        task.Repository = "";
                    }
                }
            }

            return View(db.UserTasks.ToList());
        }

        // GET: UserTasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTask userTask = db.UserTasks.Find(id);
            if (userTask == null)
            {
                return HttpNotFound();
            }
            var userForTask = UserManager.FindById(userTask.User);
            if (userForTask != null)
                userTask.User = userForTask.Lastname + " " + userForTask.Firstname;
            return View(userTask);
        }

        // GET: UserTasks/Create
        [Authorize(Roles = "Teacher,Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserTasks/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher,Admin")]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Tag,Repository")] UserTask userTask)
        {
            if (ModelState.IsValid)
            {
                userTask.User = User.Identity.GetUserId();
                db.UserTasks.Add(userTask);
                db.SaveChanges();

                if (userTask.Repository != null) {
                    return RedirectToAction("Details", "Repositories", new { id = userTask.Repository });
                }

                return RedirectToAction("Index");
            }

            return View(userTask);
        }

        // GET: UserTasks/Edit/5
        [Authorize(Roles = "Teacher,Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTask userTask = db.UserTasks.Find(id);
            if (userTask == null)
            {
                return HttpNotFound();
            }
            return View(userTask);
        }

        // POST: UserTasks/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher,Admin")]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Tag,Repository")] UserTask userTask)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userTask).State = EntityState.Modified;
                db.SaveChanges();

                if (userTask.Repository != null)
                {
                    return RedirectToAction("Details", "Repositories", new { id = userTask.Repository });
                }
                return RedirectToAction("Index");
            }
            return View(userTask);
        }

        // GET: UserTasks/Delete/5
        [Authorize(Roles = "Teacher,Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTask userTask = db.UserTasks.Find(id);
            if (userTask == null)
            {
                return HttpNotFound();
            }

            var userForTask = UserManager.FindById(userTask.User);
            if (userForTask != null)
                userTask.User = userForTask.Lastname + " " + userForTask.Firstname;
            return View(userTask);
        }

        // POST: UserTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher,Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            UserTask userTask = db.UserTasks.Find(id);
            var toRepo = userTask.Repository;

            db.UserTasks.Remove(userTask);
            db.SaveChanges();

            if (userTask.Repository != null)
            {
                return RedirectToAction("Details", "Repositories", new { id = toRepo });
            }
            return RedirectToAction("Index");
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
