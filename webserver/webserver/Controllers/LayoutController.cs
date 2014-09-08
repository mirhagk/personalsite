using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace webserver.Controllers
{
    public abstract class LayoutController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Request.Params["Partial"] != null)
            {
                ViewBag.AlternateLayout = "~/Views/PartialLayout.cshtml";
            }
            base.OnActionExecuting(filterContext);
        }

    }
}
