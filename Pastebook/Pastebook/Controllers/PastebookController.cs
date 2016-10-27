using Pastebook.Models;
using PastebookEF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PastebookBusinessLayer.BusinessLayer;

namespace Pastebook.Controllers
{
    public class PastebookController : Controller
    {
        AccountManager accountManager = new AccountManager();
        CountryManager countryManager = new CountryManager();
        InteractionManager interactionManager = new InteractionManager();
        PostManager postManager = new PostManager();

        [HttpGet]
        public ActionResult Index()
        {
            if (Session["Username"] != null)
            {
                PASTEBOOK_USER model = new PASTEBOOK_USER();
                model = accountManager.RetrieveUserByUsername(Session["Username"].ToString());
                return View("~/Views/Pastebook/Home.cshtml", model);
            }
            else
                return RedirectToAction("Login", "Pastebook");
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
            registerViewModel.ListOfCountryModel = countryManager.RetrieveAllCountries();
            return View("~/Views/Home/Register.cshtml", registerViewModel);
        }

        [HttpPost, Route("account/register/")]
        public ActionResult Register(RegisterViewModel registerViewModel, PASTEBOOK_USER user)
        {
            user.PASSWORD = registerViewModel.Password;
            user.DATE_CREATED = DateTime.Now;

            if (accountManager.CheckUserIfExist_Email(user.EMAIL_ADDRESS))
            {
                ModelState.AddModelError("User.EMAIL_ADDRESS", "Email Already Taken");
            }

            if (accountManager.CheckUserIfExist_Username(user.USER_NAME))
            {
                ModelState.AddModelError("User.USER_NAME", "Username Already Taken");
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (ModelState.IsValid)
            {
                if (user.GENDER == null)
                    user.GENDER = "U";

                accountManager.RegisterUser(user);

                Session["UserId"] = user.ID;
                Session["Username"] = user.USER_NAME;
                Session["Email"] = user.EMAIL_ADDRESS;
                Session["UserFullname"] = user.FIRST_NAME + " " + user.LAST_NAME;
                return RedirectToAction("Index", "Pastebook");
            }

            registerViewModel.ListOfCountryModel = countryManager.RetrieveAllCountries();
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
                ModelState.AddModelError("PASSWORD","Incorrect email address or password");
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

        [HttpGet, Route("{username}/")]
        public ActionResult UserProfile(string username)
        {
            if (Session["Username"] != null)
            {
                ProfileViewModel profileViewModel = new ProfileViewModel();
                profileViewModel.User = accountManager.RetrieveUserByUsername(username);
                profileViewModel.CountryName = profileViewModel.User.REF_COUNTRY.COUNTRY;

                List<int> listOfFriendId = new List<int>();
                profileViewModel.ListOfCountryModel = countryManager.RetrieveAllCountries();
                profileViewModel.ListOfFriends = interactionManager.RetrieveFriends(profileViewModel.User.ID, out listOfFriendId);
                return View("~/Views/Pastebook/Profile.cshtml",profileViewModel);  
            }
            else
                return RedirectToAction("Index", "Pastebook");
        }

        [HttpGet, Route("friends/")]
        public ActionResult Friends()
        {
            List<PASTEBOOK_FRIEND> listOfFriend = new List<PASTEBOOK_FRIEND>();
            FriendListViewModel friendListViewModel = new FriendListViewModel();
            List<int> listOfFriendId = new List<int>();

            if (Session["Username"] != null)
            {
                listOfFriend = interactionManager.RetrieveFriends((int)Session["UserId"], out listOfFriendId);

                foreach (var friend in listOfFriend)
                {
                    if (friend.USER_ID == (int)Session["UserId"])
                    {
                        friendListViewModel.ListOfFriendsWithDetails.Add(new FriendUserModel() { Friend = friend, FriendDetails = friend.PASTEBOOK_USER });
                    }

                    else if (friend.FRIEND_ID == (int)Session["UserId"])
                    {
                        friendListViewModel.ListOfFriendsWithDetails.Add(new FriendUserModel() { Friend = friend, FriendDetails = friend.PASTEBOOK_USER1 });
                    }
                }

                friendListViewModel.ListOfFriendsWithDetails.OrderBy(x => x.FriendDetails.FIRST_NAME);
                return View(friendListViewModel);
            }
            else
                return RedirectToAction("Index", "Pastebook");
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
                    user = accountManager.RetrieveUserByUsername(Session["Username"].ToString());
                    user.PROFILE_PIC = array;

                    bool result = false;
                    result = accountManager.UpdateUser(user);
                }
            }

            return RedirectToAction("UserProfile", "Pastebook", new { username = Session["Username"].ToString() });
        }

