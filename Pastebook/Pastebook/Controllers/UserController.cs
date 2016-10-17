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
        CountryManager countryManager = new CountryManager();
        // GET: User

        [HttpGet]
        public ActionResult Index()
        {
            if (Session["User"] != null)
                return View(userManger.RetrieveAllUser());

            else
                return RedirectToAction("Index","Home");
        }


        [HttpGet]
        public ActionResult Register()
        {
            RegisterViewModel registerViewModel = new RegisterViewModel();
            registerViewModel.ListOfCountryModel = countryManager.RetrieveAllCountries(); 
            return View(registerViewModel);
        }

        [HttpPost]
        public ActionResult Register(UserModel user)
        {
            user.DateCreated = DateTime.Now;
            userManger.RegisterUser(user);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserModel user)
        {
            if (userManger.LoginUser(user.EmailAddress, user.Password))
            {
                Session["User"] = user.EmailAddress;
                return RedirectToAction("Index");
            }

            else
            {
                return RedirectToAction("Index","Home");
            }
        }

        public ActionResult Logout()
        {
            Session["User"] = null;
            return RedirectToAction("Index","Home");
        }
    }
}