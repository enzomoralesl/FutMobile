using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FutMobile.Models
{
    public class Post
    {
        public string UserLogin { get; set; }
        public DateTime Data { get; set; }
        public string Titulo { get; set; }
        public string Msg { get; set; }
    }
}