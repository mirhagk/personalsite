using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NathanJervis.Models
{
    public class HelperModel
    {
        public enum HelperModelType
        {
            Heading, Subheading, NavPage, NavOption
        }
        public HelperModelType Type { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
    }
}
