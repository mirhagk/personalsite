using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webserver.Models
{
    public class BlogConfig
    {
        public class Post
        {
            public string Title { get; set; }
            public string Url { get; set; }
            public bool Debug { get; set; }
        }
        public Post[] Posts { get; set; }
    }
    public class BlogModel
    {
        public string BlogPost { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public string Title { get; set; }
    }
}