using Pastebook.Managers;
using Pastebook.Models;
using PastebookDataAccess.Managers;
using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Pastebook.Controllers
{
    public class HomeController : Controller
    {
        _UserManager userManager = new _UserManager();

        DataAccessFriendManager daFriendManager = new DataAccessFriendManager();
        DataAccessUserManager daUserManager = new DataAccessUserManager();
        DataAccessCountryManager daCountryManager = new DataAccessCountryManager();

        public ActionResult Index()
        {
            if (Session["Username"] != null)
            {
                return RedirectToAction("Newsfeed", "Pastebook");
            }

            else
                return View();

        }

        [HttpGet]
        public ActionResult Register()
        {
            RegisterViewModel registerViewModel = new RegisterViewModel();
            registerViewModel.ListOfCountryModel = daCountryManager.RetrieveAllCountry();
            return View(registerViewModel);
        }

        [HttpPost]
        public ActionResult Register(PASTEBOOK_USER user)
        {
            user.DATE_CREATED = DateTime.Now;
            daUserManager.RegisterUser(user);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(PASTEBOOK_USER user)
        {
            if (userManager.LoginUser(user.EMAIL_ADDRESS, user.PASSWORD, out user))
            {
                Session["UserId"] = user.ID;
                Session["Username"] = user.USER_NAME;
                Session["Email"] = user.EMAIL_ADDRESS;
                Session["UserFullname"] = user.FIRST_NAME + " " + user.LAST_NAME;
                return RedirectToAction("Newsfeed","Pastebook");
            }

            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();

            return RedirectToAction("Index");
        }

        public PartialViewResult GetPastebookNavBar()
        {
            return PartialView("~/Views/Pastebook/_PastebookNavbarPartialView.cshtml");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}