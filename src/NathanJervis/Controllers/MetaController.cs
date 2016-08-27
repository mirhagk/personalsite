using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Parser.Html;

namespace NathanJervis.Controllers
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
        private async Task<IEnumerable<string>> GetAllLinksForPage(string url)
        {
            try
            {
                string root = GetHost(url);
                var client = new HttpClient();
                var parser = new HtmlParser();
                var data = await parser.ParseAsync(await client.GetStringAsync(url));

                var links = data.QuerySelectorAll("a").Select(x => x.Attributes["href"].Value).Where(x => string.IsNullOrWhiteSpace(x));

                return links.Where((x) => x.StartsWith("/")).Select((x) => root + x)
                    .Union(links.Where((x) => HasSameHost(root,x)));
            }
            catch
            {
                return new List<string>(0);
            }
        }
        private async Task<List<string>> GetAllLinksForSite(string root)
        {
            List<string> links = new List<string>();
            links.AddRange(await GetAllLinksForPage(root));
            links = links.Distinct().ToList();
            for (int i = 0; i < links.Count; i++)
            {
                links.AddRange(await GetAllLinksForPage(links[i]));
                //TODO: This code seems quite fishy. Not sure how it's working correctly
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
        public async Task<ActionResult> SelfTest(string root)
        {
            List<string> pages = await GetAllLinksForSite(root);
            var client = new HttpClient();
            try
            {
                foreach (var page in pages)
                {
                    await client.GetStringAsync(page);
                }
                return new ContentResult() { Content = "Success" };
            }
            catch
            {
                return new StatusCodeResult(404);
            }
        }
    }
}
