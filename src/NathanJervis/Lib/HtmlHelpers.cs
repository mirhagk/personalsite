using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NathanJervis.Lib
{
    public static class HtmlHelperExtensions
    {
        private static string MakeID(string x)
        {
            return x.Replace("[^a-zA-Z]*", "");
        }
        public static HtmlString Heading(this HtmlHelper helper, string text)
        {
            // <h2 id="@MakeID(text)" class="content-subhead"><a href="#@MakeID(text)">@text</a></h2>
            return new HtmlString($"<h2 id=\"{MakeID(text)}\" class=\"content-subhead\"><a href=\"#{MakeID(text)}\">{text}</a></h2>");
        }
    }
}
