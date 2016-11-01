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
        ValidationManager validator = new ValidationManager();
        public JsonResult CheckAboutMeIfValid(string aboutme)
        {
            string errorText = string.Empty;

            if (validator.CheckIfOutOfStringLimit(aboutme, 2000))
            {
                errorText = "Maximum characters for \"About Me\" description is 2000";
            }

            return Json(new { result = errorText}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateProfile(string aboutme)
        {
            PASTEBOOK_USER user = new PASTEBOOK_USER();
            user = accountManager.GetUser(x=>x.USER_NAME == Session["Username"].ToString());
            user.ABOUT_ME = aboutme;
            bool result = false;
            result = accountManager.UpdateUser(user);

            return RedirectToAction("GetProfileDetails", "Pastebook", new { username = Session["Username"].ToString() });
        }

        [HttpGet]
        [Route("settings/updateinformation")]
        [PastebookAuthorize]
        public ActionResult UpdateInformation()
        {
            EditProfileViewModel editProfileViewModel = new EditProfileViewModel();
            editProfileViewModel.User = accountManager.GetUser(x=>x.ID == (int)Session["UserId"]);

            List<SelectListItem> listOfCountries = new List<SelectListItem>();

            foreach (var country in countryManager.GetAllCountries())
            {
                listOfCountries.Add(new SelectListItem
                {
                    Text = country.COUNTRY,
                    Value = country.ID.ToString()
                });
            }

            List<SelectListItem> listOfGenders = new List<SelectListItem>();

            listOfGenders.Add(new SelectListItem { Text = "Male", Value = "M" });
            listOfGenders.Add(new SelectListItem { Text = "Female", Value = "F" });

            ViewBag.Countries = listOfCountries;
            ViewBag.Genders = listOfGenders;
            return View("~/Views/Pastebook/UpdateInformation.cshtml", editProfileViewModel);
        }

        [HttpGet]
        [Route("settings/updateemail")]
        [PastebookAuthorize]
        public ActionResult UpdateEmail()
        {
            EditProfileViewModel editProfileViewModel = new EditProfileViewModel();
            editProfileViewModel.User = accountManager.GetUser(x=>x.ID == (int)Session["UserId"]);
            return View("~/Views/Pastebook/UpdateEmail.cshtml", editProfileViewModel);
        }

        [HttpGet]
        [Route("settings/changepassword")]
        [PastebookAuthorize]
        public ActionResult ChangePassword()
        {
            EditProfileViewModel editProfileViewModel = new EditProfileViewModel();
            editProfileViewModel.User = accountManager.GetUser(x=>x.ID == (int)Session["UserId"]);
            return View("~/Views/Pastebook/ChangePassword.cshtml", editProfileViewModel);
        }

        [HttpPost]
        [Route("settings/updateinformation")]
        public ActionResult UpdateInformation(PASTEBOOK_USER user)
        {
            bool result = false;
            int errorCount = 0;
            PASTEBOOK_USER originalUser = new PASTEBOOK_USER();
            originalUser = accountManager.GetUser(x=>x.EMAIL_ADDRESS == Session["Email"].ToString());

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

            if (accountManager.GetUser(x => x.USER_NAME == user.USER_NAME && x.ID != (int)Session["UserId"]) != null)
            {
                ModelState.AddModelError("USER.USER_NAME", "Username already taken");
                errorCount++;
            }

            if (errorCount == 0)
            {
                result = accountManager.UpdateUser(originalUser);
                Session["Username"] = originalUser.USER_NAME;
                Session["UserFullname"] = originalUser.FIRST_NAME + " " + originalUser.LAST_NAME;
                ViewBag.Result = true;
            }

            EditProfileViewModel editProfileViewModel = new EditProfileViewModel();
            editProfileViewModel.User = user;
            editProfileViewModel.ListOfCountryModel = countryManager.GetAllCountries();

            List<SelectListItem> listOfCountries = new List<SelectListItem>();

            foreach (var country in countryManager.GetAllCountries())
            {
                listOfCountries.Add(new SelectListItem
                {
                    Text = country.COUNTRY,
                    Value = country.ID.ToString()
                });
            }

            List<SelectListItem> listOfGenders = new List<SelectListItem>();

            listOfGenders.Add(new SelectListItem { Text = "Male", Value = "M" });
            listOfGenders.Add(new SelectListItem { Text = "Female", Value = "F" });

            ViewBag.Countries = listOfCountries;
            ViewBag.Genders = listOfGenders;

            return View("~/Views/Pastebook/UpdateInformation.cshtml", editProfileViewModel);
        }

        [HttpPost]
        [Route("settings/updateemail")]
        public ActionResult UpdateEmail(PASTEBOOK_USER user, EditProfileViewModel editProfileViewModel)
        {
            bool result = false;
            int errorCount = 0;
            PASTEBOOK_USER originalUser = new PASTEBOOK_USER();

            originalUser = accountManager.GetUser(x=>x.ID == (int)Session["UserId"]);
            originalUser.EMAIL_ADDRESS = user.EMAIL_ADDRESS;
            
            if (accountManager.GetUser(x => x.EMAIL_ADDRESS == user.EMAIL_ADDRESS && x.ID == (int)Session["UserId"]) != null)
            {
                ModelState.AddModelError("USER.EMAIL_ADDRESS", "This is your current email address");
                errorCount++;
            }

            if (accountManager.GetUser(x => x.EMAIL_ADDRESS == user.EMAIL_ADDRESS) != null)
            {
                ModelState.AddModelError("USER.EMAIL_ADDRESS", "Email already taken");
                errorCount++;
            }


            if (!accountManager.IsPasswordMatch(editProfileViewModel.CurrentPassword, originalUser.SALT, originalUser.PASSWORD))
            {
                ModelState.AddModelError("CurrentPassword", "Incorrect Password");
                errorCount++;
            }

            if(errorCount==0)
            {
                result = accountManager.UpdateUser(originalUser);
                Session["Email"] = originalUser.EMAIL_ADDRESS;
                editProfileViewModel.CurrentPassword = null;
                ViewBag.Result = true;
            }
            editProfileViewModel.User.EMAIL_ADDRESS = user.EMAIL_ADDRESS;

            return View("~/Views/Pastebook/UpdateEmail.cshtml", editProfileViewModel);
        }

        [HttpPost]
        [Route("settings/changepassword")]
        public ActionResult ChangePassword(PASTEBOOK_USER user, EditProfileViewModel editProfileViewModel)
        {
            bool result = false;
            PASTEBOOK_USER originalUser = new PASTEBOOK_USER();
            int errorCount = 0;
            originalUser = accountManager.GetUser(x=>x.USER_NAME == Session["Username"].ToString());

            if (accountManager.IsPasswordMatch(editProfileViewModel.Password, originalUser.SALT, originalUser.PASSWORD))
            {
                ModelState.AddModelError("Password", "This is your current password");
                errorCount++;
            }

            if (!accountManager.IsPasswordMatch(editProfileViewModel.CurrentPassword, originalUser.SALT, originalUser.PASSWORD))
            {
                ModelState.AddModelError("CurrentPassword", "Incorrect Password");
                errorCount++;
            }

            if (errorCount==0)
            {
                originalUser.PASSWORD = editProfileViewModel.Password;
                string salt = string.Empty;
                originalUser.PASSWORD = accountManager.GenerateHashedPassword(originalUser.PASSWORD, out salt);
                originalUser.SALT = salt;
                editProfileViewModel.Password = null;
                editProfileViewModel.ConfirmPassword = null;
                editProfileViewModel.CurrentPassword = null;
                result = accountManager.UpdateUser(originalUser);
                ViewBag.Result = true;
            }


            return View("~/Views/Pastebook/ChangePassword.cshtml", editProfileViewModel);
        }

        public JsonResult CheckIfEmailYourCurrentEmail(string email)
        {
            return Json( new { result = accountManager.GetUser(x => x.EMAIL_ADDRESS == email && x.ID == (int)Session["UserId"]) != null}, JsonRequestBehavior.AllowGet);
        }


        public JsonResult CheckIfUsernameYourCurrentUsername(string username)
        {
            return Json(new { result = accountManager.GetUser(x => x.USER_NAME == username && x.ID == (int)Session["UserId"]) != null }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckIfPasswordYourCurrentPassword(string password)
        {
            PASTEBOOK_USER user = accountManager.GetUser(x => x.USER_NAME == Session["Username"].ToString());

            return Json(new {result = accountManager.IsPasswordMatch(password, user.SALT, user.PASSWORD)}, JsonRequestBehavior.AllowGet);
        }
    }
}