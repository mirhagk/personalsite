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
        private string GetHost(string url)
        {
            return "http://" + new Uri(url).Host.Replace("www.", "");
        }
        private bool HasSameHost(string root, string url)
        {
            return Uri.IsWellFormedUriString(url, UriKind.Absolute) && GetHost(url) == root;
        }
        private IEnumerable<string> GetAllLinksForPage(string url)
        {
            try
            {
                string root = GetHost(url);
                var client = new System.Net.WebClient();
                CsQuery.CQ data = client.DownloadString(url);
                var links = data["a"].Select((x) => x["href"]).Where((x)=>!String.IsNullOrWhiteSpace(x));

                return links.Where((x) => x.StartsWith("/")).Select((x) => root + x)
                    .Union(links.Where((x) => HasSameHost(root,x)));
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
                if (i >= 100 || links.Count>=500)
                {
                    links.Add("too many pages, can't crawl them all");
                    break;
                }
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
