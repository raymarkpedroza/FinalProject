using Pastebook.Models;
using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PastebookBusinessLayer.BusinessLayer;

namespace Pastebook.Controllers
{
    public class ProfileController:Controller
    {
        AccountManager accountManager = new AccountManager();
        CountryManager countryManager = new CountryManager();
        InteractionManager interactionManager = new InteractionManager();

        [HttpPost]
        public ActionResult UpdateProfile(string aboutme)
        {
            PASTEBOOK_USER user = new PASTEBOOK_USER();
            user = accountManager.RetrieveUserByUsername(Session["Username"].ToString());
            user.ABOUT_ME = aboutme;
            bool result = false;
            result = accountManager.UpdateUser(user);

            return RedirectToAction("GetProfileDetails", "Pastebook", new { username = Session["Username"].ToString() });
        }

        [HttpGet]
        [Route("settings/updateinformation")]
        public ActionResult UpdateInformation()
        {
            if (Session["Username"] != null)
            {
                EditProfileViewModel editProfileViewModel = new EditProfileViewModel();
                editProfileViewModel.User = accountManager.RetrieveUserById((int)Session["UserId"]);
                editProfileViewModel.ListOfCountryModel = countryManager.RetrieveAllCountries();
                return View("~/Views/Pastebook/UpdateInformation.cshtml", editProfileViewModel);
            }
            else
                return RedirectToAction("Index", "Pastebook");
          
        }

        [HttpGet]
        [Route("settings/updateemail")]
        public ActionResult UpdateEmail()
        {
            if (Session["Username"] != null)
            {
                EditProfileViewModel editProfileViewModel = new EditProfileViewModel();
                editProfileViewModel.User = accountManager.RetrieveUserById((int)Session["UserId"]);
                return View("~/Views/Pastebook/UpdateEmail.cshtml", editProfileViewModel);
            }
            else
                return RedirectToAction("Index", "Pastebook");
            
        }

        [HttpGet]
        [Route("settings/changepassword")]
        public ActionResult ChangePassword()
        {
            if (Session["Username"] != null)
            {
                EditProfileViewModel editProfileViewModel = new EditProfileViewModel();
                editProfileViewModel.User = accountManager.RetrieveUserById((int)Session["UserId"]);
                return View("~/Views/Pastebook/ChangePassword.cshtml", editProfileViewModel);
            }
            else
                return RedirectToAction("Index", "Pastebook");
        }

        [HttpPost]
        [Route("settings/updateinformation")]
        public ActionResult UpdateInformation(PASTEBOOK_USER user)
        {
            bool result = false;
            PASTEBOOK_USER originalUser = new PASTEBOOK_USER();
            originalUser = accountManager.RetrieveUserByEmail(Session["Email"].ToString());

            originalUser.USER_NAME = user.USER_NAME;
            originalUser.FIRST_NAME = user.FIRST_NAME;
            originalUser.LAST_NAME = user.LAST_NAME;
            originalUser.BIRTHDAY = user.BIRTHDAY;
            originalUser.COUNTRY_ID = user.COUNTRY_ID;

            if (user.GENDER == null)
                originalUser.GENDER = "U";

            else
                originalUser.GENDER = user.GENDER;

            originalUser.MOBILE_NO = user.MOBILE_NO;

            result = accountManager.UpdateUser(originalUser);

            Session["Username"] = originalUser.USER_NAME;
            Session["UserFullname"] = originalUser.FIRST_NAME + " " + originalUser.LAST_NAME;
            return RedirectToAction("UpdateInformation");
        }

        [HttpPost]
        [Route("settings/updateemail")]
        public ActionResult UpdateEmail(PASTEBOOK_USER user, EditProfileViewModel editProfileViewModel)
        {
            bool result = false;
            PASTEBOOK_USER originalUser = new PASTEBOOK_USER();

            originalUser = accountManager.RetrieveUserById((int)Session["UserId"]);
            originalUser.EMAIL_ADDRESS = user.EMAIL_ADDRESS;

            if (!accountManager.IsPasswordMatch(editProfileViewModel.CurrentPassword, originalUser.SALT, originalUser.PASSWORD))
            {
                ModelState.AddModelError("CurrentPassword", "Incorrect Password");
            }

            else
            {
                result = accountManager.UpdateUser(originalUser);
                Session["Email"] = originalUser.EMAIL_ADDRESS;

            }

            return View("~/Views/Pastebook/UpdateEmail.cshtml", editProfileViewModel);
        }

        [HttpPost]
        [Route("settings/changepassword")]
        public ActionResult ChangePassword(PASTEBOOK_USER user, EditProfileViewModel editProfileViewModel)
        {
            bool result = false;
            PASTEBOOK_USER originalUser = new PASTEBOOK_USER();
            originalUser = accountManager.RetrieveUserByUsername(Session["Username"].ToString());

            if (!accountManager.IsPasswordMatch(editProfileViewModel.CurrentPassword, originalUser.SALT, originalUser.PASSWORD))
            {
                ModelState.AddModelError("CurrentPassword", "Incorrect Password");
            }

            else
            {
                originalUser.PASSWORD = editProfileViewModel.Password;
                string salt = string.Empty;
                originalUser.PASSWORD = accountManager.GeneratePasswordHash(originalUser.PASSWORD, out salt);
                originalUser.SALT = salt;
                result = accountManager.UpdateUser(originalUser);
            }

            return View("~/Views/Pastebook/ChangePassword.cshtml", editProfileViewModel);
        }
    }
}