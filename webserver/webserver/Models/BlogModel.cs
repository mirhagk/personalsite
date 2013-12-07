using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webserver.Models
{
    public class BlogModel
    {
        public string BlogPost { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
    }
}