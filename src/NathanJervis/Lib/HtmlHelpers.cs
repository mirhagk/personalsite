using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NathanJervis.Controllers;
using NathanJervis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NathanJervis.Lib
{
    public class HeadingTagHelper : TagHelper
    {
        private static string MakeID(string x)
        {
            return Regex.Replace(x, "[^a-zA-Z]*", "");
        }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "h2";
            output.Attributes.Add("class", "content-subhead");
            output.Attributes.Add("id", MakeID((await output.GetChildContentAsync()).GetContent()));
            foreach (var line in context.Items.Select(x => $"{x.Key}: {x.Value}"))
                output.Content.AppendLine(line);
        }
    }
    public class NavPageTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var content = (await output.GetChildContentAsync()).GetContent();
            //<li @(String.Equals(ViewBag.Title, Html.GetPage(Model.Title)) ? "class='pure-menu-selected'" : "")><a href="@(Html.GetURL(Model.Title))">@Model.Title</a></li>
            output.TagName = "li";
            var attributes = new TagHelperAttributeList();

            var builder = new TagBuilder("a");
            var page = content == "Home" ? "Index" : content;
            builder.Attributes.Add("href", "/Home/" + page);
            builder.InnerHtml.Append(content);

            output.Content.SetHtmlContent(builder);
        }
    }
    public static class HtmlHelperExtensions
    {
        static async Task<HtmlString> Helper(this HtmlHelper helper, HelperModel.HelperModelType type, string text = null, string title = null, string url = null)
        {
            var viewEngine = helper.ViewContext.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
            var controller = new PartialController(viewEngine);
            return new HtmlString(await controller.RenderPartialViewToString("Helpers", new HelperModel { Type = type, Text = text, Title = title, Url = url }));
        }
        public static async Task<HtmlString> SubHeading(this HtmlHelper helper, string text)
            => await helper.Helper(HelperModel.HelperModelType.Subheading, text);
        public static async Task<HtmlString> Heading(this HtmlHelper helper, string text)
            => await helper.Helper(HelperModel.HelperModelType.Heading, text);
        public static async Task<HtmlString> NavPage<T>(this HtmlHelper<T> helper, string title)
            => await helper.Helper(HelperModel.HelperModelType.NavPage, title: title);
        public static async Task<HtmlString> NavOption<T>(this HtmlHelper<T> helper, string title, string url)
            => await helper.Helper(HelperModel.HelperModelType.NavOption, title: title, url: url);
         

        public static string GetAbsURL<T>(this IHtmlHelper<T> helper, string url)
        {
            return helper.ViewContext.HttpContext.Request.PathBase.Value.TrimEnd('/') + url;
        }
        public static string GetURL<T>(this IHtmlHelper<T> helper, string page)
        {
            return helper.GetAbsURL("/Home/" + helper.GetPage(page));
        }
        public static string GetPage(string page)
        {
            if (String.Equals("home", page, StringComparison.OrdinalIgnoreCase))
                return "index";
            return page;
        }
        public static string GetPage<T>(this IHtmlHelper<T> helper, string page)
        {
            return GetPage(page);
        }
    }
}
