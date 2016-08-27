using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Dynamic;
using Microsoft.AspNetCore.Hosting;

namespace NathanJervis.Controllers
{
    public class BlogController : LayoutController
    {
        IHostingEnvironment Environment { get; }
        public BlogController(IHostingEnvironment appEnv)
        {
            Environment = appEnv;
        }
        //
        // GET: /Blog/
        private Models.BlogConfig LoadConfig()
        {
            var config = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.BlogConfig>(GetTextFromPath("~/Content/Blog/blog_config.json"));
            //if (!System.Diagnostics.Debugger.IsAttached)
#if !DEBUG
                config.Posts = config.Posts.Where((x) => !x.Debug).ToArray();
#endif
            return config;
        }
        private string GetTextFromPath(string path)
        {
            return System.IO.File.ReadAllText(Environment.ContentRootPath + Url.Content(path));
        }
        private string GetBlogContent(string url)
        {
            return CommonMark.CommonMarkConverter.Convert(GetTextFromPath("~/Content/Blog/" + url + ".md"));
        }
        private int IndexOfPost(Models.BlogConfig.Post[] posts, string url)
        {
            for(int i=0;i<posts.Length;i++)
            {
                if (string.Equals(posts[i].Url, url, StringComparison.OrdinalIgnoreCase))
                    return i;
            }
            return -1;
        }
        public override RedirectToActionResult RedirectToAction(string actionName, string controllerName, object routeValues)
        {
            if (Request.Query["Partial"].SingleOrDefault() != null)
                throw new NotImplementedException();
                //routeValues.Add("Partial", Request.Params["Partial"]);
            return base.RedirectToAction(actionName, controllerName, routeValues);
        }
        public ActionResult Index()
        {
            var config = LoadConfig();
            return RedirectToAction("Post","Blog",new {title = config.Posts.Last().Url});
        }
        public ActionResult First()
        {
            var config = LoadConfig();
            return RedirectToAction("Post", "Blog", new { title = config.Posts.First().Url });
        }
        public ActionResult Last()
        {
            var config = LoadConfig();
            return RedirectToAction("Post", "Blog", new { title = config.Posts.Last().Url });
        }
        public ActionResult Post(string title)
        {
            var config = LoadConfig();
            var index = IndexOfPost(config.Posts, title);
            var post = config.Posts[index];
            Models.BlogModel model = new Models.BlogModel();
            model.BlogPost = GetBlogContent(post.Url);
            model.Title = post.Title;

            if (config.Posts.Length <= index + 1)
                model.Next = null;
            //model.Next = "last";
            else
                model.Next = "post?title=" + config.Posts[index + 1].Url;

            if (index < 1)
                model.Previous = null;
            //model.Previous = "first";
            else
                model.Previous = "post?title=" + config.Posts[index - 1].Url;

            return View(model);
        }

    }
}
