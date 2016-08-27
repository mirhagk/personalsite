using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.Threading.Tasks;

namespace NathanJervis.Controllers
{
    public class PartialController : Controller
    {
        CompositeViewEngine ViewEngine { get; }
        public PartialController(CompositeViewEngine viewEngine)
        {
            ViewEngine = viewEngine;
        }

        IList<KeyValuePair<string, string>> _parameters;
        IList<KeyValuePair<string, string>> parameters
        {
            get
            {
                return _parameters ?? (parameters = Request.Query.Keys.Select(x => new KeyValuePair<string,string>(x,Request.Query[x] )).ToList());
            }
            set
            {
                _parameters = value;
            }
        }

        //
        // GET: /Partial/
        private object InvokeMethod(Controller controller, string methodName)
        {
            var methods = controller.GetType().GetMethods().Where(m => m.Name.ToLowerInvariant() == methodName);
            methods = methods.Where(m => m.GetParameters().Length == parameters.Count);
            methods = methods.Where(m => m.GetParameters().Zip(parameters, (x, y) => new { x, y }).All(x => x.x.Name.ToLowerInvariant() == x.y.Key.ToLowerInvariant()));
            var method = methods.First();
            return method.Invoke(controller, parameters.Select(x => x.Value).ToArray());
        } 

        public ActionResult Render(string origController, string origAction, string id)
        {
            origController = origController.ToLowerInvariant();
            origAction = (origAction ?? "index").ToLowerInvariant();


            var controllerType= System.Reflection.Assembly.GetEntryAssembly().GetTypes().SingleOrDefault(t => t.Name.ToLowerInvariant() == origController + "controller");
            if (controllerType != null)
            {

                var controller = controllerType.GetConstructor(new Type[] { }).Invoke(new object[] { }) as Controller;
                controller.ControllerContext = this.ControllerContext;
                controller.Url = this.Url;
                //TODO Investigate fixing this
                //ControllerContext.ActionDescriptor.ControllerName = controller.;
                //this.ControllerContext.Controller = controller;


                //var parameters = RouteData.Values.Where(x => !NotParams.Contains(x.Key)).ToDictionary(x => x.Key, x => x.Value);

                var result = InvokeMethod(controller, origAction);
                //var result = controller.GetType().GetMethods().Single(m => m.Name.ToLowerInvariant() == origAction).Invoke(controller, new object[] { });
                if (result is ViewResult)
                {
                    ViewBag.AlternateLayout = "~/Views/PartialLayout.cshtml";
                    return View(String.Format("~/Views/{0}/{1}.cshtml", origController, origAction), (result as ViewResult).Model);
                }
                else if (result is RedirectToRouteResult)
                {
                    var routeValues = (result as RedirectToRouteResult).RouteValues;
                    if (routeValues.ContainsKey("controller") && routeValues.ContainsKey("action"))
                    {
                        string[] NotParams = { "action", "controller", "origAction", "origController" };
                        parameters = routeValues.Where(x => !NotParams.Contains(x.Key)).Select(x=>new KeyValuePair<string,string>(x.Key,x.Value.ToString())).ToList();
                        return Render(routeValues["controller"].ToString(), routeValues["action"].ToString(), null);
                    }
                    //(result as RedirectToRouteResult).RouteValues
                }
                return result as ActionResult;
            }
            return new StatusCodeResult(400);
            //throw new (400, "Could not render the partial page");
            //If we couldn't figure out routing, just send it back to the regular router
            //return RedirectToAction(origAction, origController, new { id = id });
        }
        protected async Task<string> RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.Values["action"] as string;

            ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var actionContext = new ActionContext(HttpContext, RouteData, new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor());
                var viewResult = ViewEngine.FindView(actionContext,viewName,false);
                var viewContext = new Microsoft.AspNetCore.Mvc.Rendering.ViewContext(actionContext, viewResult.View, ViewData, TempData, sw, new Microsoft.AspNetCore.Mvc.ViewFeatures.HtmlHelperOptions { });
                viewContext.ViewBag.AlternateLayout = "~/Views/PartialLayout.cshtml";
                await viewResult.View.RenderAsync(viewContext);
                //viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

    }
}
