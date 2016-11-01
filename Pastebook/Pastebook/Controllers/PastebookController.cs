using Pastebook.Models;
using PastebookEF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PastebookBusinessLayer.BusinessLayer;
using System.Web.Security;

namespace Pastebook.Controllers
{
    public class PastebookController : Controller
    {
        AccountManager accountManager = new AccountManager();
        CountryManager countryManager = new CountryManager();
        InteractionManager interactionManager = new InteractionManager();
        PostManager postManager = new PostManager();
        ValidationManager validationManager = new ValidationManager();

        [PastebookAuthorize]
        [HttpGet]
        public ActionResult Index()
        {
            PASTEBOOK_USER model = new PASTEBOOK_USER();
            model = accountManager.GetUser(x=>x.USER_NAME == Session["Username"].ToString());
            return View("~/Views/Pastebook/Home.cshtml", model);
        }

        [HttpGet, Route("account/login/")]
        public ActionResult Login()
        {
            if (Session["Username"] != null)
            {
                return RedirectToAction("Index", "Pastebook");
            }

            else
                return View("~/Views/Home/Index.cshtml");
        }

        [HttpGet, Route("account/register/")]
        public ActionResult Register()
        {
            RegisterViewModel registerViewModel = new RegisterViewModel();

            List<SelectListItem> listOfGenders = new List<SelectListItem>();

            listOfGenders.Add(new SelectListItem { Text = "Male", Value = "M" });
            listOfGenders.Add(new SelectListItem { Text = "Female", Value = "F" });


            List<SelectListItem> listOfCountries = new List<SelectListItem>();

            foreach (var country in countryManager.GetAllCountries())
            {
                listOfCountries.Add(new SelectListItem
                {
                    Text = country.COUNTRY,
                    Value = country.ID.ToString()
                });
            }

            ViewBag.Genders = listOfGenders;
            ViewBag.Countries = listOfCountries;

            return View("~/Views/Home/Register.cshtml", registerViewModel);
        }

        [HttpPost, Route("account/register/")]
        public ActionResult Register(RegisterViewModel registerViewModel, PASTEBOOK_USER user)
        {
            user.PASSWORD = registerViewModel.Password;
            user.DATE_CREATED = DateTime.Now;

            #region[CheckIfTaken]
            //check if user name and email is taken
            if (accountManager.GetUser(x => x.EMAIL_ADDRESS == user.EMAIL_ADDRESS) != null)
            {
                ModelState.AddModelError("User.EMAIL_ADDRESS", "Email Already Taken");
            }

            if (accountManager.GetUser(x => x.USER_NAME == user.USER_NAME) != null)
            {
                ModelState.AddModelError("User.USER_NAME", "Username Already Taken");
            }
            #endregion

            #region[RegisterIfValid]
            if (ModelState.IsValid)
            {
                if (user.GENDER == null)
                    user.GENDER = "U";

                accountManager.Register(user);

                Session["UserId"] = user.ID;
                Session["Username"] = user.USER_NAME;
                Session["Email"] = user.EMAIL_ADDRESS;
                Session["UserFullname"] = user.FIRST_NAME + " " + user.LAST_NAME;
                return RedirectToAction("Index", "Pastebook");
            }
            #endregion

            List<SelectListItem> listOfGenders = new List<SelectListItem>();

            listOfGenders.Add(new SelectListItem { Text = "Male", Value = "M" });
            listOfGenders.Add(new SelectListItem { Text = "Female", Value = "F" });


            List<SelectListItem> listOfCountries = new List<SelectListItem>();

            foreach (var country in countryManager.GetAllCountries())
            {
                listOfCountries.Add(new SelectListItem
                {
                    Text = country.COUNTRY,
                    Value = country.ID.ToString()
                });
            }

            ViewBag.Genders = listOfGenders;
            ViewBag.Countries = listOfCountries;

            return View("~/Views/Home/Register.cshtml", registerViewModel);
        }

        [HttpPost, Route("account/login/")]
        public ActionResult Login(PASTEBOOK_USER user)
        {
            if (accountManager.LoginUser(user.EMAIL_ADDRESS, user.PASSWORD, out user))
            {
                Session["UserId"] = user.ID;
                Session["Username"] = user.USER_NAME;
                Session["Email"] = user.EMAIL_ADDRESS;
                Session["UserFullname"] = user.FIRST_NAME + " " + user.LAST_NAME;
                return RedirectToAction("Index", "Pastebook");
            }

            else
            {
                ModelState.AddModelError("PASSWORD", "Incorrect email address or password");
                return View("~/Views/Home/Index.cshtml");
            }
        }

        [Route("account/logout/")]
        public ActionResult Logout()
        {
            Session.RemoveAll();
            return View("~/Views/Home/Index.cshtml");
        }

        public PartialViewResult GetPastebookNavBar()
        {
            return PartialView("~/Views/Pastebook/_PastebookNavbarPartialView.cshtml");
        }

