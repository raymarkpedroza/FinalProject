using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pastebook.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        //[HttpGet, Route("error")]
        public ActionResult ErrorPage()
        {
            return View("~/Views/Shared/ErrorPage.cshtml");
        }

        //[HttpGet, Route("error")]
        public ActionResult ErrorPage404()
        {
            return View("~/Views/Shared/ErrorPage404.cshtml");
        }
    }
}