using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Milty.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "UserTasks");
            //return View();
        }
    }
}