        public PartialViewResult GetProfileDetails(string username)
        {
            ProfileViewModel profileViewModel = new ProfileViewModel();
            List<int> listOfFriendId = new List<int>();

            profileViewModel.User = accountManager.RetrieveUserByUsername(username);
            profileViewModel.CountryName = profileViewModel.User.REF_COUNTRY.COUNTRY;

            profileViewModel.ListOfCountryModel = countryManager.RetrieveAllCountries();
            profileViewModel.ListOfFriends = interactionManager.RetrieveFriends(profileViewModel.User.ID, out listOfFriendId);

            return PartialView("~/Views/Pastebook/_ProfileDetailsPartialView.cshtml", profileViewModel);
        }

        public PartialViewResult GetFriendList(int id)
        {
            List<PASTEBOOK_FRIEND> listOfFriend = new List<PASTEBOOK_FRIEND>();
            FriendListViewModel friendListViewModel = new FriendListViewModel();
            List<int> listOfFriendId = new List<int>();

            listOfFriend = interactionManager.RetrieveFriends(id, out listOfFriendId);

            foreach (var friend in listOfFriend)
            {
                if (friend.USER_ID == id)
                {
                    friendListViewModel.ListOfFriendsWithDetails.Add(new FriendUserModel() { Friend = friend, FriendDetails = friend.PASTEBOOK_USER });
                }

                else if(friend.FRIEND_ID == id)
                {
                    friendListViewModel.ListOfFriendsWithDetails.Add(new FriendUserModel() { Friend = friend, FriendDetails = friend.PASTEBOOK_USER1 });
                }
            }

            friendListViewModel.ListOfFriendsWithDetails.OrderBy(x => x.FriendDetails.FIRST_NAME);

            return PartialView("~/Views/Pastebook/_FriendListPartialView.cshtml", friendListViewModel);
        }

        [Route("posts/{postId:int}/")]
        public ActionResult Posts(int postId)
        {
            if (Session["Username"] != null)
            {
                PASTEBOOK_POST post = new PASTEBOOK_POST();
                post = postManager.RetrievePost(postId);
                return View(post);
            }
            else
                return RedirectToAction("Index", "Pastebook");
        }

        [Route("search/")]
        public ActionResult Search(SearchModel search)
        {
            if (Session["Username"] != null)
            {
                List<PASTEBOOK_USER> searchResults = new List<PASTEBOOK_USER>();
                searchResults = accountManager.SearchUserByName(search.Name);

                ResultsViewModel resultsViewModel = new ResultsViewModel();
                resultsViewModel.searchResults = searchResults;
                resultsViewModel.searchQuery = search.Name;
                return View(resultsViewModel);
            }
            else
                return RedirectToAction("Index", "Pastebook");
           
        }

        public JsonResult CheckEmailIfExists(string email)
        {
            bool result = false;

            result = accountManager.CheckUserIfExist_Email(email);

            return Json(new { result = result });
        }

        public JsonResult CheckIfUsernameExist(string username)
        {
            bool result = false;

            result = accountManager.CheckUserIfExist_Username(username);

            return Json(new { result = result });
        }

    }
}