using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace webserver.Controllers
{
    public class MetaController : Controller
    {
        //
        // GET: /Meta/

        public ActionResult Robots()
        {
            Response.ContentType = "text/plain";
            return View();
        }
        public ActionResult Sitemap()
        {
            Response.ContentType = "application/xml";
            return View();
        }
        private IEnumerable<string> GetAllLinksForPage(string url)
        {
            try
            {
                string root = "http://" + new System.Uri(url).Host;
                var client = new System.Net.WebClient();
                CsQuery.CQ data = client.DownloadString(url);
                return data["a"].Select((x) => x["href"]).Where((x) => x.StartsWith("/")).Select((x) => root + x);
            }
            catch
            {
                return new List<string>(0);
            }
        }
        private List<string> GetAllLinksForSite(string root)
        {
            List<string> links = new List<string>();
            links.AddRange(GetAllLinksForPage(root));
            links = links.Distinct().ToList();
            for (int i = 0; i < links.Count; i++)
            {
                links.AddRange(GetAllLinksForPage(links[i]));
                links = links.Distinct().ToList();
            }
            return links;
        }
        public ActionResult CrawlSite(string root)
        {
            var result = new ContentResult();
            result.Content = "sucess";

            result.Content = string.Join("<br />\n", GetAllLinksForSite(root));
            return result;
        }
        public ActionResult SelfTest(string root)
        {
            List<string> pages = GetAllLinksForSite(root);
            var client = new System.Net.WebClient();
            try
            {
                foreach (var page in pages)
                {
                    client.DownloadString(page);
                }
                return new ContentResult() { Content = "Success" };
            }
            catch
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
            }
        }
    }
}
