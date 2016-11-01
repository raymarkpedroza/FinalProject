using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pastebook.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet, Route("error/generalerror")]
        public ActionResult ErrorPage()
        {
            return View("~/Views/Shared/ErrorPage.cshtml");
        }

        [HttpGet, Route("error/error404")]
        public ActionResult ErrorPage404()
        {
            return View("~/Views/Shared/ErrorPage404.cshtml");
        }
    }
}