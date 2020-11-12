using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FutMobile.Context;
using FutMobile.Models;
using System.Web.Http;

namespace FutMobile.Controllers
{
    public class TeamsController : ApiController
    {
        private ContextoTime db = new ContextoTime();

        // GET: Teams
        public List<Time> Get()
        {
            var times = db.Time.ToList();
            return times;
        }
    }
}