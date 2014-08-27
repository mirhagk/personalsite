using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace webserver.Controllers
{
    public class PartialController : Controller
    {
        //
        // GET: /Partial/

        public ContentResult Render(string origController, string origAction, string id)
        {
            var controller = new HomeController();
            controller.ControllerContext = this.ControllerContext;
            this.ControllerContext.Controller = controller;
            //System.Web.Mvc.ContentResult
            //return PartialView(String.Format("~/Views/{0}/{1}.cshtml", origController, origAction));
            string partial = RenderPartialViewToString(String.Format("~/Views/{0}/{1}.cshtml", origController, origAction),null);
            return new ContentResult() { Content = partial, ContentType = "text/html", ContentEncoding = System.Text.Encoding.UTF8 };
            //return System.Web.Mvc.
        }
        protected string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewContext.ViewBag.AlternateLayout = "~/Views/PartialLayout.cshtml";
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

    }
}
