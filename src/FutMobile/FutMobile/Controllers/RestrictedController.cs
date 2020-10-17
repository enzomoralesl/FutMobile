using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FutMobile.Controllers
{
    public class RestrictedController : Controller
    {
        // GET: Restricted
        [Authorize]
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult Ajuda()
        {
            return View("Ajuda");
        }
    }
}