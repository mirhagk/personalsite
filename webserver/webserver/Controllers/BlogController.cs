using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace webserver.Controllers
{
    public class BlogController : Controller
    {
        //
        // GET: /Blog/

        public ActionResult Index()
        {
            var mdProcessor = new MarkdownSharp.Markdown();
            Models.BlogModel model = new Models.BlogModel();
            model.BlogPost = mdProcessor.Transform(System.IO.File.ReadAllText(
            Server.MapPath(Url.Content("/Content/blog/")+"LaTeX for the web - Sensible defaults.md")));
            return View(model);
        }

    }
}
