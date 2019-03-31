using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Milty.Models;

namespace Milty.Controllers
{
    [Authorize]
    public class RepositoriesController : Controller
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

        // GET: Repositories
        public ActionResult Index()
        {
            return View(db.Repositories.ToList());
        }

        // GET: Repositories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repository repository = db.Repositories.Find(id);
            if (repository == null)
            {
                return RedirectToAction("Index");
            }

            IEnumerable<UserTask> userTasks = db.UserTasks.Where(task => task.Repository.Equals(id.ToString()));

            foreach (UserTask task in userTasks)
            {
                var userForTask = UserManager.FindById(task.User);
                if (userForTask != null)
                    task.User = userForTask.Lastname + " " + userForTask.Firstname;
            }

            DetailsRepositoryViewModel model = new DetailsRepositoryViewModel {
                Id = repository.Id,
                Name = repository.Name,
                tasks = userTasks
            };

            return View(model);
        }

        // GET: Repositories/Create
        [Authorize(Roles = "Teacher,Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Repositories/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher,Admin")]
        public ActionResult Create([Bind(Include = "Id,Name")] Repository repository)
        {
            if (ModelState.IsValid)
            {
                db.Repositories.Add(repository);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(repository);
        }

        // GET: Repositories/Edit/5
        [Authorize(Roles = "Teacher,Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repository repository = db.Repositories.Find(id);
            if (repository == null)
            {
                return HttpNotFound();
            }
            return View(repository);
        }

        // POST: Repositories/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher,Admin")]
        public ActionResult Edit([Bind(Include = "Id,Name")] Repository repository)
        {
            if (ModelState.IsValid)
            {
                db.Entry(repository).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(repository);
        }

        // GET: Repositories/Delete/5
        [Authorize(Roles = "Teacher,Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repository repository = db.Repositories.Find(id);
            if (repository == null)
            {
                return HttpNotFound();
            }
            return View(repository);
        }

        // POST: Repositories/Delete/5
        [Authorize(Roles = "Teacher,Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Repository repository = db.Repositories.Find(id);
            db.Repositories.Remove(repository);
            db.SaveChanges();
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
