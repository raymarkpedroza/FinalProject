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
        UserManager userManager = new UserManager();
        CountryManager countryManager = new CountryManager();
        PostManager postManager = new PostManager();
        FriendManager friendManager = new FriendManager();

        [HttpGet]
        public ActionResult Index()
        {
            if (Session["Username"] != null)
            {
                UserModel model = new UserModel();
                model = userManager.RetrieveUserById((int)Session["UserId"]);
                return View(model);
            }
            else
                return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public new ActionResult Profile(int id)
        {
            if (Session["Username"] != null)
            {
                ProfileViewModel profileViewModel = new ProfileViewModel();
                profileViewModel.User = userManager.RetrieveUserById(id);
                profileViewModel.User.CountryName = countryManager.RetrieveCountry(profileViewModel.User.CountryId).Country;

                profileViewModel.ListOfFriends = friendManager.RetrieveFriends(id);
                return View(profileViewModel);
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
            userManager.RegisterUser(user);
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
            if (userManager.LoginUser(user.EmailAddress, user.Password, out user))
            {
                Session["UserId"] = user.Id;
                Session["Username"] = user.Username;
                Session["Email"] = user.EmailAddress;
                Session["UserFullname"] = user.Firstname + " " + user.Lastname;
                return RedirectToAction("Index");
            }

            else
            {
                return RedirectToAction("Index","Home");
            }
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();

            return RedirectToAction("Index","Home");
        }

        public JsonResult AddFriend(int friendId)
        {
            FriendModel friend = new FriendModel();
            friend.UserId = (int)Session["UserId"];
            friend.FriendId = friendId;
            friend.CreatedDate = DateTime.Now;
            friend.IsBlocked = 'N';
            friend.Request = 'N';

            int result = 0;

            result = friendManager.AddFriend(friend);

            return Json(new { result = result });
        }
    }
}