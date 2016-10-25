using Pastebook.Managers;
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

        [HttpGet]
        public ActionResult UpdateInformation()
        {
            EditProfileViewModel editProfileViewModel = new EditProfileViewModel();
            editProfileViewModel.User = accountManager.RetrieveUserById((int)Session["UserId"]);
            editProfileViewModel.ListOfCountryModel = countryManager.RetrieveAllCountries();
            return View("~/Views/Pastebook/UpdateInformation.cshtml", editProfileViewModel);
        }

        [HttpGet]
        public ActionResult UpdateEmail()
        {
            EditProfileViewModel editProfileViewModel = new EditProfileViewModel();
            editProfileViewModel.User = accountManager.RetrieveUserById((int)Session["UserId"]);
            return View("~/Views/Pastebook/UpdateEmail.cshtml", editProfileViewModel);
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            EditProfileViewModel editProfileViewModel = new EditProfileViewModel();
            editProfileViewModel.User = accountManager.RetrieveUserById((int)Session["UserId"]);
            return View("~/Views/Pastebook/ChangePassword.cshtml", editProfileViewModel);
        }

        [HttpPost]
        public ActionResult UpdateInformation(PASTEBOOK_USER user)
        {
            bool result = false;
            PASTEBOOK_USER originalUser = new PASTEBOOK_USER();
            originalUser = accountManager.RetrieveUserById((int)Session["UserId"]);

            originalUser.USER_NAME = user.USER_NAME;
            originalUser.FIRST_NAME = user.FIRST_NAME;
            originalUser.LAST_NAME = user.LAST_NAME;
            originalUser.BIRTHDAY = user.BIRTHDAY;
            originalUser.COUNTRY_ID = user.COUNTRY_ID;
            originalUser.GENDER = user.GENDER;
            originalUser.MOBILE_NO = user.MOBILE_NO;

            result = accountManager.UpdateUser(originalUser);

            Session["Username"] = originalUser.USER_NAME;
            Session["UserFullname"] = originalUser.FIRST_NAME + " " + originalUser.LAST_NAME;
            return RedirectToAction("UpdateInformation");
        }

        [HttpPost]
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