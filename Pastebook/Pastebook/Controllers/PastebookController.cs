using Pastebook.Managers;
using Pastebook.Models;
using PastebookDataAccess.Managers;
using PastebookEF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pastebook.Controllers
{
    public class PastebookController : Controller
    {
        DataAccessUserManager daUserManager = new DataAccessUserManager();
        DataAccessCountryManager daCountryManager = new DataAccessCountryManager();
        DataAccessFriendManager daFriendManager = new DataAccessFriendManager();

        _PostManager postManager = new _PostManager();

        [HttpGet]
        public ActionResult Home()
        {
            if (Session["Username"] != null)
            {
                PASTEBOOK_USER model = new PASTEBOOK_USER();
                model = daUserManager.RetrieveUserByUsername(Session["Username"].ToString());
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
                profileViewModel.User = daUserManager.RetrieveUserByUsername(username);
                profileViewModel.CountryName = profileViewModel.User.REF_COUNTRY.COUNTRY;

                profileViewModel.ListOfCountryModel = daCountryManager.RetrieveAllCountry();
                profileViewModel.ListOfFriends = daFriendManager.RetrieveFriends(profileViewModel.User.ID);
                return View(profileViewModel);
            }
            else
                return RedirectToAction("Index", "Home");
        }

        public ActionResult Friends()
        {
            List<PASTEBOOK_FRIEND> listOfFriend = new List<PASTEBOOK_FRIEND>();
            FriendListViewModel friendListViewModel = new FriendListViewModel();

            listOfFriend = daFriendManager.RetrieveFriends((int)Session["UserId"]);

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
                    user = daUserManager.RetrieveUserByUsername(Session["Username"].ToString());
                    user.PROFILE_PIC = array;

                    int result = daUserManager.UpdateProfile(user);
                }
            }

            return RedirectToAction("Index", "Pastebook", new { username = Session["Username"].ToString() });
        }

        public ActionResult UpdateProfile(string gender, int country, DateTime birthday, string mobilenumber, string email, string aboutme)
        {
            PASTEBOOK_USER user = new PASTEBOOK_USER();
            user = daUserManager.RetrieveUserByUsername(Session["Username"].ToString());
            user.GENDER = gender;
            user.COUNTRY_ID = country;
            user.BIRTHDAY = birthday;
            user.MOBILE_NO = mobilenumber;
            user.EMAIL_ADDRESS = email;
            user.ABOUT_ME = aboutme;
            int result = daUserManager.UpdateProfile(user);

            return RedirectToAction("GetProfileDetails", "Pastebook", new { username = Session["Username"].ToString() });
        }

        public PartialViewResult GetProfileDetails(string username)
        {
            ProfileViewModel profileViewModel = new ProfileViewModel();
            profileViewModel.User = daUserManager.RetrieveUserByUsername(username);
            profileViewModel.CountryName = profileViewModel.User.REF_COUNTRY.COUNTRY;

            profileViewModel.ListOfCountryModel = daCountryManager.RetrieveAllCountry();
            profileViewModel.ListOfFriends = daFriendManager.RetrieveFriends(profileViewModel.User.ID);

            return PartialView("~/Views/Pastebook/_ProfileDetailsPartialView.cshtml", profileViewModel);
        }

        public PartialViewResult GetFriendList(int id)
        {
            List<PASTEBOOK_FRIEND> listOfFriend = new List<PASTEBOOK_FRIEND>();
            FriendListViewModel friendListViewModel = new FriendListViewModel();

            listOfFriend = daFriendManager.RetrieveFriends(id);

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
            UserPostModel PostWithPoster = new UserPostModel();
            PostWithPoster = postManager.RetrievePost(postId);
            return View(PostWithPoster);
        }

        public ActionResult Search(SearchModel search)
        {
            List<PASTEBOOK_USER> searchResults = new List<PASTEBOOK_USER>();
            searchResults = daUserManager.SearchUser(search.Name);

            ResultsViewModel resultsViewModel = new ResultsViewModel();
            resultsViewModel.searchResults = searchResults;
            return View(resultsViewModel);
        }
    }
}