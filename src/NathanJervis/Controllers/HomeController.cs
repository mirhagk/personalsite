using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NathanJervis.Controllers
{
    public class HomeController : LayoutController
    {
        //
        // GET: /Home/

        public ViewResult Index()
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
            System.Threading.Thread.Sleep(2000);
            return View();
        }

    }
}
