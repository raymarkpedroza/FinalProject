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
    public class PastebookController : Controller
    {
        DataAccessUserManager daUserManager = new DataAccessUserManager();
        DataAccessCountryManager daCountryManager = new DataAccessCountryManager();
        DataAccessFriendManager daFriendManager = new DataAccessFriendManager();

        _PostManager postManager = new _PostManager();

        [HttpGet]
        public ActionResult Newsfeed()
        {
            if (Session["Username"] != null)
            {
                PASTEBOOK_USER model = new PASTEBOOK_USER();
                model = daUserManager.RetrieveUserById((int)Session["UserId"]);
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
                profileViewModel.User = daUserManager.RetrieveUserById(id);
                profileViewModel.CountryName = daCountryManager.RetrieveCountry((int)profileViewModel.User.COUNTRY_ID).COUNTRY;

                profileViewModel.ListOfFriends = daFriendManager.RetrieveFriends(id);
                return View(profileViewModel);
            }
            else
                return RedirectToAction("Index", "Home");
        }

       
    }
}