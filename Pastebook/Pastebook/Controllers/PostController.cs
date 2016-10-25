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
    public class PostController : Controller
    {
        PostManager postManager = new PostManager();
        InteractionManager interactionManager = new InteractionManager();
        NotificationManager notifactionManager = new NotificationManager();

        public JsonResult CreatePost(string content, int profileOwner)
        {
            PASTEBOOK_POST post = new PASTEBOOK_POST();
            post.CONTENT = content;
            post.POSTER_ID = (int)Session["UserId"];
            post.PROFILE_OWNER_ID = profileOwner;
            post.CREATED_DATE = DateTime.Now;

            bool result = false;
            result = postManager.CreatePost(post);

            return Json(new { result = result });
        }

        public PartialViewResult GetTimelinePosts(int id)
        {
            PostViewModel postView = new PostViewModel();
            postView.ListOfPost = postManager.RetrieveTimelinePost(id);
            return PartialView("~/Views/Pastebook/_PostPartialView.cshtml", postView);
        }

        public PartialViewResult GetNewsfeedPosts(int id)
        {
            PostViewModel postView = new PostViewModel();
            List<int> listOfFriendIds = new List<int>();

            interactionManager.RetrieveFriends((int)Session["UserId"], out listOfFriendIds);
            postView.ListOfPost = postManager.RetrieveNewsfeedPost(id, listOfFriendIds);
            return PartialView("~/Views/Pastebook/_PostPartialView.cshtml", postView);
        }

       
    }
}