        [PastebookAuthorize]
        [HttpGet, Route("{username}/")]
        public ActionResult UserProfile(string username)
        {
            ProfileViewModel profileViewModel = new ProfileViewModel();
            profileViewModel.User = accountManager.GetUserWithCountry(x=>x.USER_NAME == username);
            profileViewModel.CountryName = profileViewModel.User.REF_COUNTRY.COUNTRY;

            profileViewModel.ListOfCountryModel = countryManager.GetAllCountries();
            profileViewModel.ListOfFriends = interactionManager.GetListOfFriendRequest(profileViewModel.User.ID);
            return View("~/Views/Pastebook/Profile.cshtml", profileViewModel);
        }

        [PastebookAuthorize]
        [HttpGet, Route("friends/")]
        public ActionResult Friends()
        {
            List<PASTEBOOK_FRIEND> listOfFriend = new List<PASTEBOOK_FRIEND>();
            FriendListViewModel friendListViewModel = new FriendListViewModel();
            List<int> listOfFriendId = new List<int>();

            listOfFriend = interactionManager.GetFriendList((int)Session["UserId"]);

            foreach (var friend in listOfFriend)
            {
                if (friend.USER_ID == (int)Session["UserId"])
                {
                    friendListViewModel.ListOfFriendsWithDetails.Add(new FriendUserModel() { Friend = friend, FriendDetails = friend.PASTEBOOK_USER });
                }
            }

            return View(friendListViewModel);
        }

        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string pic = Path.GetFileName(file.FileName);

                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();

                    PASTEBOOK_USER user = new PASTEBOOK_USER();
                    user = accountManager.GetUser(x=>x.USER_NAME == Session["Username"].ToString());
                    user.PROFILE_PIC = array;

                    bool result = false;
                    result = accountManager.UpdateUser(user);
                }
            }

            ProfileViewModel profileViewModel = new ProfileViewModel();
            profileViewModel.User = accountManager.GetUserWithCountry(x => x.USER_NAME == Session["Username"].ToString());
            profileViewModel.CountryName = profileViewModel.User.REF_COUNTRY.COUNTRY;

            profileViewModel.ListOfCountryModel = countryManager.GetAllCountries();
            profileViewModel.ListOfFriends = interactionManager.GetListOfFriendRequest(profileViewModel.User.ID);
            ViewBag.Result = true;

            return View("~/Views/Pastebook/Profile.cshtml", profileViewModel);
        }

        public PartialViewResult GetProfileDetails(string username)
        {
            ProfileViewModel profileViewModel = new ProfileViewModel();
            List<int> listOfFriendId = new List<int>();

            profileViewModel.User = accountManager.GetUserWithCountry(x=>x.USER_NAME == username);
            profileViewModel.CountryName = profileViewModel.User.REF_COUNTRY.COUNTRY;

            profileViewModel.ListOfCountryModel = countryManager.GetAllCountries();
            profileViewModel.ListOfFriends = interactionManager.GetFriendList(profileViewModel.User.ID);

            return PartialView("~/Views/Pastebook/_ProfileDetailsPartialView.cshtml", profileViewModel);
        }

        public PartialViewResult GetFriendList(int id)
        {
            List<PASTEBOOK_FRIEND> listOfFriend = new List<PASTEBOOK_FRIEND>();
            FriendListViewModel friendListViewModel = new FriendListViewModel();
            List<int> listOfFriendId = new List<int>();

            listOfFriend = interactionManager.GetFriendList(id);

            foreach (var friend in listOfFriend)
            {
                if (friend.USER_ID == id)
                {
                    friendListViewModel.ListOfFriendsWithDetails.Add(new FriendUserModel() { Friend = friend, FriendDetails = friend.PASTEBOOK_USER });
                }
            }

            friendListViewModel.ListOfFriendsWithDetails.OrderBy(x => x.FriendDetails.FIRST_NAME);

            return PartialView("~/Views/Pastebook/_FriendListPartialView.cshtml", friendListViewModel);
        }

        [PastebookAuthorize]
        [Route("posts/{postId:int}/")]
        public ActionResult Posts(int postId)
        {
            PASTEBOOK_POST post = new PASTEBOOK_POST();
            post = postManager.GetPost(postId);
            return View(post);
        }

        [Route("search/")]
        public ActionResult Search(SearchModel search)
        {
            List<PASTEBOOK_USER> searchResults = new List<PASTEBOOK_USER>();
            ResultsViewModel resultsViewModel = new ResultsViewModel();
            if (!validationManager.CheckIfIsNullOrEmpty(search.Name))
            {
                searchResults = accountManager.SearchUsers(search.Name);
                resultsViewModel.searchResults = searchResults;
                resultsViewModel.searchQuery = search.Name;
            }

            return View(resultsViewModel);
        }

        public JsonResult CheckEmailIfExists(string email)
        {
            bool result = false;

            result = accountManager.GetUser(x=>x.EMAIL_ADDRESS == email) != null;

            return Json(new { result = result });
        }

        public JsonResult CheckIfUsernameExist(string username)
        {
            bool result = false;

            result = accountManager.GetUser(x=>x.USER_NAME == username) != null;
            return Json(new { result = result });
        }

    }
}