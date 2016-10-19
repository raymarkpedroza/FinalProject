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
        PostManager postManager = new PostManager();

        [HttpGet]
        public ActionResult Index()
        {
            if (Session["Username"] != null)
            {
                UserModel model = new UserModel();
                model = userManger.RetrieveUserById((int)Session["UserId"]);
                return View(model);
            }
            else
                return RedirectToAction("Index", "Home");
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
            if (userManger.LoginUser(user.EmailAddress, user.Password, out user))
            {
                Session["UserId"] = user.Id;
                Session["Username"] = user.Username;
                Session["Email"] = user.EmailAddress;
                return RedirectToAction("Index");
            }

            else
            {
                return RedirectToAction("Index","Home");
            }
        }

        public ActionResult Logout()
        {
            Session["Username"] = null;
            return RedirectToAction("Index","Home");
        }
    }
}