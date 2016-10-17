using Pastebook.Managers;
using Pastebook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pastebook.Controllers
{
    public class UserController : Controller
    {
        UserManager userManger = new UserManager();

        // GET: User

        [HttpGet]
        public ActionResult Index()
        {
            return View(userManger.RetrieveAllUser());
        }


        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserModel user)
        {
            userManger.RegisterUser(user);
            return RedirectToAction("Index");
        }
    }
}