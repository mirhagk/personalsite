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

        public ActionResult Render(string origController, string origAction, string id)
        {
            origController = origController.ToLowerInvariant();
            origAction = (origAction ?? "index").ToLowerInvariant();

            var controllerType= System.Reflection.Assembly.GetExecutingAssembly().GetTypes().SingleOrDefault(t => t.Name.ToLowerInvariant() == origController + "controller");
            if (controllerType != null)
            {

                var controller = controllerType.GetConstructor(new Type[] { }).Invoke(new object[] { }) as Controller;
                controller.ControllerContext = this.ControllerContext;
                controller.Url = this.Url;
                this.ControllerContext.Controller = controller;

                var result = controller.GetType().GetMethods().Single(m => m.Name.ToLowerInvariant() == origAction).Invoke(controller, new object[] { });
                if (result is ViewResult)
                {
                    ViewBag.AlternateLayout = "~/Views/PartialLayout.cshtml";
                    return View(String.Format("~/Views/{0}/{1}.cshtml", origController, origAction), (result as ViewResult).Model);
                }
                return result as ActionResult;
            }
            throw new HttpException(400, "Could not render the partial page");
            //If we couldn't figure out routing, just send it back to the regular router
            return RedirectToAction(origAction, origController, new { id = id });
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
