using Pastebook.Managers;
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
        public ActionResult Home()
        {
            if (Session["Username"] != null)
            {
                PASTEBOOK_USER model = new PASTEBOOK_USER();
                model = accountManager.RetrieveUserByUsername(Session["Username"].ToString());
                return View(model);
            }
            else
                return RedirectToAction("Index", "Home");
        }

        public ActionResult Index(string username)
        {
            if (Session["Username"] != null)
            {
                ProfileViewModel profileViewModel = new ProfileViewModel();
                profileViewModel.User = accountManager.RetrieveUserByUsername(username);
                profileViewModel.CountryName = profileViewModel.User.REF_COUNTRY.COUNTRY;

                List<int> listOfFriendId = new List<int>();
                profileViewModel.ListOfCountryModel = countryManager.RetrieveAllCountries();
                profileViewModel.ListOfFriends = interactionManager.RetrieveFriends(profileViewModel.User.ID, out listOfFriendId);
                return View(profileViewModel);
            }
            else
                return RedirectToAction("Index", "Home");
        }

        public ActionResult Friends()
        {
            List<PASTEBOOK_FRIEND> listOfFriend = new List<PASTEBOOK_FRIEND>();
            FriendListViewModel friendListViewModel = new FriendListViewModel();
            List<int> listOfFriendId = new List<int>();

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

            return RedirectToAction("Index", "Pastebook", new { username = Session["Username"].ToString() });
        }

        public ActionResult UpdateProfile(string gender, int country, DateTime birthday, string mobilenumber, string email, string aboutme)
        {
            PASTEBOOK_USER user = new PASTEBOOK_USER();
            user = accountManager.RetrieveUserByUsername(Session["Username"].ToString());
            user.GENDER = gender;
            user.COUNTRY_ID = country;
            user.BIRTHDAY = birthday;
            user.MOBILE_NO = mobilenumber;
            user.EMAIL_ADDRESS = email;
            user.ABOUT_ME = aboutme;
            bool result = false; 
            result = accountManager.UpdateUser(user);

            return RedirectToAction("GetProfileDetails", "Pastebook", new { username = Session["Username"].ToString() });
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

        public ActionResult Posts(int postId)
        {
            PASTEBOOK_POST post = new PASTEBOOK_POST();
            post = postManager.RetrievePost(postId);
            return View(post);
        }

        public ActionResult Search(SearchModel search)
        {
            List<PASTEBOOK_USER> searchResults = new List<PASTEBOOK_USER>();
            searchResults = accountManager.SearchUserByName(search.Name);

            ResultsViewModel resultsViewModel = new ResultsViewModel();
            resultsViewModel.searchResults = searchResults;
            return View(resultsViewModel);
        }

        public ActionResult EditProfile()
        {
            EditProfileViewModel editProfileViewModel = new EditProfileViewModel();

            editProfileViewModel.User = accountManager.RetrieveUserById((int)Session["UserId"]);
            editProfileViewModel.ListOfCountryModel = countryManager.RetrieveAllCountries();

            return View(editProfileViewModel);
        }
    }
}