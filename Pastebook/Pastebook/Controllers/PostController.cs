using Pastebook.Models;
using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PastebookBusinessLayer.BusinessLayer;
using System.Text.RegularExpressions;

namespace Pastebook.Controllers
{
    public class PostController : Controller
    {
        PostManager postManager = new PostManager();
        InteractionManager interactionManager = new InteractionManager();
        NotificationManager notifactionManager = new NotificationManager();
        ValidationManager validationManager = new ValidationManager();

        public JsonResult CreatePost(string content, int profileOwner)
        {
            PASTEBOOK_POST post = new PASTEBOOK_POST();
            post.CONTENT = content.Trim();
            post.POSTER_ID = (int)Session["UserId"];
            post.PROFILE_OWNER_ID = profileOwner;
            post.CREATED_DATE = DateTime.Now;

            return Json(new { result = postManager.CreatePost(post) });
        }

        public JsonResult CheckIfPostValid(string content)
        {
            string errorText = string.Empty;

            if (validationManager.CheckIfOutOfStringLimit(content, 1000))
            {
                errorText = "Maximum characters for posting is 1000 characters";
            }

            if (validationManager.CheckIfWhiteSpace(content))
            {
                errorText = "Posting white spaces is not allowed";
            }

            if (validationManager.CheckIfIsNullOrEmpty(content))
            {
                errorText = "Posting empty content is not allowed";
            }

            return Json(new { result = errorText }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetTimelinePosts(int id)
        {
            PostViewModel postView = new PostViewModel();
            postView.ListOfPost = postManager.GetTimelinePost(id);
            return PartialView("~/Views/Pastebook/_PostPartialView.cshtml", postView);
        }

        public PartialViewResult GetNewsfeedPosts(int id)
        {
            PostViewModel postView = new PostViewModel();
            List<int> listOfPoster = new List<int>();

            if (Session != null && Session["UserId"] != null)
            {
                listOfPoster = interactionManager.GetFriendList((int)Session["UserId"]).Select(x=>x.FRIEND_ID).ToList();
                listOfPoster.Add((int)Session["UserId"]);
                postView.ListOfPost = postManager.GetNewsfeedPost(listOfPoster);
            }

            return PartialView("~/Views/Pastebook/_PostPartialView.cshtml", postView);

        }
    }
}