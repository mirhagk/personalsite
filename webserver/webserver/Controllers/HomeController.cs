using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace webserver.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Projects()
        {
            return View();
        }
        public ActionResult Experience()
        {
            return View();
        }
        public ActionResult Education()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }

    }
}
